using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protal : MonoBehaviour
{
    Animator anim;
    //public BoxCollider2D coll;
    BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        //GameManager.instance.IsExitDoor(this);
        coll.enabled = true;
    }

    //public void OpenDoor()//gamemanager 调用
    //{
    //    anim.Play("doorExitOpen");
    //    coll.enabled = true;
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //下一关
            GameManager.instance.NextLevel();
        }
    }
}
