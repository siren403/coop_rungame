using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;
using PrefabLoader;

public class CScenePlayGame : MonoBehaviour
{
    public enum StageType { None = 0, Air, Desert, Snow, Sea }

    private PlayGamePrefabs mPlayGamePrefabs = new PlayGamePrefabs();

    [ReadOnly]
    public CPlayer InstPlayer = null;
    [ReadOnly]
    public CTargetCamera InstTargetCamera = null;
    public CTrackFactory InstTrackCreator = null;

    private void Awake()
    {
        CreateBasicPrefabs();
        CHanMapDataMgr.GetInst().CreateHan();
        InstTrackCreator.CreateTrack();
    }


    [Button]
    public void CreateBasicPrefabs()
    {
        mPlayGamePrefabs.Load();
        if(InstPlayer == null)
        {
            InstPlayer = Instantiate(mPlayGamePrefabs.PFPlayer, Vector3.zero, Quaternion.identity);
        }
        if(InstTargetCamera == null)
        {
            InstTargetCamera = Instantiate(mPlayGamePrefabs.PFTargetCamera, Vector3.zero, Quaternion.identity);
            InstTargetCamera.SetTarget(InstPlayer.gameObject);
            InstTargetCamera.UpdatePosition();
        }
    }
}
