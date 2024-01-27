using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFadeInOut : MonoBehaviour
{
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
        SetAlpha(0);
    }

    public void FadeIn()
    {
        gameObject.LeanCancel();
        gameObject.LeanValue(0, 1, 2f)
            .setOnUpdate((float val) =>
            {
                SetAlpha(val);
            })
            .setEaseOutSine();
    }
    public void FadeOut()
    {
        gameObject.LeanCancel();
        gameObject.LeanValue(1, 0, 2f)
            .setOnUpdate((float val) =>
            {
                SetAlpha(val);
            })
            .setEaseOutSine();
    }

    private void SetAlpha(float alpha)
    {
        Color textColor = _text.color;
        textColor.a = alpha;
        _text.color = textColor;
    }

}
