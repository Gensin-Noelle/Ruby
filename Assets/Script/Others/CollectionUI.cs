using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectionUI : MonoBehaviour
{
    public int startBerryQuantity;
    public Text StrawberryText;

    public static int currentBerryQuantity;
    // Start is called before the first frame update
    void Start()
    {
        // 获取当前加载的场景
        Scene currentScene = SceneManager.GetActiveScene();

        // 获取当前场景在Build Settings中的索引
        int sceneIndex = currentScene.buildIndex;
        int strawberry = PlayerPrefs.GetInt("Strawberry", 0);
        if (strawberry != 0 && sceneIndex == 2) currentBerryQuantity = strawberry;
        else currentBerryQuantity = startBerryQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        StrawberryText.text = currentBerryQuantity.ToString();
    }
}
