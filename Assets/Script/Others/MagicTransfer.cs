using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicTransfer : MonoBehaviour
{
    public Transform backMagic;
    public GameObject tipsPrefab;
    private bool isMagicCircle;
    private Transform playerTransform;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        counter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        EnterDoor();
    }

    void EnterDoor()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (tipsPrefab.activeSelf) tipsPrefab.SetActive(false);
            if (isMagicCircle)
            {
                playerTransform.position = backMagic.position;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isMagicCircle = true;
            if (counter > 0)
            {
                DisplayTips();
                counter--;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isMagicCircle = false;
        }
        if (tipsPrefab != null && tipsPrefab.activeSelf)
        {
            tipsPrefab.SetActive(false);
        }

    }

    private void DisplayTips()
    {
        tipsPrefab.SetActive(true);
        Text tipsText = tipsPrefab.transform.Find("DialogBoxText").GetComponent<Text>();
        if (tipsText != null) tipsText.text = "\n\n\n按'F'进行传送";
        else Debug.Log("MagicTransfer:未能获取Text组件,请检查游戏物体上是否有该组件");
    }
}
