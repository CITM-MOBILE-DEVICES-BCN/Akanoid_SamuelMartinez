using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        continueButton.SetActive(false);
        if(GameManager.instance.facade.GetSavedLevel() > 0)
        {
            continueButton.SetActive(true);
        }
    }

    
}
