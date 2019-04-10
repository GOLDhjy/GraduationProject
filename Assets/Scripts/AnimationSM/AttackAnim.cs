using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyService;

public class AttackAnim : StateMachineBehaviour
{
    float PressTime;
    float BorderTime;
    bool Cont = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        MyEventSystem.Instance.Subscribe(AttackArgs.Id, OnEventAttack);
        PressTime = Time.time;
        Cont = false;
        //animator.SetBool("IsCombat", false);
        Debug.Log("改为不进行连续攻击");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(stateInfo.shortNameHash);
        //Debug.Log("时间"+stateInfo.normalizedTime);
        //Debug.Log(animator.GetBool("IsCombat"));
        if (stateInfo.normalizedTime >= 0.4f && stateInfo.normalizedTime<0.5)
        {
            BorderTime = Time.time;
        }
        if(stateInfo.normalizedTime>0.5f && stateInfo.normalizedTime <= 0.7f)
        {
            if (PressTime >= BorderTime && !stateInfo.IsName("2Handed_Attack_6"))
            {
                if (!animator.GetBool("IsCombat"))
                {
                    animator.SetBool("IsCombat", true);
                    //animator.SetTrigger("CombatT");
                //Cont = true;
                    Debug.Log("连续攻击");
                }
            }
            else
            {
                if(animator.GetBool("IsCombat"))
                {
                    animator.SetBool("IsCombat", false);
                    Debug.Log("改为不进行连续攻击");
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(stateInfo.shortNameHash+"播放完毕");
        // Debug.Log(animator.GetBool("IsCombat"));
        if (!animator.GetBool("IsCombat"))
        //if(!Cont)
        {
            MyEventSystem.Instance.Invoke(AttackArgs.Id, this, new AttackArgs() { Attack = false });
            Debug.Log("退出攻击动画");
            
        }
        MyEventSystem.Instance.UnSubscribe(AttackArgs.Id, OnEventAttack);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
    public void OnEventAttack(object sender,GameEventArgs  gameEventArgs)
    {
        AttackArgs args = gameEventArgs as AttackArgs;
        if(args.Attack == true)
        {
            PressTime = Time.time;
        }
    }
}
