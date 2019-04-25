using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        //在此处添加所有系统
        Add(new InputSystem(contexts));
        Add(new PlayerSystem(contexts));
        Add(new GameSystem(contexts));
        Add(new LastInitSystem(contexts));
        Add(new AddGameStateSystem(contexts));
        //顺序说明：
        //输入系统>角色系统 > 
        //Game系统（里面包括摄像机系统）>
        //最终初始化系统（因为初始化中有角色引用摄像机，而摄像机也引用角色的情况，这里要解决的就是防止空引用，其它情况也适用）>
        //游戏的状态控制系统,里面是对游戏不同状态下的不同逻辑的执行，所以应该在全部初始化完毕后再执行
    }
}
