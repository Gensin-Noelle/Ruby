using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private PauseMenu pause;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SetOrder), 0.1f);
        pause = transform.parent.GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        //按下C切换背景，因为Setting渲染的层级最高，在切换背景时会暴露，所以在切换背景是隐藏
        if (Input.GetKeyDown(KeyCode.C) && Time.timeScale != 0)
        {
            gameObject.SetActive(false);
            Invoke(nameof(SetVisible), 1.2f);
        }
    }

    public void ClickEvent()
    {
        if (PauseMenu.GameIsPaused) pause.Resume();
        else pause.Pause();
    }

    private void SetOrder()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.SetSiblingIndex(GetCanvasChildNum());
    }

    private void SetVisible()
    {
        gameObject.SetActive(true);
    }

    private int GetCanvasChildNum()
    {
        // 获取物体的父物体
        Transform parentTransform = gameObject.transform.parent;

        if (parentTransform != null)
        {
            // 返回父物体的子物体数量
            return parentTransform.childCount;
        }
        else
        {
            // 如果父物体为空，则返回 0
            return 0;
        }
    }

    private void SetInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //订阅场景加载事件，当场景加载时，将设置图标设置为不可见
        SceneChange.UpdateSceneEvent += SetInvisible;
    }

    private void OnDisable()
    {
        SceneChange.UpdateSceneEvent -= SetInvisible;
    }
}
