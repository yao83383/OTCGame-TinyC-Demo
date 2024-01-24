using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 endPosition;
    private Vector3 startPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = eventData.position;
        transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endPosition = transform.position;
    }

}
