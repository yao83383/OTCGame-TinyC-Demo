using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TC2Block : MonoBehaviour
{
	//
	public Image lockImage;
	//物品类型图片
	public Image image;
	//左上角显示等级数字图片
	public Text numberText;

	public TC2World world;
	//0:代表为free地块 123:分别为粮食，工业，文化 5:野兽 6:战争
	public int kind;

	public int land;
	//左上角显示等级数字
	public int level;

	public int number;

	public int speed;

	public Vector3 moveSpeed;

	public Vector3 aimPosition;
	//地块坐标 实例 通过实例获得坐标
	public TC2BlockSlot BlockSlotInst;

	public bool canMove;

	public bool moving;

	public bool justSwitch;

	public bool combining;

	public bool skill;

	public bool onMouse;

	public Animator animator;

	public float moveTime;

	public List<TC2Block> sameBlock = new List<TC2Block>();

	public Outline outline1;

	public Outline outline2;

	public bool holding;
	/*
	private void Awake()
	{
		outline2 = outline1.transform.GetComponents<Outline>()[1];
	}

	private void Start()
	{
		moveSpeed = Vector3.zero;
	}

	public void StartMove(float time)
	{
		moving = true;
		moveTime = time;
		if (!world.movingBlocks.Contains(this))
		{
			world.movingBlocks.Add(this);
		}
	}

	private void Update()
	{
		if (world.showingBlock == this && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
		{
			StartDrag();
			holding = true;
		}
		if (holding && (Input.GetMouseButtonUp(0) || Input.touchCount == 0))
		{
			EndDrag();
			holding = false;
		}
	}

	private void FixedUpdate()
	{
		if (moving)
		{
			base.transform.position = Vector3.SmoothDamp(base.transform.position, aimPosition, ref moveSpeed, moveTime);
			if (Vector3.Distance(base.transform.position, aimPosition) <= 10f)
			{
				base.transform.position = aimPosition;
				moving = false;
				world.movingBlocks.Remove(this);
				if (moveTime > 0.15f)
				{
					//每个格子的大小为10个单位
					base.transform.localPosition = new Vector3((x - 5) * 100 + 50, (y - 5) * 100 + 50);
					DisAppear();
				}
				else
				{
					BigCheckSame();
				}
			}
		}
		if (justSwitch && world.movingBlocks.Count == 0 && world.appearBlocks.Count == 0)
		{
			justSwitch = false;
		}
		if (combining && world.movingBlocks.Count == 0)
		{
			combining = false;
			Combine();
		}
	}

	private void Combine()
	{
		if (level < world.datas.limits[kind] - 1)
		{
			level++;
		}
		if (number < 3)
		{
			Debug.Log("!!!!!!!!!!");
		}
		world.datas.combineCount++;
		if (kind != 4)
		{
			world.SetFloatUI(base.transform.position, kind, number);
			world.buttomUI.ChangeResource(kind, number);
		}
		FreshBlock(newBlock: true);
		world.datas.NewBlock();
	}

	public void FreshBlock(bool newBlock)
	{
		image.gameObject.SetActive(value: true);
		numberText.enabled = true;
		world.buildings.Remove(this);
		if (newBlock)
		{
			justSwitch = true;
			speed = world.speed;
			if (kind != 0)
			{
				number = level;
			}
			if (world.menuData.hard == 1)
			{
				switch (level)
				{
				case 1:
					number = 4;
					break;
				case 2:
					number = 7;
					break;
				case 3:
					number = 10;
					break;
				case 4:
					number = 13;
					break;
				case 5:
					number = 20;
					break;
				}
				if (kind == 1 && level == 5)
				{
					number = 18;
				}
			}
			animator.enabled = true;
			animator.SetBool("appear", value: true);
			animator.Play("blockAppear", 0, 0f);
			if (x > 4)
			{
				if (y > 4)
				{
					land = 4;
				}
				else
				{
					land = 2;
				}
			}
			else if (y > 4)
			{
				land = 3;
			}
			else
			{
				land = 1;
			}
			if (kind == 1 && level > 3)
			{
				land = Random.Range(1, 3);
			}
			if (kind == 3 || kind == 4)
			{
				land = 1;
			}
			if (kind == 4)
			{
				level = world.buildings.Count;
			}
			switch (kind)
			{
			case 1:
				world.sound.PlaySound(world.sound.moveSounds[0]);
				break;
			case 2:
				world.sound.PlaySound(world.sound.moveSounds[1]);
				break;
			case 3:
				world.sound.PlaySound(world.sound.moveSounds[2]);
				break;
			case 5:
				world.sound.PlaySound(world.sound.moveSounds[3]);
				break;
			case 6:
				if (world.buttomUI.timeScore.backAge < 5)
				{
					world.sound.PlaySound(world.sound.moveSounds[4]);
				}
				else
				{
					world.sound.PlaySound(world.sound.moveSounds[8]);
				}
				break;
			case 7:
				world.BoomHere(base.transform.position);
				break;
			}
		}
		switch (kind)
		{
		case 1:
			if (level < 4)
			{
				image.sprite = world.sprites[level - 1 + (land - 1) * 4];
			}
			else if (level == 4)
			{
				image.sprite = world.sprites[3 + (land - 1) * 4];
			}
			else
			{
				image.sprite = world.sprites[3 + (land - 1) * 4 + 8];
			}
			break;
		case 2:
			if (level <= 4)
			{
				image.sprite = world.sprites[level - 1 + (land - 1) * 4 + 16];
			}
			else
			{
				image.sprite = world.sprites[69];
			}
			break;
		case 3:
			image.sprite = world.sprites[level - 1 + 64];
			break;
		case 4:
			if (level > 15)
			{
				image.sprite = world.sprites[95];
			}
			else
			{
				image.sprite = world.sprites[level + 80];
			}
			if (!onMouse)
			{
				world.buildings.Add(this);
			}
			break;
		case 5:
			image.sprite = world.sprites[land - 1 + 32];
			break;
		case 6:
			level = world.buttomUI.timeScore.backAge + 1;
			number = world.buttomUI.timeScore.backAge + Random.Range(6, 8);
			image.sprite = world.sprites[world.buttomUI.timeScore.backAge + 36];
			if (!world.wars.Contains(this))
			{
				world.wars.Add(this);
			}
			break;
		case 7:
			image.sprite = world.sprites[Random.Range(42, 48)];
			break;
		}
		if (kind == 5 || kind == 6 || kind == 7)
		{
			canMove = false;
			lockImage.enabled = true;
		}
		else
		{
			canMove = true;
			lockImage.enabled = false;
		}
		if (kind == 4 || kind == 5 || kind == 7)
		{
			numberText.text = null;
		}
		else
		{
			numberText.text = number.ToString();
		}
		if (world.menuData.hard == 1)
		{
			if (number > level && kind < 4)
			{
				numberText.color = new Color(0.6f, 0.85f, 0.6f, 1f);
				outline1.effectColor = new Color(0f, 0f, 0f, 1f);
				outline2.effectColor = new Color(0f, 0f, 0f, 1f);
			}
			else
			{
				numberText.color = new Color(0.25f, 0.15f, 0.15f, 1f);
				outline1.effectColor = new Color(1f, 1f, 1f, 1f);
				outline2.effectColor = new Color(1f, 1f, 1f, 1f);
			}
		}
		base.transform.localPosition = new Vector3((x - 5) * 100 + 50, (y - 5) * 100 + 50);
		if (kind == 4 && world.buildings.Count >= 16)
		{
			world.Achi("10");
		}
	}

	public void EndAnimator()
	{
		animator.enabled = false;
		base.transform.localScale = new Vector3(0.95f, 0.95f, 1f);
		for (int i = 0; i < world.appearBlocks.Count; i++)
		{
			if (world.appearBlocks[i].x == x && world.appearBlocks[i].y == y)
			{
				world.appearBlocks.RemoveAt(i);
				break;
			}
		}
		world.combiningBlocks.Remove(this);
		//use skill
		if (skill)
		{
			sameBlock.Clear();
			sameBlock.Add(this);
			if (kind == 2)
			{
				for (int j = y - 1; j < y + 2; j++)
				{
					for (int k = x - 1; k < x + 2; k++)
					{
						if (j >= 0 && j < world.hightLimit && k >= 0 && k < world.wideLimit)
						{
							TC2Block block = world.blockCells[k, j];
							if (block.kind == kind && (block.x != x || block.y != y))
							{
								sameBlock.Add(block);
							}
						}
					}
				}
			}
			else
			{
				for (int l = y - 2; l < y + 3; l++)
				{
					for (int m = x - 2; m < x + 3; m++)
					{
						if (l >= 0 && l < world.hightLimit && m >= 0 && m < world.wideLimit)
						{
							Block block2 = world.blockCells[m, l];
							if (block2.kind == kind && (block2.x != x || block2.y != y))
							{
								sameBlock.Add(block2);
							}
						}
					}
				}
			}
			StartCombine();
			skill = false;
		}
		else
		{
			BigCheckSame();
			if (kind != 0)
			{
				world.freeBlocks.Remove(this);
			}
		}
	}

	public void DisAppear()
	{
		CheckNull();
		animator.enabled = false;
	}

	public void HightLight()
	{
		if (!moving)
		{
			base.transform.localScale = new Vector3(1.05f, 1.05f, 1f);
			world.showingBlock = this;
		}
	}

	public void NoHightLight()
	{
		base.transform.localScale = new Vector3(0.95f, 0.95f, 1f);
		world.showingBlock = null;
		world.mouse.information.gameObject.SetActive(value: false);
		world.showBlockCount = 0;
	}

	public void StartDrag()
	{
		if (kind != 0 && !moving && canMove && world.appearBlocks.Count <= 0 && world.movingBlocks.Count <= 0 && world.combiningBlocks.Count <= 0)
		{
			image.gameObject.SetActive(value: false);
			world.StartDrag(this);
		}
	}

	public void EndDrag()
	{
		world.EndDrag();
		if (world.movingBlock == null)
		{
			return;
		}
		if (world.showingBlock == null || world.showingBlock == this || !world.showingBlock.canMove || world.showingBlock.x >= world.wideLimit || world.showingBlock.y >= world.hightLimit)
		{
			image.gameObject.SetActive(value: true);
		}
		else
		{
			world.sound.PlaySound(world.sound.moveSounds[6]);
			Block showingBlock = world.showingBlock;
			showingBlock.justSwitch = true;
			List<int> list = new List<int>();
			list.Add(showingBlock.kind);
			list.Add(showingBlock.land);
			list.Add(showingBlock.number);
			list.Add(showingBlock.level);
			showingBlock.kind = kind;
			showingBlock.land = land;
			showingBlock.number = number;
			showingBlock.level = level;
			showingBlock.speed = world.speed;
			kind = list[0];
			land = list[1];
			number = list[2];
			level = list[3];
			showingBlock.transform.SetAsLastSibling();
			showingBlock.speed = world.speed;
			speed = world.speed;
			showingBlock.CheckNull();
			CheckNull();
			showingBlock.BigCheckSame();
			if (kind != 0)
			{
				aimPosition = base.transform.position;
				base.transform.position = showingBlock.transform.position;
				StartMove(0.1f);
				world.sound.PlaySound(world.sound.moveSounds[6]);
			}
			world.NextYear();
		}
		world.movingBlock = null;
	}

	public void CheckNull()
	{
		if (kind == 0)
		{
			if (!world.freeBlocks.Contains(this))
			{
				world.freeBlocks.Add(this);
			}
			image.gameObject.SetActive(value: false);
			speed = world.speed;
			world.buildings.Remove(this);
		}
		else
		{
			if (world.freeBlocks.Contains(this))
			{
				world.freeBlocks.Remove(this);
			}
			image.gameObject.SetActive(value: true);
			FreshBlock(newBlock: false);
		}
	}

	public bool BigCheckSame()
	{
		if (kind == 0 || kind > 3)
		{
			return false;
		}
		sameBlock.Clear();
		sameBlock.Add(this);
		int num = 1;
		int num2 = 1;
		do
		{
			num2 = num;
			for (int i = 0; i < num2; i++)
			{
				MiddleCheckSame(sameBlock[i]);
			}
			num = sameBlock.Count;
		}
		while (num > num2);
		if (sameBlock.Count > 2)
		{
			StartCombine();
			return true;
		}
		return false;
	}

	private void MiddleCheckSame(Block b)
	{
		SmallCheckSame(b.x - 1, b.y);
		SmallCheckSame(b.x + 1, b.y);
		SmallCheckSame(b.x, b.y + 1);
		SmallCheckSame(b.x, b.y - 1);
	}

	private void SmallCheckSame(int xx, int yy)
	{
		if (xx < 0 || yy < 0 || xx >= world.wideLimit || yy >= world.hightLimit || sameBlock.Contains(world.blockCells[xx, yy]))
		{
			return;
		}
		switch (kind)
		{
		case 1:
			if (world.datas.unlockTechs.Contains(35))
			{
				if (world.blockCells[xx, yy].kind == kind && world.blockCells[xx, yy].level == level)
				{
					sameBlock.Add(world.blockCells[xx, yy]);
				}
			}
			else if (world.blockCells[xx, yy].kind == kind && world.blockCells[xx, yy].level == level && world.blockCells[xx, yy].land == land)
			{
				sameBlock.Add(world.blockCells[xx, yy]);
			}
			break;
		case 2:
			if (world.datas.unlockTechs.Contains(23))
			{
				if (world.blockCells[xx, yy].kind == kind && world.blockCells[xx, yy].level == level)
				{
					sameBlock.Add(world.blockCells[xx, yy]);
				}
			}
			else if (world.blockCells[xx, yy].kind == kind && world.blockCells[xx, yy].level == level && world.blockCells[xx, yy].land == land)
			{
				sameBlock.Add(world.blockCells[xx, yy]);
			}
			break;
		case 3:
			if (world.blockCells[xx, yy].kind == kind && world.blockCells[xx, yy].level == level)
			{
				sameBlock.Add(world.blockCells[xx, yy]);
			}
			break;
		}
	}

	public void StartCombine()
	{
		if (!world.combiningBlocks.Contains(this))
		{
			world.combiningBlocks.Add(this);
		}
		for (int i = 1; i < sameBlock.Count; i++)
		{
			sameBlock[i].kind = 0;
			sameBlock[i].sameBlock.Clear();
			sameBlock[i].aimPosition = base.transform.position;
			sameBlock[i].StartMove(0.2f);
			number += sameBlock[i].number;
		}
		world.sound.PlaySound(world.sound.moveSounds[7]);
		combining = true;
		base.transform.SetAsLastSibling();
	}

	public void BeDestroy()
	{
		animator.enabled = true;
		if (kind == 5)
		{
			world.datas.monsterKill++;
		}
		if (kind == 6)
		{
			world.datas.wars++;
		}
		kind = 0;
		canMove = true;
		animator.SetBool("appear", value: false);
	}
	*/
}
