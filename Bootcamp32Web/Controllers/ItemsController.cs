using Bootcamp32Web.Models;
using Bootcamp32Web.VIewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Bootcamp32Web.Controllers
{
    public class ItemsController : Controller
    {
        bootcamp32dbEntities context = new bootcamp32dbEntities();
        // GET: Items
        [HttpGet]
        public ActionResult Index()
        {
            //var item = context.TB_M_Item.ToList();
            //var get = from s in item
            //           select s.name;
            //ItemVM item = new ItemVM
            //{
            //    Items = context.TB_M_Item.ToList()
            //};
            //ViewBag.Supplier = context.TB_M_Supplier.Select(s => new SelectListItem { Text = s.name, Value = s.Id.ToString() });
            ViewBag.Supplier = context.TB_M_Supplier.ToList();
            return View(List());
        }

        public JsonResult List()
        {
            var data = context.TB_M_Item.ToList();
            var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            //var get = from s in context.TB_M_Item
            //          select new { s.name, s.price, s.stock };
            //var json = JsonConvert.SerializeObject(get, new JsonSerializerSettings
            //{
            //    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            //    Formatting = Formatting.Indented
            //});
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // GET: Items/Details/5
        public ActionResult Details(int id)
        {
            var item = context.TB_M_Item.Find(id);
            return View(item);
        }

        //GET: Items/Create
        public ActionResult Create()
        {
            //ViewBag.Supplier = context.TB_M_Supplier.ToList();
            ViewBag.Supplier = context.TB_M_Supplier.Select(s => new SelectListItem { Text = s.name, Value = s.Id.ToString() });
            return View();
        }

        //POST: Items/Create
        //public ActionResult Create(TB_M_Item item)
        [HttpPost]
        public JsonResult Create(ItemVM item)
        {
            //try
            //{
            // TODO: Add insert logic here
            TB_M_Item push = new TB_M_Item();
            push.name = item.name;
            push.price = item.price;
            push.stock = item.stock;
            push.TB_M_Supplier = context.TB_M_Supplier.Find(item.Supplier_Id);
            context.TB_M_Item.Add(push);
            context.SaveChanges();
            //ItemVM itemVM = new ItemVM
            //{
            //    Items = context.TB_M_Item.ToList()
            //};
            //List<TB_M_Item> obj;
            //obj = context.TB_M_Item.ToList();
            
            return Json(context.SaveChanges(), JsonRequestBehavior.AllowGet);
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int id)
        {
            var item = context.TB_M_Item.Find(id);
            ItemVM itemVM = new ItemVM
            {
                Id = item.Id,
                name = item.name,
                price = item.price,
                stock = item.stock,
                Supplier_Id = item.TB_M_Supplier.Id
            };
            ViewBag.Supplier = context.TB_M_Supplier.Select(s => new SelectListItem { Text = s.name, Value = s.Id.ToString() });
            return View(itemVM);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ItemVM item)
        {
            try
            {
                // TODO: Add update logic here
                var push = context.TB_M_Item.Find(id);
                push.name = item.name;
                push.price = item.price;
                push.stock = item.stock;
                push.TB_M_Supplier = context.TB_M_Supplier.Find(item.Supplier_Id);
                //push.TB_M_Supplier = context.TB_M_Supplier.Find(item.TB_M_Supplier);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var item = context.TB_M_Item.Find(id);
                context.TB_M_Item.Remove(item);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
