using prjMvcDmeo.Models;
using prjMvcDmeo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDmeo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            if (Session[CDictionary.SK_IS_通過驗證]==null)
                return RedirectToAction("Login");
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLoginViewModel vm)
        {
            CCustomerFactory f = new CCustomerFactory();
            if (f.is驗證帳號與密碼(vm.txtAccount, vm.txtPassword))
            {
                Session[CDictionary.SK_IS_通過驗證] = true;
                return RedirectToAction("Home");
            }
            return View();
        }
    }
}