using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFishGuy : Enemy, IDamageable
{
    public float scale;
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

    public void SwalowBigBang()
    {
        targetPoint.GetComponent<BigBang>()?.OffBigBang();
        targetPoint.gameObject.SetActive(false);

        transform.localScale *= scale;
    }
}
