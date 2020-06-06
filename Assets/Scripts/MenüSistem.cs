using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenüSistem : MonoBehaviour
{
  

    public void Oyna()
    {
        Application.LoadLevel("level");
    }


    public void TekrarOyna()
    {
        Application.LoadLevel("level");
    }

    public void MenuyeDon()
    {
        Application.LoadLevel("AnaMenu");
    }

    public void Cik()
    {
        Application.Quit();
    }

}

