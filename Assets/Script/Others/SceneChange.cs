using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject screenChange;
    private Animator screenAnim;

    public delegate void OnSceneChange();
    public static event OnSceneChange UpdateSceneEvent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        screenAnim = screenChange.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //判断碰撞物体的标签是否为玩家，类型是否为胶囊体
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            // 设置生命值和草莓数量,继承到下个场景
            PlayerPrefs.SetInt("PlayerHealth", HealthBar.HealthCurrent);
            PlayerPrefs.SetInt("Strawberry", CollectionUI.currentBerryQuantity);

            if (screenAnim.GetBool("isChange")) screenAnim.SetBool("isChange", false);
            else screenAnim.SetBool("isChange", true);
            UpdateSceneEvent?.Invoke();
            Invoke(nameof(DelayLoadScene), 0.5f);
        }
    }

    private void DelayLoadScene()
    {
        SceneManager.LoadSceneAsync("Scene1");
    }
}
