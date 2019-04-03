using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyService;

public class AttackAnim : StateMachineBehaviour
{
    float PressTime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MyEventSystem.Instance.Subscribe(AttackArgs.Id, OnEventAttack);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Time.time - PressTime < GameConfigService.Instance.AttackWindowTime)
        {
            Debug.Log(Time.time - PressTime);
            MyEventSystem.Instance.Invoke(AttackArgs.Id, this, new AttackArgs() { Attack = true });
        }
        else
        {
            MyEventSystem.Instance.Invoke(AttackArgs.Id, this, new AttackArgs() { Attack = false });
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
