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

        // DBのMoviesというテーブルと接続（この場合、MovieViewModelはMoviesテーブルの各カラムの型を定義している）
        public DbSet<TestApp.Models.MovieViewModel> Movies { get; set; }
    }
}
