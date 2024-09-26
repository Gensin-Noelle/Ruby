using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    public GameObject mainCamera;
    public float mapWidth;
    public float mapNums;
    private float totalWidth;
    public float transitionSpeed = 5f; // 过渡速度

    private Vector3 targetPosition;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mapWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        totalWidth = mapWidth * mapNums;

        targetPosition = transform.position;
    }

    void Update()
    {
        if (mainCamera.transform.position.x > transform.position.x + totalWidth / 2)
        {
            targetPosition.x += totalWidth;
        }
        else if (mainCamera.transform.position.x < transform.position.x - totalWidth / 2)
        {
            targetPosition.x -= totalWidth;
        }

        // 平滑移动背景
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
    }
}
