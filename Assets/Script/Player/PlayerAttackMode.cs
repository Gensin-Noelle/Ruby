using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(Time.timeScale != 0f)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        
    }
}
