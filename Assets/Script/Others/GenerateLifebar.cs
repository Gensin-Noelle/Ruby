using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLifebar : MonoBehaviour
{
    public RectTransform CanvasRect;
    public Canvas canvas;
    public GameObject lifebarPrefab;
    public int currentEnemyNum;
    public int xOffset;
    public int yOffset;


    private readonly Dictionary<int, GameObject> lifebarsSet = new();
    private readonly Dictionary<int, int> healthMaxSet = new();

    void Start()
    {
        List<int> keys = BaseEnemy.enemyPosSet.Keys.ToList();
        foreach (int key in keys)
        {
            GameObject lifebar = Instantiate(lifebarPrefab, Vector3.zero, Quaternion.identity, canvas.transform);
            lifebarsSet[key] = lifebar;
            healthMaxSet[key] = GetMaxHealth(key); // 获取最大生命值
        }
        HiddenLifebar();//无效果QAQ
        DestroyEnemyLifebar(currentEnemyNum);
    }

    void Update()
    {
        UpdateLifebars();
    }

    int GetMaxHealth(int enemyID)
    {
        if (BaseEnemy.enemyHealthSet.ContainsKey(enemyID))
        {
            List<int> healthList = BaseEnemy.enemyHealthSet[enemyID];
            // 第一个元素是最大生命值
            return healthList[0];
        }
        else
        {
            // 没有找到对应的生命值列表，返回默认值
            return 100;
        }
    }

    void UpdateLifebars()
    {
        foreach (int key in lifebarsSet.Keys)
        {
            if (lifebarsSet[key] != null) // 添加对 lifebar 是否为 null 的检查
            {
                if (BaseEnemy.enemyPosSet.ContainsKey(key))
                {
                    Vector2 position = BaseEnemy.enemyPosSet[key][0];
                    int health = BaseEnemy.enemyHealthSet[key][0];
                    int healthMax = healthMaxSet[key];
                    LifebarFollow(lifebarsSet[key].GetComponent<RectTransform>(), position);
                    UpdateLifebarStatus(lifebarsSet[key], health, healthMax);
                }
            }
        }
    }

    private void LifebarFollow(RectTransform uiElement, Vector2 position)
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(position);
        Vector2 worldObjectScreenPos = new Vector2((viewportPos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f) + xOffset, (viewportPos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f) + yOffset);
        uiElement.anchoredPosition = worldObjectScreenPos;
    }

    private void UpdateLifebarStatus(GameObject lifebarObject, int health, int healthMax)
    {
        if (health > 0)
        {
            // 获取生命条的血量显示文本组件
            Text healthText = lifebarObject.GetComponentInChildren<Text>();
            Image healthFill = lifebarObject.transform.Find("EnemyHealth").GetComponent<Image>();
            if (healthText != null)
            {
                // 更新血量显示文本
                healthText.text = health.ToString() + "/" + healthMax.ToString();
            }
            // 更新血量填充比例
            if (healthFill != null)
            {
                healthFill.fillAmount = (float)health / healthMax;
            }
        }
        else
        {
            Destroy(lifebarObject);
            // lifebarsSet.Remove(key);
            // lifebarObject.SetActive(false);
        }
    }

    void OnSceneLoaded()
    {
        if (lifebarsSet != null)
        {
            for (int i = 0; i < lifebarsSet.Count; i++)
            {
                int key = lifebarsSet.Keys.ElementAt(i);
                Destroy(lifebarsSet[key]);
            }
        }
    }

    void HiddenLifebar()
    {
        // 订阅场景加载事件
        SceneChange.UpdateSceneEvent += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 取消订阅事件，以避免内存泄漏
        SceneChange.UpdateSceneEvent -= OnSceneLoaded;
    }

    public void DestroyEnemyLifebar(int currentEnemyNum)
    {
        // 获取 Canvas 的父物体
        Transform parent = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();

        // 如果找到 Canvas
        if (parent != null)
        {
            // 创建一个列表来存储生命条对象
            List<GameObject> lifebars = new List<GameObject>();

            // 遍历 Canvas 的所有子物体
            for (int i = 0; i < parent.childCount; i++)
            {
                // 获取子物体的 transform
                Transform childTransform = parent.GetChild(i);

                // 获取子物体的 GameObject
                GameObject childObject = childTransform.gameObject;

                // 检查子物体的标签是否为 EnemyLifebar
                if (childObject.CompareTag("EnemyLifebar"))
                {
                    // 将 EnemyLifebar 加入到生命条列表中
                    lifebars.Add(childObject);
                }
            }

            // 如果生命条列表不为空
            if (lifebars.Count > 0)
            {
                // 创建一个列表来存储要删除的生命条对象
                List<GameObject> lifebarsToRemove = new List<GameObject>();
                int reality = lifebars.Count - currentEnemyNum;
                // 遍历生命条列表
                foreach (GameObject lifebar in lifebars)
                {
                    if (reality > 0)
                    {
                        // 销毁生命条对象
                        Destroy(lifebar);

                        // 将要删除的生命条对象添加到要删除的列表中
                        lifebarsToRemove.Add(lifebar);
                    }

                    // 减少敌人数量
                    reality--;
                }
            }
        }
    }
}

