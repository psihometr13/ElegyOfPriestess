using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : BaseState
{
    public Dead(Enemy enemy, StateMachine stateMachine, EnemyData enemyData, string animationState) : base(enemy, stateMachine, enemyData, animationState)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void Check()
    {
        base.Check();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        
        enemy.StartCoroutine("Res");
        if (!Upd_PlayerControl.Instance.Save3.enabled)
        {
            Debug.Log("1save");
            Upd_PlayerControl.Instance.Save3.enabled = true;
        }
        else if (!Upd_PlayerControl.Instance.Save2.enabled && !Upd_PlayerControl.Instance.Save3.enabled)
        {
            Debug.Log("2save");
            Upd_PlayerControl.Instance.Save2.enabled = true;
        }
        else if (!Upd_PlayerControl.Instance.Save1.enabled && !Upd_PlayerControl.Instance.Save2.enabled && !Upd_PlayerControl.Instance.Save3.enabled)
        {
            Debug.Log("3save");
            Upd_PlayerControl.Instance.Save1.enabled = true;

        }

        if (enemyData.spirit) Upd_PlayerControl.Instance.ResetStates();
        enemy.isDead = false;
        base.LogicUpdate();
    }
}
