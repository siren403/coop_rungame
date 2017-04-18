using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefabLoader
{
    public abstract class PrefabLoader
    {
        public abstract void Load();
        protected virtual void PrefabLoad<T>(ref T tVariable,string tPath) where T : MonoBehaviour
        {
            if(tVariable == null)
            {
                tVariable = Resources.Load<T>(tPath);
            }
        }
    }
}
