using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    public static int HealthCurrent;
    public int HealthMax;

    private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();

        // 获取当前加载的场景
        Scene currentScene = SceneManager.GetActiveScene();

        // 获取当前场景在Build Settings中的索引
        int sceneIndex = currentScene.buildIndex;
        int health = PlayerPrefs.GetInt("PlayerHealth");
        if (health != 0 && sceneIndex == 2) HealthCurrent = health;
        else HealthCurrent = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)HealthCurrent / HealthMax;
        healthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
    }
}
