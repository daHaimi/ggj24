using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractableDestroyableWithItem : Interactable
{
    public ItemSo NecessaryItem;
    public float Distance = 3;
    public UnityEvent OnDestroy;

    private bool _interactionStarted;
    // equals time in seconds
    private float _destructionProgressMax = 3;
    private float _destructionProgress;


    void Update()
    {
        if (_interactionStarted)
        {
            if (Vector3.Distance(
                    CharController.Instance.PlayerFigure.transform.position,
                    transform.position
                ) > Distance)
            {
                _destructionProgress = 0;
                _interactionStarted = false;
            }
            else
                _destructionProgress += Time.deltaTime;

            if (_destructionProgress < _destructionProgressMax)
                UiIngameController.Instance.ShowItemUsage(NecessaryItem, _destructionProgress / _destructionProgressMax);
            else
            {
                // hide by sending "full" value 1
                UiIngameController.Instance.ShowItemUsage(NecessaryItem, 1);
                OnDestroy.Invoke();
                Destroy(gameObject);
            }
        }

    }

    public override void Interact()
    {
        if (_interactionStarted || !GameController.Instance.CheckForItem(NecessaryItem))
            return;

        _interactionStarted = true;
    }

}
