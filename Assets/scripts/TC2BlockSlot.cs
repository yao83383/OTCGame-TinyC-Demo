using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TC2BlockSlot : MonoBehaviour, IDropHandler
{
	
	public enum NearDirect
	{
		Left = 1,
		Right = 2,
		Up = 3,
		Down = 4
	}
	//Check NearBlock
	[HideInInspector]
	private bool LeftAvailable;
	[HideInInspector]
	private bool RightAvailable;
	[HideInInspector]
	private bool UpAvailable;
	[HideInInspector]
	private bool DownAvailable;
	//Edge
	public Image LeftEdge;
	public Image RightEdge;
	public Image UpEdge;
	public Image DownEdge;
	public Image BackGround;

	public TC2World WorldRef;
	//地块坐标
	[HideInInspector]
	public Vector2Int BlockLocation;

    //代表地块是否已放置物品
	[HideInInspector]
    public TC2Block BlockInst;

    //代表地块是否可放置物品
    [HideInInspector]
	public bool IsAvailable;
	
	private void OnDestroy()
	{
		WorldRef.BlockSlots.Remove(BlockLocation);
		RefreshSlotEdgeStateOnDestory();
	}

    public void Awake()
    {
        BlockLocation.x = transform.GetSiblingIndex() / 10;
        BlockLocation.y = transform.GetSiblingIndex() % 10;
		WorldRef = transform.parent.GetComponent<TC2World>();
		WorldRef.BlockSlots.Add(BlockLocation, this);

		//BlockInst = null;
		//IsAvailable = true;

		RefreshSlotEdgeStateOnCreate();
	}

	public TC2BlockSlot GetSlotByDirect(Vector2Int InBlockLocation, NearDirect InDirectEnum)
	{
        TC2BlockSlot TempSlot;
        switch (InDirectEnum)
        {
            case NearDirect.Left:
                {
                    WorldRef.BlockSlots.TryGetValue(new Vector2(InBlockLocation.x, InBlockLocation.y - 1), out TempSlot);
                    break;
                }
            case NearDirect.Right:
                {
                    WorldRef.BlockSlots.TryGetValue(new Vector2(InBlockLocation.x + 1, InBlockLocation.y), out TempSlot);
                    break;
                }
            case NearDirect.Up:
                {

                    WorldRef.BlockSlots.TryGetValue(new Vector2(InBlockLocation.x, InBlockLocation.y + 1), out TempSlot);
                    break;
                }
            default:
                {
                    WorldRef.BlockSlots.TryGetValue(new Vector2(InBlockLocation.x, InBlockLocation.y - 1), out TempSlot);
                    break;
                }
        }
		return TempSlot;
    }


    //删除Slot时 周边4个Slots都需要各自刷新一次
    public void RefreshSlotEdgeStateOnDestory()
	{
		if (LeftAvailable)
		{
			RefreshSingleDirect(NearDirect.Left, ref LeftAvailable, LeftEdge);
		}
        if (RightAvailable)
        {
			RefreshSingleDirect(NearDirect.Down, ref DownAvailable, DownEdge);
		}
        if (UpAvailable)
        {
			RefreshSingleDirect(NearDirect.Up, ref UpAvailable, UpEdge);
		}
        if (DownAvailable)
        {
			RefreshSingleDirect(NearDirect.Right, ref RightAvailable, RightEdge);
		}
    }

    //创建Slot时 周边4个Slots都需要各自刷新一次
    public void RefreshSlotEdgeStateOnCreate()
    {
		RefreshSlotEdgesState();
	}

    //在创建新Slot时需要刷新周边Slot
    public void RefreshSlotEdgesState()
    {
		RefreshSingleDirect(NearDirect.Up,		ref UpAvailable,	UpEdge);
		RefreshSingleDirect(NearDirect.Left,	ref LeftAvailable,	LeftEdge);
		RefreshSingleDirect(NearDirect.Down,	ref DownAvailable,	DownEdge);
		RefreshSingleDirect(NearDirect.Right,	ref RightAvailable,	RightEdge);
    }

	private void RefreshSingleDirect(NearDirect InDirectEnum, ref bool IsAvailable, Image InImage)
	{
        if (CheckAvailableByDirect(InDirectEnum))
        {
            DownEdge.color = new Color(InImage.color.r, InImage.color.g, InImage.color.b, 1);
			IsAvailable = true;
		}
        else
        {
            DownEdge.color = new Color(InImage.color.r, InImage.color.g, InImage.color.b, 0);
			IsAvailable = false;
        }
    }

    private bool CheckAvailableByDirect(NearDirect InDirectEnum)
	{
		bool IsAvaiable = false;
		TC2BlockSlot TempSlot;
		TempSlot = GetSlotByDirect(BlockLocation, InDirectEnum);
		if (TempSlot)
        {
			IsAvaiable = TempSlot.IsAvailable;
        }
        return IsAvaiable;
	}

    public void OnDrop(PointerEventData eventData)
    {
		GameObject dropped = eventData.pointerDrag;

		TC2Block DropedBlock = dropped.GetComponent<TC2Block>();

		SwitchBlockByBlock(ref DropedBlock, ref this.BlockInst);
	}

	public void SwitchBlockByBlock(ref TC2Block InDragItem, ref TC2Block InTargetItem)
	{
		//交换场景中位置
		DragableItem draggableItem = InDragItem.GetComponent<DragableItem>();
		this.BlockInst.transform.SetParent(draggableItem.parentBeforeDrag);
		draggableItem.parentBeforeDrag = BackGround.transform;


		//交换Block存储的所属Slot实例
		TC2BlockSlot TempSlot = InTargetItem.BlockSlotInst;
		InTargetItem.BlockSlotInst = InDragItem.BlockSlotInst;
		InDragItem.BlockSlotInst = TempSlot;

        //交换Slot所拥有的Block实例
        TC2Block TempBlock = InDragItem;
		InDragItem = InTargetItem;
		InTargetItem = TempBlock;

	}
}
