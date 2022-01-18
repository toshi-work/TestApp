#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Data
{
    // DBのテーブルと接続を定義しているクラスファイル
    public class TestAppContext : DbContext
    {
        public TestAppContext (DbContextOptions<TestAppContext> options)
            : base(options)
        {
        }

        // new TestAppContext() で使用したい場合はこのメソッドをオーバーライドして書かないといけない
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // appsettings.jsonを読み込む
        //    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //    // DB接続
        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("TestAppContext"));
        //}

        // DBのMoviesというテーブルと接続（この場合、MovieViewModelはMoviesテーブルの各カラムの型を定義している）
        public DbSet<TestApp.Models.MovieViewModel> Movies { get; set; }
    }
}
