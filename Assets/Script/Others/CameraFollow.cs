using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform traget;
    public float smoothing;

    public Vector2 minPosition;
    public Vector2 maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameController.camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
        GameController.soundEffect = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundEffect>();
    }

    private void LateUpdate() 
    {
        if(traget != null)
        {
            if(transform.position != traget.position)
            {
                Vector3 tragetPos = traget.position;
                //限制相机的活动范围
                tragetPos.x = Mathf.Clamp(tragetPos.x, minPosition.x, maxPosition.x);
                tragetPos.y = Mathf.Clamp(tragetPos.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, tragetPos, smoothing); //插值
            }
        }
    }


    public void SetCamPosLimit(Vector2 minPos, Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
