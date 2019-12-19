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
    public class ItemsController : ApiController
    {
        IItemRepository item = new ItemRepository();
        IItemService itemService = new ItemService();

        public ItemsController() { }
        public ItemsController(IItemService itemService)
        {
            this.itemService = itemService;
        }
        // GET: api/Items
        public IEnumerable<Item> Get()
        {
            return itemService.Get();
        }

        // GET: api/Items/5
        public Item Get(int id)
        {
            return itemService.Get(id);
        }

        // POST: api/Items
        public HttpResponseMessage Post(ItemVM itemVM)
        {
            if (itemService.Create(itemVM) == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            //return supplier.Create(supplierVM);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT: api/Items/5
        public int Put(int id, ItemVM itemVM)
        {
            return itemService.Update(id, itemVM);
        }

        // DELETE: api/Items/5
        public int Delete(int id)
        {
            return itemService.Delete(id);
        }
    }
}
