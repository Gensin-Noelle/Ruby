using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class FlyingEnemy : BaseEnemy
{
    public float radius;
    public GameObject enemyBullet;
    public float bulletSpeed = 10f;
    public float fireRate = 2f; // 每秒发射的子弹数量

    private float nextFireTime;

    private Transform playerTransform;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance < radius && Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    public override void EnemyDied()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            //实例化掉落物
            Instantiate(dropObject, new Vector2(playerTransform.position.x, playerTransform.position.y + 2.0f), Quaternion.identity);

        }
    }
}
