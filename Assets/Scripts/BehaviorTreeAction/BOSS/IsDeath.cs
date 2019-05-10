using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using MyService;
namespace BehaviorDesigner.Runtime.Tasks
{
    public class IsDeath : Action
    {
        public SharedGameObject Self;
        GameObject self;
        BossController scrip;
        public override void OnStart()
        {
            if (GetDefaultGameObject(Self.Value) == null)
            {
                Debug.LogError("this gameobject is null");
            }
            else
            {
                self = GetDefaultGameObject(Self.Value);
            }
            if (self.GetComponent<BossController>() == null)
            {
                Debug.LogError("this gameobject has not this BossController");
            }
            else
            {
                scrip = self.GetComponent<BossController>();
            }

            
        }

        public override TaskStatus OnUpdate()
        {
            if (scrip.GetBossHp()<=0)
            {
                scrip.m_State = BossStateEnum.Death;
                ParticlesService.Instance.PlayPrefabParticle(self.transform.Find("Effect").gameObject, "Effect1");
                UIService.Instance.PopView();
                AudioService.Instance.Play(Contexts.sharedInstance.game.mainCameraEntity.gameTransform.Value.gameObject, AudioEnum.BGM, "IdleBGM");

                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}
