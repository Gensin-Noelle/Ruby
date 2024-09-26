using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_DialogControl : MonoBehaviour
{
    public GameObject Dialog;
    // Start is called before the first frame update
    void Start()
    {
        Dialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Dialog.SetActive(true);
            Invoke(nameof(HiddenDialog),3.0f);
        }
    }

    private void HiddenDialog()
    {
        Dialog.SetActive(false);
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Dialog.SetActive(false);
        }
    }
}
