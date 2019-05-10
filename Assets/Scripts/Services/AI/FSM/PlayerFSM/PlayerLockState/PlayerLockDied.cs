using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
namespace MyService
{
    public class PlayerLockDied : LockAIState
    {
        public PlayerLockDied()
        {
            type = AIStateEnum.Died;
        }

        public override void EnterState()
        {
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Death;
            AIPlayerController.PlayerEntity.animator.Value.SetBool("Died", true);
            UIService.Instance.PushView(GameConfigService.Instance.UIPrefabPath + "FailingCanvas");

            Thread thread = new Thread(StapGame);
            thread.Start();


        }

        private void StapGame(object obj)
        {
            MyWaitForSeconds(5);
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
