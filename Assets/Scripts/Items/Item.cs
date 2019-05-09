using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyService
{
    public class Item 
    {
        public Main main = new Main();
        protected string m_name;
        protected int id;
        protected string iconName;
        protected string describe;

        public const string DefaultIconName = "JazzCreateFontUI";
        public virtual string Name { get => m_name; set => m_name = value; }
        public virtual int Id { get => id; set => id = value; }
        public virtual string IconName { get => iconName; set => iconName = value; }
        public virtual string Describe { get => describe; set => describe = value; }

        public Sprite GetItemIcon(string name)
        {
            Sprite tmp = ResourceService.Instance.LoadAsset<Sprite>(GameConfigService.Instance.UIIcon + name);
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
        public virtual void UseItem()
        { }
        public virtual IEnumerator UseItemIEnumerator()
        {
            return null;
        }

    }
}