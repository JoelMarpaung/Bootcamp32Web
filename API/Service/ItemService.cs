using API.Service.Interface;
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
    public class ItemService : IItemService
    {
        IItemRepository itemRepository = new ItemRepository();
        int result = 0;
        public ItemService() { }
        public ItemService(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }
        public int Create(ItemVM itemVM)
        {
            if (Validator(itemVM))
            {
                result = itemRepository.Create(itemVM);
            }
            return result;
        }

        public int Delete(int id)
        {
            return itemRepository.Delete(id);
        }

        public IEnumerable<Item> Get()
        {
            return itemRepository.Get();
        }

        public Item Get(int id)
        {
            return itemRepository.Get(id);
        }

        public int Update(int id, ItemVM itemVM)
        {
            if (Validator(itemVM))
            {
                result = itemRepository.Update(id, itemVM);
            }
            return result;
        }

        public bool Validator(ItemVM itemVM)
        {
            if (itemVM.name != null && itemVM.price > 0 && itemVM.stock > 0 && itemVM.name != "")
            {
                return true;
            }
            return false;
        }
    }
}