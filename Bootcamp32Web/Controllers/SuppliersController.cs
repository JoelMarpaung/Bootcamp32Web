using Bootcamp32Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bootcamp32Web.Controllers
{
    public class SuppliersController : Controller
    {
        bootcamp32dbEntities context = new bootcamp32dbEntities();
        // GET: Suppliers
        public ActionResult Index()
        {
            var supplier = context.TB_M_Supplier.ToList();
            return View(supplier);
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int id)
        {
            var supplier = context.TB_M_Supplier.Find(id);
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                TB_M_Supplier supplier = new TB_M_Supplier
                {
                    name = collection["name"],
                    email = collection["email"],
                    createDate = DateTimeOffset.Now
                };
                context.TB_M_Supplier.Add(supplier);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int id)
        {
            var supplier = context.TB_M_Supplier.Find(id);
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var supplier = context.TB_M_Supplier.Find(id);
                supplier.name = collection["name"];
                supplier.email = collection["email"];
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var supplier = context.TB_M_Supplier.Find(id);
                context.TB_M_Supplier.Remove(supplier);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Suppliers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
