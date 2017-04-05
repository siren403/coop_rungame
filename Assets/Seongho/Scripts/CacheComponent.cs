using UnityEngine;

public class CacheComponent<T> where T : Component
{
    private T mComponent = null;

    public CacheComponent(MonoBehaviour target)
    {
        mComponent = target.GetComponent<T>();
    }

    public T Get()
    {
        return mComponent;
    }


}
