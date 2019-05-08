using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class BackPack
    {
        private Item[] currentPack = new Item[5];
        private Dictionary<int, Dictionary<Item, int>> m_ItemsPack = new Dictionary<int, Dictionary<Item, int>>();
        private Item m_CurrentItem = null;
        /// <summary>
        /// 物品ID，物品,物品数量
        /// </summary>
        public Dictionary<int, Dictionary<Item, int>> ItemsPack { get => m_ItemsPack; set => m_ItemsPack = value; }
        public Item[] CurrentPack { get => currentPack; set => currentPack = value; }
        public Item CurrentItem { get => m_CurrentItem; set => m_CurrentItem = value; }

        public int GetItemCount()
        {
            return ItemsPack.Count();
        }
        public void AddItemToBackPack(Item item)
        {
            if (item == null)
            {
                Debug.LogError("添加物品为空");
                return;
            }
            if (ItemsPack.ContainsKey(item.Id))
            {
                Dictionary<Item, int> tmp;
                ItemsPack.TryGetValue(item.Id, out tmp);
                tmp[tmp.First().Key]++;
            }
            else
            {
                Dictionary<Item, int> tmp = new Dictionary<Item, int>();
                tmp.Add(item, 1);
                ItemsPack.Add(item.Id, tmp);
            }
            //m_BackPack.Add(item);
        }
        //public void DeleteItemFromBackPack(Item item)
        //{
        //    if (num >= 0 && num < ItemsPack.Count)
        //    {
        //        ItemsPack.Remove(num);
        //    }
        //    else
        //    {
        //        Debug.LogError("添加物品为空");
        //        return;
        //    }

        //}
        public void DeleteItemFromBackPack(Item item)
        {
            if (item == null)
            {
                Debug.LogError("要丢弃物品为空");
                return;
            }
            if (ItemsPack.ContainsKey(item.Id))
            {
                Dictionary<Item, int> tmp;
                ItemsPack.TryGetValue(item.Id, out tmp);
                tmp[tmp.First().Key]--;
                if (tmp[tmp.First().Key] == 0)
                {
                    ItemsPack.Remove(item.Id);
                }
            }
            else
            {
                Debug.LogError("BackPack Dont Contain This Item");
            }
        }


        public void UseItemAtBackPack(Item item)
        {
            ItemManagerService.Instance.ProductItem(item);

            //ItemsPack[num].First().Key.UseItem();
        }
    }
}
