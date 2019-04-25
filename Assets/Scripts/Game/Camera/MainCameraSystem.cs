using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;
using MyService;
public class MainCameraSystem : IInitializeSystem, IExecuteSystem
{
    GameContext m_GameContext;
    PlayerContext PlayerContext;
    GameEntity m_MainCamera;
    PlayerEntity PlayerEntity;
    public MainCameraSystem(Contexts contexts)
    {
        m_GameContext = contexts.game;
        
        PlayerContext = contexts.player;
    }
    public void Initialize()
    {
        //if (AIScene.Instance.CurrentState == null || AIScene.Instance.CurrentState.Type == SceneEnum.Battle)
        //{

            //m_MainCamera = m_GameContext.mainCameraEntity;
            //AudioService.Instance.PlayWithAS(m_MainCamera.audioSource.Value, AudioEnum.BGM, "BattleBGM");

            m_GameContext.isMainCamera = true;
            PlayerEntity = PlayerContext.localPlayerEntity;
            m_MainCamera = m_GameContext.mainCameraEntity;
            m_MainCamera.AddCamera(Camera.main);

            // GameObject.DontDestroyOnLoad(Camera.main.gameObject);

            m_MainCamera.AddGameTransform(Camera.main.transform);
            m_MainCamera.gameTransform.Value.gameObject.AddComponent<AudioSource>();
            AudioSource audioSource = m_MainCamera.gameTransform.Value.GetComponent<AudioSource>();
            m_MainCamera.AddAudioSource(audioSource);
            AudioService.Instance.PlayWithAS(m_MainCamera.audioSource.Value, AudioEnum.BGM, "BattleBGM");
        //}
        //AudioService.Instance.PlayWiehAS(m_MainCamera.audioSource.Value, AudioEnum.BGM, "1");
    }
    public void Execute()
    {
       
    }

    
}

