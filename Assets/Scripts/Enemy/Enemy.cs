using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyBaseState currentState;//当前的状态

    public Animator anim;
    public int animState;//动画状态

    private GameObject sign1;// !

    [Header("基础状态")]
    public float health;
    public bool isDead;
    public bool hasBigBang;
    public bool isBoss;

    [Header("移动")]
    public float speed;
    public Transform pointA;
    public Transform pointB;
    public Transform targetPoint;

    [Header("攻击")]
    public float attackRate;//攻击cd
    public float skillRate;//技能cd
    public float attackRange;//攻击距离
    public float skillRange;//技能距离
    public float nextAttack = 0;
    public float nextSkill = 0;

    public List<Transform> attackList = new List<Transform>();//创建列表

    public PatrolState patrolState=new PatrolState();//巡逻状态
    public AttackState attackState=new AttackState();//攻击状态

    public virtual void Initial()
    {
        
        anim = GetComponent<Animator>();
        sign1=transform.GetChild(0).gameObject;//获得当前对象的第一个子物体

        //GameManager.instance.IsEnemy(this);
    }
    public void Awake()
    {
        Initial();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.IsEnemy(this);
        //SwitchPoint();
        TransitonToState(patrolState);
        if(isBoss)
        {
            UIManager.instance.SetBossHealth(health);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //if(Mathf.Abs(transform.position.x-targetPoint.position.x)<0.01f)
        //{
        //    SwitchPoint();
        //}
        //MoveToTarget();
        anim.SetBool("dead", isDead);

        if(isDead )
        {
            UIManager.instance.UpdateBossHealth(health);//补充代码
            GameManager.instance.EnemyDead(this);
            return;
        }

        currentState.OnUpdate(this);//更新状态
        anim.SetInteger("state", animState);

        if (isBoss )
        {
            UIManager.instance.UpdateBossHealth(health);
            
        }
    }

    public void TransitonToState(EnemyBaseState state)//切换状态
    {
        currentState = state;
        currentState.EnterState(this);//进入当前状态
    }
    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);//朝目标移动
        FilpDirection();//翻转
    }
    public virtual void Attack()//攻击对player
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                Debug.Log("普通攻击");
                //播放攻击动画
                anim.SetTrigger("attack");
                nextAttack = Time.time + attackRate;
            }
        }
    }
    public virtual void Skill()//对bigbang
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextSkill)
            {
                Debug.Log("技能攻击");
                //播放攻击动画
                anim.SetTrigger("skill");
                nextSkill = Time.time + skillRate;
            }
        }
    }
    public void FilpDirection()//翻转
    {
        if(transform.position.x<targetPoint.position.x)
        {
            transform.rotation= Quaternion.Euler(0f,0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

    }

    public void SwitchPoint()//切换点
    {
        if(Mathf.Abs(pointA.position.x-transform.position.x)> Mathf.Abs(pointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;   
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!attackList.Contains(collision.transform)&&!hasBigBang&&!GameManager.instance.gameOver)//如果列表没有攻击目标
        {
            attackList.Add(collision.transform);//添加到攻击列表
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);//移除攻击列表
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isDead && !GameManager.instance.gameOver)
        {
            StartCoroutine(OnSign());//开启协程
        }

    }

    //协程函数
    IEnumerator OnSign()
    {
        sign1.SetActive(true);
        yield return new WaitForSeconds(sign1.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
                       //协程的等待时间                       //片段信息:第一个默认layer的第一个默认动画的片段的长度
        sign1.SetActive(false);
    }
}
