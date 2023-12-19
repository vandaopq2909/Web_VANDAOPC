using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_VANDAOPC.Models;

namespace Web_VANDAOPC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string Search = "", string SortOrder = "", int page = 1)
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;


            //get all product
            List<Product> listProduct = db.Products.ToList();

            //search
            if (Search != "")
            {                
                //get list prduct for search by name 
                listProduct = db.Products.Where(x => x.Name.Contains(Search)).ToList();
                ViewBag.Search = Search;
            }               

            //sort
            switch (SortOrder)
            {
                case "Name":
                    listProduct = listProduct.OrderBy(row => row.Name).ToList();
                    break;
                case "Price-A-Z":
                    listProduct = listProduct.OrderBy(row => row.Price).ToList();
                    break;
                case "Price-Z-A":
                    listProduct = listProduct.OrderByDescending(row => row.Price).ToList();
                    break;
                case "Id":
                    listProduct = listProduct.OrderBy(row => row.Id).ToList();
                    break;
                case "CatId":
                    listProduct = listProduct.OrderBy(row => row.CatId).ToList();
                    break;
                default:
                    break;
               
            }
            ViewBag.SortOrder = SortOrder;

            //phân trang
            int NoOfRecordPerPage = 4;
            int NoOfPages = Convert.ToInt32(Math.Ceiling
                (Convert.ToDouble(listProduct.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.page = page;
            ViewBag.NoOfPages = NoOfPages;
            listProduct = listProduct.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(listProduct);
        }
        public ActionResult GetAllProducts(string SortOrder = "", int page = 1)
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            //get all product
            List<Product> listProduct = db.Products.ToList();

            //sort
            switch (SortOrder)
            {
                case "Name":
                    listProduct = listProduct.OrderBy(row => row.Name).ToList();
                    break;
                case "Price-A-Z":
                    listProduct = listProduct.OrderBy(row => row.Price).ToList();
                    break;
                case "Price-Z-A":
                    listProduct = listProduct.OrderByDescending(row => row.Price).ToList();
                    break;
                case "Id":
                    listProduct = listProduct.OrderBy(row => row.Id).ToList();
                    break;
                case "CatId":
                    listProduct = listProduct.OrderBy(row => row.CatId).ToList();
                    break;
                default:
                    break;
            }
            ViewBag.SortOrder = SortOrder;

            //phân trang
            int NoOfRecordPerPage = 4;
            int NoOfPages = Convert.ToInt32(Math.Ceiling
                (Convert.ToDouble(listProduct.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.page = page;
            ViewBag.NoOfPages = NoOfPages;
            listProduct = listProduct.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(listProduct);
        }
        public ActionResult Detail(int Id) 
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            //get detail product
            Product product = db.Products.Where(row => row.Id == Id).FirstOrDefault();
            return View(product);
        }
        public ActionResult SearchByCategory(int Id, int page = 1)
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            //get Category Name
            ViewBag.CatName = db.Categories.Where(row => row.CatId == Id).FirstOrDefault().Name.ToString();
            
            //get list product by category id
            List<Product> listSearch = db.Products.Where(row => row.CatId == Id).ToList();
            //get category id
            ViewBag.CatId = Id;

            //phân trang
            int NoOfRecordPerPage = 4;
            int NoOfPages = Convert.ToInt32(Math.Ceiling
                (Convert.ToDouble(listSearch.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.page = page;
            ViewBag.NoOfPages = NoOfPages;
            listSearch = listSearch.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(listSearch);
        }
        public ActionResult Create()
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase imageFile)
        {
            if(ModelState.IsValid)
            {
                MyDBContext db = new MyDBContext();

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("Image", "Kích thước file phải nhỏ hơn 2MB.");
                        return View();
                    }

                    var allowEx = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowEx.Contains(fileEx))
                    {
                        ModelState.AddModelError("Image", "Chỉ chấp nhận file ảnh jpg hoặc png.");
                        return View();
                    }

                    product.Img = "";
                    db.Products.Add(product);
                    db.SaveChanges();

                    Product pro = db.Products.ToList().Last();

                    var fileName = pro.Id.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageFile.SaveAs(path);

                    pro.Img = fileName;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    product.Img = "";
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }                   
            }
            else 
                return View();
        }
        public ActionResult Edit(int Id)
        {
            MyDBContext db = new MyDBContext();

            //get list category
            List<Category> listCategory = db.Categories.ToList();
            ViewBag.listCategory = listCategory;

            //get product need edit
            Product product = db.Products.Where(x => x.Id == Id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase imageFile)
        {
            if(ModelState.IsValid)
            {
                MyDBContext db = new MyDBContext();
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("Image", "Kích thước file phải nhỏ hơn 2MB.");
                        return View();
                    }

                    var allowEx = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowEx.Contains(fileEx))
                    {
                        ModelState.AddModelError("Image", "Chỉ chấp nhận file ảnh jpg hoặc png.");
                        return View();
                    }

                    //product.Img = "";
                    Product newPro = db.Products.Where(x => x.Id == product.Id).FirstOrDefault();
                    newPro.Name = product.Name;
                    newPro.Description = product.Description;
                    newPro.Price = product.Price;
                    newPro.Status = product.Status;
                    newPro.Brand = product.Brand;
                    newPro.CatId = product.CatId;
                    newPro.Details = product.Details;

                    var fileName = product.Id.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    imageFile.SaveAs(path);

                    newPro.Img = fileName;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Product newPro = db.Products.Where(x => x.Id == product.Id).FirstOrDefault();
                    newPro.Name = product.Name;
                    newPro.Description = product.Description;
                    newPro.Price = product.Price;
                    newPro.Status = product.Status;
                    newPro.Brand = product.Brand;
                    newPro.CatId = product.CatId;
                    newPro.Details = product.Details;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }   
            }
            else
            {
                return View();
            }           
        }
        public ActionResult Delete(int Id)
        {
            MyDBContext db = new MyDBContext();

            //get product need delete
            Product pro = db.Products.Where(x => x.Id == Id).FirstOrDefault(); 
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}