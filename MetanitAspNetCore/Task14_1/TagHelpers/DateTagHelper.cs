using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task14_1.TagHelpers
{
    public class DateTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.SelfClosing;
            output.Content.SetContent($"Current Day {DateTime.Now.ToString("dd.MM.yyyy")}");
        }
    }
}
