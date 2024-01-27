using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : BaseSceneConsistentController
{
    #region singleton
    private static AudioController _cachedInstance;
    public static AudioController Instance
    {
        get
        {
            if (_cachedInstance == null)
                _cachedInstance = Instantiate(GlobalDataSo.Instance.AudioController);

            return _cachedInstance;
        }
    }
    #endregion

    [SerializeField]
    private AudioSource _audioSourceSfx;
    [SerializeField]
    private AudioSource _audioSourceBgm;

    /// <summary>
    /// Play a sound effect once.
    /// </summary>
    /// <param name="audioClip">Sound effect to be played</param>
    /// <param name="volume">The Volume it'll be played at</param>
    public void PlaySound(AudioClip audioClip, float volume = 1)
    {
        _audioSourceSfx.PlayOneShot(audioClip, volume);
    }

    /// <summary>
    /// Play a music track.
    /// If another one is already being played, it will be faded out.
    /// </summary>
    /// <param name="audioClip">Music track to be playe</param>
    public void PlayMusic(AudioClip audioClip)
    {
        if (!gameObject.LeanIsTweening() && _audioSourceBgm.isPlaying)
        {
            var oldVolume = _audioSourceBgm.volume;
            LeanTween.value(gameObject, oldVolume, 0, 1)
                .setOnUpdate((newVolume) =>
                {
                    // Lower volume
                    _audioSourceBgm.volume = newVolume;
                })
                .setOnComplete(() =>
                {
                    // Play new track
                    _audioSourceBgm.Stop();
                    _audioSourceBgm.volume = oldVolume;
                    PlayNewTrack(audioClip);
                });
        }
        else
            PlayNewTrack(audioClip);
    }

    private void PlayNewTrack(AudioClip audioClip)
    {
        _audioSourceBgm.clip = audioClip;
        _audioSourceBgm.Play();
    }
}
