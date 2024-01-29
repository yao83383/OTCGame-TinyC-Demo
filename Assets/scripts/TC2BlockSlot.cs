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
	//�ؿ�����
	[HideInInspector]
	public Vector2Int BlockLocation;

    //����ؿ��Ƿ��ѷ�����Ʒ
	[HideInInspector]
    public TC2Block BlockInst;

    //����ؿ��Ƿ�ɷ�����Ʒ
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


    //ɾ��Slotʱ �ܱ�4��Slots����Ҫ����ˢ��һ��
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

    //����Slotʱ �ܱ�4��Slots����Ҫ����ˢ��һ��
    public void RefreshSlotEdgeStateOnCreate()
    {
		RefreshSlotEdgesState();
	}

    //�ڴ�����Slotʱ��Ҫˢ���ܱ�Slot
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
		//����������λ��
		DragableItem draggableItem = InDragItem.GetComponent<DragableItem>();
		this.BlockInst.transform.SetParent(draggableItem.parentBeforeDrag);
		draggableItem.parentBeforeDrag = BackGround.transform;


		//����Block�洢������Slotʵ��
		TC2BlockSlot TempSlot = InTargetItem.BlockSlotInst;
		InTargetItem.BlockSlotInst = InDragItem.BlockSlotInst;
		InDragItem.BlockSlotInst = TempSlot;

        //����Slot��ӵ�е�Blockʵ��
        TC2Block TempBlock = InDragItem;
		InDragItem = InTargetItem;
		InTargetItem = TempBlock;

	}
}
