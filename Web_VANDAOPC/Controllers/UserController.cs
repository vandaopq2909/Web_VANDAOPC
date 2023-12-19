using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Web_VANDAOPC.Models;

namespace Web_VANDAOPC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login() 
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            if (user != null)
            {
                User myUser = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                if (myUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(user.Password, myUser.Password))
                    {
                        HttpCookie authCookie = new HttpCookie("auth", myUser.UserName);
                        HttpCookie NameUserCookie = new HttpCookie("NameUser", myUser.DisplayName);
                        HttpCookie roleCookie = new HttpCookie("role", myUser.Role);
                        Response.Cookies.Add(authCookie);
                        Response.Cookies.Add(roleCookie);
                        Response.Cookies.Add(NameUserCookie);

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("Password", "Username hoặc Password không chính xác!");
            }
            return View();
        }
        public ActionResult Logout()
        {
            HttpCookie authCookie = new HttpCookie("auth");
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpCookie roleCookie = new HttpCookie("role");
            roleCookie.Expires = DateTime.Now.AddDays(-1);
            HttpCookie NameUserCookie = new HttpCookie("NameUser");
            NameUserCookie.Expires = DateTime.Now.AddDays(-1);



            Response.Cookies.Add(authCookie);
            Response.Cookies.Add(roleCookie);
            Response.Cookies.Add(NameUserCookie);

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, string retypePassword)
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (user.Password != retypePassword)
            {
                ModelState.AddModelError("retypePassword", "Mật khẩu không khớp!");
                return View();
            }

            User myUser = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập này đã tồn tại!");
                return View();
            }
            myUser = db.Users.Where(u => u.EmailAddress == user.EmailAddress).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("EmailAddress", "Địa chỉ email này đã tồn tại!");
                return View();
            }

            myUser = new User();
            myUser.UserName = user.UserName;
            myUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            myUser.EmailAddress = user.EmailAddress;
            myUser.PhoneNumber = user.PhoneNumber;
            myUser.DisplayName = user.DisplayName;
            myUser.Role = "user";
            db.Users.Add(myUser);
            db.SaveChanges();

            return RedirectToAction("Login", "User");
        }
    }
}