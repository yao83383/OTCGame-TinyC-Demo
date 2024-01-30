using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image Icon;
    [HideInInspector] 
    public Transform parentBeforeDrag;
    
    private TC2Block BlockInst;
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        //transform.SetAsLastSibling();
        Icon.raycastTarget = false;

        BlockInst = GetComponent<TC2Block>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = eventData.position;
        transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentBeforeDrag);
        transform.SetAsLastSibling();
        Icon.raycastTarget = true;

        BlockInst.StartMove();
    }
}
