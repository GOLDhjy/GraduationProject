using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace MyService
{

    //强化武器，增加攻击力
    public class StrengthenItem : Item
    {
        public float Value;
        public float UseTime = 30f;

        public StrengthenItem()
        {
            Name = "阿攻糖";
            Id = 2;
            IconName = "Icon_Fireball_128";
        }

        public override IEnumerator UseItemIEnumerator()
        {
            PlayerController.Instance.ChangeATKToPlayer(Value);
            yield return new WaitForSeconds(UseTime);
            PlayerController.Instance.ChangeATKToPlayer(-Value);
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
