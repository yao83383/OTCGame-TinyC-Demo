using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroShow : MonoBehaviour
{
	public int number;

	public int x;

	public int y;

	public Image image;

	public World world;

	public void EndHeroShow()
	{
		HeroAction();
		base.gameObject.SetActive(value: false);
	}

	public void HeroAction()
	{
		switch (number)
		{
		case 0:
		{
			world.sound.PlaySound(world.sound.skillSounds[1]);
			for (int num31 = -2; num31 < 3; num31++)
			{
				for (int num32 = -2; num32 < 3; num32++)
				{
					int num33 = x + num32;
					int num34 = y + num31;
					if (num33 >= 0 && num33 < world.wideLimit && num34 >= 0 && num34 < world.hightLimit && world.blockCells[num33, num34].kind == 5)
					{
						world.blockCells[num33, num34].BeDestroy();
						world.buttomUI.ChangeResource(2, 8);
						world.SetFloatUI(world.blockCells[num33, num34].transform.position, 2, 8);
					}
				}
			}
			break;
		}
		case 1:
		{
			world.sound.PlaySound(world.sound.moveSounds[7]);
			for (int num8 = -2; num8 < 3; num8++)
			{
				for (int num9 = -2; num9 < 3; num9++)
				{
					int num10 = x + num9;
					int num11 = y + num8;
					if (num10 >= 0 && num10 < world.wideLimit && num11 >= 0 && num11 < world.hightLimit && world.blockCells[num10, num11].kind == 2)
					{
						world.buttomUI.ChangeResource(2, 7 + world.blockCells[num10, num11].number);
						world.SetFloatUI(world.blockCells[num10, num11].transform.position, 2, 7 + world.blockCells[num10, num11].number);
						world.blockCells[num10, num11].BeDestroy();
					}
				}
			}
			break;
		}
		case 2:
		{
			for (int num45 = -2; num45 < 3; num45++)
			{
				for (int num46 = -2; num46 < 3; num46++)
				{
					int num47 = x + num46;
					int num48 = y + num45;
					if (num47 >= 0 && num47 < world.wideLimit && num48 >= 0 && num48 < world.hightLimit && world.blockCells[num47, num48].kind == 2)
					{
						world.blockCells[num47, num48].level = world.datas.limits[2] - 1;
						world.blockCells[num47, num48].FreshBlock(newBlock: true);
					}
				}
			}
			world.buttomUI.ChangeResource(2, 10);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 2, 10);
			break;
		}
		case 3:
		{
			for (int j = -2; j < 3; j++)
			{
				for (int k = -2; k < 3; k++)
				{
					int num2 = x + k;
					int num3 = y + j;
					if (num2 >= 0 && num2 < world.wideLimit && num3 >= 0 && num3 < world.hightLimit && world.blockCells[num2, num3].kind == 3)
					{
						world.blockCells[num2, num3].level = world.datas.limits[3] - 1;
						world.blockCells[num2, num3].FreshBlock(newBlock: true);
					}
				}
			}
			world.buttomUI.ChangeResource(3, 10);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 3, 10);
			break;
		}
		case 4:
		{
			for (int num14 = -2; num14 < 3; num14++)
			{
				for (int num15 = -2; num15 < 3; num15++)
				{
					int num16 = x + num15;
					int num17 = y + num14;
					if (num16 >= 0 && num16 < world.wideLimit && num17 >= 0 && num17 < world.hightLimit && world.blockCells[num16, num17].kind == 1)
					{
						world.blockCells[num16, num17].level = world.datas.limits[1] - 1;
						world.blockCells[num16, num17].FreshBlock(newBlock: true);
					}
				}
			}
			world.buttomUI.ChangeResource(1, 10);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 1, 10);
			break;
		}
		case 5:
		{
			world.sound.PlaySound(world.sound.skillSounds[15]);
			for (int l = -3; l < 4; l++)
			{
				for (int m = -3; m < 4; m++)
				{
					int num5 = x + m;
					int num6 = y + l;
					if (num5 >= 0 && num5 < world.wideLimit && num6 >= 0 && num6 < world.hightLimit && world.blockCells[num5, num6].kind == 7)
					{
						world.blockCells[num5, num6].BeDestroy();
						world.buttomUI.ChangeResource(1, 20);
						world.SetFloatUI(world.blockCells[num5, num6].transform.position, 1, 20);
					}
				}
			}
			break;
		}
		case 6:
		{
			world.buttomUI.ChangeResource(1, 60);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 1, 60);
			for (int i = 0; i < 3; i++)
			{
				Vector2Int vector2Int = FindNearbyEmpty();
				if (vector2Int.x >= 0)
				{
					world.blockCells[vector2Int.x, vector2Int.y].kind = 6;
					world.blockCells[vector2Int.x, vector2Int.y].FreshBlock(newBlock: true);
				}
			}
			break;
		}
		case 7:
		{
			world.sound.PlaySound(world.sound.skillSounds[15]);
			for (int num49 = -3; num49 < 4; num49++)
			{
				for (int num50 = -3; num50 < 4; num50++)
				{
					int num51 = x + num50;
					int num52 = y + num49;
					if (num51 >= 0 && num51 < world.wideLimit && num52 >= 0 && num52 < world.hightLimit && world.blockCells[num51, num52].kind == 6)
					{
						world.blockCells[num51, num52].BeDestroy();
						world.buttomUI.ChangeResource(1, 15);
						world.SetFloatUI(world.blockCells[num51, num52].transform.position, 1, 15);
					}
				}
			}
			break;
		}
		case 8:
		{
			world.sound.PlaySound(world.sound.otherSounds[1]);
			TimeScore timeScore = world.buttomUI.timeScore;
			timeScore.turn -= 15;
			timeScore.year = CountYear(timeScore.turn);
			timeScore.lastTurn -= 15;
			timeScore.UpdateTime();
			break;
		}
		case 9:
		{
			Vector2Int vector2Int5 = FindNearbyEmpty();
			world.datas.wonderBuild++;
			world.blockCells[vector2Int5.x, vector2Int5.y].kind = 4;
			world.blockCells[vector2Int5.x, vector2Int5.y].FreshBlock(newBlock: true);
			world.light1.transform.position = world.blockCells[vector2Int5.x, vector2Int5.y].transform.position;
			world.light1.Build();
			break;
		}
		case 10:
		{
			for (int num23 = -3; num23 < 4; num23++)
			{
				for (int num24 = -3; num24 < 4; num24++)
				{
					int num25 = x + num24;
					int num26 = y + num23;
					if (num25 >= 0 && num25 < world.wideLimit && num26 >= 0 && num26 < world.hightLimit && world.blockCells[num25, num26].kind != 0 && world.blockCells[num25, num26].kind < 4)
					{
						world.blockCells[num25, num26].FreshBlock(newBlock: true);
					}
				}
			}
			break;
		}
		case 11:
		{
			for (int num18 = 0; num18 < 8; num18++)
			{
				Vector2Int vector2Int4 = FindNearbyEmpty();
				if (vector2Int4.x >= 0)
				{
					world.blockCells[vector2Int4.x, vector2Int4.y].kind = 2;
					world.blockCells[vector2Int4.x, vector2Int4.y].level = world.datas.lows[2];
					world.blockCells[vector2Int4.x, vector2Int4.y].FreshBlock(newBlock: true);
				}
			}
			break;
		}
		case 12:
		{
			for (int num12 = 0; num12 < 8; num12++)
			{
				Vector2Int vector2Int3 = FindNearbyEmpty();
				if (vector2Int3.x >= 0)
				{
					world.blockCells[vector2Int3.x, vector2Int3.y].kind = 1;
					world.blockCells[vector2Int3.x, vector2Int3.y].level = world.datas.lows[1];
					world.blockCells[vector2Int3.x, vector2Int3.y].FreshBlock(newBlock: true);
				}
			}
			break;
		}
		case 13:
		{
			for (int n = 0; n < 8; n++)
			{
				Vector2Int vector2Int2 = FindNearbyEmpty();
				if (vector2Int2.x >= 0)
				{
					world.blockCells[vector2Int2.x, vector2Int2.y].kind = 3;
					world.blockCells[vector2Int2.x, vector2Int2.y].level = world.datas.lows[3];
					world.blockCells[vector2Int2.x, vector2Int2.y].FreshBlock(newBlock: true);
				}
			}
			break;
		}
		case 14:
		{
			world.sound.PlaySound(world.sound.skillSounds[5]);
			int num53 = world.rock;
			if (num53 > 100)
			{
				num53 = 100;
			}
			world.buttomUI.ChangeResource(3, num53);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 3, num53);
			break;
		}
		case 15:
		{
			world.sound.PlaySound(world.sound.skillSounds[11]);
			int num44 = world.food;
			if (num44 > 100)
			{
				num44 = 100;
			}
			world.buttomUI.ChangeResource(2, num44);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 2, num44);
			break;
		}
		case 16:
		{
			world.sound.PlaySound(world.sound.skillSounds[6]);
			int num43 = world.datas.nextTechNeed - world.culture + 1;
			world.buttomUI.ChangeResource(1, num43);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 1, num43);
			break;
		}
		case 17:
		{
			world.sound.PlaySound(world.sound.otherSounds[2]);
			for (int num39 = -2; num39 < 3; num39++)
			{
				for (int num40 = -2; num40 < 3; num40++)
				{
					int num41 = x + num40;
					int num42 = y + num39;
					if (num41 >= 0 && num41 < world.wideLimit && num42 >= 0 && num42 < world.hightLimit && world.blockCells[num41, num42].kind == 1)
					{
						world.buttomUI.ChangeResource(1, 8);
						world.SetFloatUI(world.blockCells[num41, num42].transform.position, 1, 8);
					}
				}
			}
			break;
		}
		case 18:
		{
			world.sound.PlaySound(world.sound.otherSounds[3]);
			for (int num35 = -2; num35 < 3; num35++)
			{
				for (int num36 = -2; num36 < 3; num36++)
				{
					int num37 = x + num36;
					int num38 = y + num35;
					if (num37 >= 0 && num37 < world.wideLimit && num38 >= 0 && num38 < world.hightLimit && world.blockCells[num37, num38].kind == 2)
					{
						world.buttomUI.ChangeResource(2, 8);
						world.SetFloatUI(world.blockCells[num37, num38].transform.position, 2, 8);
					}
				}
			}
			break;
		}
		case 19:
		{
			world.sound.PlaySound(world.sound.otherSounds[4]);
			for (int num27 = -2; num27 < 3; num27++)
			{
				for (int num28 = -2; num28 < 3; num28++)
				{
					int num29 = x + num28;
					int num30 = y + num27;
					if (num29 >= 0 && num29 < world.wideLimit && num30 >= 0 && num30 < world.hightLimit && world.blockCells[num29, num30].kind == 3)
					{
						world.buttomUI.ChangeResource(3, 8);
						world.SetFloatUI(world.blockCells[num29, num30].transform.position, 3, 8);
					}
				}
			}
			break;
		}
		case 20:
		{
			world.sound.PlaySound(world.sound.skillSounds[1]);
			for (int num19 = -3; num19 < 4; num19++)
			{
				for (int num20 = -3; num20 < 4; num20++)
				{
					int num21 = x + num20;
					int num22 = y + num19;
					if (num21 >= 0 && num21 < world.wideLimit && num22 >= 0 && num22 < world.hightLimit && world.blockCells[num21, num22].kind > 4)
					{
						world.blockCells[num21, num22].BeDestroy();
					}
				}
			}
			break;
		}
		case 21:
		{
			world.sound.PlaySound(world.sound.skillSounds[6]);
			int num13 = world.culture;
			if (num13 > 100)
			{
				num13 = 100;
			}
			world.buttomUI.ChangeResource(1, num13);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 1, num13);
			break;
		}
		case 22:
		{
			world.sound.PlaySound(world.sound.otherSounds[6]);
			int num7 = world.culture;
			if (num7 > 120)
			{
				num7 = 120;
			}
			world.buttomUI.ChangeResource(3, num7);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 3, num7);
			break;
		}
		case 23:
		{
			world.sound.PlaySound(world.sound.skillSounds[11]);
			int num4 = world.rock;
			if (num4 > 120)
			{
				num4 = 120;
			}
			world.buttomUI.ChangeResource(2, num4);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 2, num4);
			break;
		}
		case 24:
		{
			world.sound.PlaySound(world.sound.skillSounds[8]);
			int num = world.food;
			if (num > 120)
			{
				num = 120;
			}
			world.buttomUI.ChangeResource(1, num);
			world.SetFloatUI(world.blockCells[x, y].transform.position, 1, num);
			break;
		}
		}
	}

	private int CountYear(int t)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < t; i++)
		{
			num2 = ((num >= 47) ? ((num >= 80) ? ((num >= 200) ? ((num >= 230) ? ((num >= 290) ? ((num >= 320) ? ((num >= 420) ? ((num >= 440) ? (num2 + 1) : (num2 + 2)) : (num2 + 3)) : (num2 + 5)) : (num2 + 10)) : (num2 + 20)) : (num2 + 30)) : (num2 + 60)) : (num2 + 100));
			num++;
		}
		return num2;
	}

	public Vector2Int FindNearbyEmpty()
	{
		Vector2Int result = new Vector2Int(-2, -2);
		if (world.blockCells[x, y].kind == 0)
		{
			result = new Vector2Int(x, y);
		}
		else
		{
			List<Vector2Int> list = new List<Vector2Int>();
			int num = 1;
			do
			{
				for (int i = -num; i < 1 + num; i++)
				{
					for (int j = -num; j < 1 + num; j++)
					{
						int num2 = x + j;
						int num3 = y + i;
						if (num2 >= 0 && num2 < world.wideLimit && num3 >= 0 && num3 < world.hightLimit && world.blockCells[num2, num3].kind == 0)
						{
							list.Add(new Vector2Int(num2, num3));
						}
					}
				}
				num++;
			}
			while (list.Count <= 0 && num < 5);
			if (list.Count > 0)
			{
				return list[Random.Range(0, list.Count)];
			}
		}
		return result;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
