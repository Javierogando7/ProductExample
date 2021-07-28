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
    public class ColorPricesController : Controller
    {
        private readonly ProductContext db = new ProductContext();

        public ActionResult Create(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = productId;
            return View(new ColorPrice { ProductId = productId.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,ColorName,Price")] ColorPrice colorPrice)
        {
            if (ModelState.IsValid)
            {
                db.Colors.Add(colorPrice);
                db.SaveChanges();
                return RedirectToAction("Edit", "Products", new { id = colorPrice.ProductId });
            }

            return View(colorPrice);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorPrice colorPrice = db.Colors.FirstOrDefault(c => c.Id == id);
            if (colorPrice == null)
            {
                return HttpNotFound();
            }
            return View(colorPrice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,ColorName,Price")] ColorPrice colorPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colorPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Products", new { id = colorPrice.ProductId });
            }
            return View(colorPrice);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorPrice colorPrice = db.Colors.FirstOrDefault(c => c.Id == id);
            if (colorPrice == null)
            {
                return HttpNotFound();
            }
            return View(colorPrice);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ColorPrice colorPrice = db.Colors.FirstOrDefault(c => c.Id == id);
            db.Colors.Remove(colorPrice);
            db.SaveChanges();
            return RedirectToAction("Edit", "Products", new { id = colorPrice.ProductId });
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
