using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Globomatics.Web.ValueProviders;

public class SessionValueProvider : BindingSourceValueProvider
{
    private readonly ISession _session;

    public SessionValueProvider(BindingSource bindingSource, ISession session) :
        base(bindingSource)
    {
        _session = session;
    }


    public override bool ContainsPrefix(string prefix)
    {
        return _session.Keys.Contains(prefix);
    }

    public override ValueProviderResult GetValue(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return ValueProviderResult.None;
        }

        if (_session.Keys.Contains(key))
        {
            return new ValueProviderResult(_session.GetString(key));
        }

        return ValueProviderResult.None;
    }
}