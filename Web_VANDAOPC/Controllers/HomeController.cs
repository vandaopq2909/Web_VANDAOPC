using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_VANDAOPC.Models;

namespace Web_VANDAOPC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index(string Search = "")
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            ViewBag.Search = Search;
            if (Search == "")
            {
                //get list PC Gaming
                List<Product> listPCGaming = db.Products.Where(x => x.CatId == 1).Take(4).ToList();
                ViewBag.listPCGaming = listPCGaming;

                //get list PC Làm Việc
                List<Product> listPCWork = db.Products.Where(x => x.CatId == 2).Take(4).ToList();
                ViewBag.listPCWork = listPCWork;

            }
            else
            {
                //search
                List<Product> listSearch = db.Products.Where(x => x.Name.Contains(Search)).ToList();
                ViewBag.listSearch = listSearch;
            }
            return View();
        }
        public ActionResult About()
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;
            return View();
        }
    }
}