using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyService
{
    public class Item : MonoBehaviour
    {

        Texture GetItemIcon(string name)
        {
            Texture tmp = ResourceService.Instance.LoadAsset<Texture>(GameConfigService.Instance.UIIcon + name);
            if (tmp)
            {
                return tmp;
            }
            else
            {
                Debug.LogError("获取图标错误");
                return null;
            }
        }
        public virtual IEnumerator UseItem()
        {
            return null;
        }

    }
}