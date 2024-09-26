using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartsHitControl : PlayerAttackMode
{
    public GameObject dartsPrefab;
    public int dartsMax;
    public float dartsCDTime;
    private int dartsDefult;
    private bool isCoolingDown = false;
    // Start is called before the first frame update
    void Start()
    {
        dartsDefult = dartsMax;
    }

    public override void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isCoolingDown)
        {
            if (dartsMax > 0)
            {
                Instantiate(dartsPrefab, transform.position, transform.rotation);
                dartsMax--;
            }
            else StartCoroutine(DelayedAction());
        }
    }

    IEnumerator DelayedAction()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(dartsCDTime);
        dartsMax = dartsDefult;
        isCoolingDown = false;
    }
}
