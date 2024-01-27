using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip MusicTrack;

    void Start()
    {
        AudioController.Instance.PlayMusic(MusicTrack);
    }
}
