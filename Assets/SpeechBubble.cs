using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class SpeechBubble : MonoBehaviour
{
    public string Content;
    public float AnimationLength = 0.3f;
    public float Angle = 45;

    private Text _text;
    private Vector3 _vectorFullSize;

    void OnEnable()
    {
        _vectorFullSize = transform.localScale;

        gameObject.LeanScale(_vectorFullSize, AnimationLength)
            .setFrom(Vector3.zero)
            .setEaseOutSine();
        gameObject.LeanRotateY(Angle, AnimationLength);

    }

    void Start()
    {
        _text = GetComponentInChildren<Text>();
        _text.text = Content;
    }

    public void UpdateContent(string content)
    {
        _text.text = Content = content;

        // small bounce effect
        gameObject.LeanCancel();
        gameObject.LeanScale(_vectorFullSize, AnimationLength / 2)
            .setFrom(_vectorFullSize * 1.2f)
            .setEaseOutBounce();
    }

    public void CloseSpeechBubble()
    {
        gameObject.LeanCancel();
        gameObject.LeanScale(Vector3.zero, AnimationLength)
            .setFrom(_vectorFullSize)
            .setEaseOutSine()
            .setOnComplete(() => Destroy(gameObject));
    }
}
