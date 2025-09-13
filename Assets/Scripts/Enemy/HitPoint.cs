using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public bool bigbangAvilable;
    int direction;//方向

    public void OnTriggerEnter2D(Collider2D other)
    {
        //获得攻击方向
        if(transform.position.x>other.transform.position.x)//说明NPC在other的右边
        {
            direction = -1;//方向是左边
        }    
        else
        {
            direction = 1;
        }

        if(other.CompareTag("Player"))
        {
            Debug.Log("玩家受伤");
            other.GetComponent<IDamageable>().GetHit(1);
            //other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 1) * 10, ForceMode2D.Impulse);
        }

        if(other.CompareTag("BigBang") && bigbangAvilable)
        {
            Debug.Log("踢走炸弹");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction,1)*10,ForceMode2D.Impulse);//x为左，y=1表示向上升，综合就是斜上方,Impulse冲击力
        }
    }
}
