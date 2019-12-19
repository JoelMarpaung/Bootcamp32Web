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
    public class ItemRepository : IItemRepository
    {
        MyContext myContext = new MyContext();
        public int Create(ItemVM itemVM)
        {
            var item = myContext.Items.Where(i =>
            i.Name.ToLower() == itemVM.name.ToLower()).FirstOrDefault();
            int result = 0;
            if (item == null)
            {
                var supplier = myContext.Suppliers.Find(itemVM.Supplier_Id);
                var push = new Item(itemVM, supplier);
                myContext.Items.Add(push);
                result = myContext.SaveChanges();
            }
            return result;
        }

        public int Delete(int id)
        {
            var delete = myContext.Items.Where(s => s.IsDelete == false && s.Id == id).FirstOrDefault();
            if (delete != null)
            {
                delete.Delete();
            }
            return myContext.SaveChanges();
        }

        public IEnumerable<Item> Get()
        {
            return myContext.Items.Include("Supplier").Where(s => s.IsDelete == false).OrderByDescending(i=>i.Id);
        }

        public Item Get(int id)
        {
            return myContext.Items.Include("Supplier").Where(s => s.IsDelete == false && s.Id == id).FirstOrDefault();
        }

        public int Update(int id, ItemVM itemVM)
        {
            var item = myContext.Items.Where(i =>
            i.Name.ToLower() == itemVM.name.ToLower() && i.Id != id).FirstOrDefault();
            int result = 0;
            if (item == null)
            {
                var supplier = myContext.Suppliers.Find(itemVM.Supplier_Id);
                var update = myContext.Items.Where(i => i.IsDelete == false && i.Id == id).FirstOrDefault();
                if (update != null)
                {
                    update.Update(itemVM, supplier);
                }
                result = myContext.SaveChanges();
            }
            return result;
        }
    }
}
