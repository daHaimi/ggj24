using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private static string _lastScene;

    private float _cooldown = 1f;

    void Start()
    {
        AudioController.Instance.PlaySound(GlobalDataSo.Instance.SfxGameOver);
    }

    void Update()
    {
        if(_cooldown > 0)
            _cooldown -= Time.deltaTime;

        if (_cooldown <= 0 && Input.GetButtonDown("Fire1"))
        {
            ReturnToLastScene();
        }
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
