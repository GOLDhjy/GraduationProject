using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class PlayerFreeModeState : IPlayerViewModeState
    {
        private AIPlayer aIPlayer;

        public PlayerFreeModeState()
        {
            type = ViewModeEnum.Free;
        }


        public AIPlayer AIPlayer
        {
            get
            {
                if(aIPlayer == null)
                    aIPlayer = new AIPlayer(PlayerViewModeController.PlayerEntity, PlayerViewModeController.MainCamera);
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
