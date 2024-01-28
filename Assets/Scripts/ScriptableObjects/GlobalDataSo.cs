using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// The GlobalDataSo allows access to Data like Prefabs, AudioClips etc. from anywhere.
/// For this to work, a GlobalData metadata file has to be created in the Assets/Resources folder.
/// There, it is possible to drag the respective Prefabs etc. into the fields provided
/// in the GlobalDataSo class.
/// </summary>
[CreateAssetMenu(fileName = "GlobalData", menuName = "ScriptableObjects/GlobalDataSo", order = 1)]
public class GlobalDataSo : ScriptableObject
{
    #region singleton
    private static GlobalDataSo _cachedInstance;
    public static GlobalDataSo Instance
    {
        get
        {
            if (_cachedInstance == null)
                _cachedInstance = Resources.Load<GlobalDataSo>("GlobalData");

            return _cachedInstance;
        }
    }
    #endregion

    [Header("Controllers")]
    public GameController GameController;
    public SceneTransitionController SceneTransitionController;
    public AudioController AudioController;
    public SpeechBubbleController SpeechBubbleController;

    [Header("Prefabs")]
    public SpeechBubble PrefabSpeechBubble;
    public PanelItem PrefabPanelItem;
    public List<GameObject> SpawnableCars;

    [Header("Sounds")]
    public AudioClip SfxSceneTransition;
    public AudioClip SfxPickupItem;
    public AudioClip SfxDropItem;
    public AudioClip SfxMarkerBeep;
    public AudioClip SfxRefillLaughMeter;
    public AudioClip SfxGameOver;
    public List<AudioClip> SfxHumans;

    // Global static vars
    public const string TAG_INTERACTABLE = "Interactable";
}
