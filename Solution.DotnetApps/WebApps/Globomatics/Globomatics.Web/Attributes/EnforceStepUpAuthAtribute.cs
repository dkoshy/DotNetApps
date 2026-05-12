using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Globomatics.Web.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class EnforceStepUpAuthAttribute : Attribute, IAuthorizationFilter
{

    public string StepUpAllowPathName = "StepupAllowPath";
    public void OnAuthorization(AuthorizationFilterContext context)
    {

        string? email = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        string? stepUpAllowPath = context.HttpContext.Session.GetString(email + StepUpAllowPathName)?.ToLower();

        if (!string.IsNullOrWhiteSpace(stepUpAllowPath))
        {
            context.HttpContext.Session.Remove(email + StepUpAllowPathName);
            if (context.HttpContext.Request.Path.ToString().ToLower().Equals(stepUpAllowPath))
            {
                return;
            }
            context.HttpContext.Response.Redirect("/Identity/Account/LoginWith2fa?ReturnUrl=" + context.HttpContext.Request.Path);
        }
        else
        {
            context.HttpContext.Response.Redirect("/Identity/Account/LoginWith2fa?ReturnUrl=" + context.HttpContext.Request.Path);
        }
    }
}
