using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class PlayerLockModeState : IPlayerViewModeState
    {
        public AIPlayer AIPlayer;

        public PlayerLockModeState()
        {
            type = ViewModeEnum.Lock;
        }

        public override void EnterState()
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
