using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;

namespace Globomatics.Web.ValueProviders;

public class SessionValueProviderFactory : IValueProviderFactory
{
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        
        var session = context.ActionContext.HttpContext.Session;

        if( session is not  null && session.Keys.Any())
        {
            var sessionValueProvider = new SessionValueProvider(CustomModelBindingSource.Session,
                session);
            context.ValueProviders.Add(sessionValueProvider);
        }
        return Task.CompletedTask;
    }
}
