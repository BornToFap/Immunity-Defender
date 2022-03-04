using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField] public int playerHeath;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private int attackpower;
    Rigidbody2D myrigid;
    BoxCollider2D mycirclecollider;
    CapsuleCollider2D mycapsulcollider;
    Vector2 m_Velocity = Vector2.zero;
    bool FacingRight = true;
    [SerializeField] private float KnockBackForce;
    [SerializeField] private LayerMask platform_mask;
    [SerializeField] private GameObject mesh;
    Animator myanim;

    void Start()
    {
       
        
        mesh = gameObject.transform.GetChild(0).gameObject;
        myrigid = gameObject.GetComponent<Rigidbody2D>();
        mycirclecollider = GetComponent<BoxCollider2D>();
        mycapsulcollider = GetComponent<CapsuleCollider2D>();
        myanim = GetComponent<Animator>();
        TimeBtwAttack = startTimeBtwAttack;



    }
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.J))
        {

            Debug.Log("I hit you");
            myanim.SetBool("Attack", true);
            FindObjectOfType<LevelManager>().Play("PlayerAttack");
          //  Attack();

        }
        else
        {
            myanim.SetBool("Attack", false);
        }
       
   
        Jump();
        if (DetectGround() == true)
        {
            myanim.SetBool("Jump", false);
        }
        else
        {
            myanim.SetBool("Jump", true);
        }

        UpdateAnimClipTimes();
    }
    void FixedUpdate()
    {
       
        if (playerHeath <= 0)
        {
            //FindObjectOfType<GameManagerSystem>().RestartingOnDeath();
           // Time.timeScale = 0f;
            Destroy(this.gameObject);
           
        }
      //  Attack();
        PlayerMovement();
        PlayerHealthUI();
        knockBackDirection();



    }

    private void PlayerMovement()
    {

        float Hinput = Input.GetAxis("Horizontal");
        float MovemenCalculation = Hinput * MovementSpeed * Time.deltaTime;
        Vector2 TargetInput = new Vector2(MovemenCalculation * 10, myrigid.velocity.y);
        myrigid.velocity = Vector2.SmoothDamp(myrigid.velocity, TargetInput, ref m_Velocity, 0.05f);

        if (Hinput > 0 && !FacingRight)
        {
            Flip();
        }

        else if (Hinput < 0 && FacingRight)
        {
            Flip();
        }

        myanim.SetFloat("Speed", Mathf.Abs(MovemenCalculation));
    }


    private void Jump()
    {
        if (DetectGround() && Input.GetButtonDown("Jump"))
        {
            FindObjectOfType<LevelManager>().Play("PlayerJump");
            myrigid.AddForce(new Vector2(0f, JumpForce));
        }

    }



    [SerializeField] private float TimeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float AttackRangeX, AttackRangeY;
    public LayerMask EnemyLayers;
    public bool canAttack;
 


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(AttackRangeX, AttackRangeY));
    }



    private void Attack()
    {
        if (TimeBtwAttack <= 0)
        {

            if (Input.GetKeyDown("j"))
            {
                myanim.SetBool("Attack", true);
                FindObjectOfType<LevelManager>().Play("PlayerAttack");
                
               
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(AttackRangeX, AttackRangeY), 0, EnemyLayers);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponentInParent<Covid>().Damage(attackpower);
                    Debug.Log("Damage Enemy");

                }
                TimeBtwAttack = startTimeBtwAttack;

            }
        }
        else
        {
            myanim.SetBool("Attack", false);
            TimeBtwAttack = TimeBtwAttack - Time.fixedDeltaTime;
        }



    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool DetectGround()
    {
        float extraHeight = 0.1f;
        RaycastHit2D groundHit = Physics2D.CapsuleCast(mycapsulcollider.bounds.center, mycapsulcollider.bounds.size, CapsuleDirection2D.Horizontal, 0f, Vector2.down, extraHeight, platform_mask);
        // RaycastHit2D groundHit = Physics2D.BoxCast(mycirclecollider.bounds.center, mycirclecollider.bounds.size, 0f, Vector2.down, extraHeight, platform_mask);
        Color raycolor;
        if (groundHit.collider != null)
        {

            raycolor = Color.green;

        }
        else
        {
            raycolor = Color.red;
        }
        Debug.DrawRay(mycapsulcollider.bounds.center, Vector2.down * (mycapsulcollider.bounds.extents.y + extraHeight), raycolor);
      //  Debug.DrawRay(transform.position, raycastdirect * 3f, raycolor);
        return groundHit.collider != null;
    }
    public Vector2 raycastdirect;
    private bool knockBackDirection()
    {
        if (transform.localScale.x >= 1)
        {
            raycastdirect = Vector2.right;
        }
        else
        {
            raycastdirect = Vector2.left;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastdirect, 3f, EnemyLayers);
        Color raycolor;
        if (hit.collider != null)
        {

            raycolor = Color.green;

        }
        else
        {
            raycolor = Color.red;
        }
        Debug.DrawRay(transform.position, raycastdirect * 3f, raycolor);
        return hit.collider != null;
    }
    public void KnockBack()
    {

        if (transform.localScale.x > 0 && transform.position.x > 0 && knockBackDirection())
        {
            myrigid.AddForce(new Vector2(KnockBackForce * (-100 * transform.localScale.x), KnockBackForce));
        }
        else if (transform.localScale.x < 0 && transform.position.x > 0 && !knockBackDirection())
        {
            myrigid.AddForce(new Vector2(KnockBackForce * (100 * transform.localScale.x), KnockBackForce));
        }
        else if (transform.localScale.x < 0 && transform.position.x > 0 && knockBackDirection())
        {
            myrigid.AddForce(new Vector2(KnockBackForce * (-100 * transform.localScale.x), KnockBackForce));
        }
        else if (transform.localScale.x > 0 && transform.position.x > 0 && !knockBackDirection())
        {
            myrigid.AddForce(new Vector2(KnockBackForce * (100 * transform.localScale.x), KnockBackForce));
        }
    }
    public bool Invincible;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Invincible == false)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                FindObjectOfType<LevelManager>().Play("PlayerHit");
                KnockBack();
                playerHeath--;
                Debug.Log("Knockbacked");
            }
        }

    }
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("I hit you");
            collision.gameObject.GetComponentInParent<Covid>().Damage(attackpower);
        }



        if (collision.gameObject.tag == "VitaminC")
        {
            FindObjectOfType<LevelManager>().Play("PowerUpAudio");
            Destroy(collision.gameObject);
            NumHeart++;
        }
        else if (collision.gameObject.tag == "VitaminD")
        {
            FindObjectOfType<LevelManager>().Play("PowerUpAudio");
            Destroy(collision.gameObject);
            attackpower++;
        }
        else if (collision.gameObject.tag == "VitaminE")
        {
            FindObjectOfType<LevelManager>().Play("PowerUpAudio");
            Destroy(collision.gameObject);
            playerHeath++;
        }
        else if (collision.gameObject.tag == "Zinc")
        {
            FindObjectOfType<LevelManager>().Play("PowerUpAudio");
            Destroy(collision.gameObject);
            Invincible = true;
            StartCoroutine(InvincibleTimer());
        }


    }

    IEnumerator InvincibleTimer()
    {
        if (Invincible == true)
        {
            yield return new WaitForSeconds(5f);
            Invincible = false;

        }
    }

    /*
    IEnumerator deactivateText()
    {
        yield return new WaitForSeconds(1f);
        mesh.SetActive(false);
    }
    */

    public Vector3 playerpos() { return transform.position; }

    [SerializeField] public int NumHeart;
    [SerializeField] private Image[] Hearts;
    [SerializeField] private Sprite FullHearts;
    [SerializeField] private Sprite EmptyHearts;

    private void PlayerHealthUI()
    {
        if (playerHeath > NumHeart)
        {
            playerHeath = NumHeart;
        }
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < playerHeath)
            {
                Hearts[i].sprite = FullHearts;
            }
            else
            {
                Hearts[i].sprite = EmptyHearts;
            }
            if (i < NumHeart)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }
        }
    }
    [SerializeField] private float attackTime;
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = myanim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "PlayerAttack":
                    attackTime = clip.length;
                    break;
            }
        }
    }
}
