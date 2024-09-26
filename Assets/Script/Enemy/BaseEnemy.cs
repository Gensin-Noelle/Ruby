using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseEnemy : MonoBehaviour
{
    public int damage;
    public int health;
    public float speed;
    public float damageCoolTime;
    public GameObject dropObject;
    public GameObject bloodEffect;
    public GameObject floatPoint;
    public static Dictionary<int, List<Vector2>> enemyPosSet = new();
    public static Dictionary<int, List<int>> enemyHealthSet = new();

    private SpriteRenderer enemyRenderer;
    private PlayerController player;
    private Animator TempPlayerAnimator;
    private float coolDownTimer;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
        int instanceID = gameObject.GetInstanceID();
        enemyHealthSet[instanceID] = new List<int> { health };
        enemyPosSet[instanceID] = new List<Vector2> { transform.position };
    }
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        coolDownTimer = damageCoolTime;
        try
        {
            TempPlayerAnimator = player.GetComponent<Animator>();
        }
        catch (Exception e)
        {
            Debug.Log("异常信息:" + e.Message);
        }

    }

    // Update is called once per frame
    public virtual void Update()
    {
        int instanceID = gameObject.GetInstanceID();
        enemyPosSet[instanceID][0] = transform.position;
        EnemyDied();
    }

    public virtual void EnemyDied()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            //实例化掉落物
            if (TempPlayerAnimator.GetFloat("Look X") >= 0)
            {
                Instantiate(dropObject, new Vector2(transform.position.x + 1, transform.position.y + 0.5f), Quaternion.identity);
            }
            else
            {
                Instantiate(dropObject, new Vector2(transform.position.x - 1, transform.position.y + 0.5f), Quaternion.identity);
            }
        }
    }


    public void TakeDamage(int damage, bool isShake)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        //将生命值更新到字典中
        enemyHealthSet[gameObject.GetInstanceID()][0] = health;
        if (enemyRenderer != null)
        {
            enemyRenderer.color = Color.red;
            //出血粒子特效
            Instantiate(bloodEffect, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
            //伤害值显示
            Instantiate(floatPoint, transform.position, Quaternion.identity).GetComponent<TextMeshPro>().text = damage.ToString();
            //相机抖动
            if (isShake)
            {
                GameController.camShake.Shake();
            }
            //音效
            GameController.soundEffect.EnemyInjuredPlay();
            StartCoroutine(DelayedAction(0.2f));
        }
    }

    IEnumerator DelayedAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        enemyRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //判断碰撞物体的标签是否为玩家，类型是否为胶囊体
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (player != null)
            {
                if (coolDownTimer > 0)
                {
                    coolDownTimer -= Time.deltaTime;
                }
                else
                {
                    player.DamagePlayer(damage);
                    coolDownTimer = damageCoolTime;
                }
            }

        }
    }
}
