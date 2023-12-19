using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_VANDAOPC.Models;

namespace Web_VANDAOPC.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            MyDBContext db = new MyDBContext(); 
            List<Cart> listCart = db.Carts.ToList();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            return View(listCart);
        }
        public ActionResult Add(int Id = 0)
        {
            if (Id > 0)
            {
                MyDBContext db = new MyDBContext();
                Cart cartItem = db.Carts.Where(cart => cart.Id == Id).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    Cart cart = new Cart();
                    cart.Id = Id;
                    cart.Quantity = 1;
                    db.Carts.Add(cart);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id) 
        {
            MyDBContext db = new MyDBContext();
            Cart cart = db.Carts.Where(x => x.Id == Id).FirstOrDefault();
            db.Carts.Remove(cart); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult UpdateQuantity(int quantity = 0, int proid = 0)
        {
            MyDBContext db = new MyDBContext();
            if (quantity > 0)
            {
                Cart cartItem = db.Carts.Where(cart => cart.Id == proid).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Pay()
        {
            MyDBContext db = new MyDBContext();
            //get carts
            List<Cart> listCart = db.Carts.ToList();
            ViewBag.listCart = listCart;

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            return View();
        }
        [HttpPost]
        public ActionResult Pay(User customer, string CustomerAddress)
        {
            MyDBContext db = new MyDBContext();
            //get carts
            List<Cart> listCart = db.Carts.ToList();
            ViewBag.listCart = listCart;

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (customer.PhoneNumber == null)
            {
                ModelState.AddModelError("PhoneNumber", "Số Điện Thoại Không Được Bỏ Trống!");
                return View();
            }
            if (CustomerAddress == null)
            {
                ModelState.AddModelError("CustomerAddress", "Địa Chỉ Giao Hàng Không Được Bỏ Trống!");
                return View();               
            }   
            else
            {
                // Lấy tất cả các dòng từ bảng
                var allEntities = db.Carts.ToList();

                // Xóa tất cả các dòng
                db.Carts.RemoveRange(allEntities);

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }   
            return RedirectToAction("Thanks");
        }
        public ActionResult Thanks()
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            return View();
        }
    }
}