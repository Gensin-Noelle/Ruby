// using System.Numerics;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public Transform[] movePos;

    private int i;
    private float defaultTime;
    private Transform playerDefaultTransform;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        defaultTime = waitTime;
        playerDefaultTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if(waitTime < 0.0f)
            {
                if(i == 0) i = 1;
                else i = 0;
                waitTime = defaultTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = playerDefaultTransform;
        }
    }
}
