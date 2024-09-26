using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuAnim : MonoBehaviour
{
    public Animator continueAmia;
    public Animator quitAmia;
    public Animator introduceAmia;

    public void ContinueEnter()
    {
        continueAmia.SetBool("Continue", true);
    }

    public void ContinueExit()
    {
        continueAmia.SetBool("Continue", false);
    }

    public void QuitEnter()
    {
        quitAmia.SetBool("Quit", true);
    }

    public void QuitExit()
    {
        quitAmia.SetBool("Quit", false);
    }

    public void IntroduceEnter()
    {
        introduceAmia.SetBool("Introduce", true);
    }

    public void IntroduceExit()
    {
        introduceAmia.SetBool("Introduce", false);
    }

}
