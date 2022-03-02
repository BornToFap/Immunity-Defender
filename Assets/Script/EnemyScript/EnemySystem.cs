using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Animations;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] public int Enemyhealth;
    [SerializeField] protected float MovementSpeed;
    [SerializeField] protected int EnemyDamage;
    [SerializeField] protected BoxCollider2D enemycollider;
    [SerializeField] protected LayerMask platform_mask;
    [SerializeField] protected BoxCollider2D detectcollider;
    [SerializeField] protected Transform groundcheckpos;
     protected Rigidbody2D EnemyRigid;
    [SerializeField] protected int  Vidnum, Vstatus = 0;
   [SerializeField] Vector2 startingpos;
    protected Animator anim;

   [SerializeField] protected Transform player;
    [SerializeField] protected float attackDistance;


  //  [SerializeField] protected Transform PointA, PointB;
  //  [SerializeField] protected Vector3 PointAtrans, PointBtrans;
    protected virtual void Initialization()
    {

        EnemyRigid = GetComponentInChildren<Rigidbody2D>();
        enemycollider = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponent<Animator>();
        if (PlayerPrefs.GetInt("VID" + Vidnum, Vstatus) == 0)
        {
            PlayerPrefs.SetInt("VID" + Vidnum, Vstatus);
            Debug.Log("VirusStatus:" + PlayerPrefs.GetInt("VID" + Vidnum, Vstatus));
        }

        startingpos = transform.position;


    }
    private void Start()
    {
        Initialization();
       
    }

    [SerializeField] protected bool Ispattroling, IsAttackmode,  isChasing, onChase, FixedPattrol;
    protected virtual void FixedUpdate()
    {
      //  Vector3.MoveTowards(transform.position, PointBtrans, speedtowards * Time.deltaTime);


        if (!RaycastDetectPLayer() && Ispattroling)
        {
            Pattroling();
       
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < attackDistance && RaycastDetectPLayer() && IsAttackmode == true)
        {
           FindObjectOfType<LevelManager>().Play("EnemyAttack");
            Attack();
       
        }
       else if(onChase == true){
            Chase();

            if (isChasing == false)
            {
               
              transform.position = Vector2.MoveTowards(transform.position, startingpos, speedtowards * Time.deltaTime);
              transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
          
            }
        }

        if(Enemyhealth < 3 && !RaycastDetectPLayer() && ihavebeenhit == true)
        {

            IhavebeenHIt();
            // IhavebeenHIt();

            // MovementSpeed *= -1;
        }


        Dead();
        RaycastDetectPLayer();
     
        
    }
    public bool ihavebeenhit;
    private void IhavebeenHIt()
    {
       
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        ihavebeenhit = false;
        if (Ispattroling)
        {
            MovementSpeed *= -1;
        }
        
        
    }

    protected virtual void Dead()
    {
        if(Enemyhealth <= 0)
        {
            Vstatus = 1;
            if (PlayerPrefs.GetInt("VID" + Vidnum, Vstatus) <= 0)
            {
                PlayerPrefs.SetInt("VID" + Vidnum, 1);
            }
            Debug.Log("VirusStatus:" + PlayerPrefs.GetInt("VID" + Vidnum, Vstatus));
           FindObjectOfType<LevelManager>().Play("EnemyDeath");
            Destroy(this.gameObject);
        }
        
    }
    protected virtual void Pattroling()
    {

        EnemyRigid.velocity = new Vector2(MovementSpeed * Time.deltaTime, EnemyRigid.velocity.y);
        isChasing = false; 
        if (!GroundCheck() || detectcollider.IsTouchingLayers(platform_mask))
        {
            fliping();
        }
    }
 
    protected void fliping()
    {
        if(isChasing == false)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            MovementSpeed *= -1;
        }
    }

    protected bool GroundCheck()
    {
        float extraHeight = 0.3f;
        RaycastHit2D groundHit = Physics2D.Raycast(detectcollider.bounds.center, Vector2.down, enemycollider.bounds.extents.y + extraHeight, platform_mask);
        Color raycolor;
        if (groundHit.collider != null)
        {

            raycolor = Color.green;

        }
        else
        {
            raycolor = Color.red;
        }
        Debug.DrawRay(groundcheckpos.position, Vector2.down * (enemycollider.bounds.extents.y + extraHeight), raycolor);
        return groundHit.collider != null;
    }

    [SerializeField] public int DistanceTarget;
    [SerializeField] private float speedtowards;
   protected virtual void Chase()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < DistanceTarget && RaycastDetectPLayer())
        {
            isChasing = true;
           FindObjectOfType<LevelManager>().Play("EnemyChase");
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speedtowards * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y,1f);
        }
        else
        {

            isChasing = false;
            
        }

    }

    [SerializeField] protected float raycastLenght;
    [SerializeField] protected LayerMask raycastMask;
    Vector2 raycastdirect;
    private bool RaycastDetectPLayer()
    {
        if(transform.localScale.x > 1)
        {
            raycastdirect = Vector2.right;
        }
        else
        {
            raycastdirect = Vector2.left;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastdirect, raycastLenght, raycastMask);
        Color raycolor;
        if (hit.collider != null)
        {

            raycolor = Color.green;

        }
        else
        {
            raycolor = Color.red;
        }
        Debug.DrawRay(groundcheckpos.position, raycastdirect * raycastLenght, raycolor);
        return hit.collider != null;

    }
    protected virtual void Attack(){
       
    }
    [SerializeField] private float KnockBackForce;
  public virtual void Damage(int playerdamage)
    {
        ihavebeenhit = true;
        Enemyhealth = Enemyhealth - playerdamage;
       FindObjectOfType<LevelManager>().Play("EnemyHit");

        if (transform.localScale.x > 0 && transform.position.x > 0 && RaycastDetectPLayer())
        {
            EnemyRigid.AddForce(new Vector2(KnockBackForce * (-100 * transform.localScale.x), KnockBackForce));
        }
        else if (transform.localScale.x < 0 && transform.position.x > 0 && !RaycastDetectPLayer())
        {
            EnemyRigid.AddForce(new Vector2(KnockBackForce * (100 * transform.localScale.x), KnockBackForce));
        }
        else if (transform.localScale.x < 0 && transform.position.x > 0 && RaycastDetectPLayer())
        {
            EnemyRigid.AddForce(new Vector2(KnockBackForce * (-100 * transform.localScale.x), KnockBackForce));
        }
        else if (transform.localScale.x > 0 && transform.position.x > 0 && !RaycastDetectPLayer())
        {
            EnemyRigid.AddForce(new Vector2(KnockBackForce * (100 * transform.localScale.x), KnockBackForce));
        }
    }
}

