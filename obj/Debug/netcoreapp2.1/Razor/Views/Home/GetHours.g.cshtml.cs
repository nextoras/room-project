#pragma checksum "C:\Users\Admin\Documents\Arduino\server\Views\Home\GetHours.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "430c006bab18a4832eb6e59e90957622b23a4509"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_GetHours), @"mvc.1.0.view", @"/Views/Home/GetHours.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/GetHours.cshtml", typeof(AspNetCore.Views_Home_GetHours))]
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
#line 1 "C:\Users\Admin\Documents\Arduino\server\Views\_ViewImports.cshtml"
using server;

#line default
#line hidden
#line 2 "C:\Users\Admin\Documents\Arduino\server\Views\_ViewImports.cshtml"
using server.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"430c006bab18a4832eb6e59e90957622b23a4509", @"/Views/Home/GetHours.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f876df581793917ea0db3940c2e67350895bc819", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_GetHours : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Hours>>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Admin\Documents\Arduino\server\Views\Home\GetHours.cshtml"
  
    //ViewData["Title"] = "Список смартфонов";
    Layout = null;

#line default
#line hidden
            BeginContext(95, 28, true);
            WriteLiteral("<!DOCTYPE html>\r\n \r\n<html>\r\n");
            EndContext();
            BeginContext(123, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3d7f543840c249a493a556f1b10d5795", async() => {
                BeginContext(129, 37, true);
                WriteLiteral("\r\n    <title>Значения часов</title>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(173, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(175, 806, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ead439bffada499eb9037da815d0d24e", async() => {
                BeginContext(181, 314, true);
                WriteLiteral(@"
    <h3>Часы</h3>
    <table>
        <tr>
            <td>Id</td>
            <td></td><td></td><td></td>
            <td>Дата</td>
            <td></td>
            <td></td>
            <td></td>
            <td>Влажность</td>
            <td></td>
            <td>Температура</td>
        </tr>
");
                EndContext();
#line 26 "C:\Users\Admin\Documents\Arduino\server\Views\Home\GetHours.cshtml"
         foreach (var item in Model)
            {

#line default
#line hidden
                BeginContext(548, 38, true);
                WriteLiteral("            <tr>\r\n                <td>");
                EndContext();
                BeginContext(587, 7, false);
#line 29 "C:\Users\Admin\Documents\Arduino\server\Views\Home\GetHours.cshtml"
               Write(item.Id);

#line default
#line hidden
                EndContext();
                BeginContext(594, 108, true);
                WriteLiteral("</td>\r\n                <td></td>\r\n                <td></td>\r\n                <td></td>\r\n                <td>");
                EndContext();
                BeginContext(703, 9, false);
#line 33 "C:\Users\Admin\Documents\Arduino\server\Views\Home\GetHours.cshtml"
               Write(item.Date);

#line default
#line hidden
                EndContext();
                BeginContext(712, 108, true);
                WriteLiteral("</td>\r\n                <td></td>\r\n                <td></td>\r\n                <td></td>\r\n                <td>");
                EndContext();
                BeginContext(821, 13, false);
#line 37 "C:\Users\Admin\Documents\Arduino\server\Views\Home\GetHours.cshtml"
               Write(item.Humidity);

#line default
#line hidden
                EndContext();
                BeginContext(834, 54, true);
                WriteLiteral("</td>\r\n                <td></td>\r\n                <td>");
                EndContext();
                BeginContext(889, 16, false);
#line 39 "C:\Users\Admin\Documents\Arduino\server\Views\Home\GetHours.cshtml"
               Write(item.Temperature);

#line default
#line hidden
                EndContext();
                BeginContext(905, 44, true);
                WriteLiteral("</td>\r\n                \r\n            </tr>\r\n");
                EndContext();
#line 42 "C:\Users\Admin\Documents\Arduino\server\Views\Home\GetHours.cshtml"
        }

#line default
#line hidden
                BeginContext(960, 14, true);
                WriteLiteral("    </table>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(981, 9, true);
            WriteLiteral("\r\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Hours>> Html { get; private set; }
    }
}
#pragma warning restore 1591
