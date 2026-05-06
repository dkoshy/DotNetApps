using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Globomatics.Web.ValueProviders;

public static class CustomModelBindingSource
{
    public static readonly BindingSource Session = new BindingSource(
        "Session",
        "BindingSource_Session",
        isGreedy: false,
        isFromRequest: false);
}
