using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
    public abstract class ResourceLoader
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
