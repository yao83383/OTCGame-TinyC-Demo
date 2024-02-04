using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TC2SortOrder
{
    BlockSlotNormal = 1,
    BlockSlotOnDrag = 2
}

[RequireComponent(typeof(Image))]
public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Image Icon;
    [HideInInspector] 
    public Transform parentBeforeDrag;
    
    private TC2Block BlockInst;

    public void Awake()
    {
        Icon = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        //transform.SetAsLastSibling();
        Icon.raycastTarget = false;

        BlockInst = GetComponent<TC2Block>();
        BlockInst.GetComponent<Canvas>().sortingOrder = (int)TC2SortOrder.BlockSlotOnDrag;
        BlockInst.MoveAnimator.SetBool("Move", true);

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
        BlockInst.GetComponent<Canvas>().sortingOrder = (int)TC2SortOrder.BlockSlotNormal;
        BlockInst.MoveAnimator.SetBool("Move", false);
    }
}
