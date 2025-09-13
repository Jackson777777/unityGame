using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;//0 idle
        enemy.SwitchPoint();
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            enemy.animState = 1;//1 run
            enemy.MoveToTarget();
        }

        if (Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) < 0.01f)
        {
            //enemy.SwitchPoint();
            enemy.TransitonToState(enemy.patrolState);
        }
        
        if(enemy.attackList.Count > 0)
        {
            enemy.TransitonToState(enemy.attackState);//如果有攻击目标就切换状态
        }
        
    }
}
