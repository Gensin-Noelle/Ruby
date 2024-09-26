using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float climbSpeed;
    public int health;
    public float hitBoxCDTime;
    private Rigidbody2D rd;
    private Animator myAnimator;
    private BoxCollider2D myFeet;
    private SpriteRenderer myRenderer;
    private bool isGround;
    private bool canDoubleJump;
    private Transform attackTransform;
    private Transform darts;
    private PolygonCollider2D polygonCollider;
    // private CapsuleCollider2D capsuleCollider;
    //用于实现爬梯子
    private bool isLadder;
    private float defaultgravity;

    //事件
    // public static event RotationUpdate OnRotationUpdate;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        attackTransform = transform.GetChild(0);
        myRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        darts = transform.GetChild(1);
        defaultgravity = rd.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        CheckLadder();
        Run();
        Jump();
        Flip();
        Climb();
        OutOfWorldDetector();
    }

    // public delegate void RotationUpdate(string rotation);

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    void Flip()
    {
        //实现武器碰撞框和飞镖的翻转
        bool plaryHasXAxisSpeed = Mathf.Abs(rd.velocity.x) > Mathf.Epsilon;
        if (plaryHasXAxisSpeed)
        {
            if (rd.velocity.x > 0)
            {
                attackTransform.localRotation = Quaternion.Euler(0, 0, 0);
                darts.SetLocalPositionAndRotation(new Vector2(MathF.Abs(darts.localPosition.x), darts.localPosition.y), Quaternion.Euler(0, 0, 0));
            }
            if (rd.velocity.x < 0)
            {
                attackTransform.localRotation = Quaternion.Euler(0, 180, 0);
                darts.SetLocalPositionAndRotation(new Vector2(-MathF.Abs(darts.localPosition.x) , darts.localPosition.y), Quaternion.Euler(0, 180, 0));
            }
        }
    }

    void Run()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(horizontal * moveSpeed, rd.velocity.y);
        rd.velocity = playerVel;
        //判断玩家是否有向左右移动的速度
        bool plaryHasXAxisSpeed = Mathf.Abs(rd.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isMove", plaryHasXAxisSpeed);
        if (horizontal > 0)
        {
            myAnimator.SetFloat("Look X", horizontal);
        }
        if (horizontal < 0)
        {
            myAnimator.SetFloat("Look X", -1);
        }


    }

    void Jump()
    {
        if (isGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVel = new(0.0f, jumpSpeed);
                rd.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
        }
        else
        {
            if (canDoubleJump && Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                rd.velocity = Vector2.up * jumpVel;
                canDoubleJump = false;
            }
        }

    }

    void Climb()
    {
        if (isLadder)
        {
            float moveY = Input.GetAxis("Vertical");
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnimator.SetFloat("Look Y", 1);
                rd.gravityScale = 0.0f;
                rd.velocity = new Vector2(rd.velocity.x, moveY * climbSpeed);

            }
            else
            {

                myAnimator.SetFloat("Look Y", -1);
                rd.velocity = new Vector2(rd.velocity.x, 0.0f);

            }
        }
        else
        {
            myAnimator.SetFloat("Look Y", 0);
            rd.gravityScale = defaultgravity;
        }
    }

    void OutOfWorldDetector()
    {
        //检测是否掉出世界，掉出世界就返回主菜单
        if (transform.position.y < -10)
        {
            SceneManager.LoadSceneAsync("Menu");
        }
    }

    void Attack() //废用，由角色攻击来调用角色动画
    {
        if (Input.GetMouseButtonDown(0))
        {
            myAnimator.SetTrigger("Launch");
        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            HealthBar.HealthCurrent = 0;
        }
        else
        {
            HealthBar.HealthCurrent = health;
        }
        myAnimator.SetTrigger("Hit");
        //角色受击音效
        GameController.soundEffect.PlayerInjuredPlay();
        //是否开启第3个碰撞体（多边形碰撞体），完成持续碰撞检测，用于地刺功能实现
        polygonCollider.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
        // BlinkPlayer(3, 2.0f);
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCDTime);
        polygonCollider.enabled = true;
    }

    //弃用,角色受伤闪烁改由动画实现
    void BlinkPlayer(int numBlinks, float seconds)
    {
        // myRenderer.enabled = false;
        // myRenderer.color = Color.red;
        StartCoroutine(DoBlinks(numBlinks, seconds));

    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            myRenderer.enabled = !myRenderer.enabled;
            yield return new WaitForSeconds(seconds / (numBlinks * 2));
        }
        myRenderer.enabled = true;
    }
}

