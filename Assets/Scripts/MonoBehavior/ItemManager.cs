using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyService
{
    public class ItemManager : MonoBehaviour
    {
        private Queue<Item> Products = new Queue<Item>();


        private void Update()
        {
            if (Products.Count>0)
            {
                Item item = Products.Dequeue();
                StartCoroutine(item.UseItemIEnumerator());
            }
        }

        public void ProductItem(Item item)
        {
            if (item == null)
            {
                Debug.LogError("物品为空，使用失败");
                return;
            }
            Products.Enqueue(item);
        }
    }
}
