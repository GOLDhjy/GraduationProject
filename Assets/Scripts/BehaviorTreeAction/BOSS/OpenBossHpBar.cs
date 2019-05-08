﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using MyService;
using UnityEngine.UI;
using UI;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class OpenBossHpBar : Action
    {
        public SharedGameObject HpBar = null;
    public override void OnStart()
        {
            if (HpBar.Value == null)
            {
                HpBar.Value = UIService.Instance.PushView(GameConfigService.Instance.UIPrefabPath + "EnemyCanvas");
                HpBar.Value.GetComponent<EnemyView>().SetTarget(this.gameObject);
            }
        }

        //public override TaskStatus OnUpdate()
        //{

        //}
    }
}