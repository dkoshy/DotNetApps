using BethanysPieShopHRM.Shared.Domain;

namespace BethanysHRM.App.Services.Contracts;

public interface IJobCategoryService
{
    Task<IEnumerable<JobCategory>> GetJobCategories();
    Task<JobCategory> GetJobCategoryById(int id);
}
