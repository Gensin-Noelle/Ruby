using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
    public float startWaitTime;
    private float waitTime;

    public Transform leftPos;
    public Transform rightPos;
    public Transform movePos;
    private Vector2 lastPosition;
    private Animator enemyAnimator;

    public override void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
        lastPosition = transform.position;
        enemyAnimator = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        // 计算移动方向
        Vector2 direction = (Vector2)transform.position - lastPosition;

        // 更新上一帧的位置
        lastPosition = transform.position;

        // 将移动方向传递给动画控制器等
        // UpdateAnimation(direction.x);
        // 更新动画状态
        if (direction.magnitude > 0.01f)
        {
            enemyAnimator.SetBool("isMove", true);
            UpdateAnimation(direction.x);
        }
        else
        {
            enemyAnimator.SetBool("isMove", false);
        }
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos.position) < 0.1f || Vector2.Distance(transform.position, lastPosition) < 0.1f)
        {
            if (waitTime <= 0)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void UpdateAnimation(float lookX)
    {
        // 设置动画控制器中的 "MoveX" 参数
        if (lookX > 0)
        {
            enemyAnimator.SetFloat("Look X", 1);
        }
        if (lookX < 0)
        {
            enemyAnimator.SetFloat("Look X", -1);
        }


    }

    Vector2 GetRandomPos()
    {
        return new Vector2(Random.Range(leftPos.position.x, rightPos.position.x), transform.position.y);
    }

}
