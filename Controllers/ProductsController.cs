using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Product Product { get; set; }
        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult ProductIndex()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Product = new Product();
            if (id == null)
            {
                //create
                return View(Product);
            }
            //update
            Product = _db.Products.FirstOrDefault(u => u.ProductId == id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Product.ProductId == 0)
                {
                    //create
                    _db.Products.Add(Product);
                }
                else
                {
                    _db.Products.Update(Product);
                }
                _db.SaveChanges();
                return RedirectToAction("ProductIndex");
            }
            return View(Product);
        }
        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Products.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var productFromDb = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == id);
            if (productFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Products.Remove(productFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}