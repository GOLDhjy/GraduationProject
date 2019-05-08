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
         
        public float Value = 50;

        public BloodVial()
        {
            Name = "血瓶";
            Id = 1;
            IconName = "Icon_Leafs_128";
        }

        public override IEnumerator UseItemIEnumerator()
        {
            ParticlesService.Instance.PlayPrefabParticle(PlayerController.Instance.GetPlayerGameObject(), "Effect7");
            yield return new WaitForSeconds(1.5f);
            PlayerController.Instance.ChangeHpToPlayer(Value);
            Debug.Log("给玩家加血" + Value.ToString());
            ParticlesService.Instance.StopPrefabParticle(PlayerController.Instance.GetPlayerGameObject(), "Effect7");

            yield return null;

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