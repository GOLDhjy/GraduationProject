using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;

public class MainCameraSystem : IInitializeSystem, IExecuteSystem
{
    GameContext m_GameContext;
    PlayerContext PlayerContext;
    GameEntity m_MainCamera;
    PlayerEntity PlayerEntity;
    public MainCameraSystem(Contexts contexts)
    {
        m_GameContext = contexts.game;
        m_GameContext.isMainCamera = true;
        PlayerContext = contexts.player;
    }
    public void Initialize()
    {
        PlayerEntity = PlayerContext.localPlayerEntity;
        m_MainCamera = m_GameContext.mainCameraEntity;
        m_MainCamera.AddCamera(Camera.main);
        m_MainCamera.AddGameTransform(Camera.main.transform);
    }
    public void Execute()
    {
        m_MainCamera.viewModeController.Value.DoState();
    }

    
}

