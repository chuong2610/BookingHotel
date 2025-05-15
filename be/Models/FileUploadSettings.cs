namespace BookingHotel.Models{
    public class FileUploadSettings{
        public string UploadPath { get; set; } = string.Empty;
        public string[] AllowedExtensions { get; set; } = Array.Empty<string>();
        public long MaxFileSize { get; set; }
    }

    public class FileUploadResponse{
        public string FilePath { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}