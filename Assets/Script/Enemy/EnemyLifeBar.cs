using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//弃用
public class EnemyLifeBar : MonoBehaviour
{
    public int healthMax;
    public Text healthText;
    public Image health;


    // private void OnEnable()
    // {
    //     // 订阅事件
    //     GenerateLifebar.OnHealthUpdate += UpdateHealthBar;
    // }

    // private void OnDisable()
    // {
    //     // 取消订阅事件，防止内存泄漏
    //     GenerateLifebar.OnHealthUpdate -= UpdateHealthBar;
    // }

    void UpdateHealthBar(int currentHealth)
    {
        if (currentHealth >= 0)
        {
            health.fillAmount = (float)currentHealth / (float)healthMax;
            healthText.text = currentHealth.ToString() + "/" + healthMax.ToString();
        }
        // else gameObject.SetActive(false);

    }

    void Start()
    {
        // 初始化血条
        health.fillAmount = 1.0f;
        healthText.text = healthMax.ToString() + "/" + healthMax.ToString();
    }
}



