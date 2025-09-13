using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public bool bigbangAvilable;
    int direction;//����

    public void OnTriggerEnter2D(Collider2D other)
    {
        //��ù�������
        if(transform.position.x>other.transform.position.x)//˵��NPC��other���ұ�
        {
            direction = -1;//���������
        }    
        else
        {
            direction = 1;
        }

        if(other.CompareTag("Player"))
        {
            Debug.Log("�������");
            other.GetComponent<IDamageable>().GetHit(1);
            //other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 1) * 10, ForceMode2D.Impulse);
        }

        if(other.CompareTag("BigBang") && bigbangAvilable)
        {
            Debug.Log("����ը��");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction,1)*10,ForceMode2D.Impulse);//xΪ��y=1��ʾ���������ۺϾ���б�Ϸ�,Impulse�����
        }
    }
}
