using System.Text.RegularExpressions;

namespace Globomatics.Web.Transformers;
/// <summary>
/// This will trasform the value of the url parameter slug to a specifed regx pattern.
/// It is also called when url created using ancor tag helper. 
/// </summary>
public class SlugParameterTrsformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value is not string)
        {
            return null;
        }

        return Regex.Replace(value.ToString()!,
            @"[ :!]+", "-"
            , RegexOptions.CultureInvariant | RegexOptions.IgnoreCase
            , TimeSpan.FromMilliseconds(200)).ToLowerInvariant().Trim('-');
    }
}
