using Bootcamp32Web.Models;
using Data.Model;
using Data.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Bootcamp32Web.Controllers
{
    public class ItemsController : Controller
    {
        bootcamp32dbEntities context = new bootcamp32dbEntities();
        readonly HttpClient client = new HttpClient();

        public ItemsController()
        {
            client.BaseAddress = new Uri("http://localhost:61413/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult Index()
        {
            return View(List());
        }

        public JsonResult List()
        {
            var response = client.GetAsync("items");
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IList<Item>>();
                data.Wait();
                //var json = JsonConvert.SerializeObject(data.Result, Formatting.None, new JsonSerializerSettings()
                //{
                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //});
                var json = data.Result;
                return Json(json, JsonRequestBehavior.AllowGet);
            }

            return Json("Internal Server Error", JsonRequestBehavior.AllowGet);
        }

        //public JsonResult Supplier()
        //{
        //    var response = client.GetAsync("suppliers");
        //    response.Wait();
        //    var result = response.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var data = result.Content.ReadAsAsync<IList<Supplier>>();
        //        data.Wait();
        //        //var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
        //        //{
        //        //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //        //});
        //        var json = data.Result;
        //        return Json(json, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json("Internal Server Error", JsonRequestBehavior.AllowGet);
        //}

        public JsonResult InsertOrUpdate(ItemVM itemVM)
        {
            var myContent = JsonConvert.SerializeObject(itemVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (itemVM.Id.Equals(0))
            {
                var result = client.PostAsync("Items", byteContent).Result;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = client.PutAsync("Items/" + itemVM.Id, byteContent).Result;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetById(int id)
        {
            var response = client.GetAsync("items/" + id);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<Item>();
                data.Wait();
                //var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
                //{
                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //});
                var json = data.Result;
                return Json(json, JsonRequestBehavior.AllowGet);
            }

            return Json("Internal Server Error", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var result = client.DeleteAsync("Items/" + id).Result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Items
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    //var item = context.TB_M_Item.ToList();
        //    //var get = from s in item
        //    //           select s.name;
        //    //ItemVM item = new ItemVM
        //    //{
        //    //    Items = context.TB_M_Item.ToList()
        //    //};
        //    //ViewBag.Supplier = context.TB_M_Supplier.Select(s => new SelectListItem { Text = s.name, Value = s.Id.ToString() });
        //    ViewBag.Supplier = context.TB_M_Supplier.ToList();
        //    return View(List());
        //}

        //public JsonResult List()
        //{
        //    var data = context.TB_M_Item.ToList();
        //    var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
        //    {
        //        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //    });
        //    //var get = from s in context.TB_M_Item
        //    //          select new { s.name, s.price, s.stock };
        //    //var json = JsonConvert.SerializeObject(get, new JsonSerializerSettings
        //    //{
        //    //    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        //    //    Formatting = Formatting.Indented
        //    //});
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}

        //// GET: Items/Details/5
        //public ActionResult Details(int id)
        //{
        //    var item = context.TB_M_Item.Find(id);
        //    return View(item);
        //}

        ////GET: Items/Create
        //public ActionResult Create()
        //{
        //    //ViewBag.Supplier = context.TB_M_Supplier.ToList();
        //    ViewBag.Supplier = context.TB_M_Supplier.Select(s => new SelectListItem { Text = s.name, Value = s.Id.ToString() });
        //    return View();
        //}

        ////POST: Items/Create
        ////public ActionResult Create(TB_M_Item item)
        //[HttpPost]
        //public JsonResult Create(ItemVM item)
        //{
        //    //try
        //    //{
        //    // TODO: Add insert logic here
        //    TB_M_Item push = new TB_M_Item();
        //    push.name = item.name;
        //    push.price = item.price;
        //    push.stock = item.stock;
        //    push.TB_M_Supplier = context.TB_M_Supplier.Find(item.Supplier_Id);
        //    context.TB_M_Item.Add(push);
        //    context.SaveChanges();
        //    //ItemVM itemVM = new ItemVM
        //    //{
        //    //    Items = context.TB_M_Item.ToList()
        //    //};
        //    //List<TB_M_Item> obj;
        //    //obj = context.TB_M_Item.ToList();

        //    return Json(context.SaveChanges(), JsonRequestBehavior.AllowGet);
        //    //}
        //    //catch
        //    //{
        //    //    return View();
        //    //}
        //}

        //// GET: Items/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var item = context.TB_M_Item.Find(id);
        //    ItemVM itemVM = new ItemVM
        //    {
        //        Id = item.Id,
        //        name = item.name,
        //        price = item.price,
        //        stock = item.stock,
        //        Supplier_Id = item.TB_M_Supplier.Id
        //    };
        //    ViewBag.Supplier = context.TB_M_Supplier.Select(s => new SelectListItem { Text = s.name, Value = s.Id.ToString() });
        //    return View(itemVM);
        //}

        //// POST: Items/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, ItemVM item)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        var push = context.TB_M_Item.Find(id);
        //        push.name = item.name;
        //        push.price = item.price;
        //        push.stock = item.stock;
        //        push.TB_M_Supplier = context.TB_M_Supplier.Find(item.Supplier_Id);
        //        //push.TB_M_Supplier = context.TB_M_Supplier.Find(item.TB_M_Supplier);
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Items/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var item = context.TB_M_Item.Find(id);
        //        context.TB_M_Item.Remove(item);
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
