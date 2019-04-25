using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyService
{
    public class PauseState : IStateScene
    {
        public PauseState() 
        {
            Type = SceneEnum.Pause;
        }

        public override void EnState()
        {
            UIService.Instance.PushView(GameConfigService.Instance.UIPrefabPath + "SelectionCanvas");
            Time.timeScale = 0;
        }

        public override void ExitState()
        {
            UIService.Instance.PopView();
            Time.timeScale = 1;
        }

        public override void OnState()
        {

        }
    }
}
