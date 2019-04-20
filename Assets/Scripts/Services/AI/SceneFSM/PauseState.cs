using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        public override void ExitState()
        {

        }

        public override void OnState()
        {

        }
    }
}
