using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 endPosition;
    private Vector3 startPosition;

    Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //startPosition = transform.position;

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = eventData.position;
        transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //endPosition = transform.position;
        transform.SetParent(parentAfterDrag);
    }

}
