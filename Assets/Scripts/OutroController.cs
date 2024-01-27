using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroController : MonoBehaviour
{
    public TextFadeInOut TextThankYou;
    public List<TextFadeInOut> TextsCredits;

    private bool _allowContinue;

    void Start()
    {
        Camera.main.gameObject.LeanRotateX(0, 10)
            .setOnComplete(() =>
            {
                TextThankYou.FadeIn();

                // randomize which credits are shown when
                foreach (var text in TextsCredits)
                    gameObject.LeanValue(0, 1, Random.Range(0, 1.5f))
                        .setOnComplete(() =>
                        {
                            _allowContinue = true;
                            text.FadeIn();
                        });

            })
            .setEaseInOutSine();
    }

    void Update()
    {
        if (_allowContinue && Input.anyKeyDown)
            // TODO: Implement menu scene!
            SceneTransitionController.Instance.TransitionToScene("menuScene");
    }

}
