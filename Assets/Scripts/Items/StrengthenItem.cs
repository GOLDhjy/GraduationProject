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
        public float Value = 10;
        public float UseTime = 30f;

        public StrengthenItem()
        {
            Name = "飞雪";
            Id = 2;
            IconName = "Icon_Fireball_128";
            Describe = Name + "：强化自己，提高攻击力" + Value + "，持续" + UseTime+"秒";
        }

        public override IEnumerator UseItemIEnumerator()
        {
            PlayerController.Instance.ChangeATKToPlayer(Value);
            ParticlesService.Instance.PlayPrefabParticle(PlayerController.Instance.GetPlayerMainSword(), "Effect5");
            yield return new WaitForSeconds(UseTime);
            ParticlesService.Instance.StopPrefabParticle(PlayerController.Instance.GetPlayerMainSword(), "Effect5");
            PlayerController.Instance.ChangeATKToPlayer(-Value);
            yield return null;
        }
        public override void UseItem()
        {
            
            main.StartCoroutine("UseItemIEnumerator");
        }
        public override string Name { get => this.m_name; set => this.m_name = value; }
        public override int Id { get => this.id; set => this.id = value; }
        public override string IconName { get => this.iconName; set => this.iconName = value; }
        public override string Describe { get => base.Describe; set => base.Describe = value; }
    }
}
