using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject strawberry;
    public GameObject tips;
    private bool canOpen;
    private bool isOpen;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canOpen && !isOpen)
            {
                animator.SetTrigger("isOpen");
                isOpen = true;
                Instantiate(strawberry, transform.position, Quaternion.identity);
                Invoke(nameof(OnDestroy), 1.0f);
            }
            if(tips.activeSelf)
            {
                tips.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
            tips.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
            tips.SetActive(false);
        }
    }
}
