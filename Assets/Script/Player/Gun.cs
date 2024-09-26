using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : PlayerAttackMode
{
    public GameObject bulletPrefab;
    public Transform muzzieTransform;
    public Camera cam;
    public float launchingSpeed;
    private Vector3 mousePos;
    private Vector2 gunDirection;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame

    public override void Attack()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        gunDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;

        // 获取玩家的朝向
        //float lookX = playerAnimator.GetFloat("Look X");
        //int yRotation = (lookX >= 0) ? 0 : 180;

        // 根据玩家的朝向调整枪口方向
        transform.rotation = Quaternion.Euler(0, 0, angle + 45);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzieTransform.position, Quaternion.Euler(transform.eulerAngles));
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // 根据玩家的朝向偏移子弹发射方向
            Vector2 direction = (Quaternion.Euler(0, 0, -45) * muzzieTransform.right).normalized;

            // 设置子弹的初始速度方向
            bulletRb.velocity = direction * launchingSpeed;
            GameController.camShake.Shake();
            GameController.soundEffect.PlayerShotPlay();
        }
    }
}
