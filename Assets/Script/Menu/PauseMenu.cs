using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject introduceBox;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = pauseMenu.GetComponent<RectTransform>();
        // 设置物体在 Canvas 中的渲染顺序
        rectTransform.SetSiblingIndex(transform.childCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && introduceBox.activeSelf)
        {
            introduceBox.SetActive(false);
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync("Menu");
    }

    public void Introduce()
    {
        if (transform.childCount > 0)
        {
            Text introduceText = introduceBox.transform.GetChild(1).GetComponent<Text>();
            if (introduceText != null)
            {
                introduceText.text = "按“WASD”上下左右移动\n按“space”跳跃\n按“C”切换背景\n按“E”释放飞镖\n按“ESC”呼出暂停菜单\n按“Shift”切换武器\n鼠标左键发射子弹\n\n按“F”交互/隐藏对话框";
                introduceBox.SetActive(true);
                Resume();
            }
            else Debug.Log("Canvas->IntroduceBox:未能成功获取Text组件");

        }
        else Debug.Log("Canvas->IntroduceBox:未发现子物体");
    }
}
