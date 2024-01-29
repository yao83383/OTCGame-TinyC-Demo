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
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        //transform.SetAsLastSibling();
        Icon.raycastTarget = false;
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
    }
}
