using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;
using MyService;
public class PlayerMoveSystem : IExecuteSystem , IInitializeSystem
{
    InputEntity InputEntity;
    PlayerEntity PlayerEntity;
    InputContext InputContext;
    Contexts m_Contexts;
    public PlayerMoveSystem(Contexts contexts)
    {
        m_Contexts = contexts;
        //InputEntity = contexts.input.keyboardEntity;
        //PlayerEntity = contexts.player.localPlayerEntity;
        //LogService.Instance.Log(LogLevel.info, PlayerEntity.rotateSpeed.ToString());
    }

    public void Execute()
    {

        //if (InputEntity.horizontal.Value > 0.05 || InputEntity.vertical.Value > 0.05)
        //{
        //Move();
        //Turn();
        //}
        PlayerEntity.viewMode.Vlaue.DoState();
    }

    public void Initialize()
    {
        InputEntity = m_Contexts.input.keyboardEntity;
        PlayerEntity = m_Contexts.player.localPlayerEntity;
        //LogService.Instance.Log(LogLevel.info, PlayerEntity.rotateSpeed.Value.ToString());
        if (PlayerEntity.gameObject.gameobject == null)
            Debug.LogError("为空");
    }

    void Move()
    {
        if(InputEntity == null)
        {
            Debug.LogError("InputEntity");
            return;
        }
        if(PlayerEntity == null)
        {
            Debug.LogError("PlayerEntity");
            return;
        }

        Vector3 TargetPosition = new Vector3(InputEntity.horizontal.Value, 0, InputEntity.vertical.Value) * PlayerEntity.moveSpeed.Value * Time.deltaTime;
        PlayerEntity.rigidbody.Value.MovePosition(PlayerEntity.gameObject.gameobject.transform.position+ TargetPosition);
    }
    void Turn()
    {
        Vector3 TargetDirection = new Vector3(InputEntity.horizontal.Value, 0, InputEntity.vertical.Value);
        Debug.Log(TargetDirection.ToString());
        var dir = Vector3.Slerp(PlayerEntity.transform.Value.forward, TargetDirection, GameConfigService.Instance.PlayerRotateSpeed * Time.deltaTime);
        PlayerEntity.transform.Value.rotation = Quaternion.LookRotation(dir);
        //Quaternion TargetQuaternion = Quaternion.LookRotation(TargetDirection, Vector3.up);
        //Quaternion NewQuaternion = Quaternion.Lerp(PlayerEntity.rigidbody.Value.rotation, TargetQuaternion, 1f);
        //PlayerEntity.rigidbody.Value.MoveRotation(NewQuaternion);

    }
}
