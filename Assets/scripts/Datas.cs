using System.Collections.Generic;
using UnityEngine;

public class Datas : MonoBehaviour
{
	public List<int> limits = new List<int>();

	public List<int> lows = new List<int>();

	public List<int> gailv = new List<int>();

	public List<int> pool = new List<int>();

	public int poolNumber;

	public World world;

	public List<NewTechJson> newTechjsons = new List<NewTechJson>();

	public List<SkillJson> skilljsons = new List<SkillJson>();

	public List<BlockGuide> blockGuides = new List<BlockGuide>();

	public List<HeroJson> heroJsons = new List<HeroJson>();

	public List<Sprite> heroSprites = new List<Sprite>();

	public List<WonderName> wonderNames = new List<WonderName>();

	public List<Age> ages = new List<Age>();

	public List<int> unlockTechs = new List<int>();

	public int nowTech;

	public int nowAge;

	public int nextTechNeed;

	public int AddReported;

	public int foodProduction;

	public int industryProduction;

	public int cultureProduction;

	public int combineCount;

	public int wonderBuild;

	public int monsterKill;

	public int wars;

	public int nuclearBomb;

	public List<int> skillCounts = new List<int>();

	private void Start()
	{
	}

	public void NewBlock()
	{
		if (poolNumber >= pool.Count)
		{
			NewPoor();
		}
		int num = pool[poolNumber];
		poolNumber++;
		world.NewBlock(999, 999, num, lows[num]);
	}

	public void NewPoor()
	{
		pool.Clear();
		List<int> list = new List<int>();
		for (int i = 0; i < gailv.Count; i++)
		{
			for (int j = 0; j < gailv[i]; j++)
			{
				list.Add(i);
			}
		}
		int count = list.Count;
		for (int k = 0; k < count; k++)
		{
			int index = Random.Range(0, list.Count);
			pool.Add(list[index]);
			list.RemoveAt(index);
		}
		poolNumber = 0;
	}

	public void CheckTech(int tech)
	{
		switch (tech)
		{
		case 1:
			world.buttomUI.skillNumbers.Add(1);
			world.buttomUI.UpdateSkill();
			break;
		case 2:
			limits[2] = 3;
			break;
		case 3:
			limits[1] = 3;
			break;
		case 4:
			world.buttomUI.skillNumbers.Add(2);
			world.buttomUI.UpdateSkill();
			break;
		case 5:
			limits[3] = 3;
			break;
		case 6:
			world.buttomUI.skillNumbers.Add(3);
			world.buttomUI.UpdateSkill();
			break;
		case 7:
			world.buttomUI.skillNumbers.Add(4);
			world.buttomUI.UpdateSkill();
			break;
		case 8:
			world.buttomUI.skillNumbers.Add(5);
			world.buttomUI.UpdateSkill();
			break;
		case 9:
			limits[1] = 4;
			break;
		case 10:
			world.buttomUI.skillNumbers.Add(6);
			world.buttomUI.UpdateSkill();
			break;
		case 11:
			limits[2] = 4;
			break;
		case 12:
			limits[3] = 4;
			break;
		case 13:
		{
			lows[1] = 2;
			for (int num17 = 0; num17 < world.hightLimit; num17++)
			{
				for (int num18 = 0; num18 < world.wideLimit; num18++)
				{
					if (world.blockCells[num18, num17].kind == 1 && world.blockCells[num18, num17].level < 2)
					{
						world.blockCells[num18, num17].level = 2;
						world.blockCells[num18, num17].FreshBlock(newBlock: true);
					}
				}
			}
			for (int num19 = 0; num19 < world.appearBlocks.Count; num19++)
			{
				if (world.appearBlocks[num19].kind == 1 && world.appearBlocks[num19].level < 2)
				{
					world.appearBlocks[num19].level = 2;
				}
			}
			break;
		}
		case 14:
		{
			lows[2] = 2;
			for (int num11 = 0; num11 < world.hightLimit; num11++)
			{
				for (int num12 = 0; num12 < world.wideLimit; num12++)
				{
					if (world.blockCells[num12, num11].kind == 2 && world.blockCells[num12, num11].level < 2)
					{
						world.blockCells[num12, num11].level = 2;
						world.blockCells[num12, num11].FreshBlock(newBlock: true);
					}
				}
			}
			for (int num13 = 0; num13 < world.appearBlocks.Count; num13++)
			{
				if (world.appearBlocks[num13].kind == 2 && world.appearBlocks[num13].level < 2)
				{
					world.appearBlocks[num13].level = 2;
				}
			}
			world.buttomUI.skillNumbers.Remove(0);
			world.buttomUI.UpdateSkill();
			break;
		}
		case 15:
		{
			lows[3] = 2;
			for (int num3 = 0; num3 < world.hightLimit; num3++)
			{
				for (int num4 = 0; num4 < world.wideLimit; num4++)
				{
					if (world.blockCells[num4, num3].kind == 3 && world.blockCells[num4, num3].level < 2)
					{
						world.blockCells[num4, num3].level = 2;
						world.blockCells[num4, num3].FreshBlock(newBlock: true);
					}
				}
			}
			for (int num5 = 0; num5 < world.appearBlocks.Count; num5++)
			{
				if (world.appearBlocks[num5].kind == 3 && world.appearBlocks[num5].level < 2)
				{
					world.appearBlocks[num5].level = 2;
				}
			}
			break;
		}
		case 16:
			world.buttomUI.skillNumbers.Add(7);
			world.buttomUI.skillNumbers.Remove(5);
			world.buttomUI.UpdateSkill();
			break;
		case 17:
			world.buttomUI.skillNumbers.Add(8);
			world.buttomUI.UpdateSkill();
			break;
		case 18:
			lows[6]++;
			world.buttomUI.skillNumbers.Add(9);
			world.buttomUI.skillNumbers.Remove(1);
			world.buttomUI.UpdateSkill();
			break;
		case 19:
			world.buttomUI.UpdateSkill();
			break;
		case 20:
			limits[1] = 5;
			break;
		case 21:
			limits[3] = 5;
			break;
		case 22:
			limits[2] = 5;
			break;
		case 23:
		{
			world.buttomUI.skillNumbers.Remove(8);
			for (int num23 = 0; num23 < world.hightLimit; num23++)
			{
				for (int num24 = 0; num24 < world.wideLimit; num24++)
				{
					if (world.blockCells[num24, num23].kind == 2)
					{
						world.blockCells[num24, num23].BigCheckSame();
					}
				}
			}
			world.buttomUI.UpdateSkill();
			break;
		}
		case 24:
			world.buttomUI.skillNumbers.Add(10);
			world.buttomUI.skillNumbers.Remove(3);
			world.buttomUI.UpdateSkill();
			break;
		case 25:
		{
			gailv[5] = 0;
			NewPoor();
			world.buttomUI.skillNumbers.Remove(9);
			world.buttomUI.UpdateSkill();
			for (int num20 = 0; num20 < world.hightLimit; num20++)
			{
				for (int num21 = 0; num21 < world.wideLimit; num21++)
				{
					if (world.blockCells[num21, num20].kind == 5)
					{
						world.blockCells[num21, num20].BeDestroy();
					}
				}
			}
			for (int num22 = 0; num22 < world.appearBlocks.Count; num22++)
			{
				if (world.appearBlocks[num22].kind == 5)
				{
					world.appearBlocks[num22].kind = 1;
					world.appearBlocks[num22].level = 3;
				}
			}
			break;
		}
		case 26:
			world.buttomUI.skillNumbers.Add(11);
			world.buttomUI.UpdateSkill();
			break;
		case 27:
		{
			lows[1] = 3;
			for (int num14 = 0; num14 < world.hightLimit; num14++)
			{
				for (int num15 = 0; num15 < world.wideLimit; num15++)
				{
					if (world.blockCells[num15, num14].kind == 1 && world.blockCells[num15, num14].level < 3)
					{
						world.blockCells[num15, num14].level = 3;
						world.blockCells[num15, num14].FreshBlock(newBlock: true);
					}
				}
			}
			for (int num16 = 0; num16 < world.appearBlocks.Count; num16++)
			{
				if (world.appearBlocks[num16].kind == 1 && world.appearBlocks[num16].level < 3)
				{
					world.appearBlocks[num16].level = 3;
				}
			}
			break;
		}
		case 28:
			world.buttomUI.skillNumbers.Add(12);
			world.buttomUI.UpdateSkill();
			lows[6]++;
			break;
		case 29:
		{
			lows[3] = 3;
			for (int num8 = 0; num8 < world.hightLimit; num8++)
			{
				for (int num9 = 0; num9 < world.wideLimit; num9++)
				{
					if (world.blockCells[num9, num8].kind == 3 && world.blockCells[num9, num8].level < 3)
					{
						world.blockCells[num9, num8].level = 3;
						world.blockCells[num9, num8].FreshBlock(newBlock: true);
					}
				}
			}
			for (int num10 = 0; num10 < world.appearBlocks.Count; num10++)
			{
				if (world.appearBlocks[num10].kind == 3 && world.appearBlocks[num10].level < 3)
				{
					world.appearBlocks[num10].level = 3;
				}
			}
			break;
		}
		case 30:
			world.buttomUI.skillNumbers.Add(13);
			world.buttomUI.skillNumbers.Remove(7);
			world.buttomUI.UpdateSkill();
			break;
		case 31:
			limits[1] = 6;
			break;
		case 32:
			world.buttomUI.skillNumbers.Add(14);
			world.buttomUI.UpdateSkill();
			break;
		case 33:
		{
			world.speed = 2;
			for (int num6 = 0; num6 < world.hightLimit; num6++)
			{
				for (int num7 = 0; num7 < world.wideLimit; num7++)
				{
					if (world.blockCells[num7, num6].speed == 1)
					{
						world.blockCells[num7, num6].speed = 2;
					}
				}
			}
			break;
		}
		case 34:
		{
			lows[2] = 3;
			for (int n = 0; n < world.hightLimit; n++)
			{
				for (int num = 0; num < world.wideLimit; num++)
				{
					if (world.blockCells[num, n].kind == 2 && world.blockCells[num, n].level < 3)
					{
						world.blockCells[num, n].level = 3;
						world.blockCells[num, n].FreshBlock(newBlock: true);
					}
				}
			}
			for (int num2 = 0; num2 < world.appearBlocks.Count; num2++)
			{
				if (world.appearBlocks[num2].kind == 2 && world.appearBlocks[num2].level < 3)
				{
					world.appearBlocks[num2].level = 3;
				}
			}
			break;
		}
		case 35:
		{
			for (int l = 0; l < world.hightLimit; l++)
			{
				for (int m = 0; m < world.wideLimit; m++)
				{
					if (world.blockCells[m, l].kind == 1)
					{
						world.blockCells[m, l].BigCheckSame();
					}
				}
			}
			break;
		}
		case 36:
			limits[2] = 6;
			break;
		case 37:
			world.buttomUI.skillNumbers.Add(15);
			world.buttomUI.UpdateSkill();
			break;
		case 38:
			limits[3] = 6;
			break;
		case 39:
			world.buttomUI.skillNumbers.Add(16);
			world.buttomUI.UpdateSkill();
			break;
		case 40:
		{
			lows[1] = 5;
			for (int i = 0; i < world.hightLimit; i++)
			{
				for (int j = 0; j < world.wideLimit; j++)
				{
					if (world.blockCells[j, i].kind == 1 && world.blockCells[j, i].level < 5)
					{
						world.blockCells[j, i].level = 5;
						world.blockCells[j, i].FreshBlock(newBlock: true);
					}
				}
			}
			for (int k = 0; k < world.appearBlocks.Count; k++)
			{
				if (world.appearBlocks[k].kind == 1 && world.appearBlocks[k].level < 5)
				{
					world.appearBlocks[k].level = 5;
				}
			}
			break;
		}
		case 41:
			world.vectory.gameObject.SetActive(value: true);
			break;
		}
	}
}
