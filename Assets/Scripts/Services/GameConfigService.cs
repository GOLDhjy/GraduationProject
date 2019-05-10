using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class GameConfigService : Singleton<GameConfigService>
    {
        //资源必须在Assets/Resources/里面才能被加载



        public string PlayerPrefabPath = "Prefabs/Player";
        public string UIPrefabPath = "Prefabs/UI/";
        public string MusicPath = "Musics/";
        public string UIIcon = "UI/UITextures/Items/";
        //粒子预制体路径
        public string ParticlesPrefabPath = "Prefabs/PC Effects/";

        public float AttackWindowTime = 0.2f;
        public float PlayerMoveSpeed = 3;
        public float PlayerRotateSpeed = 8f;
        public float CameraMoveSpeed = 2f;


        //游戏设置信息
        //游戏音乐大小
        public float MusicVolume = 1;
        //游戏音效大小
        public float AudioVolume = 1;

       
    }

}
