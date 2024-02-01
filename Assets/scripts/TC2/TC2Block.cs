using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DragableItem))]
public class TC2Block : MonoBehaviour
{
	//TC2Blocks
	//�Ƿ��ǿ�ƥ������
	public bool IsMatchable;
	//
	public Image lockImage;
	//��Ʒ����ͼƬ
	public Image image;
	//���Ͻ���ʾ�ȼ�����ͼƬ
	public Text numberText;

	//0:����Ϊfree�ؿ� 123:�ֱ�Ϊ��ʳ����ҵ���Ļ� 5:Ұ�� 6:ս��
	public int kind;

	public int land;
	//���Ͻ���ʾ�ȼ�����
	public int level;

	public int number;

	public int speed;

	public Vector3 moveSpeed;

	public TC2Block aimBlock;
	//�ؿ����� ʵ�� ͨ��ʵ���������
	public TC2BlockSlot BlockSlotInst;

	public bool canMove;

	public bool moving;

	public bool justSwitch;

	public bool combining;

	public bool skill;

	public bool onMouse;

	public Animator animator;

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
        kind = 1;
        IsMatchable = true;
    }

	public void RegisterToSlot()
	{
        Transform SlotTrans = transform.parent.parent;
        TC2BlockSlot SlotInst = SlotTrans.GetComponent<TC2BlockSlot>();
        BlockSlotInst = SlotInst;
        SlotInst.BlockInst = this;
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
	
	}
}
