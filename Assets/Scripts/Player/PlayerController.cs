using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IDamageable
{
    private Rigidbody2D rb;
    private Animator anim;
    private FixedJoystick joystick;

    public float speed;
    public float jumpForce;

    [Header("���״̬")]
    public float health;
    public bool isDead;

    [Header("������")]
    public Transform groundCheck;
    public float checkRadius;//��ⷶΧ
    public LayerMask groundLayer;//���ͼ��

    [Header("״̬���")]
    public bool isGround;
    public bool isJump;
    public bool isHurt;
    private bool canJump;

    [Header("��Ծ����Ч��")]
    public GameObject jumpFX;
    public GameObject landFX;

    [Header("����")]
    public GameObject bigbang;
    public float nextAttack = 0;//��һ�ι�����ʱ��//ʵ���ۼ�
    public float attackRate;//����Ƶ��



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();

        GameManager.instance.IsPlayer(this);

        health=GameManager.instance.LoadHealth();

        UIManager.instance.UpdateHealth(health);//����Ѫ��
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("dead", isDead);
        if(isDead)
        {
            return;
        }
        //�����ж��Ƿ����ڲ������˶���
        isHurt = anim.GetCurrentAnimatorStateInfo(1).IsName("player_hit");
        CheckInput();
        //PhysicsCheck();
    }

    public void FixedUpdate()
    {
        if(isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        PhysicsCheck();
        //Movement();
        //PlayerJump();
        //������״̬�ſ����ƶ�����Ծ
        if (!isHurt)
        {
            Movement();//input�Ḳ��Rigidbody���ٶȣ�������isHurt�������Ϳ����� Player ��������
            PlayerJump();
        }
    }
    void CheckInput()
    {
        if (Input.GetButtonDown("Jump")&&isGround)
        {
            canJump = true;
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    void Movement()
    {
        //����
        //float horizontalInput = Input.GetAxis("Horizontal");//-1��1 ����С��
        //float horizontalInput = Input.GetAxisRaw("Horizontal");//-1��1 ������С��
        //ҡ��
        float horizontalInput = joystick.Horizontal;


        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);


        //����
        //if (horizontalInput != 0)
        //{
        //    transform.localScale = new Vector3(horizontalInput, 1, 1);//����ķ�ת
        //}

        //ҡ��
        if (horizontalInput > 0)
        {//����rotation��ת180��ʵ��
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }
     void PlayerJump()
    {
        if(canJump==true)
        {
            isJump = true;
            jumpFX.SetActive(true);//��ʾ����Ч��
            jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);//����ƫ��
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //rb.gravityScale = 4;//������������������Ϊ4
            canJump = false;
        }
    }

    public void ButtonJump()
    {
        if (isGround)
        {
            canJump = true;
        }
        //canJump = true;
    }
    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            Instantiate(bigbang,transform.position, bigbang.transform.rotation);

            nextAttack = Time.time + attackRate;//cd

        }
    }
    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//Բ�μ��

        //if(isGround==true)
        //{
        //    rb.gravityScale = 1;
        //}
        if (isGround)
        {
            rb.gravityScale = 1;
            isJump = false;
        }
        else
        {
            rb.gravityScale = 4;
        }
    }

    public void LandFX()//����Ч�����¼�
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.55f, 0);//����ƫ��
    }

    public void OnDrawGizmos()
    {// ��ʾ��ⷶΧ
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    public void GetHit(float damage)
    {
        if(!anim.GetCurrentAnimatorStateInfo(1).IsName("playerHit"))//�ڲ��Ŷ������˺�ʱ�򲻻�����
        {
            health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            anim.SetTrigger("hit");

            UIManager.instance.UpdateHealth(health);
        }
        
    }
}
