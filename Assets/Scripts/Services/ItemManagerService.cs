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
        public void ProductItem(Item item)
        {
            if (GameObject.Find("Main").GetComponent<ItemManager>() == null)
            {
                Debug.LogError("Main Cant find ItemManager");
                return;
            }
            GameObject.Find("Main").GetComponent<ItemManager>().ProductItem(item);
        }
    }
}
