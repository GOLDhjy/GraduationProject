using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using MyService;
public class PlayerSwordCollider : MonoBehaviour
{
    List<Collider> m_colliders = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BossOfEnemy")
        {
            if (other.gameObject.GetComponent<BossController>() == null)
            {
                Debug.LogError("Can ont find BossController");
                return;
            }
            other.gameObject.GetComponent<BossController>().ChangeHp(-PlayerController.Instance.GetPlayerATK());


        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
