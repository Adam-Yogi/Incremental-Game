using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapArea : MonoBehaviour,IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventdata)
    {
        GameManager.instance.CollectByTap(eventdata.position, transform);
    }
}
