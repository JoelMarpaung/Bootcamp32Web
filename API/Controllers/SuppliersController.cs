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
        // GET: api/Suppliers
        public IEnumerable<Supplier> Get()
        {
            return supplier.Get();
        }

        // GET: api/Suppliers/5
        public Supplier Get(int id)
        {
            return supplier.Get(id);
        }

        // POST: api/Suppliers
        public int Post(SupplierVM supplierVM)
        {
            return supplier.Create(supplierVM);
        }

        // PUT: api/Suppliers/5
        public int Put(int id, SupplierVM supplierVM)
        {
            return supplier.Update(id, supplierVM);
        }

        // DELETE: api/Suppliers/5
        public int Delete(int id)
        {
            return supplier.Delete(id);
        }
    }
}
