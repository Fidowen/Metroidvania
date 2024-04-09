using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement Settings:")]
    [SerializeField] private float walkSpeed = 1;
    [Space(5)]


    [Header("Vertical Movement Settings")]
    [SerializeField] private float jumpForce = 45f;
    private int jumpBufferCounter = 0;
    [SerializeField] private int jumpBufferFrames;
    private float coyoteTimeCounter = 0;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int airJumpCounter = 0;
    [SerializeField] private int maxAirJumps;
    [Space(5)]


    [Header("Ground Check Settings:")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    [Space(5)]


    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] GameObject dashEffect;
    [Space(5)]

    [Header("Attacker")]
    public BulletPool bulletPool;
    private float BulletInterval = 2;
    private float Bulletspeed = 5;
    private float lastTime;
    [SerializeField] private Transform spawnPoint;
    private Vector2 spawnDir;





    [Header("Save and Load")]
    [SerializeField] private string savescene;
    [SerializeField] private Vector2 savepos;

    [Space(5)]
    PlayerStateList pState;
    SaveHistory saveHistory;
    private Rigidbody2D rb;
    private float xAxis;
    private float gravity;
    Animator anim;
    private bool canDash = true;
    private bool dashed;
    


    public static PlayerController Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        pState = GetComponent<PlayerStateList>();

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        UpdateJumpVariables();

        if (pState.dashing) return;
        Flip();
        Move();
        Jump();
        StartDash();
        Attack();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    void Flip()
    {
        if (xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if (xAxis > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
        anim.SetBool("Walking", rb.velocity.x != 0 && Grounded());
    }

    void StartDash()
    {
        if(Input.GetButtonDown("Dash") && canDash && !dashed)
        {
            StartCoroutine(Dash());
            dashed = true;
        }

        if (Grounded())
        {
            dashed = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        pState.dashing = true;
        anim.SetTrigger("Dashing");
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        if (Grounded()) Instantiate(dashEffect, transform);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = gravity;
        pState.dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround) 
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround) 
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Jump()
    {

        if (!pState.jumping && jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);

            pState.jumping = true;           
        }
        if (!Grounded() && airJumpCounter < maxAirJumps && Input.GetButtonDown("Jump"))
        {
            pState.jumping = true;

            airJumpCounter++;

            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

            pState.jumping = false;
        }

        anim.SetBool("Jumping", !Grounded());
    }
    void Attack()
    {
        if (Input.GetButtonDown("Attack") /*&& Time.deltaTime - lastTime > BulletInterval*/)
        {
            Debug.Log("5465");
            GameObject Bullets = bulletPool.GetBulletObject(Random.Range(0, bulletPool.bullets.Length));
            Bullets.SetActive(true);
            Bullets.transform.position = spawnPoint.position;

        }
    }
    void UpdateJumpVariables()
    {
        if (Grounded())
        {
            pState.jumping = false;
            coyoteTimeCounter = coyoteTime;
            airJumpCounter = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferFrames;
        }
        else
        {
            jumpBufferCounter--;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            pState.Isintrun = true;
            Debug.Log("我碰到NPC");
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        pState.Isintrun = false;
        Debug.Log("我離開NPC");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SavePoint")
        {
            Debug.Log("我撞到存檔點");
            SaveEnemy();
        }
    }
    public void SaveEnemy()//存檔
    {
        savescene = SceneManager.GetActiveScene().name;
        savepos = this.gameObject.transform.position;
    }
    public void DeadandLoad()//讀檔加載
    {
        SceneManager.LoadScene(savescene);
        this.transform.position = savepos;
    }
}
