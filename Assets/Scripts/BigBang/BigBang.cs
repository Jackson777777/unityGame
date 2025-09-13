using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBang : MonoBehaviour
{
    private Animator anim;

    public float startTime;
    public float waitTime;
    public float bigbangForce;//ը������

    private Collider2D col;
    private Rigidbody2D rb;

    [Header("���")]
    public float bigbangRadius;//��Χ
    public LayerMask taegetLayer;//Ŀ��ͼ��
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;//��ʼ��ʱ�������Ϸ��ʱ��
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
    {// ��ʾ��ⷶΧ
        Gizmos.DrawWireSphere(transform.position, bigbangRadius);

    }
    public void Explotion()//�����¼�
    {
        col.enabled = false;//ȡ��bigbang��Collideder
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, bigbangRadius, taegetLayer);//�����Χ���е�Collideder����

        rb.gravityScale = 0;//ȡ������

        foreach (var item in aroundObjects)//item�Ǳ����Ĵ�����
        {
            Vector3 pos = transform.position - item.transform.position;//���λ��

            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * bigbangForce, ForceMode2D.Impulse);//Impulse�����
            
            if(item.CompareTag("BigBang")&&item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bigbangOff"))
            {
                item.GetComponent<BigBang>().OnBigBang();
            }

            if(item.CompareTag("Player"))
            {
                item.GetComponent<IDamageable>().GetHit(3);//ը��ը��ҵ��˺�Ϊ3
            }

            if (item.CompareTag("Enemy"))
            {
                item.GetComponent<IDamageable>().GetHit(3);//ը��ը��ҵ��˺�Ϊ3
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
