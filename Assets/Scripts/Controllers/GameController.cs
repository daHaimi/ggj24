using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for Base Game Logic
/// </summary>
public class GameController : BaseSceneConsistentController
{
    #region singleton
    private static GameController _cachedInstance;
    public static GameController Instance
    {
        get
        {
            if (_cachedInstance == null)
                _cachedInstance = Instantiate(GlobalDataSo.Instance.GameController);

            return _cachedInstance;
        }
    }
    #endregion

    
}
