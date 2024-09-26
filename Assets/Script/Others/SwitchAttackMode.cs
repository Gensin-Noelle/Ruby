using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    这个脚本用来切换角色的攻击方式
*/
public class SwitchAttackMode : MonoBehaviour
{
    private GameObject closeAttack;
    private GameObject dartsAttack;
    private GameObject gunAttack;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        closeAttack = transform.Find("RubyAttack").gameObject;
        dartsAttack = transform.Find("DartsHit").gameObject;
        gunAttack = transform.Find("Gun").gameObject;
        counter = 0;

        // 默认攻击方式
        SetAttackMode(counter);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            counter++;
            SetAttackMode(counter);
        }
    }

    void SetAttackMode(int mode)
    {
        if (closeAttack != null && dartsAttack != null && gunAttack != null)
        {
            closeAttack.SetActive(mode % 2 == 0);
            dartsAttack.SetActive(mode % 2 == 0);
            gunAttack.SetActive(mode % 2 != 0);
        }
        else
        {
            Debug.Log("Ruby->SwitchAttackMode:未能获取相关组件");
        }
    }
}
