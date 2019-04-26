using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class BattleScene : IStateScene
    {
        public BattleScene()
        {
            Type = SceneEnum.Battle;
        }

        public override void EnState()
        {
            UIService.Instance.PushView(GameConfigService.Instance.UIPrefabPath + "HUDCanvas");
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
