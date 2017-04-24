using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Inspector;

public class CLoadScene : MonoBehaviour
{
    public string TargetSceneName = string.Empty;

    [Button]
    public void LoadScene()
    {
        if (string.IsNullOrEmpty(TargetSceneName) == false)
        {
            SceneManager.LoadScene(TargetSceneName);
        }
    }
}
