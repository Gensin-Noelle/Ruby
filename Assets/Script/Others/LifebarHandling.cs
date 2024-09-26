using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifebarHandling : MonoBehaviour
{
    // Start is called before the first frame update
    public int lastSceneEnemyNum;
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("EnemyLifebar");
        if (gameObjects != null)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (lastSceneEnemyNum > 0)
                {
                    Destroy(gameObject);
                }
                lastSceneEnemyNum--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
