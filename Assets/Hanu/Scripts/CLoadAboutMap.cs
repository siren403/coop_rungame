using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLoadAboutMap
{

    public CTrackTile PFStartTrack = null;
    public CTrackTile PFVerticalTrack = null;
    public CTrackTile PFHorizontalTrack = null;

    public CTrackTile PFUpLeftTrack = null;
    public CTrackTile PFLeftUpTrack = null;
    public CTrackTile PFUpRightTrack = null;
    public CTrackTile PFRightUpTrack = null;
    public CTrackTile PFEndTrack = null;

    public Dictionary<CTrackCreater.TRACKKIND, CTrackTile> TrackKind = null;




    public void LoadAboutPrefabs()
    {
       
        PFStartTrack = Resources.Load<CTrackTile>("Prefabs/PFStartTrack");

        PFVerticalTrack = Resources.Load<CTrackTile>("Prefabs/PFVerticalTrack");
        PFHorizontalTrack = Resources.Load<CTrackTile>("Prefabs/PFHorizontalTrack");

        PFUpLeftTrack = Resources.Load<CTrackTile>("Prefabs/PFUpLeftTrack");
        PFLeftUpTrack = Resources.Load<CTrackTile>("Prefabs/PFLeftUpTrack");
        PFUpRightTrack = Resources.Load<CTrackTile>("Prefabs/PFUpRightTrack");
        PFRightUpTrack = Resources.Load<CTrackTile>("Prefabs/PFRightUpTrack");

        PFEndTrack = Resources.Load<CTrackTile>("Prefabs/PFEndTrack");
        


        TrackKind = new Dictionary<CTrackCreater.TRACKKIND, CTrackTile>();

        TrackKind.Add(CTrackCreater.TRACKKIND.START, PFStartTrack);

        TrackKind.Add(CTrackCreater.TRACKKIND.VERTICAL, PFVerticalTrack);
        TrackKind.Add(CTrackCreater.TRACKKIND.HORIZONTAL, PFHorizontalTrack);

        TrackKind.Add(CTrackCreater.TRACKKIND.UPLEFT, PFUpLeftTrack);
        TrackKind.Add(CTrackCreater.TRACKKIND.LEFTUP, PFLeftUpTrack);
        TrackKind.Add(CTrackCreater.TRACKKIND.UPRIGHT, PFUpRightTrack);
        TrackKind.Add(CTrackCreater.TRACKKIND.RIGHTUP, PFRightUpTrack);
        TrackKind.Add(CTrackCreater.TRACKKIND.END, PFEndTrack);
        
    }


    public CTrackTile GetPrefab(CTrackCreater.TRACKKIND tTrackKind)
    {
        return TrackKind[tTrackKind];
    }

}
