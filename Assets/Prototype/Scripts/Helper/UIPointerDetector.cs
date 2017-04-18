using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIPointerDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private Action<PointerEventData> CallOnPointerDown = null;
    private Action<PointerEventData> CallOnPointerUp = null;

    public void SetOnPointerDown(Action<PointerEventData> callBack)
    {
        CallOnPointerDown = callBack;
    }
    public void SetOnPointerUp(Action<PointerEventData> callBack)
    {
        CallOnPointerUp = callBack;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CallOnPointerDown.SafeInvoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CallOnPointerUp.SafeInvoke(eventData);
    }
}
