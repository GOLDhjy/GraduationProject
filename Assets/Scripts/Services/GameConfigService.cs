using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class GameConfigService : Singleton<GameConfigService>
    {
        public string PlayerPrefabPath = "Prefabs/Player";


        public float AttackWindowTime = 0.2f;

        public float PlayerMoveSpeed = 3;
        public float PlayerRotateSpeed = 8f;


        public float CameraMoveSpeed = 2f; 
    }

}
