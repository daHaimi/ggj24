using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(
                Transform.FindFirstObjectByType<Button>().gameObject
            );
    }

    public void StartGame()
    {
        SceneTransitionController.Instance.TransitionToScene("IntroScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
