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
        public string IconName = "Icon_Leafs_128";
        public float Value = 10;
        public override IEnumerator UseItem()
        {
            PlayerController.Instance.ChangePlayerHp(Value);
            Debug.Log("给玩家加血" + Value.ToString());
            return null;

        }

    }
}