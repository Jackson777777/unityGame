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

    public void PickUpBigBang()//�����¼�
    {
        if(targetPoint != null&&targetPoint.CompareTag("BigBang")&&!hasBigBang)
        {
            targetPoint.gameObject.transform.position = pickupPoint.position;//ը��������䵽piupPoint������

            targetPoint.SetParent(pickupPoint);//��ը������ΪpiupPoint���Ӽ�

            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;//��Ϊ�˶�״̬���൱��ȡ������

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

            if(FindObjectOfType<PlayerController>().gameObject.transform.position.x-transform.position.x<0)//��������
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * power, ForceMode2D.Impulse);
            }
            else//�����BigGuy�ұ�
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * power, ForceMode2D.Impulse);
            }
        }

        hasBigBang = false;

        
    }
    
}
