using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCoinObject : CPlacementObject
{
    public GameObject InstBody = null;
    public GameObject InstParticle = null;
    public GameObject[] Inst = null;
    /*
    private void Awake()
    {
        Inst = this.gameObject.GetComponentsInChildren<GameObject>();
        foreach(GameObject tInst in Inst)
        {
            Debug.Log("asdass");
        }
        if (Inst[0] == null)
        {
            Debug.Log("null");
        }
        else
        {
            Debug.Log("asdasd");
            InstBody = Inst[0];
            InstParticle = Inst[1];
        }
    }
    
        
      */ 
    

    protected override void OnPlayerEnter(CPlayer tPlayer)
    {
        tPlayer.ScenePlayGame.OnIncrementCoin();
        InstBody.SetActive(false);
        InstParticle.SetActive(true);
        StartCoroutine(MoveTo(tPlayer));
        Invoke("ObjectDestroy", 2.0f);
    }


    public IEnumerator MoveTo(CPlayer tPlayer)
    {
        for (;;)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, tPlayer.transform.position, 1000.0f);

            yield return new WaitForSeconds(0.0f);
        }
    }

    private void ObjectDestroy()
    {
        StopCoroutine("MoveTo");
        Destroy(this.gameObject);
    }

    protected override void OnPlayerTriggerEnter(CPlayer tPlayer)
    {
        if(tPlayer.IsMagnet == true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, tPlayer.transform.position, mMagnetDistanceDelta);
        } 
    }
}
