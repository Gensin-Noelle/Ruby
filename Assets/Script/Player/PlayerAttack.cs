using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerAttackMode
{
    public int damage;
    public float time;
    private Animator animator;
    private PolygonCollider2D coll2D;
    // Start is called before the Dfirst frame update
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();
    }

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float horizontal = Input.GetAxis("Horizontal");

            coll2D.enabled = true; //开启碰撞检测
            animator.SetTrigger("Launch");
            StartCoroutine(DisableHitBox()); //启动协程
        }
    }

    IEnumerator DisableHitBox() //创建一个协程，用于关闭碰撞检测
    {
        yield return new WaitForSeconds(time);
        coll2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            other.GetComponent<BaseEnemy>().TakeDamage(damage, true);
        }
    }
}
