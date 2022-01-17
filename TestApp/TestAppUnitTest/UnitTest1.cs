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

            // �߂�l���`�F�b�N
            Assert.Equal("This is my default action...", view);
        }

        [Fact]
        public void Movies_IndexTest()
        {
            // �^�̓o�^
            var services = new ServiceCollection();
            // �C���X�^���X��񋟂��Ă����l�����
            using var provider = services.BuildServiceProvider();
            var context = provider.GetService<TestAppContext>();

            // TestAppContext�̃C���X�^���X������Ɏ擾�o�����ꍇ
            if (context != null)
            {
                var controller = new MoviesController(context);
                var view = controller.Index() as IActionResult;

            }
            

            // �߂�l���`�F�b�N
            //Assert.Equal("This is my default action...", view);
        }
    }
}