using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Controllers;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using TestApp.Data;
using System;
using TestApp.Models;

namespace TestAppUnitTest
{
    public class UnitTest1
    {
        // DbContext���i�[����C���X�^���X�ϐ���錾
        private TestAppContext _context;
        public UnitTest1()
        {
            // Unit�e�X�g�p��DB���`
            var options = new DbContextOptionsBuilder<TestAppContext>()
              .UseInMemoryDatabase(databaseName: "UnitTestDB")  // databaseName�͓K���ɒ�`
              .Options;
            
            // DB�C���X�^���X�̐���
            _context = new TestAppContext(options);
        }

        private void ContextError()
        {
            throw new NullReferenceException();
        }

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
        public async void Movies_CreateTest()
        {
            // TestAppContext�̃C���X�^���X������Ɏ擾�o�����ꍇ
            if (_context != null)
            {
                var controller = new MoviesController(_context);

                // �f�[�^�쐬
                MovieViewModel model = new MovieViewModel();
                model.Title = "xUnitTest Title";
                model.Genre = "commedy";
                model.ReleaseDate = DateTime.Now;
                model.Price = (decimal)9.87;

                // �f�[�^�ۑ������̎��s
                await controller.Create(model);

                // �쐬�����f�[�^���擾
                MovieViewModel? data = await _context.Movies.FirstOrDefaultAsync(x => x.Title == model.Title);

                // Null�łȂ����m�F
                Assert.NotNull(data);

                // �{�e�X�g�ō쐬�����f�[�^�ƈ�v���邩�m�F
                if(data != null) Assert.True(model.ReleaseDate == data.ReleaseDate);
            } 
            else
            {
                this.ContextError();
            }
        }
    }
}