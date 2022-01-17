using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Controllers;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using TestApp.Data;
using Microsoft.Extensions.DependencyInjection;

namespace TestAppUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void HomeTest()
        {
            var controller = new HomeController();
            var view = controller.Index() as ViewResult;

            Assert.NotNull(view);
        }

        [Fact]
        public void HelloWorld_IndexTest()
        {
            var controller = new HelloWorldController();
            var view = controller.Index() as string;

            // 戻り値をチェック
            Assert.Equal("This is my default action...", view);
        }

        [Fact]
        public void Movies_IndexTest()
        {
            // 型の登録
            var services = new ServiceCollection();
            // インスタンスを提供してくれる人を作る
            using var provider = services.BuildServiceProvider();
            var context = provider.GetService<TestAppContext>();

            // TestAppContextのインスタンスが正常に取得出来た場合
            if (context != null)
            {
                var controller = new MoviesController(context);
                var view = controller.Index() as IActionResult;

            }
            

            // 戻り値をチェック
            //Assert.Equal("This is my default action...", view);
        }
    }
}