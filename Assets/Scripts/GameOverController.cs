using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private static string _lastScene;

    void Start()
    {
        AudioController.Instance.PlaySound(GlobalDataSo.Instance.SfxGameOver);
    }

    public static void CallGameOver()
    {
        _lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameOverScene");
    }

    public static void ReturnToLastScene()
    {
        SceneTransitionController.Instance.TransitionToScene(_lastScene);
    }
}
