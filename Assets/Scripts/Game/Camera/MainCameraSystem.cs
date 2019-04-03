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
    GameEntity m_MainCamera;
    public MainCameraSystem(Contexts contexts)
    {
        m_GameContext = contexts.game;
        m_GameContext.isMainCamera = true;
    }
    public void Initialize()
    {
        m_MainCamera = m_GameContext.mainCameraEntity;
        m_MainCamera.AddCamera(Camera.main);
        m_MainCamera.AddViewModeController(new MyService.CameraViewModeController(m_MainCamera));
    }
    public void Execute()
    {
        m_MainCamera.viewModeController.Value.ChangeState(MyService.ViewModeEnum.Free);
    }

    
}

