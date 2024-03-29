﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using PWApplication.BLL.Enums;

namespace PWApplication.TagHelpers
{
    public class SortHeaderTagHelper : TagHelper
    {
        public SortState Current { get; set; }

        public string Action { get; set; }

        private IUrlHelperFactory urlHelperFactory;
        public SortHeaderTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";
            string url = urlHelper.Action(Action, PageUrlValues);
            output.Attributes.SetAttribute("href", url);
           
            if (Current.ToString().StartsWith(PageUrlValues["sortorder"].ToString().Substring(0,4)))
            {
                TagBuilder tag = new TagBuilder("i");
                tag.AddCssClass("glyphicon");

                if (Current == SortState.AmountAsc || Current == SortState.CorrespondentNameAsc || 
                    Current ==  SortState.DateAsc)
                {
                    tag.AddCssClass("glyphicon-chevron-up");
                } 
                else
                {
                    tag.AddCssClass("glyphicon-chevron-down");
                }  

                output.PreContent.AppendHtml(tag);
            }
        }
    }
}
