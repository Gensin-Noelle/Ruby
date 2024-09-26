using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRefresh : MonoBehaviour
{
    public GameObject enemy;
    public int count;

    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
