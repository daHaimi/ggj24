using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows placing SpeechBubble Conversations from anywhere.
/// A speech bubble will be connected to a GameObject;
/// Only one conversation can be had at a time!
/// </summary>
public class SpeechBubbleController : BaseSceneConsistentController
{
    #region singleton
    private static SpeechBubbleController _cachedInstance;
    public static SpeechBubbleController Instance
    {
        get
        {
            if (_cachedInstance == null)
                _cachedInstance = Instantiate(GlobalDataSo.Instance.SpeechBubbleController);

            return _cachedInstance;
        }
    }
    #endregion

    private SpeechBubble PrefabSpeechBubble
    {
        get
        {
            return GlobalDataSo.Instance.PrefabSpeechBubble;
        }
    }

    private Dictionary<GameObject, SpeechBubble> _dictSpeechBubbles = new();

    /// <summary>
    /// Place a speech bubble above a GameObject.
    /// </summary>
    /// <param name="gameObject">Target GameObject</param>
    /// <param name="content">Speech bubble text</param>
    /// <param name="height">The height above the character</param>
    /// <param name="hardOverride">Destroy ongoing conversation if necessary and force new one</param>
    public void PlaceSpeechBubble(GameObject gameObject, string content, int height = 2, bool hardOverride = false)
    {
        // Make sure there isn't already an ongoing conversation
        // Delete ongoing conversation if override is set
        if (IsInConversation(gameObject))
        {
            if (hardOverride)
                DestroySpeechBubbleForGameObject(gameObject);
            else
            {
                _dictSpeechBubbles[gameObject].UpdateContent(content);
                return;
            }
        }

        // Instantiate new speech bubble
        var speechBubble = Instantiate(
            PrefabSpeechBubble,
            // place above gameObject
            gameObject.transform.position + new Vector3(0, height, 0),
            Quaternion.identity
        );
        speechBubble.Content = content;
        
        _dictSpeechBubbles.Add(gameObject, speechBubble);
    }

    /// <summary>
    /// Close an existing conversation, if it exists.
    /// </summary>
    /// <param name="gameObject">Target GameObject</param>
    public void CloseSpeechBubble(GameObject gameObject)
    {
        if (IsInConversation(gameObject))
            DestroySpeechBubbleForGameObject(gameObject);
    }

    private void DestroySpeechBubbleForGameObject(GameObject gameObject)
    {
        _dictSpeechBubbles[gameObject].CloseSpeechBubble();
        _dictSpeechBubbles.Remove(gameObject);
    }

    private bool IsInConversation(GameObject gameObject)
    {
        return _dictSpeechBubbles.ContainsKey(gameObject);
    }
}
