using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        //Debug.Log("!!!!!");
        enemy.animState = 2;
        enemy.targetPoint = enemy.attackList[0];
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (enemy.hasBigBang)//有炸弹就不会再判断攻击
        {
            return;
        }

        if (enemy.attackList.Count==0)
        {
            enemy.TransitonToState(enemy.patrolState);
        }

        if(enemy.attackList.Count>1)
        {
            for (int i = 0; i < enemy.attackList.Count; i++)
            {
                if (Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].position.x) < 
                    Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x))//第i给目标的位置离我更近
                {
                    enemy.targetPoint = enemy.attackList[i];
                }
            }
        }
        if(enemy.attackList.Count == 1)
        {
            enemy.targetPoint = enemy.attackList[0];
        }
        //if (enemy.attackList.Count == 1)
        //{
        //    enemy.targetPoint = enemy.attackList[0];
        //}
        if (enemy.targetPoint.CompareTag("Player"))
        {
            enemy.Attack();
        }

        if (enemy.targetPoint.CompareTag("BigBang"))
        {
            enemy.Skill();
        }


        enemy.MoveToTarget();
    }
}
