using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class PlayerLockModeState : IPlayerViewModeState
    {
        private LockAIPlayer aIPlayer;

        public PlayerLockModeState()
        {
            type = ViewModeEnum.Lock;
        }

        public LockAIPlayer AIPlayer
        {
            get
            {
                if (aIPlayer == null)
                    aIPlayer = new LockAIPlayer(PlayerViewModeController.PlayerEntity, PlayerViewModeController.MainCamera);
                return aIPlayer;
            }
            set
            {
                aIPlayer = value;
            }
        }

        public override void EnterState()
        {
            AIPlayer.SubEvent();
            MyEventSystem.Instance.Invoke(ChangeToMovementArgs.Id, this, new ChangeToMovementArgs() { Movement = true });
            //AIPlayer.CurrentState.EnterState();
        }

        public override void ExitState()
        {
            //AIPlayer.CurrentState.ExitState();
            AIPlayer.UnSubEvent();
            
        }

        public override void OnState()
        {



            AIPlayer.DoState();
        }
    }
}
