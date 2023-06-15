using prjMvcDmeo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDmeo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult List()
        {
            CCustomerFactory f = new CCustomerFactory();
            string keyword = Request.Form["txtKeyword"];

            List<CCustomer> datas = null;
            if(string.IsNullOrEmpty(keyword))
                datas = f.queryAll();
            else
                datas = f.queryByKeyword(keyword);
            return View(datas);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            CCustomer x = (new CCustomerFactory()).queryById((int)id);
            if(x==null)
                return RedirectToAction("List");
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(CCustomer x)
        {
            (new CCustomerFactory()).update(x);
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
                (new CCustomerFactory()).delete((int)id);
            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactory()).create(x);
            return RedirectToAction("List");
        }
    }
}