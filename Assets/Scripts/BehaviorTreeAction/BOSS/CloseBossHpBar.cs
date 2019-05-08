using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using MyService;
using UnityEngine.UI;
using UI;

namespace BehaviorDesigner.Runtime.Tasks
{
    
    public class CloseBossHpBar : Action
    {
        public SharedGameObject HPbar;
        public override void OnStart()
        {
            if (HPbar.Value != null)
            {
                UIService.Instance.PopView();
                HPbar.Value = null;
            }
        }

        //public override TaskStatus OnUpdate()
        //{

        //}
    }
}
