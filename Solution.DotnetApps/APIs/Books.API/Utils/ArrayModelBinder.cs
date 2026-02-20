using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace Books.API.Utils;

public class ArrayModelBinder : IModelBinder
{


    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        //Check model type is IEnumerable
        if(!bindingContext.ModelMetadata.IsEnumerableType)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        //get inputed value through value providers.
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName)
                    .ToString();

        //if value is null or empty, return null
        if(string.IsNullOrWhiteSpace(value))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        //find the enumerable type and converter
        var elementType = bindingContext.ModelType.GetTypeInfo()
                            .GetGenericArguments()[0];
        var converter = TypeDescriptor.GetConverter(elementType);

        var Values = value.Split(new[] {","},StringSplitOptions.RemoveEmptyEntries)
                              .Select(v => converter.ConvertFromString(v.Trim()))
                              .ToArray();

        //set the model value and return success
        var typedValues = Array.CreateInstance(elementType,Values.Length);
        Values.CopyTo(typedValues, 0);
        bindingContext.Model = typedValues;
        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
        return Task.CompletedTask;

    }

}
