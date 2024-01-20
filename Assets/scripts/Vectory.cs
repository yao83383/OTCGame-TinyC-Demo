using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vectory : MonoBehaviour
{
	public World world;

	public List<Rocket> rockets = new List<Rocket>();

	private int underCount;

	private int rocketCount;

	private int count;

	private int[] launch = new int[17]
	{
		50, 100, 100, 100, 100, 70, 100, 100, 20, 100,
		40, 80, 100, 50, 100, 20, 60
	};

	public GameObject rocket;

	public GameObject report;

	public CanvasGroup thanks;

	public Text text1;

	public Text text2;

	public Text text3;

	public List<Text> reports = new List<Text>();

	private int thankCount;

	private int endCount;

	private void Start()
	{
		StartVectory();
	}

	private void FixedUpdate()
	{
		underCount++;
		if (rocketCount < count && underCount == launch[rocketCount])
		{
			rockets[rocketCount].transform.GetComponent<Animator>().enabled = true;
			underCount -= launch[rocketCount];
			rocketCount++;
			if (rocketCount == count - 8)
			{
				thankCount = 1;
			}
		}
		if (thankCount > 0)
		{
			thanks.alpha = (float)thankCount * 0.005f;
			thankCount++;
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1) && thankCount > 100)
		{
			world.menuData.canHard = 1;
			PlayerPrefs.SetInt("canHard", 1);
			SceneManager.LoadScene("Menu");
		}
	}

	private void FreshReport()
	{
		if (world.menuData.english == 0)
		{
			switch (world.menuData.difficulty)
			{
			case 1:
				reports[0].text = "简单难度";
				break;
			case 2:
				reports[0].text = "普通难度";
				break;
			case 3:
				reports[0].text = "困难难度";
				break;
			case 4:
				reports[0].text = "极难难度";
				break;
			}
			reports[1].text = "合成次数：  " + world.datas.combineCount;
			reports[2].text = "生产粮食：  " + world.datas.foodProduction;
			reports[3].text = "生产工业：  " + world.datas.industryProduction;
			reports[4].text = "生产文化：  " + world.datas.cultureProduction;
			reports[5].text = "建成奇观：  " + world.datas.wonderBuild;
			reports[6].text = "击退野兽：  " + world.datas.monsterKill;
			reports[7].text = "经历战争：  " + world.datas.wars;
			reports[8].text = "核弹爆炸：  " + world.datas.nuclearBomb;
			int num = 0;
			int index = 0;
			int num2 = 0;
			for (int i = 0; i < world.datas.skillCounts.Count; i++)
			{
				if (world.datas.skillCounts[i] > num)
				{
					num2 += world.datas.skillCounts[i];
					num = world.datas.skillCounts[i];
					index = i;
				}
			}
			reports[9].text = "使用技能：  " + num2;
			reports[10].text = "青睐技能：  " + world.datas.skilljsons[index].name;
			return;
		}
		switch (world.menuData.difficulty)
		{
		case 1:
			reports[0].text = "Eazy Mode:";
			break;
		case 2:
			reports[0].text = "Normal Mode:";
			break;
		case 3:
			reports[0].text = "Hard Mode:";
			break;
		case 4:
			reports[0].text = "Hell Mode:";
			break;
		}
		reports[1].text = "Combine Count:  " + world.datas.combineCount;
		reports[2].text = "Food Product:  " + world.datas.foodProduction;
		reports[3].text = "Industry Product:  " + world.datas.industryProduction;
		reports[4].text = "Culture Product:  " + world.datas.cultureProduction;
		reports[5].text = "Build Wonder:  " + world.datas.wonderBuild;
		reports[6].text = "Kill Beast:  " + world.datas.monsterKill;
		reports[7].text = "War OutBreak:  " + world.datas.wars;
		reports[8].text = "Nuclear Bomb:  " + world.datas.nuclearBomb;
		int num3 = 0;
		int index2 = 0;
		int num4 = 0;
		for (int j = 0; j < world.datas.skillCounts.Count; j++)
		{
			if (world.datas.skillCounts[j] > num3)
			{
				num4 += world.datas.skillCounts[j];
				num3 = world.datas.skillCounts[j];
				index2 = j;
			}
		}
		reports[9].text = "Use Sill:  " + num4;
		reports[10].text = "Faver Sill:  " + world.datas.skilljsons[index2].english;
	}

	private void UnlockAchi()
	{
		world.Achi("08");
		if (world.buttomUI.timeScore.backAge < 5)
		{
			world.Achi("09");
		}
		switch (world.menuData.difficulty)
		{
		case 1:
			world.Achi("11");
			break;
		case 2:
			world.Achi("11");
			world.Achi("12");
			break;
		case 3:
			world.Achi("11");
			world.Achi("12");
			world.Achi("13");
			break;
		case 4:
			world.Achi("11");
			world.Achi("12");
			world.Achi("13");
			world.Achi("14");
			break;
		}
	}

	public void StartVectory()
	{
		if (world.datas.AddReported == 1)
		{
			report.SetActive(value: true);
			FreshReport();
		}
		else
		{
			report.SetActive(value: false);
		}
		world.buttomUI.timeScore.audioSource.clip = world.buttomUI.timeScore.audioClips[7];
		world.buttomUI.timeScore.audioSource.volume = 0.3f;
		world.buttomUI.timeScore.audioSource.Play();
		if (world.menuData.english == 0)
		{
			if (world.menuData.canHard == 0)
			{
				text2.text = "已解锁英雄模式";
			}
			else
			{
				text2.text = null;
			}
		}
		else
		{
			if (world.menuData.canHard == 0)
			{
				text2.text = "Unlock Hero Mode.";
			}
			else
			{
				text2.text = null;
			}
			text1.text = "Victory!";
			text3.text = "Right click back to menu.";
		}
		count = 15;
		if (world.freeBlocks.Count < 15)
		{
			do
			{
				Block block = world.blockCells[Random.Range(1, 9), Random.Range(1, 9)];
				if (block.kind != 4)
				{
					block.kind = 0;
					block.DisAppear();
				}
			}
			while (world.freeBlocks.Count < 15);
		}
		for (int i = 0; i < count; i++)
		{
			Block block2 = world.freeBlocks[Random.Range(0, world.freeBlocks.Count)];
			world.freeBlocks.Remove(block2);
			world.rockets[i].transform.position = block2.transform.position;
			world.rockets[i].gameObject.SetActive(value: true);
			world.rockets[i].transform.GetChild(0).GetComponent<Animator>().enabled = false;
			world.rockets[i].transform.GetChild(0).GetComponent<Rocket>().vectory = true;
			rockets.Add(world.rockets[i].transform.GetChild(0).GetComponent<Rocket>());
			world.rockets[i].transform.SetAsFirstSibling();
		}
		UnlockAchi();
	}
}
