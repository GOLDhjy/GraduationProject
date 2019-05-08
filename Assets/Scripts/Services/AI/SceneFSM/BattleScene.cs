using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class BattleScene : IStateScene
    {
        static bool IsFirstIni = true;
        public BattleScene()
        {
            Type = SceneEnum.Battle;
        }

        public override void EnState()
        {
            //保证HUD在UI的最底部
            if (IsFirstIni)
            {
                UIService.Instance.PushView(GameConfigService.Instance.UIPrefabPath + "HUDCanvas");
                IsFirstIni = false;
            } 
        }

        public override void ExitState()
        {
           // UIService.Instance.PopView();
        }

        public override void OnState()
        {
        }
    }
}
