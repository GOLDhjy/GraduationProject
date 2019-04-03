using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyService
{
    public enum AIStateEnum
    {
        INVALID = -1,
        Movement,
        Jump,
        Attack,
        Stop,
        Died,
        Roll
    }
    public class AIState
    {
        public AIStateEnum type = AIStateEnum.INVALID;
        protected AIPlayer AIPlayerController;
        protected bool quit = false;
        public virtual void EnterState()
        {
            quit = false;
        }
        public virtual void OnState()
        {
            
        }
        public virtual void ExitState()
        {
            quit = true;
        }
        public void SetController(AIPlayer aIPlayer)
        {
            AIPlayerController = aIPlayer;
        }
    }
}
