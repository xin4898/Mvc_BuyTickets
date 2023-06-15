using prjMvcDmeo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDmeo.Controllers
{
    public class AController : Controller
    {
        static int count = 0;
        public ActionResult demoFileUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult demoFileUpload(HttpPostedFileBase photo)
        {
            photo.SaveAs(@"C:\QNote\8000.jpg");
            return View();
        }
        public ActionResult showCountByCookie()
        {
            int count = 0;
            HttpCookie x = Request.Cookies["COUNT"];
            if (x != null)
                count = Convert.ToInt32(x.Value);
            count++;
            x = new HttpCookie("COUNT");
            x.Value = count.ToString();
            x.Expires = DateTime.Now.AddSeconds(20);
            Response.Cookies.Add(x);
            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult showCountBySession()
        {
            int count = 0;
            if (Session["COUNT"] != null)
                count = (int)Session["COUNT"];
            count++;
            Session["COUNT"] = count;
            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult showCount()
        {
            count++;
            ViewBag.COUNT = count;
            return View();
        }

        public ActionResult demoForm()
        {
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]) &&
                !string.IsNullOrEmpty(Request.Form["txtB"]))
            {
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                double c = Convert.ToDouble(Request.Form["txtC"]);
                ViewBag.a = a;
                ViewBag.b = b;
                ViewBag.c = c;
                double r = b * b - 4 * a * c;
                r = Math.Sqrt(r);
                ViewBag.ANS = ((-b + r) / (2 * a)).ToString("0.0#") +
                    " Or X=" + ((-b - r) / (2 * a)).ToString();
            }
            return View();
        }


        public string testingQuery()
        {
            CCustomerFactory f = new CCustomerFactory();
            List<CCustomer> x = f.queryAll();
            return "目前客戶筆數：" + x.Count.ToString();
        }
        public string testingDelete(int? id)
        {
            if (id == null)
                return "沒有指定id";
            (new CCustomerFactory()).delete((int)id);
            return "刪除資料成功";
        }
        public string testingUpdate()
        {
            CCustomer x = new CCustomer()
            {
                fId = 5,
                fName = "Jerry Kuo",
                fPhone = "0916225446",
                fEmail = "jerry@kmt.org.tw",
                // fAddress = "Kaoshung",
                fPassword = "1234"
            };
            (new CCustomerFactory()).update(x);
            return "修改資料成功";
        }
        public string testingInsert()
        {
            CCustomer x = new CCustomer()
            {
                fName = "Tom",
                //fPhone = "0933654125",
                fEmail = "tom@gmail.com",
                // fAddress = "Kaoshung",
                fPassword = "1234"
            };
            (new CCustomerFactory()).create(x);
            return "新增資料成功";
        }
        public ActionResult bindingById(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    x = new CCustomer();
                    x.fId = (int)reader["fId"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                }
                con.Close();
            }
            return View(x);
        }
        public ActionResult showById(int? id)
        {
            if (id != null)
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    CCustomer x = new CCustomer();
                    x.fId = (int)reader["fId"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    ViewBag.KK = x;
                }
                con.Close();
            }
            return View();
        }
        public string queryById(int? id)
        {
            if (id == null)
                return "沒有指定 id";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
            string s = "找不到任何符合條件的資料";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                s = "客戶：" + reader["fName"].ToString() + " <br/> " + reader["fPhone"].ToString();
            }
            con.Close();
            return s;
        }

        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }

        public string demoParameter(int? id)
        {
            if (id == null)
                return "沒有指定id";
            if (id == 0)
                return "XBox 加入購物車成功";
            else if (id == 1)
                return "PS5 加入購物車成功";
            else if (id == 2)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoRequest()
        {
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "XBox 加入購物車成功";
            else if (id == "1")
                return "PS5 加入購物車成功";
            else if (id == "2")
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\qnote\01.jpg");
            Response.End();
            return "";
        }
        public string sayHello()
        {
            return "Hello ASP.NET MVC.";
        }
        [NonAction]



        public string lotto()
        {

            return (new CLottoGen()).getNumbers();
        }
        // GET: A

    }
}