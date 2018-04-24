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

    private float nextTime;
    [SerializeField]
    private float shootWait;
    private Animator animator;

    public bool Attack {get; set;}

    public bool Jump {get; set;}

    public bool OnGround {get; set;}

    public bool Sit {get; set;}
    
    public GameObject bulletPrefab; 
    public Transform shotSpawn;

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

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            if (Time.time >= nextTime)
            {
                nextTime = Time.time + shootWait;
                animator.SetTrigger("attack");
                Shoot();
            }
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

    private void Shoot()
    {
        if (isFacingRight)
        {
            GameObject tmp = Instantiate(bulletPrefab, shotSpawn.position, Quaternion.identity);
            tmp.GetComponent<Bullet>().Initialize(Vector2.right);
        } else {
            GameObject tmp = Instantiate(bulletPrefab, shotSpawn.position, Quaternion.identity);
            tmp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
    }
}