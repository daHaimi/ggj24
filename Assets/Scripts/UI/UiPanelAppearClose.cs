using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPanelAppearClose : MonoBehaviour
{
    private float _rectPosX;

    void Awake()
    {
        _rectPosX = GetComponent<RectTransform>().localPosition.x;
    }

    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        ShowPanel();
    }

    public void ShowPanel()
    {
        gameObject.LeanCancel();
        gameObject.LeanScale(Vector3.one, 0.5f)
            .setEaseInCubic();
        gameObject.LeanMoveLocalX(_rectPosX, 0.5f)
            .setFrom(_rectPosX + 1024)
            .setEaseOutSine();
    }

    public void ClosePanel()
    {
        gameObject.LeanCancel();
        gameObject.LeanScale(Vector3.zero, 0.5f)
            .setOnComplete(() => gameObject.SetActive(false))
            .setEaseOutSine();
    }


}
