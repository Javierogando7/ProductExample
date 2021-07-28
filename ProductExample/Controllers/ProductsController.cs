using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductExample.Models;

namespace ProductExample.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext db = new ProductContext();
        public ProductsController()
        {
            SeedProducts();
        }

        public void SeedProducts()
        {
            if (db.Products.ToList().Count <= 0)
            {
                var colors = new List<ColorPrice>
            {
                new ColorPrice
                {
                    ColorName = "Blue",
                    Price = 2.00M,
                    ProductId = 1
                }
            };
                var newProduct = new Product
                {
                    Name = "Nombre del producto 1",
                    Description = "Descripcion del producto 1",
                    ColorPrices = colors
                };
                var colors2 = new List<ColorPrice>
            {
                new ColorPrice
                {
                    ColorName = "Red",
                    Price = 3.00M,
                    ProductId = 2
                },
                new ColorPrice
                {
                    ColorName = "Blue",
                    Price = 2.00M,
                    ProductId = 2
                }
            };
                var newProduct2 = new Product
                {
                    Name = "Nombre del producto 2",
                    Description = "Descripcion del producto 2",
                    ColorPrices = colors2
                };
                db.Products.Add(newProduct);
                db.Products.Add(newProduct2);
                db.SaveChanges();
            }
        }

        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        public ActionResult Details(int? id, int colorId = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(p => p.ColorPrices).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorId = colorId;
            return View(product);
        }

        public ActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(p => p.ColorPrices).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
