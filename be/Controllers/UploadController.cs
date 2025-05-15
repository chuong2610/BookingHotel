using BookingHotel.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace BookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly IWebHostEnvironment _evn;
        
        public UploadController(FileUploadSettings fileUploadSettings, IWebHostEnvironment evn)
        {
            _fileUploadSettings = fileUploadSettings;
            _evn = evn;
        }

        [HttpPost("image")]
        public async Task<ActionResult> UploadImage(IFormFile file)
        {
            try{
                if (file == null || file.Length == 0)
            {
                return BadRequest(new FileUploadResponse
                {
                    Message = "No file uploaded",
                    Success = false
                });
            }

             if (file.Length > _fileUploadSettings.MaxFileSize)
            {
                return BadRequest(new FileUploadResponse
                {
                    Message = "File size exceeds the limit",
                    Success = false
                });
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_fileUploadSettings.AllowedExtensions.Contains(extension))
            {
                return BadRequest(new FileUploadResponse
                {
                    Message = "Invalid file type",
                    Success = false
                });
            }

           

            var uploadPath = Path.Combine(_evn.ContentRootPath, _fileUploadSettings.UploadPath);
            Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(uploadPath, fileName);
            if(Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var image = await  SixLabors.ImageSharp.Image.LoadAsync(file.OpenReadStream());
            var FilePath = Path.Combine(_fileUploadSettings.UploadPath, fileName);
            await image.SaveAsync(filePath);
            return Ok(new FileUploadResponse
            {
                FilePath = filePath,
                Message = "File uploaded successfully",
                Success = true
            });
            }catch (Exception ex)
            {
                return BadRequest(new FileUploadResponse
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }
        
    }
}