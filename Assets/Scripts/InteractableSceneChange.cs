using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSceneChange : Interactable
{
    public string TargetScene;

    private bool _triggered;

    /// <summary>
    /// Player can interact and trigger a scene change.
    /// </summary>
    public override void Interact()
    {
        if (_triggered)
            return;

        _triggered = true;

        SceneTransitionController.Instance.TransitionToScene(TargetScene);
    }
}
