using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestPlayer : MonoBehaviour
{
    private static TestPlayer instance;
    public static TestPlayer Instance {
         get {
             if (instance == null)
             {
                instance = GameObject.FindObjectOfType<TestPlayer>();   
             } 
             return instance;
        }
    }

    
    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform[] groundPositions;

    [SerializeField]
    private float groundRadius;

    [SerializeField] 
    private float speed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private bool airControl;

    private bool isFacingRight;
    private Animator animator;

    public bool Attack {get; set;}

    public bool Jump {get; set;}

    public bool OnGround {get; set;}

    public bool Sit {get; set;}
    
    public Rigidbody2D Rigidbody { get; set;}
    private void Awake()
    {   
        isFacingRight = true;
        Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        OnGround = IsGrounded();

        HandleMovement(horizontal);

        ChangeDirection(horizontal);
        //Handle animation layers
        HandleLayers();
    }

    private bool IsGrounded()
    {
        // if falling 
        if (Rigidbody.velocity.y <= 0) {
            foreach (Transform groundPoint in groundPositions)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position,groundRadius, whatIsGround);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject != gameObject)
                    {
                        return true;                        
                    }
                }
            }
        }   
        return false;
    }

    // Can remove for restructure
    // private void HandleAttacks()
    // {
    //     //if attack && !_isGrounded
    //     //      set trigger animation Jump Attack

    //     if(Attack && OnGround && !Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
    //         Animator.SetTrigger("attack");
    //         Rigidbody.velocity = Vector2.zero;
    //     }
    //     if(_jumpAttack && !OnGround && !Animator.GetCurrentAnimatorStateInfo(1).IsName("Jump Attack")) {
    //         Animator.SetBool("jumpAttack", true);
    //     }
    //     if(!_jumpAttack && !Animator.GetCurrentAnimatorStateInfo(1).IsName("Jump Attack")) {
    //         Animator.SetBool("jumpAttack", false);
    //     }
    // }

    //Need improve
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            animator.SetTrigger("attack");
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            animator.SetBool("sit", true);
        } else if (!Input.GetKey(KeyCode.DownArrow)) {
            animator.SetBool("sit", false);
        }
    }
    
    //Need improve
    private void HandleMovement(float horizontal)
    {
        if (Rigidbody.velocity.y < 0) 
        {
            animator.SetBool("landing", true);
        }
        if (!Attack && !Sit && (OnGround || airControl))
        {
            Rigidbody.velocity = new Vector2 (horizontal * speed, Rigidbody.velocity.y);
        }
        if (OnGround && Jump) 
        {
            Rigidbody.AddForce(new Vector2(0, jumpForce));
        }
        if (OnGround && Sit)
        {
            animator.SetBool("sit", true);
            Rigidbody.velocity = new Vector2(horizontal  * speed / 2, Rigidbody.velocity.y);
        }
        animator.SetFloat("speed", Mathf.Abs(horizontal));
        // if (Rigidbody.velocity.y < 0) {
        //     Animator.SetBool("landing", true);
        // }
        // if (!Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
        //     Rigidbody.velocity = new Vector2(horizontal * speed, Rigidbody.velocity.y);
        // }
        // if (OnGround && Jump) {
        //     OnGround = false;
        //     Rigidbody.AddForce(new Vector2(0, jumpForce));
        //     Animator.SetTrigger("jump");
        // }
        // if (Sit) {
        //     Animator.SetBool("sit", true);
        // } else if (!Sit) {
        //     Animator.SetBool("sit", false);
        // }
        // if (!Jump && OnGround)
        //     Animator.SetFloat("speed", Mathf.Abs(horizontal));
        // else 
        //     Animator.SetFloat("speed", 0.0f);
    }

    private void HandleLayers()
    {
        if (!OnGround) {
            animator.SetLayerWeight(1,1);
        }
        else {
            animator.SetLayerWeight(1,0);
        }
    }

    private void ChangeDirection(float horizontal)
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }
}