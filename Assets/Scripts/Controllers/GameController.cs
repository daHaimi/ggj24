using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
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
