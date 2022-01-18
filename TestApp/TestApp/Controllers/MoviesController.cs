﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly TestAppContext _context;

        public MoviesController(TestAppContext context)
        {
            // DI でインスタンス化しない場合は以下の記述が必要
            //var options = new DbContextOptions<TestAppContext>();
            //var context = new TestAppContext(options);

            // DBをインスタンス化しているようなもの(ヤンマー見積システムでいうと"h_mitumori"をインスタンス化)
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            // Moviesテーブルの全データを配列化してViewに渡している
            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        // int? → ?はNullでも可能ということ。だからパラメータが無くてもOK
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // idと一致するデータをMoviesテーブルから取得する
            var data = await _context.Movies.FirstOrDefaultAsync(d => d.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // [Bind("Id,Title,ReleaseDate,Genre,Price")] をつけない場合はMovieViewModelのプロパティすべてに値を設定してPOSTすることが可能になる
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] MovieViewModel movies)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] MovieViewModel movies)
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // エラーメッセージを画面に表示させる
                        throw;
                    }
                }
                // 更新がうまく行けばIndex画面へリダイレクト
                return RedirectToAction(nameof(Index));
            }
            // 
            return View(movies);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movies = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
