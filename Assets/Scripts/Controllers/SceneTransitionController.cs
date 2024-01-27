using Assets.Scripts.Controllers;
using EasyTransition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controller for Scene Transitions
/// </summary>
public class SceneTransitionController : BaseSceneConsistentController
{
    #region singleton
    private static SceneTransitionController _cachedInstance;
    public static SceneTransitionController Instance
    {
        get
        {
            if (_cachedInstance == null)
                _cachedInstance = Instantiate(GlobalDataSo.Instance.SceneTransitionController);

            return _cachedInstance;
        }
    }
    #endregion

    [Header("Transition Settings")]
    public TransitionSettings DefaultTransitionSettings;

    /// <summary>
    /// Transition to another scene with a transition effect.
    /// </summary>
    /// <param name="sceneName">Target scene to transition to</param>
    public void TransitionToScene(string sceneName)
    {
        TransitionManager.Instance().Transition(sceneName, DefaultTransitionSettings, 0);
        AudioController.Instance.PlaySound(GlobalDataSo.Instance.SfxSceneTransition);
    }

    /// <summary>
    /// Plays a transition effect without transitioning to another scene.
    /// </summary>
    /// <param name="cutPointCallback">Gets called in the middle of the transition, aka when the screen is black.</param>
    public void Transition(Action cutPointCallback = null)
    {
        TransitionManager.Instance().Transition(DefaultTransitionSettings, 0);
        UnityAction cutPointFunc = null;

        if(cutPointCallback != null)
        {
            cutPointFunc = () => {
                cutPointCallback.Invoke();
                TransitionManager.Instance().onTransitionCutPointReached -= cutPointFunc;
            };
            TransitionManager.Instance().onTransitionCutPointReached += cutPointFunc;
        }

        AudioController.Instance.PlaySound(GlobalDataSo.Instance.SfxSceneTransition);
    }
}
