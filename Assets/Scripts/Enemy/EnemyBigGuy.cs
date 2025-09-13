using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigGuy : Enemy, IDamageable
{
    public Transform pickupPoint;

    public float power;
    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }

    public void PickUpBigBang()//动画事件
    {
        if(targetPoint != null&&targetPoint.CompareTag("BigBang")&&!hasBigBang)
        {
            targetPoint.gameObject.transform.position = pickupPoint.position;//炸弹的坐标变到piupPoint的坐标

            targetPoint.SetParent(pickupPoint);//将炸弹的作为piupPoint的子集

            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;//改为运动状态，相当于取消重力

            targetPoint.tag = "NPCBigBang";
            hasBigBang= true;

        }
    }

    public void ThrowBigBang()
    {
        if (hasBigBang)
        {
            targetPoint.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;

            targetPoint.SetParent(targetPoint.parent.parent.parent);

            if(FindObjectOfType<PlayerController>().gameObject.transform.position.x-transform.position.x<0)//玩家在左边
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * power, ForceMode2D.Impulse);
            }
            else//玩家在BigGuy右边
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * power, ForceMode2D.Impulse);
            }
        }

        hasBigBang = false;

        
    }
    
}
