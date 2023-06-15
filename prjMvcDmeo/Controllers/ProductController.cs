using prjMvcDmeo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDmeo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            dbDemoEntities db = new dbDemoEntities();
            IEnumerable<tProduct> datas = null;
            if (string.IsNullOrEmpty(keyword))
            {
                datas = from p in db.tProduct
                        select p;
            }
            else
                datas = db.tProduct.Where(p=>p.fName.Contains(keyword));
            return View(datas);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");           
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == id);
            return View(prod);
        }
        [HttpPost]
        public ActionResult Edit(tProduct x)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == x.fId);
            if (prod != null)
            {
                
                if (x.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    x.photo.SaveAs(Server.MapPath("../../Images/"+ photoName));
                    prod.fImagePath = photoName;
                }
                prod.fName = x.fName;
                prod.fQty = x.fQty;
                prod.fCost = x.fCost;
                prod.fPrice = x.fPrice;
                db.SaveChanges();                
            }
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbDemoEntities db = new dbDemoEntities();
                tProduct prod=db.tProduct.FirstOrDefault(p=>p.fId== id);
                if (prod != null)
                {
                    db.tProduct.Remove(prod);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tProduct t)
        {
            dbDemoEntities db = new dbDemoEntities();
            db.tProduct.Add(t);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}