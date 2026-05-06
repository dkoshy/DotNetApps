using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.RegularExpressions;

namespace Globomatics.Web.TagHelpers
{
    [HtmlTargetElement("slug-url")]
    public class SlugAlternateTagHelper : AnchorTagHelper
    {
        public SlugAlternateTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        [HtmlAttributeName("for-product-id")]
        public Guid ProductId { get; set; }

        [HtmlAttributeName("for-ticket-name")]
        public required string TicketName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {   
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;

            var slug = Regex.Replace(TicketName, @"[:!]+", " ", RegexOptions.CultureInvariant);
            slug = slug.Trim().Replace(" ", "-").ToLowerInvariant();
            RouteValues.Add("slug" , slug);
            RouteValues.Add("productId", ProductId.ToString());
            base.Process(context, output);
        }
    }
}
