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

    [Header("玩家状态")]
    public float health;
    public bool isDead;

    [Header("地面检测")]
    public Transform groundCheck;
    public float checkRadius;//检测范围
    public LayerMask groundLayer;//检测图层

    [Header("状态检测")]
    public bool isGround;
    public bool isJump;
    public bool isHurt;
    private bool canJump;

    [Header("跳跃粒子效果")]
    public GameObject jumpFX;
    public GameObject landFX;

    [Header("攻击")]
    public GameObject bigbang;
    public float nextAttack = 0;//下一次攻击的时间//实现累加
    public float attackRate;//攻击频率



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();

        GameManager.instance.IsPlayer(this);

        health=GameManager.instance.LoadHealth();

        UIManager.instance.UpdateHealth(health);//更新血量
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("dead", isDead);
        if(isDead)
        {
            return;
        }
        //用来判断是否正在播放受伤动画
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
        //非受伤状态才可以移动和跳跃
        if (!isHurt)
        {
            Movement();//input会覆盖Rigidbody的速度，所以用isHurt来锁定就可以让 Player 被击飞了
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
        //键盘
        //float horizontalInput = Input.GetAxis("Horizontal");//-1到1 包括小数
        //float horizontalInput = Input.GetAxisRaw("Horizontal");//-1到1 不包括小数
        //摇杆
        float horizontalInput = joystick.Horizontal;


        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);


        //键盘
        //if (horizontalInput != 0)
        //{
        //    transform.localScale = new Vector3(horizontalInput, 1, 1);//人物的翻转
        //}

        //摇杆
        if (horizontalInput > 0)
        {//利益rotation翻转180来实现
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
            jumpFX.SetActive(true);//显示粒子效果
            jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);//加上偏移
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //rb.gravityScale = 4;//人物跳起来后，重力改为4
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
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//圆形检测

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

    public void LandFX()//动画效果的事件
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.55f, 0);//加上偏移
    }

    public void OnDrawGizmos()
    {// 显示检测范围
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    public void GetHit(float damage)
    {
        if(!anim.GetCurrentAnimatorStateInfo(1).IsName("playerHit"))//在播放动画的伤害时候不会受伤
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
