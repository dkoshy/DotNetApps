namespace Books.API.DataAccess;

public abstract class BaseRepo
{
    protected readonly BookManagementDbContext _dbContext;

    protected BaseRepo(BookManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public virtual async Task<bool> SaveChangesAsync()
    {
        var numberOfChanges =  await _dbContext.SaveChangesAsync();
        return numberOfChanges > 0;
    }
}
