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
        public string IconName = "Icon_Fireball_128";
        public float Value;
        public float UseTime = 30f;
        public override IEnumerator UseItem()
        {
            PlayerController.Instance.ChangePlayerATK(Value);
            yield return new WaitForSeconds(UseTime);
            PlayerController.Instance.ChangePlayerATK(-Value);
        }
    }
}
