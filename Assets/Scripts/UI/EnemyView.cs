using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
namespace UI
{
    public class EnemyView : IUserInterface
    {
        private GameObject Target = null;

        [Header("BOSS名字")]
        public Text BossName;
        [Header("血条")]
        public Slider HpBar;

        public void SetTarget(GameObject gameObject)
        {
            Target = gameObject;
        }
        private void Update()
        {
            if (Target != null)
            {
                var scripe = Target.GetComponent<BossController>();
                if (scripe == null)
                {
                    Debug.LogError("找不到Boss的控制组件-BossController");
                }
                else
                {
                    BossName.text = Target.GetComponent<BossController>().GetBossName();
                    HpBar.value = Mathf.Clamp01(Target.GetComponent<BossController>().GetBossHp() / Target.GetComponent<BossController>().GetBossMaxHp());
                }
            }
        }
    }
}
