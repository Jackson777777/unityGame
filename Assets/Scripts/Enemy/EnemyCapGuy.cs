using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCapGuy : Enemy, IDamageable
{
    SpriteRenderer spriteRenderer;
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

    public override void Initial()
    {
        base.Initial();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();  
        if(animState==0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public override void Skill()
    {
        base.Skill();

        if (anim.GetCurrentAnimatorStateInfo(1).IsName("capguySkill"))
        {
            spriteRenderer.flipX= true;

            if(transform.position.x>targetPoint.position.x)//Õ¨µ¯ÔÚ×ó±ß
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, speed * 2 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, speed * 2 * Time.deltaTime);
            }
        }
        else
        {
            spriteRenderer.flipX= false;
        }
    }
}
