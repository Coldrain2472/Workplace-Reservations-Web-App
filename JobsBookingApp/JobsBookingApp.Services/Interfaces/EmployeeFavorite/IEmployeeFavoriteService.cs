using JobsBookingApp.Services.DTOs.EmployeeFavorite;

namespace JobsBookingApp.Services.Interfaces.EmployeeFavorite
{
    public interface IEmployeeFavoriteService
    {
        Task<AddFavoriteResponse> AddFavoriteAsync(AddFavoriteRequest request);
        Task<RemoveFavoriteResponse> RemoveFavoriteAsync(RemoveFavoriteRequest request);
        Task<GetAllFavoritesResponse> GetAllFavoritesAsync(GetAllFavoritesRequest request);

        // add a favorite

        // remove a favorite

        // get all favorites for an employee
    }
}
