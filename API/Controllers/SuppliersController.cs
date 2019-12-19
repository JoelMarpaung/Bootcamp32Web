using API.Service;
using API.Service.Interface;
using Data.Model;
using Data.Repository;
using Data.Repository.Interface;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class SuppliersController : ApiController
    {
        ISupplierRepository supplier = new SupplierRepository();
        ISupplierService supplierService = new SupplierService();

        public SuppliersController() { }
        public SuppliersController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }
        // GET: api/Suppliers
        public IEnumerable<Supplier> Get()
        {
            //return supplier.Get();
            return supplierService.Get();
        }

        // GET: api/Suppliers/5
        public Supplier Get(int id)
        {
            return supplierService.Get(id);
        }

        // POST: api/Suppliers
        public HttpResponseMessage Post(SupplierVM supplierVM)
        {
            if (supplierService.Create(supplierVM)==0)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            //return supplier.Create(supplierVM);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT: api/Suppliers/5
        public int Put(int id, SupplierVM supplierVM)
        {
            return supplierService.Update(id, supplierVM);
        }

        // DELETE: api/Suppliers/5
        public int Delete(int id)
        {
            return supplierService.Delete(id);
        }
    }
}
