using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

namespace MyService
{
    public class PlayerDied : AIState
    {
        public PlayerDied()
        {
            type = AIStateEnum.Died;
        }

        public override void EnterState()
        {
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Death;
            AIPlayerController.PlayerEntity.animator.Value.SetBool("Died", true);
            UIService.Instance.PushView(GameConfigService.Instance.UIPrefabPath + "FailingCanvas");
            MyWaitForSeconds(3);
            Application.Quit();
        }

        public override void ExitState()
        {

        }

        public override void OnState()
        {

        }
        private void MyWaitForSeconds(int v)
        {
            float Firsttime = Time.realtimeSinceStartup;
            while (true)
            {
                if (Time.realtimeSinceStartup - Firsttime > v)
                {
                    break;
                }
            }
        }
    }
}
