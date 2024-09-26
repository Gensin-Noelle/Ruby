using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    public Animator playAnim;
    public Animator quitAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPointerEnter()
    {
        playAnim.SetBool("Enter", true);
    }
     public void PlayPointerExit()
    {
        playAnim.SetBool("Enter", false);
    }

       public void QuitPointerEnter()
    {
        quitAnim.SetBool("Exit", true);
    }

       public void QuitPointerExit()
    {
        quitAnim.SetBool("Exit", false);
    }
    
}
