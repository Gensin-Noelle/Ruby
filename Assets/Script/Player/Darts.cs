using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Darts : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float tuning;
    public int damage;


    private Rigidbody2D rd;
    private Transform playerTransform;
    private Vector2 startSpeed;
    private Vector2 standardSpeed;
    // private CameraShake cameraShake;


    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        standardSpeed = transform.right * speed;
        rd.velocity = standardSpeed;
        startSpeed = rd.velocity;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // cameraShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        //如果玩家存在世界里，飞镖回到玩家附件消失，否则直接消失
        if (playerTransform != null)
        {
            transform.Rotate(0, 0, rotateSpeed);
            rd.velocity -= startSpeed * Time.deltaTime;
            if (rd.velocity.x < 0.1f)
            {
                float y = Mathf.Lerp(transform.position.y, playerTransform.position.y + 0.5f, tuning);
                transform.position = new Vector3(transform.position.x, y, 0.0f);
            }
            if (Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f)
            {
                Destroy(gameObject);
            }
        }
        else Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            other.GetComponent<BaseEnemy>().TakeDamage(damage, true);
        }
    }

    // private void UpdateRotation(string rotation)
    // {
    //     if (rotation.Equals("left"))
    //     {
    //         standardSpeed = -transform.right * speed;
    //     }
    //     else if (rotation.Equals("right"))
    //     {
    //         standardSpeed = transform.right * speed;
    //     }
    //     // if (Input.GetKeyDown(KeyCode.E))
    //     // {
    //     // 更新 Rigidbody2D 的速度
    //     rd.velocity = standardSpeed;
    //     startSpeed = standardSpeed;
    //     // }
    // }

    // private void OnEnable()
    // {
    //     PlayerController.OnRotationUpdate += UpdateRotation;
    // }

    // private void OnDisable()
    // {
    //     PlayerController.OnRotationUpdate -= UpdateRotation;
    // }
}
