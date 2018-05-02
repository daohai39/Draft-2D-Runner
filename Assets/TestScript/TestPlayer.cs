using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestPlayer : TestCharacter
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
    private float jumpForce;

    [SerializeField]
    private bool airControl;

    private float nextTime;

    [SerializeField]
    private float shootWait;

    public bool Jump {get; set;}

    public bool OnGround {get; set;}

    public bool Sit {get; set;}
    
    public GameObject bulletPrefab; 
    public Transform shotSpawn;

    public override bool IsDead 
    {
        get {
            return health <= 0;
        }
    }


    private void Update() 
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (IsDead) return;
        if (!TakeDamage)
        {
            var horizontal = Input.GetAxis("Horizontal");
            OnGround = IsGrounded();

            HandleMovement(horizontal);

            ChangeDirection(horizontal);
            //Handle animation layers
            HandleLayers();
        }
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
            Animator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            if (Time.time >= nextTime)
            {
                nextTime = Time.time + shootWait;
                Animator.SetTrigger("attack");
            }
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            Animator.SetBool("sit", true);
        } else if (!Input.GetKey(KeyCode.DownArrow)) {
            Animator.SetBool("sit", false);
        }
    }
    
    //Need improve
    private void HandleMovement(float horizontal)
    {
        if (Rigidbody.velocity.y < 0) 
        {
            Animator.SetBool("landing", true);
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
            Animator.SetBool("sit", true);
            Rigidbody.velocity = new Vector2(horizontal  * speed / 2, Rigidbody.velocity.y);
        }
        Animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleLayers()
    {
        if (!OnGround) {
            Animator.SetLayerWeight(1,1);
        }
        else {
            Animator.SetLayerWeight(1,0);
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

    public void Shoot()
    {
        if (isFacingRight && Attack)
        {
            GameObject tmp = Instantiate(bulletPrefab, shotSpawn.position, Quaternion.identity);
            tmp.GetComponent<Bullet>().Initialize(Vector2.right);
        } else if (!isFacingRight && Attack) {
            GameObject tmp = Instantiate(bulletPrefab, shotSpawn.position, Quaternion.identity);
            tmp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
    }

    public override IEnumerator TakingDamage()
    {
        health -= 10;
        if (!IsDead) {
			Animator.SetTrigger("damage");
        } else {
            Animator.SetTrigger("die");
        }
        yield return null;
    }
}