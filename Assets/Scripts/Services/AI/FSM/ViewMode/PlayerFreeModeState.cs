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

        }

        public override void ExitState()
        {
            
        }

        public override void OnState()
        {
            AIPlayer.DoState();
        }
    }
}
