
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.RegularExpressions;

[HtmlTargetElement("url-with-slug")]
public class SlugTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    public SlugTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName("asp-action")]
    public string ActionMethod { get; set; }
    [HtmlAttributeName("asp-controller")]
    public string ControllerName { get; set; }

    [HtmlAttributeName("for-product-id")]
    public Guid ProductId { get; set; }

    [HtmlAttributeName("for-ticket-name")]
    public required string  TicketTitle { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        output.TagMode = TagMode.StartTagAndEndTag;

        var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

        var slug = Regex.Replace(TicketTitle, @"[ :!]+", " ");
        slug = slug.Trim().Replace(" ", "-").ToLower();

        var routeUrl = urlHelper.Action(ActionMethod, ControllerName, new { productId = ProductId, slug });
        output.Attributes.SetAttribute("href", routeUrl);
    }
}
