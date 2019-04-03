﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class GameEventArgs : EventArgs
    {
    }
    //玩家输入事件
    #region
    public class MovementArgs : GameEventArgs
    {
        public static readonly int Id = typeof(MovementArgs).GetHashCode();
        public InputEntity InputEntity;
        public bool Idle = true;
    }
    public class JumpArgs : GameEventArgs
    {
        public static readonly int Id = typeof(JumpArgs).GetHashCode();

    }
    public class RandomIdleArgs : GameEventArgs
    {
        public static readonly int Id = typeof(RandomIdleArgs).GetHashCode();
    }
    public class DodgeArgs : GameEventArgs
    {
        public static readonly int Id = typeof(DodgeArgs).GetHashCode();
        public InputEntity InputEntity;
        public bool Dodge = false;
    }
    public class StandArgs : GameEventArgs
    {
        public static readonly int Id = typeof(StandArgs).GetHashCode();
    }
    #endregion
}
