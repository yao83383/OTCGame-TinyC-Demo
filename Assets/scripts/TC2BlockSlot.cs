using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TC2BlockSlot : MonoBehaviour
{
	public enum NearDirect
	{ 
		Left	= 1,
		Right	= 2,
		Up		= 3,
		Down	= 4
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

	public World WorldRef;
	//地块坐标
	[HideInInspector]
	public Vector2 BlockLocation;

	TC2BlockSlot(int InX, int InY)
	{
		BlockLocation = new Vector2(InX, InY);
	}

	~TC2BlockSlot()
	{
		WorldRef.BlockSlots.Remove(BlockLocation);
		RefreshNearSlotsState();
	}

    public void Awake()
    {
		WorldRef.BlockSlots.Add(BlockLocation, this);
	}

	//在创建新地块时需要刷新周边BlockSlot
	public void RefreshSlotState()
	{
		CheckEdge();
	}
	//删除地块时 周边4个地块都需要各自刷新一次
	public void RefreshNearSlotsState()
	{
		if (LeftAvailable)
		{
			TC2BlockSlot TempSlot;
			WorldRef.BlockSlots.TryGetValue(new Vector2(BlockLocation.x - 1, BlockLocation.y), out TempSlot);
			TempSlot.RefreshSlotState();
		}
        if (RightAvailable)
        {
            TC2BlockSlot TempSlot;
            WorldRef.BlockSlots.TryGetValue(new Vector2(BlockLocation.x + 1, BlockLocation.y), out TempSlot);
            TempSlot.RefreshSlotState();
        }
        if (UpAvailable)
        {
            TC2BlockSlot TempSlot;
            WorldRef.BlockSlots.TryGetValue(new Vector2(BlockLocation.x, BlockLocation.y + 1), out TempSlot);
            TempSlot.RefreshSlotState();
        }
        if (DownAvailable)
        {
            TC2BlockSlot TempSlot;
            WorldRef.BlockSlots.TryGetValue(new Vector2(BlockLocation.x, BlockLocation.y - 1), out TempSlot);
            TempSlot.RefreshSlotState();
        }
    }

	public void CheckEdge()
	{
		if (CheckNearExist(NearDirect.Left))
		{
			LeftEdge.color = new Color(LeftEdge.color.r, LeftEdge.color.g, LeftEdge.color.b, 1);
			LeftAvailable = true;
		}
		else 
		{
            LeftEdge.color = new Color(LeftEdge.color.r, LeftEdge.color.g, LeftEdge.color.b, 0);
            LeftAvailable = false;
        }

        if (CheckNearExist(NearDirect.Right))
        {
			RightEdge.color = new Color(RightEdge.color.r, RightEdge.color.g, RightEdge.color.b, 1);
			RightAvailable = true;
        }
        {
            RightEdge.color = new Color(RightEdge.color.r, RightEdge.color.g, RightEdge.color.b, 0);
			RightAvailable = false;
        }

        if (CheckNearExist(NearDirect.Up))
        {
			UpEdge.color = new Color(UpEdge.color.r, UpEdge.color.g, UpEdge.color.b, 1);
			UpAvailable = true;
        }
		else
		{
			UpEdge.color = new Color(UpEdge.color.r, UpEdge.color.g, UpEdge.color.b, 0);
			UpAvailable = false;
		}

		if (CheckNearExist(NearDirect.Down))
		{
			DownEdge.color = new Color(DownEdge.color.r, DownEdge.color.g, DownEdge.color.b, 1);
			DownAvailable = true;
		}
		else
        {
            DownEdge.color = new Color(DownEdge.color.r, DownEdge.color.g, DownEdge.color.b, 0);
			DownAvailable = false;
        }
    }

    public bool CheckNearExist(NearDirect InDirectEnum)
	{
		bool SearchResult = false;
		switch (InDirectEnum)
		{
			case NearDirect.Left:
				{
					SearchResult = WorldRef.BlockSlots.ContainsKey(new Vector2(BlockLocation.x - 1, BlockLocation.y));
					break;
				}
            case NearDirect.Right:
                {
					SearchResult = WorldRef.BlockSlots.ContainsKey(new Vector2(BlockLocation.x + 1, BlockLocation.y));
					break;
                }
            case NearDirect.Up:
                {
					SearchResult = WorldRef.BlockSlots.ContainsKey(new Vector2(BlockLocation.x, BlockLocation.y + 1));
					break;
                }
            case NearDirect.Down:
                {
					SearchResult = WorldRef.BlockSlots.ContainsKey(new Vector2(BlockLocation.x, BlockLocation.y - 1));
					break;
                }
        }
		return SearchResult;
	}
}
