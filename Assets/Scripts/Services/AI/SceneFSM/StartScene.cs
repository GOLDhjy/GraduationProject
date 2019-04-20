using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class StartScene : IStateScene
    {
        public StartScene()
        {
            Type = SceneEnum.Start;
        }

        public override void EnState()
        {
        }

        public override void ExitState()
        {
        }

        public override void OnState()
        {
        }
    }
}
