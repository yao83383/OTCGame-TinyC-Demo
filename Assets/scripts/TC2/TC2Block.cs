using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DragableItem))]
public class TC2Block : MonoBehaviour
{
	//TC2Blocks
	//是否是可匹配类型
	public bool IsMatchable;
	public Animator MoveAnimator;
	public Image BlockImage;
	//
	public Image lockImage;
	//物品类型图片
	public Image image;
	//左上角显示等级数字图片
	public Text numberText;

	//0:代表为free地块 123:分别为粮食，工业，文化 5:野兽 6:战争
	public int kind;

	public int land;
	//左上角显示等级数字
	public int level;

	public int number;

	public int speed;

	public TC2World WorldRef;

	public Vector3 moveSpeed;

	public TC2Block aimBlock;
	//地块坐标 实例 通过实例获得坐标
	public TC2BlockSlot BlockSlotInst;

	public bool canMove;

	public bool moving;

	public bool justSwitch;

	public bool combining;

	public bool skill;

	public bool onMouse;

	public float moveTime = 0.5f;

	public Outline outline1;

	public Outline outline2;

	public bool holding;
	private void Awake()
	{
		InitBlock();
	}

	private void InitBlock()
	{
		BlockImage = GetComponent<Image>();
		kind = 1;
		IsMatchable = true;
	}

	public static GameObject CreateTC2Block(TC2BlockSlot InSlot)
	{
		InSlot.IsAvailable = true;
        List<Transform> children = new List<Transform>();
		InSlot.GetComponentsInChildren<Transform>(true, children);
        foreach (var child in children)
        {
            child.gameObject.SetActive(true);
        }

        GameObject TempBlockObj = GameObject.Instantiate(InSlot.WorldRef.BlockPrefab);
		TempBlockObj.transform.SetParent(children[children.Count - 1].transform);
		TempBlockObj.GetComponent<TC2Block>().RegisterToSlot();

		return TempBlockObj;
	}

	public void RegisterToSlot()
	{
        Transform SlotTrans = transform.parent.parent;
        TC2BlockSlot SlotInst = SlotTrans.GetComponent<TC2BlockSlot>();
		WorldRef = SlotInst.WorldRef;
		BlockSlotInst = SlotInst;
        SlotInst.BlockInst = this;
        if (WorldRef.FreeBlockSlots.ContainsKey(BlockSlotInst.BlockLocation))
        { 
        	WorldRef.FreeBlockSlots.Remove(BlockSlotInst.BlockLocation);
        }
    }

    public void OnSpawnNewBlock()
	{
		RegisterToSlot();
		int CombineNum = this.BlockSlotInst.CheckMatch();

		if (CombineNum >= 3)
		{
			Combine();
		}
		else
		{
			Debug.Log("Match number is " + CombineNum);
		}
	}

	public void StartMove()
	{
		moveSpeed = Vector3.zero;
		AudioClip TempClip;
		BlockSlotInst.WorldRef.sound.Sounds.TryGetValue("Move", out TempClip);
		BlockSlotInst.WorldRef.sound.PlaySound(TempClip);
		BlockSlotInst.WorldRef.NextYear();
	}

	public void Combine()
    {
        AudioClip TempClip;
		BlockSlotInst.WorldRef.sound.Sounds.TryGetValue("Combine", out TempClip);
		BlockSlotInst.WorldRef.sound.PlaySound(TempClip);
		combining = true;
    }

	private void FixedUpdate()
	{
		if (combining || moving)
		{
			base.transform.position = Vector3.SmoothDamp(base.transform.position, aimBlock.transform.position, ref moveSpeed, moveTime);

			if (Vector3.Distance(base.transform.position, aimBlock.transform.position) <= 10f)
			{
				base.transform.position = aimBlock.transform.position;
				moving = false;

				if (combining)
				{
					aimBlock.BlockSlotInst.sameBlocks.Remove(this);
					combining = false;
				}
			}
		}
	}

	public void DisAppear()
	{
		BlockSlotInst.BlockInst = null;
        if (!WorldRef.FreeBlockSlots.ContainsKey(BlockSlotInst.BlockLocation))
        {
            WorldRef.FreeBlockSlots.Add(BlockSlotInst.BlockLocation, BlockSlotInst);
        }
        this.gameObject.SetActive(false);
		Destroy(this.gameObject);
		//Destroy(this);
	}

	public void LevelUp()
	{
		Debug.Log("LevelUp!");
	}
}
