using Bootcamp32Web.Models;
using Data.Model;
using Data.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bootcamp32Web.Controllers
{
    public class SuppliersController : Controller
    {
        bootcamp32dbEntities context = new bootcamp32dbEntities();
        readonly HttpClient client = new HttpClient();

        public SuppliersController()
        {
            client.BaseAddress = new Uri("http://localhost:61413/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Suppliers
        public ActionResult Index()
        {
            //var supplier = context.TB_M_Supplier.ToList();
            //return View(List());
            return View(List());
        }

        public JsonResult List()
        {
            var response = client.GetAsync("suppliers");
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IList<Supplier>>();
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

        public JsonResult InsertOrUpdate(SupplierVM supplierVM)
        {
            var myContent = JsonConvert.SerializeObject(supplierVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (supplierVM.Id.Equals(0))
            {
                var result = client.PostAsync("Suppliers", byteContent).Result;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = client.PutAsync("Suppliers/" + supplierVM.Id, byteContent).Result;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetById(int id)
        {
            var response = client.GetAsync("suppliers/"+id);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<Supplier>();
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
            var result = client.DeleteAsync("Suppliers/" + id).Result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: Suppliers/Details/5
        //public ActionResult Details(int id)
        //{
        //    var supplier = context.TB_M_Supplier.Find(id);
        //    return View(supplier);
        //}

        //// GET: Suppliers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Suppliers/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        TB_M_Supplier supplier = new TB_M_Supplier
        //        {
        //            name = collection["name"],
        //            email = collection["email"],
        //            createDate = DateTimeOffset.Now
        //        };
        //        context.TB_M_Supplier.Add(supplier);
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Suppliers/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var supplier = context.TB_M_Supplier.Find(id);
        //    return View(supplier);
        //}

        //// POST: Suppliers/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        var supplier = context.TB_M_Supplier.Find(id);
        //        supplier.name = collection["name"];
        //        supplier.email = collection["email"];
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Suppliers/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var supplier = context.TB_M_Supplier.Find(id);
        //        context.TB_M_Supplier.Remove(supplier);
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// POST: Suppliers/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
