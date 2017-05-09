#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;

public class PlacementEditor : MonoBehaviour
{
   
    [Button]
    public void Comment()
    {
        Debug.Log("배치 오브젝트 생성,조정 후 최상위 트랙오브젝트르 Apply하지 않으면 반영 안됨.");
    }

    [Button]
    public void Boost()
    {
        GameObject tPF = Resources.Load<GameObject>("PlacementObject/PFItem");

        GameObject tInstance = Instantiate<GameObject>(tPF, Vector3.zero, Quaternion.identity);
        tInstance.transform.SetParent(this.transform);
        tInstance.transform.localPosition = Vector3.zero;

    }
    [Button]
    public void Obstacle()
    {
        GameObject tPF = Resources.Load<GameObject>("PlacementObject/PFObstacle");

        GameObject tInstance = Instantiate<GameObject>(tPF, Vector3.zero, Quaternion.identity);
        tInstance.transform.SetParent(this.transform);
        tInstance.transform.localPosition = Vector3.zero;

    }
    [Button]
    public void Coin()
    {
        GameObject tPF = Resources.Load<GameObject>("PlacementObject/PFCoin");

        GameObject tInstance = Instantiate<GameObject>(tPF, Vector3.zero, Quaternion.identity);
        tInstance.transform.SetParent(this.transform);
        tInstance.transform.localPosition = Vector3.zero;

    }
    [Button]
    public void Dash()
    {
        GameObject tPF = Resources.Load<GameObject>("PlacementObject/PFDash");

        GameObject tInstance = Instantiate<GameObject>(tPF, Vector3.zero, Quaternion.identity);
        tInstance.transform.SetParent(this.transform);
        tInstance.transform.localPosition = Vector3.zero;
    }

    [Button]
    public void Heal()
    {
        GameObject tPF = Resources.Load<GameObject>("PlacementObject/PFHeal");

        GameObject tInstance = Instantiate<GameObject>(tPF, Vector3.zero, Quaternion.identity);
        tInstance.transform.SetParent(this.transform);
        tInstance.transform.localPosition = Vector3.zero;
    }

}

#endif