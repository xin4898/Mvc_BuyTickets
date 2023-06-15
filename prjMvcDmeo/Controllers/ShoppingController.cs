using prjMvcDmeo.Models;
using prjMvcDmeo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDmeo.Controllers
{
    public class ShoppingController : Controller
    {        public ActionResult List()
        {

            dbDemoEntities db = new dbDemoEntities();
            var datas = from p in db.tProduct
                        select p;

            return View(datas);
        }
        // GET: Shopping
        public ActionResult CartView()
        {

            List<CShoppingCartItem> cart = Session[CDictionary.SK_PURCHASED_TICKETS_LIST]
                as List<CShoppingCartItem>;
            if(cart==null)
                return RedirectToAction("List");
            return View(cart);
        }
        public ActionResult AddToCart(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p=>p.fId==vm.txtFId);
            if(prod==null)
                return RedirectToAction("List");
            tShoppingCart x = new tShoppingCart();
            x.fProductId = vm.txtFId;
            x.fPrice = prod.fPrice;
            x.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            x.fCount = vm.txtCount;
            x.fCustomerId = 1;
            db.tShoppingCart.Add(x);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult AddToSession(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == vm.txtFId);
            if (prod == null)
                return RedirectToAction("List");

            List<CShoppingCartItem> cart = Session[CDictionary.SK_PURCHASED_TICKETS_LIST] as List<CShoppingCartItem>;
            if (cart == null)
            {
                cart = new List<CShoppingCartItem>();
                Session[CDictionary.SK_PURCHASED_TICKETS_LIST] = cart;
            }
            CShoppingCartItem item = new CShoppingCartItem();
            item.price =(decimal) prod.fPrice;
            item.productId = vm.txtFId;
            item.count = vm.txtCount;
            item.product = prod;
            cart.Add(item);
            return RedirectToAction("List");
        }

    }
}