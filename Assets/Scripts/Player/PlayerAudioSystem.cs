using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;
public class PlayerAudioSystem : IInitializeSystem, IExecuteSystem
{
    PlayerContext m_Contexts;
    AudioSource m_AudioSource;
    PlayerEntity m_PlayerEntity;
    public PlayerAudioSystem(Contexts contexts)
    {
        m_Contexts = contexts.player;
    }

    public void Initialize()
    {
        m_PlayerEntity = m_Contexts.localPlayerEntity;
        //m_AudioSource = m_PlayerEntity.
    }
    public void Execute()
    {
    }

    
}
