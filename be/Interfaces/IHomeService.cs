using BookingHotel.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingHotel.Interfaces
{
    public interface IHomeService
    {
        Task<BannerDto> GetBannerAsync();
        Task<AboutDto> GetAboutAsync();
        Task<List<ServiceDto>> GetServicesAsync();
        Task<List<ReviewDto>> GetReviewsAsync();
        Task<List<NewsDto>> GetLatestNewsAsync();
    }
}