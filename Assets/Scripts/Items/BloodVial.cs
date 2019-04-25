using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
namespace MyService
{
    public class BloodVial : Item
    {
         
        public float Value = 10;

        public BloodVial()
        {
            Name = "血瓶";
            Id = 1;
            IconName = "Icon_Leafs_128";
        }

        public override IEnumerator UseItemIEnumerator()
        {
            PlayerController.Instance.ChangeHpToPlayer(Value);
            Debug.Log("给玩家加血" + Value.ToString());
            return null;

        }
        public override void UseItem()
        {
            main.StartCoroutine("UseItemIEnumerator");
        }
        public override string Name { get => this.m_name; set => this.m_name = value; }
        public override int Id { get => this.id; set => this.id = value; }
        public override string IconName { get => this.iconName; set => this.iconName = value; }

    }
}