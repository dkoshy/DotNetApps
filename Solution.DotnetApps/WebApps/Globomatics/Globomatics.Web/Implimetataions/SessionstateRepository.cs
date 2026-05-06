using Globomatics.Infrastructure.Repositories;

namespace Globomatics.Web.Implimetataions;

public class SessionstateRepository : IStateRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionstateRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
    public string GetValue(string key)
    {
       return _httpContextAccessor?.HttpContext?.Session.GetString(key) ?? string.Empty;
    }

    public void SetValue(string key, string value)
    {
        _httpContextAccessor.HttpContext?.Session.SetString(key, value);
    }

    public void Remove(string key)
    {
       _httpContextAccessor.HttpContext?.Session.Remove(key);
    }

    
}
