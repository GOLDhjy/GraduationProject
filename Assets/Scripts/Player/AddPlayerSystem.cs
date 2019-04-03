using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using MyService;
using UnityEngine;
using Entitas.Unity;

public class AddPlayerSystem : IInitializeSystem
{
    readonly Transform Parent = new GameObject("MyPlayer").transform;
    readonly PlayerContext m_context;
    PlayerEntity player;
    public AddPlayerSystem(Contexts contexts)
    {
        m_context = contexts.player;
        m_context.isLocalPlayer = true;
    }

    public void Initialize()
    {  
        player = m_context.localPlayerEntity;
        player.isLocalPlayer = true;
        GameObject gameObject = ResourceService.Instance.InstantiateAsset<GameObject>(GameConfigService.Instance.PlayerPrefabPath,Vector3.forward,Quaternion.identity);
        gameObject.transform.SetParent(Parent);
        player.AddGameObject(gameObject);
        gameObject.Link(player);
        GameObject Sword = GameObject.FindGameObjectWithTag("Sword");
        LogService.Instance.Log(LogLevel.info, Sword.name);
        player.AddRigidbody(gameObject.GetComponent<Rigidbody>());
        player.AddCollider(gameObject.GetComponent<Collider>());
        player.AddAnimator(gameObject.GetComponent<Animator>());
        //if (gameObject.GetComponent<Animator>() == null)
        //    Debug.Log("动画为空");
        //else
        //{
        //    Debug.Log(player.animator.Value.GetFloat("Speed"));
        //}
        player.AddTransform(gameObject.transform);
        player.AddAnimState(AnimStateEnum.Idle);
        player.AddSword(Sword);
        player.AddMoveSpeed(GameConfigService.Instance.PlayerMoveSpeed);
        player.AddRotateSpeed(GameConfigService.Instance.PlayerRotateSpeed);
        player.AddHp(100f);
        player.isFreeView = true;
        player.AddViewMode(new PlayerViewModeController(ref player));

    }
}