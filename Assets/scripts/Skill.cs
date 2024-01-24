using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
	public Image image;

	public Text text;

	public int number;

	public int cost;

	public World world;

	public bool holding;

	private void Start()
	{
	}

	private void Update()
	{
		if (world.showingSkill == this && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
		{
			StartDrag();
			holding = true;
		}
		if (holding && (Input.GetMouseButtonUp(0) || Input.touchCount > 0))
		{
			EndDrag();
			holding = false;
		}
	}

	public void UpdateSkill(int n)
	{
		number = n;
		if (n == 0)
		{
			image.sprite = world.sprites[70];
		}
		else
		{
			image.sprite = world.sprites[47 + n];
		}
		switch (world.menuData.difficulty)
		{
		case 1:
			if (world.datas.unlockTechs.Contains(19))
			{
				cost = world.datas.skilljsons[n].cost1 - 1;
			}
			else
			{
				cost = world.datas.skilljsons[n].cost1;
			}
			break;
		case 2:
			if (world.datas.unlockTechs.Contains(19))
			{
				cost = world.datas.skilljsons[n].cost2 - 1;
			}
			else
			{
				cost = world.datas.skilljsons[n].cost2;
			}
			break;
		case 3:
			if (world.datas.unlockTechs.Contains(19))
			{
				cost = world.datas.skilljsons[n].cost3 - 1;
			}
			else
			{
				cost = world.datas.skilljsons[n].cost3;
			}
			break;
		case 4:
			if (world.datas.unlockTechs.Contains(19))
			{
				cost = world.datas.skilljsons[n].cost4 - 1;
			}
			else
			{
				cost = world.datas.skilljsons[n].cost4;
			}
			break;
		}
		if (n == 4)
		{
			cost += world.buildings.Count * (6 + 2 * world.menuData.difficulty);
		}
		text.text = cost.ToString();
	}

	public void HightLight()
	{
		base.transform.localScale = new Vector3(1.07f, 1.07f, 1f);
		world.showingSkill = this;
		world.mouse.information.gameObject.SetActive(value: true);
		if (world.menuData.english == 0)
		{
			world.mouse.inforName.text = world.datas.skilljsons[number].name;
			world.mouse.inforDiscrption.text = world.datas.skilljsons[number].discription;
		}
		else
		{
			world.mouse.inforName.text = world.datas.skilljsons[number].english;
			world.mouse.inforDiscrption.text = world.datas.skilljsons[number].englishDiscription;
		}
	}

	public void NoHighLight()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		world.showingSkill = null;
		world.mouse.information.gameObject.SetActive(value: false);
	}

	public void StartDrag()
	{
		image.gameObject.SetActive(value: false);
		world.StartSkill(number);
		world.kuanRange.number = number;
		world.kuanRange.skill = true;
	}

	public void EndDrag()
	{
		image.gameObject.SetActive(value: true);
		world.kuanRange.skill = false;
		UseSkill();
	}

	public void UseSkill()
	{
		world.mouse.skill.gameObject.SetActive(value: false);
		if (!(world.showingBlock != null))
		{
			return;
		}
		Block showingBlock = world.showingBlock;
		bool flag = false;
		switch (number)
		{
		case 0:
			if (showingBlock.kind == 0 && world.canSkill(cost))
			{
				world.NewBlock(showingBlock.x, showingBlock.y, 2, 1);
				flag = true;
			}
			break;
		case 1:
			if (showingBlock.kind == 5 && world.canSkill(cost))
			{
				world.buttomUI.ChangeResource(2, 2);
				world.SetFloatUI(showingBlock.transform.position, 2, 2);
				showingBlock.BeDestroy();
				flag = true;
			}
			break;
		case 2:
			if (showingBlock.kind == 1 && world.canSkill(cost))
			{
				world.saveGame.Open(ifsave: true);
				flag = true;
			}
			break;
		case 3:
			if (showingBlock.kind != 0 && showingBlock.kind < 5 && showingBlock.speed < 4 && world.canSkill(cost))
			{
				showingBlock.speed = 4;
				flag = true;
			}
			break;
		case 4:
			if (showingBlock.kind == 0 && world.canSkill(cost))
			{
				world.datas.wonderBuild++;
				world.NewBlock(showingBlock.x, showingBlock.y, 4, 1);
				world.light1.transform.position = showingBlock.transform.position;
				world.light1.Build();
				flag = true;
			}
			break;
		case 5:
			if (showingBlock.kind != 0 && showingBlock.kind < 4 && showingBlock.level == 1 && world.canSkill(cost))
			{
				showingBlock.level = 2;
				showingBlock.FreshBlock(newBlock: true);
				flag = true;
			}
			break;
		case 6:
			if (showingBlock.kind == 6 && world.canSkill(cost))
			{
				showingBlock.number -= 6;
				showingBlock.numberText.text = showingBlock.number.ToString();
				if (showingBlock.number <= 0)
				{
					showingBlock.number = 0;
					showingBlock.numberText.text = showingBlock.number.ToString();
					showingBlock.BeDestroy();
					world.wars.Remove(showingBlock);
				}
				flag = true;
			}
			break;
		case 7:
			if (showingBlock.kind != 0 && showingBlock.kind < 4 && showingBlock.level < 3 && world.canSkill(cost))
			{
				showingBlock.level = 3;
				showingBlock.FreshBlock(newBlock: true);
				flag = true;
			}
			break;
		case 8:
			if (showingBlock.kind == 0 && world.canSkill(cost))
			{
				world.freeBlocks.Remove(showingBlock);
				showingBlock.kind = 2;
				showingBlock.level = 2;
				showingBlock.number = 2;
				showingBlock.skill = true;
				showingBlock.FreshBlock(newBlock: true);
				if (world.menuData.hard == 1)
				{
					showingBlock.number = 2;
					showingBlock.numberText.text = 2.ToString();
				}
				showingBlock.image.sprite = world.sprites[55];
				flag = true;
			}
			break;
		case 9:
		{
			if (!world.canSkill(cost))
			{
				break;
			}
			int num = 0;
			int num2 = 0;
			if (showingBlock.x > 4)
			{
				num = 5;
			}
			if (showingBlock.y > 4)
			{
				num2 = 5;
			}
			for (int i = num2; i < num2 + 5; i++)
			{
				for (int j = num; j < num + 5; j++)
				{
					if (world.blockCells[j, i].kind == 5)
					{
						world.blockCells[j, i].BeDestroy();
						world.buttomUI.ChangeResource(2, 2);
						world.SetFloatUI(world.blockCells[j, i].transform.position, 2, 2);
					}
				}
			}
			flag = true;
			break;
		}
		case 10:
			if (showingBlock.kind != 0 && showingBlock.kind < 5 && showingBlock.speed < 6 && world.canSkill(cost))
			{
				showingBlock.speed = 6;
				flag = true;
			}
			break;
		case 11:
			if (showingBlock.kind == 0 && world.canSkill(cost))
			{
				world.NewBlock(showingBlock.x, showingBlock.y, 2, 3);
				flag = true;
			}
			break;
		case 12:
			if (showingBlock.kind != 0 && showingBlock.kind < 5 && showingBlock.speed < 100 && world.canSkill(cost))
			{
				showingBlock.speed = 100;
				flag = true;
			}
			break;
		case 13:
			if (showingBlock.kind != 0 && showingBlock.kind < 4 && showingBlock.level < 4 && world.canSkill(cost))
			{
				showingBlock.level = 4;
				showingBlock.FreshBlock(newBlock: true);
				flag = true;
			}
			break;
		case 14:
			if (showingBlock.kind == 0 && world.canSkill(cost))
			{
				world.freeBlocks.Remove(showingBlock);
				showingBlock.kind = 1;
				showingBlock.level = 4;
				showingBlock.number = 4;
				showingBlock.skill = true;
				showingBlock.FreshBlock(newBlock: true);
				if (world.menuData.hard == 1)
				{
					showingBlock.number = 4;
					showingBlock.numberText.text = 4.ToString();
				}
				showingBlock.image.sprite = world.sprites[61];
				flag = true;
			}
			break;
		case 15:
			if (showingBlock.kind == 7 && world.canSkill(cost))
			{
				showingBlock.BeDestroy();
				world.buttomUI.ChangeResource(1, 5);
				world.SetFloatUI(showingBlock.transform.position, 1, 5);
				flag = true;
			}
			break;
		case 16:
			if (showingBlock.kind == 0 && world.canSkill(cost))
			{
				world.RocketHere(showingBlock.transform.position);
				flag = true;
			}
			break;
		}
		if (flag)
		{
			world.datas.skillCounts[number]++;
			world.sound.PlaySound(world.sound.skillSounds[number]);
			if (number != 1 && number != 4 && number != 9)
			{
				world.buff.gameObject.SetActive(value: true);
				world.buff.transform.position = world.showingBlock.transform.position;
			}
		}
		else
		{
			world.sound.PlaySound(world.sound.skillSounds[17]);
		}
	}
}
