using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperDemo.TagHelpers
{
    [HtmlTargetElement("email")]
    public class EmailTagHelper:TagHelper
    {
        public string Address { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href",$"mailto:{Address}");
            output.Content.SetContent($"Send mail to {Address}");
        }
    }
}
