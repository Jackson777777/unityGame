using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTallGuy : Enemy,IDamageable
{
    public void GetHit(float damage)
    {
        health -= damage;
        if(health < 1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }

    public void Setoff()//动画事件   //吹灭炸弹
    {
        targetPoint.GetComponent<BigBang>()?.OffBigBang();//问号表示判断是否为空
    }
    
}
