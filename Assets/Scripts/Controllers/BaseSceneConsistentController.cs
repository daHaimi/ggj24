using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    /// <summary>
    /// An inheritable base controller class for all Controllers
    /// that need to consist over multiple scenes.
    /// </summary>
    public abstract class BaseSceneConsistentController : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
