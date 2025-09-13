using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyBaseState currentState;//��ǰ��״̬

    public Animator anim;
    public int animState;//����״̬

    private GameObject sign1;// !

    [Header("����״̬")]
    public float health;
    public bool isDead;
    public bool hasBigBang;
    public bool isBoss;

    [Header("�ƶ�")]
    public float speed;
    public Transform pointA;
    public Transform pointB;
    public Transform targetPoint;

    [Header("����")]
    public float attackRate;//����cd
    public float skillRate;//����cd
    public float attackRange;//��������
    public float skillRange;//���ܾ���
    public float nextAttack = 0;
    public float nextSkill = 0;

    public List<Transform> attackList = new List<Transform>();//�����б�

    public PatrolState patrolState=new PatrolState();//Ѳ��״̬
    public AttackState attackState=new AttackState();//����״̬

    public virtual void Initial()
    {
        
        anim = GetComponent<Animator>();
        sign1=transform.GetChild(0).gameObject;//��õ�ǰ����ĵ�һ��������

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
            UIManager.instance.UpdateBossHealth(health);//�������
            GameManager.instance.EnemyDead(this);
            return;
        }

        currentState.OnUpdate(this);//����״̬
        anim.SetInteger("state", animState);

        if (isBoss )
        {
            UIManager.instance.UpdateBossHealth(health);
            
        }
    }

    public void TransitonToState(EnemyBaseState state)//�л�״̬
    {
        currentState = state;
        currentState.EnterState(this);//���뵱ǰ״̬
    }
    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);//��Ŀ���ƶ�
        FilpDirection();//��ת
    }
    public virtual void Attack()//������player
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                Debug.Log("��ͨ����");
                //���Ź�������
                anim.SetTrigger("attack");
                nextAttack = Time.time + attackRate;
            }
        }
    }
    public virtual void Skill()//��bigbang
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextSkill)
            {
                Debug.Log("���ܹ���");
                //���Ź�������
                anim.SetTrigger("skill");
                nextSkill = Time.time + skillRate;
            }
        }
    }
    public void FilpDirection()//��ת
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

    public void SwitchPoint()//�л���
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
        if (!attackList.Contains(collision.transform)&&!hasBigBang&&!GameManager.instance.gameOver)//����б�û�й���Ŀ��
        {
            attackList.Add(collision.transform);//��ӵ������б�
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);//�Ƴ������б�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isDead && !GameManager.instance.gameOver)
        {
            StartCoroutine(OnSign());//����Э��
        }

    }

    //Э�̺���
    IEnumerator OnSign()
    {
        sign1.SetActive(true);
        yield return new WaitForSeconds(sign1.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
                       //Э�̵ĵȴ�ʱ��                       //Ƭ����Ϣ:��һ��Ĭ��layer�ĵ�һ��Ĭ�϶�����Ƭ�εĳ���
        sign1.SetActive(false);
    }
}
