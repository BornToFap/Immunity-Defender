using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Covid : EnemySystem
{
    // public int health { get; set; }

    /* #region Attack Variables

     public Transform raycast;
    public LayerMask raycastMask;
     public float raycastLenght;
     public float attackDistance;
     public float timer;
     private RaycastHit2D hit;
     public GameObject Target;
     public float distance;
     private bool attackmode;
     private bool inRange;
     private bool colling;
     

     #endregion*/

    public  float timer;
    public float intTime;
    private PlayerSystem thisplayer;
    public bool onStandBy;
    protected override void Initialization()
    {

        base.Initialization();

    }
    void Start()
    {
       
        Initialization();
        thisplayer = GameObject.Find("Player").GetComponent<PlayerSystem>();
        timer = intTime;
        cliptime = intcliptime;

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //    anim.SetFloat("IsWalking", transform.position.x);
        if (onStandBy)
        {
           
            anim.SetBool("canWalk", false);
            if(onChase && isChasing)
            {
                anim.SetBool("canWalk", true);
            }
        }


        
    }

    public float cliptime;
    public float intcliptime;
    protected override void Attack()
    {
       
        if (timer <= 0)
        {
            base.Attack();
            anim.SetBool("canAttack", true);
            if (cliptime <= 0)
            {
                timer = intTime;
                thisplayer.KnockBack();
                if (thisplayer.Invincible == false)
                {
                  //  FindObjectOfType<LevelManager>().Play("PlayerHit");
                    thisplayer.playerHeath--;
                }
                cliptime = intcliptime;
            }

        }
       
       // anim.SetFloat("IsWalking", transform.position.x);
        cliptime = cliptime - Time.deltaTime;
        timer = timer - Time.deltaTime;
    }

    protected override void Pattroling()
    {
      
        
        base.Pattroling();
        anim.SetBool("canWalk", true);
        anim.SetBool("canAttack", false);
       
    }
    protected override void Chase()
    {
        
            base.Chase();
        
        anim.SetBool("canWalk", true);
        anim.SetBool("canAttack", false);
        
        }

    public override void Damage(int playerdamage)
    {
      
        base.Damage(playerdamage);
    }


}
