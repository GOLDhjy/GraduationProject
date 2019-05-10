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
                AudioService.Instance.Play(Contexts.sharedInstance.game.mainCameraEntity.gameTransform.Value.gameObject, AudioEnum.BGM, "IdleBGM");
                UIService.Instance.PopView();
                HPbar.Value = null;
            }
        }

        //public override TaskStatus OnUpdate()
        //{

        //}
    }
}
