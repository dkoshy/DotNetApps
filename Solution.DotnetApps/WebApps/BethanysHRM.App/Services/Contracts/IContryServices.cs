using BethanysPieShopHRM.Shared.Domain;
namespace BethanysHRM.App.Services.Contracts;

public interface ICountryServices
{
    Task<IEnumerable<Country>> GetCountries();
    Task<Country> GetCountryById(int id);
}
