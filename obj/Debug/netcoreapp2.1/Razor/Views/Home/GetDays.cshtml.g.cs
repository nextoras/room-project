#pragma checksum "C:\Users\drako\source\repos\GIT пример\room-project\Views\Home\GetDays.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1b975cbf2afe44fe3648c43371a49f79648959b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_GetDays), @"mvc.1.0.view", @"/Views/Home/GetDays.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/GetDays.cshtml", typeof(AspNetCore.Views_Home_GetDays))]
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
#line 1 "C:\Users\drako\source\repos\GIT пример\room-project\Views\_ViewImports.cshtml"
using server;

#line default
#line hidden
#line 2 "C:\Users\drako\source\repos\GIT пример\room-project\Views\_ViewImports.cshtml"
using server.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b975cbf2afe44fe3648c43371a49f79648959b7", @"/Views/Home/GetDays.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f876df581793917ea0db3940c2e67350895bc819", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_GetDays : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Days>>
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
#line 2 "C:\Users\drako\source\repos\GIT пример\room-project\Views\Home\GetDays.cshtml"
  
    //ViewData["Title"] = "Список смартфонов";
    Layout = null;

#line default
#line hidden
            BeginContext(94, 28, true);
            WriteLiteral("<!DOCTYPE html>\r\n \r\n<html>\r\n");
            EndContext();
            BeginContext(122, 49, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d242e74b08da4afc826e6d94d7fabf24", async() => {
                BeginContext(128, 36, true);
                WriteLiteral("\r\n    <title>Значения дней</title>\r\n");
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
            BeginContext(171, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(173, 805, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "450593367dd84fdbb91ca4977c06b8ba", async() => {
                BeginContext(179, 313, true);
                WriteLiteral(@"
    <h3>Дни</h3>
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
#line 26 "C:\Users\drako\source\repos\GIT пример\room-project\Views\Home\GetDays.cshtml"
         foreach (var item in Model)
            {

#line default
#line hidden
                BeginContext(545, 38, true);
                WriteLiteral("            <tr>\r\n                <td>");
                EndContext();
                BeginContext(584, 7, false);
#line 29 "C:\Users\drako\source\repos\GIT пример\room-project\Views\Home\GetDays.cshtml"
               Write(item.Id);

#line default
#line hidden
                EndContext();
                BeginContext(591, 108, true);
                WriteLiteral("</td>\r\n                <td></td>\r\n                <td></td>\r\n                <td></td>\r\n                <td>");
                EndContext();
                BeginContext(700, 9, false);
#line 33 "C:\Users\drako\source\repos\GIT пример\room-project\Views\Home\GetDays.cshtml"
               Write(item.Date);

#line default
#line hidden
                EndContext();
                BeginContext(709, 108, true);
                WriteLiteral("</td>\r\n                <td></td>\r\n                <td></td>\r\n                <td></td>\r\n                <td>");
                EndContext();
                BeginContext(818, 13, false);
#line 37 "C:\Users\drako\source\repos\GIT пример\room-project\Views\Home\GetDays.cshtml"
               Write(item.Humidity);

#line default
#line hidden
                EndContext();
                BeginContext(831, 54, true);
                WriteLiteral("</td>\r\n                <td></td>\r\n                <td>");
                EndContext();
                BeginContext(886, 16, false);
#line 39 "C:\Users\drako\source\repos\GIT пример\room-project\Views\Home\GetDays.cshtml"
               Write(item.Temperature);

#line default
#line hidden
                EndContext();
                BeginContext(902, 44, true);
                WriteLiteral("</td>\r\n                \r\n            </tr>\r\n");
                EndContext();
#line 42 "C:\Users\drako\source\repos\GIT пример\room-project\Views\Home\GetDays.cshtml"
        }

#line default
#line hidden
                BeginContext(957, 14, true);
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
            BeginContext(978, 9, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Days>> Html { get; private set; }
    }
}
#pragma warning restore 1591
