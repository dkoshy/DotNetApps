using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Globomatics.Web.ValueProviders;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property , AllowMultiple = false , Inherited = true)]
public class FromSessionAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource? BindingSource => CustomModelBindingSource.Session;
}
