using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBang : MonoBehaviour
{
    private Animator anim;

    public float startTime;
    public float waitTime;
    public float bigbangForce;//炸弹的力

    private Collider2D col;
    private Rigidbody2D rb;

    [Header("检测")]
    public float bigbangRadius;//范围
    public LayerMask taegetLayer;//目标图层
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;//开始的时间等于游戏的时钟
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("bigbangOff"))
        {
            if (Time.time > startTime + waitTime)
            {
                anim.Play("bigbangExplotion");
            }
        }
        
    }
    public void OnDrawGizmos()
    {// 显示检测范围
        Gizmos.DrawWireSphere(transform.position, bigbangRadius);

    }
    public void Explotion()//动画事件
    {
        col.enabled = false;//取消bigbang的Collideder
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, bigbangRadius, taegetLayer);//检测周围所有的Collideder数组

        rb.gravityScale = 0;//取消重力

        foreach (var item in aroundObjects)//item是变量的代名词
        {
            Vector3 pos = transform.position - item.transform.position;//相对位置

            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * bigbangForce, ForceMode2D.Impulse);//Impulse冲击力
            
            if(item.CompareTag("BigBang")&&item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bigbangOff"))
            {
                item.GetComponent<BigBang>().OnBigBang();
            }

            if(item.CompareTag("Player"))
            {
                item.GetComponent<IDamageable>().GetHit(3);//炸弹炸玩家的伤害为3
            }

            if (item.CompareTag("Enemy"))
            {
                item.GetComponent<IDamageable>().GetHit(3);//炸弹炸玩家的伤害为3
            }
        }
    }

    public void DestroyBigBang()
    {
        Destroy(gameObject);
    }
    public void OffBigBang()
    {
        anim.Play("bigbangOff");
        gameObject.layer = LayerMask.NameToLayer("NPC");
    }
    public void OnBigBang()
    {
        startTime = Time.time;
        anim.Play("bigbangOn");
        gameObject.layer = LayerMask.NameToLayer("BigBang");
    }
}
