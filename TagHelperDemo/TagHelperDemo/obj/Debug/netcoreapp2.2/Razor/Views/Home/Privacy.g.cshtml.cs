#pragma checksum "D:\Rabindra\TagHelperDemo\TagHelperDemo\Views\Home\Privacy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "10f4f9a5b5718e7ac173cc4faa0b7b7eb5a2c438"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Privacy), @"mvc.1.0.view", @"/Views/Home/Privacy.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Privacy.cshtml", typeof(AspNetCore.Views_Home_Privacy))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Rabindra\TagHelperDemo\TagHelperDemo\Views\_ViewImports.cshtml"
using TagHelperDemo;

#line default
#line hidden
#line 2 "D:\Rabindra\TagHelperDemo\TagHelperDemo\Views\_ViewImports.cshtml"
using TagHelperDemo.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"10f4f9a5b5718e7ac173cc4faa0b7b7eb5a2c438", @"/Views/Home/Privacy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"12bde6c16f974cdffbb465d316c76cf9dfd4694b", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Privacy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\Rabindra\TagHelperDemo\TagHelperDemo\Views\Home\Privacy.cshtml"
  
    ViewData["Title"] = "Privacy Policy";

#line default
#line hidden
            BeginContext(50, 4, true);
            WriteLiteral("<h1>");
            EndContext();
            BeginContext(55, 17, false);
#line 4 "D:\Rabindra\TagHelperDemo\TagHelperDemo\Views\Home\Privacy.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(72, 27, true);
            WriteLiteral("</h1>\r\n<h1>Session Value : ");
            EndContext();
            BeginContext(100, 15, false);
#line 5 "D:\Rabindra\TagHelperDemo\TagHelperDemo\Views\Home\Privacy.cshtml"
               Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(115, 32, true);
            WriteLiteral("</h1>\r\n<h1>State Data Service : ");
            EndContext();
            BeginContext(148, 12, false);
#line 6 "D:\Rabindra\TagHelperDemo\TagHelperDemo\Views\Home\Privacy.cshtml"
                    Write(ViewBag.Data);

#line default
#line hidden
            EndContext();
            BeginContext(160, 68, true);
            WriteLiteral(" </h1>\r\n<p>Use this page to detail your site\'s privacy policy.</p>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
