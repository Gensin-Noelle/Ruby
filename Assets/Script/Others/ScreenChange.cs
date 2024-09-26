using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenChange : MonoBehaviour
{
    public GameObject img1;
    public GameObject img2;
    public float time;
    private Animator anim;
    private bool change;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        img1.SetActive(true);
        img2.SetActive(false);
        change = true;
        RectTransform rectTransform = GetComponent<RectTransform>();
        // 设置物体在 Canvas 中的渲染顺序
        rectTransform.SetSiblingIndex(GetCanvasChildNum() - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (change)
            {
                anim.SetBool("isChange", true);
                Invoke(nameof(ChangeImage), time);
            }
            else
            {
                anim.SetBool("isChange", false);
                Invoke(nameof(ChangeImage), time);
            }

        }
    }

    void ChangeImage()
    {
        if (change)
        {
            img1.SetActive(false);
            img2.SetActive(true);
            change = false;
        }
        else
        {
            img1.SetActive(true);
            img2.SetActive(false);
            change = true;
        }

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
}
