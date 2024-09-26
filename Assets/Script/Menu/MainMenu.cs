using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject introduceBox;
    public Slider slider;
    public Text progressText;

    private void Start()
    {

    }
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.F) && introduceBox != null)
        // {
        //     introduceBox.SetActive(false);
        // }
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("DemoScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int screenIndex)
    {
        StartCoroutine(AsyncLoadLevel(screenIndex));
    }

    IEnumerator AsyncLoadLevel(int screenIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(screenIndex);
        if (introduceBox != null) introduceBox.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            slider.value = progress;
            progressText.text = Mathf.FloorToInt(progress * 100f).ToString() + "%";
            yield return null;
        }

    }

    public void PointerEnterEvent()
    { }

    public void PointerExitEvent()
    { }
}
