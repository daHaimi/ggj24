using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows executing transitions from a component.
/// </summary>
public class TransitionExecutor : MonoBehaviour
{
    public UnityEvent OnCutPointReached = new();

    /// <summary>
    /// Plays the transition.
    /// </summary>
    /// <param name="sceneName">SceneName, or null if scene shouldn't be changed</param>
    public void PlayTransition(string sceneName = null)
    {
        if (string.IsNullOrEmpty(sceneName))
            SceneTransitionController.Instance.Transition(
            () =>
                {
                    OnCutPointReached.Invoke();
                });
        else
            SceneTransitionController.Instance.TransitionToScene(sceneName);
    }
}
