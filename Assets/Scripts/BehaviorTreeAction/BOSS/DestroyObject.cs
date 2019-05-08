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
    public class DestroyObject : Action
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

            //ParticlesService.Instance.StopPrefabParticle(self, "Effect11");
            GameObject.Destroy(self);
            return TaskStatus.Success;
        }
    }
}
