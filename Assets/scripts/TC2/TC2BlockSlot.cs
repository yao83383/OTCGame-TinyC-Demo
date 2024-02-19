using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TC2Sound))]
public class TC2BlockSlot : MonoBehaviour, IDropHandler
{
	//用于放置填表出现重复的情况，初始化BUG；
	[HideInInspector]
	public bool IsInited = false;
	
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

	[HideInInspector]
	public List<TC2Block> sameBlocks = new List<TC2Block>();
	//地块坐标
	//[HideInInspector]
	public Vector2Int BlockLocation;

    //代表地块是否已放置物品
	//[HideInInspector]
    public TC2Block BlockInst;

    //代表地块是否可放置物品
    [HideInInspector]
	public bool IsAvailable;
	
	private void OnDestroy()
	{
		WorldRef.BlockSlots.Remove(BlockLocation);
		RefreshSlotEdgeStateOnDestory();
	}

	public void InitTC2BlockSlot(TC2World InWorld, int InX, int InY, bool InIsAvailable)
	{
		WorldRef = InWorld;
		BlockLocation = new Vector2Int(InX, InY);
		IsAvailable = InIsAvailable;
	}

	public void Awake()
	{ }

	public void RegisterBlockSlotToGrid()
    {
        //BlockLocation.x = transform.GetSiblingIndex() / 10;
        //BlockLocation.y = transform.GetSiblingIndex() % 10;
		WorldRef = transform.parent.GetComponent<TC2World>();
		WorldRef.BlockSlots.Add(BlockLocation, this);

		//BlockInst = null;
		//IsAvailable = true;

		RefreshSlotEdgeStateOnCreate();
	}

	public TC2BlockSlot GetSlotByLocation(Vector2Int InBlockLocation)
	{
		TC2BlockSlot TempSlot;
		WorldRef.BlockSlots.TryGetValue(new Vector2Int(InBlockLocation.x, InBlockLocation.y), out TempSlot);
		return TempSlot;
	}

	public TC2BlockSlot GetSlotByDirect(Vector2Int InBlockLocation, NearDirect InDirectEnum)
	{
        TC2BlockSlot TempSlot;
        switch (InDirectEnum)
        {
            case NearDirect.Left:
                {
                    WorldRef.BlockSlots.TryGetValue(new Vector2Int(InBlockLocation.x, InBlockLocation.y - 1), out TempSlot);
                    break;
                }
            case NearDirect.Right:
                {
                    WorldRef.BlockSlots.TryGetValue(new Vector2Int(InBlockLocation.x, InBlockLocation.y + 1), out TempSlot);
                    break;
                }
            case NearDirect.Up:
                {

                    WorldRef.BlockSlots.TryGetValue(new Vector2Int(InBlockLocation.x - 1, InBlockLocation.y), out TempSlot);
                    break;
                }
            default:
                {
                    WorldRef.BlockSlots.TryGetValue(new Vector2Int(InBlockLocation.x + 1, InBlockLocation.y), out TempSlot);
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

		if (SwitchBlockByBlock(ref DropedBlock, ref this.BlockInst))
		{
			if (DropedBlock)
			{ 
				DropedBlock.BlockSlotInst.CheckMatch();
			}
			CheckMatch();
		}
	}

	public bool SwitchBlockByBlock(ref TC2Block InDragItem, ref TC2Block InTargetItem)
	{
		DragableItem TempdraggableItem = InDragItem.GetComponent<DragableItem>();
		if (InTargetItem == null)
		{
			TempdraggableItem.parentBeforeDrag = BackGround.transform;
			InDragItem.BlockSlotInst.BlockInst = null;
			InDragItem.BlockSlotInst = this;
			//return true;
		}
		else
		{
            if (InDragItem == InTargetItem)
            {
                Debug.Log("slot no need to switch");
                return false;
            }
            //交换场景中位置
            DragableItem draggableItem = InDragItem.GetComponent<DragableItem>();
            this.BlockInst.transform.SetParent(draggableItem.parentBeforeDrag);
            draggableItem.parentBeforeDrag = BackGround.transform;

            //交换Block存储的所属Slot实例
            TC2BlockSlot TempSlot = InTargetItem.BlockSlotInst;
            InTargetItem.BlockSlotInst = InDragItem.BlockSlotInst;
            InDragItem.BlockSlotInst = TempSlot;
        }
		
        //交换Slot所拥有的Block实例
        TC2Block TempBlock = InDragItem;
		InDragItem = InTargetItem;
		InTargetItem = TempBlock;

		return true;
	}

	public int CheckMatch()
	{
		if (!this.BlockInst) return 0;
		sameBlocks.Clear();
		sameBlocks.Add(this.BlockInst);
		if (sameBlocks[0].IsMatchable)
		{
			Vector2Int StartLeftLocation = sameBlocks[0].BlockSlotInst.BlockLocation;
			StartLeftLocation = new Vector2Int(StartLeftLocation.x, StartLeftLocation.y - 1);
			while (CheckToEndByDirect(StartLeftLocation, NearDirect.Left))
			{
				StartLeftLocation = new Vector2Int(StartLeftLocation.x, StartLeftLocation.y - 1);
			}

            Vector2Int StartRightLocation = sameBlocks[0].BlockSlotInst.BlockLocation;
			StartRightLocation = new Vector2Int(StartRightLocation.x, StartRightLocation.y + 1);
            while (CheckToEndByDirect(StartRightLocation, NearDirect.Right))
            {
				StartRightLocation = new Vector2Int(StartRightLocation.x, StartRightLocation.y + 1);
            }

			for(int blockIndex = sameBlocks.Count - 1; blockIndex >= 0; --blockIndex)
			{
                Vector2Int StartUpLocation = sameBlocks[0].BlockSlotInst.BlockLocation;
				StartUpLocation = new Vector2Int(StartUpLocation.x - 1, StartUpLocation.y);
                while (CheckToEndByDirect(StartUpLocation, NearDirect.Up))
                {
					StartUpLocation = new Vector2Int(StartUpLocation.x - 1, StartUpLocation.y);
                }

                Vector2Int StartDownLocation = sameBlocks[0].BlockSlotInst.BlockLocation;
				StartDownLocation = new Vector2Int(StartUpLocation.x + 1, StartUpLocation.y);
                while (CheckToEndByDirect(StartDownLocation, NearDirect.Down))
                {
					StartDownLocation = new Vector2Int(StartDownLocation.x + 1, StartDownLocation.y);
                }
            }

			if (sameBlocks.Count > 3)
			{
				Debug.Log("Match success, number is " + sameBlocks.Count);
			}
			else 
			{
				Debug.Log("Match failed, number is " + sameBlocks.Count);
			}
        }
		return sameBlocks.Count;
	}
	private bool CheckToEndByDirect(Vector2Int InBlockLoc, NearDirect InDirectEnum)
	{
		if (sameBlocks[0].IsMatchable)
		{
			TC2BlockSlot TempSlot = GetSlotByLocation(InBlockLoc);
			if (!TempSlot || !TempSlot.BlockInst) return false;
			if (TempSlot.BlockInst.kind == sameBlocks[0].kind)
			{
				if (!sameBlocks.Contains(TempSlot.BlockInst))
				{
					TempSlot.BlockInst.aimBlock = sameBlocks[0];
					sameBlocks.Add(TempSlot.BlockInst);
				}
				else
				{
					Debug.Log("Already contain: x = " + TempSlot.BlockLocation.x + ",y =  " + TempSlot.BlockLocation.y);
				}
				return true;
			}
			return false;
		}
		else
		{
			return false;
		}
	}
}
