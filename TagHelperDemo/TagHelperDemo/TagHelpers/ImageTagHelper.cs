using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperDemo.TagHelpers
{
    [HtmlTargetElement("image")]
    public class ImageTagHelper : TagHelper
    {
        public string Url { get; set; }
        public string FallbackUrl { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.Attributes.SetAttribute("src",$"{Url}");
            if (string.IsNullOrEmpty(Url))
            {
                output.Attributes.SetAttribute("src", $"{FallbackUrl}");
            }
        }
    }
}
