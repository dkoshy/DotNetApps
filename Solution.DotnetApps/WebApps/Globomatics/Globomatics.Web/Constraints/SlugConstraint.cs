
using System.Globalization;
using System.Text.RegularExpressions;

namespace Globomatics.Web.Constraints;

public class SlugConstraint : IRouteConstraint
{

    /// <summary>
    /// If this cobstraint is applied to a route thr specied parameter must match the regex patten or condtion.
    /// also used by ancor tag helper when creating url to make sure the url is valid and match the regex pattern.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="route"></param>
    /// <param name="routeKey"></param>
    /// <param name="values"></param>
    /// <param name="routeDirection"></param>
    /// <returns></returns>
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if(!values.TryGetValue(routeKey, out var slug))
        {
            return false;
        }
        var slugString = Convert.ToString(slug , CultureInfo.InvariantCulture);
        if (string.IsNullOrWhiteSpace(slugString))
        {
            return false;
        }

        return Regex.IsMatch(slugString, @"^[a-zA-Z0-9- :!]+$");
    }
}
