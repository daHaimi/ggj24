using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneTransitionController.Instance.TransitionToScene("IntroScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
