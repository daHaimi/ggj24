using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


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

    public GameController GameController;


    // Global static vars
    public const string TAG_INTERACTABLE = "Interactable";
}
