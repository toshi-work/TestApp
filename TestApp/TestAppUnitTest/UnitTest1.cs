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
        // DbContextを格納するインスタンス変数を宣言
        private TestAppContext _context;
        public UnitTest1()
        {
            // Unitテスト用のDBを定義
            var options = new DbContextOptionsBuilder<TestAppContext>()
              .UseInMemoryDatabase(databaseName: "UnitTestDB")  // databaseNameは適当に定義
              .Options;
            
            // DBインスタンスの生成
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

            // 戻り値をチェック
            Assert.Equal("This is my default action...", view);
        }

        [Fact]
        public async void Movies_CreateTest()
        {
            // TestAppContextのインスタンスが正常に取得出来た場合
            if (_context != null)
            {
                var controller = new MoviesController(_context);

                // データ作成
                MovieViewModel model = new MovieViewModel();
                model.Title = "xUnitTest Title";
                model.Genre = "commedy";
                model.ReleaseDate = DateTime.Now;
                model.Price = (decimal)9.87;

                // データ保存処理の実行
                await controller.Create(model);

                // 作成したデータを取得
                MovieViewModel? data = await _context.Movies.FirstOrDefaultAsync(x => x.Title == model.Title);

                // Nullでないか確認
                Assert.NotNull(data);

                // 本テストで作成したデータと一致するか確認
                if(data != null) Assert.True(model.ReleaseDate == data.ReleaseDate);
            } 
            else
            {
                this.ContextError();
            }
        }
    }
}