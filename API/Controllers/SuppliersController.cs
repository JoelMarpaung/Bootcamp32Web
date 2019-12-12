using Data.Model;
using Data.Repository;
using Data.Repository.Interface;
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
            var data = supplier.Get();
            return data;
        }

        // GET: api/Suppliers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Suppliers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Suppliers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Suppliers/5
        public void Delete(int id)
        {
        }
    }
}
