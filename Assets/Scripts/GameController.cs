using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameController
{
    readonly Systems m_systems;

    public GameController(Contexts contexts)
    {
        m_systems = new GameSystems(contexts);
    }
    public void Initialize()
    {
        m_systems.Initialize();
    }
    public void Execute()
    {
        m_systems.Execute();
        m_systems.Cleanup();
    }
}
