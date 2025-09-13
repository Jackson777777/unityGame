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

    public void Setoff()//�����¼�   //����ը��
    {
        targetPoint.GetComponent<BigBang>()?.OffBigBang();//�ʺű�ʾ�ж��Ƿ�Ϊ��
    }
    
}
