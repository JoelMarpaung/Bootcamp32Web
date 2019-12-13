using API.Service.Interface;
using Data.Context;
using Data.Model;
using Data.Repository;
using Data.Repository.Interface;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Service
{
    public class SupplierService : ISupplierService
    {
        ISupplierRepository supplierRepository = new SupplierRepository();
        int result = 0;

        public SupplierService() { }
        public SupplierService(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public int Create(SupplierVM supplierVM)
        {
            if (Validator(supplierVM))
            {
                result = supplierRepository.Create(supplierVM);
            }
            return result;
        }

        public int Delete(int id)
        {
            return supplierRepository.Delete(id);
        }

        public IEnumerable<Supplier> Get()
        {
            return supplierRepository.Get();
        }

        public Supplier Get(int id)
        {
            return supplierRepository.Get(id);
        }

        public int Update(int id, SupplierVM supplierVM)
        {
            if (Validator(supplierVM))
            {
                result = supplierRepository.Update(id, supplierVM);
            }
            return result;
        }

        public bool Validator(SupplierVM supplierVM)
        {
            if(supplierVM.Name!=null && supplierVM.Email != null && supplierVM.Name != "" && supplierVM.Email != "")
            {
                return true;
            }
            return false;
        }
    }
}