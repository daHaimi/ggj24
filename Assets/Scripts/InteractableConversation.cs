using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Simple Interactable that invokes a Unity Event.
/// </summary>
public class InteractableConversation : Interactable
{
    public string[] contentLines = { "Hello!" };
    public UnityEvent OnConversationEnd = new();
    public float CloseOnDistance = 3;

    private bool _conversationTriggered;
    private int currentLine = 0;

    // hacky way to do a timeout :P
    private float _nextContentLineTimeout;

    public override void Interact()
    {
        if (_conversationTriggered)
            return;

        _conversationTriggered = true;

        NextContentLine();
    }

    void Update()
    {
        if (_nextContentLineTimeout > 0)
            _nextContentLineTimeout -= Time.deltaTime;

        if (_conversationTriggered)
        {
            if (Vector3.Distance(
                CharController.Instance.PlayerFigure.position, transform.position)
                > CloseOnDistance)
            {
                // set to end line = make it close
                currentLine = contentLines.Length;
                NextContentLine();
            }

            if (CharController.Instance.Input.Interact)
                NextContentLine();
        }
    }

    private void NextContentLine()
    {
        if (_nextContentLineTimeout > 0)
            return;
        else
            _nextContentLineTimeout = 0.25f;

        if (currentLine >= contentLines.Length)
        {
            currentLine = 0;
            OnConversationEnd.Invoke();
            _conversationTriggered = false;
            SpeechBubbleController.Instance.CloseSpeechBubble(gameObject);
            return;
        }

        SpeechBubbleController.Instance
            .PlaceSpeechBubble(gameObject, contentLines[currentLine]);

        currentLine++;
    }

}
