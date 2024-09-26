using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignBox : MonoBehaviour
{
    public GameObject dialog;
    public GameObject tips;
    public Text dialogTextBox;
    private string dialogText;
    private bool isPlayerInSign;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        dialogText = "我只是一个盒子,用来测试对话框的功能QAQ";
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isPlayerInSign)
        {
            if (isActive)
            {
                dialog.SetActive(false);
                isActive = false;
            }
            else
            {
                dialog.SetActive(true);
                isActive = true;
            }
            if(tips.activeSelf)
            {
                tips.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = true;
            dialogTextBox.text = dialogText;
            tips.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = false;
            dialog.SetActive(false);
            tips.SetActive(false);
        }
    }
}


