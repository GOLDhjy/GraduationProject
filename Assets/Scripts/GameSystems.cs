using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        //在此处添加所有系统
        Add(new GameSystem(contexts));
        Add(new InputSystem(contexts));
        Add(new PlayerSystem(contexts));
        
    }
}
