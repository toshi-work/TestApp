using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;  // HtmlEncoderの名前空間

namespace TestApp.Controllers
{
    public class HelloWorldController : Controller
    {
        // IActionResultを戻り値の型に指定するとエラーになる
        public string Index()
        {
            return "This is my default action...";
        }

        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }
    }
}
