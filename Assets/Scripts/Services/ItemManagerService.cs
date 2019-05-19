using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyService
{
    public class ItemManagerService : Singleton<ItemManagerService>
    {
        ItemManager itemManager;
        public void ProductItem(Item item)
        {
            if (GameObject.Find("Main").GetComponent<ItemManager>() == null)
            {
                Debug.LogError("Main Cant find ItemManager");
                return;
            }
            itemManager = GameObject.Find("Main").GetComponent<ItemManager>();
            if (itemManager == null)
            {
                Debug.LogError("Item Manager is null");
                return;
            }
            itemManager.ProductItem(item);
        }
    }
}
