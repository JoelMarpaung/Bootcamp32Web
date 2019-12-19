using Data.Context;
using Data.Model;
using Data.Repository.Interface;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        MyContext myContext = new MyContext();
        public int Create(SupplierVM supplierVM)
        {
            var supplier = myContext.Suppliers.Where(s =>
            s.Name.ToLower() == supplierVM.Name.ToLower()
            || s.Email.ToLower() == supplierVM.Email.ToLower()).FirstOrDefault();
            int result = 0;
            if (supplier == null)
            {
                var push = new Supplier(supplierVM);
                myContext.Suppliers.Add(push);
                result = myContext.SaveChanges();
            }
            return result;
        }

        public int Delete(int id)
        {
            var delete = myContext.Suppliers.Where(s => s.IsDelete == false && s.Id == id).FirstOrDefault();
            if (delete != null)
            {
                delete.Delete();
            }
            return myContext.SaveChanges();
        }

        public IEnumerable<Supplier> Get()
        {
            return myContext.Suppliers.Where(s=>s.IsDelete==false).ToList();
        }

        public Supplier Get(int id)
        {
            return myContext.Suppliers.Where(s => s.IsDelete == false && s.Id == id).FirstOrDefault();
        }

        public int Update(int id, SupplierVM supplierVM)
        {
            var supplier = myContext.Suppliers.Where(s =>
            s.Name.ToLower() == supplierVM.Name.ToLower()
            || s.Email.ToLower() == supplierVM.Email.ToLower() && s.Id != id).FirstOrDefault();
            int result = 0;
            if (supplier == null)
            {
                var update = myContext.Suppliers.Where(s => s.IsDelete == false && s.Id == id).FirstOrDefault();
                if (update != null)
                {
                    update.Update(supplierVM);
                }
                result = myContext.SaveChanges();
            }
            return result;
        }
    }
}
