using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScore : MonoBehaviour
{
	public Image timeScore;

	public Image ageImage;

	public Text yearText;

	public Text ageText;

	public World world;

	public int turn;

	public int year;

	public int backAge;

	public Animator agePage;

	public Text ageName;

	public Text ageDiscription;

	public int lastTurn;

	public List<int> ageturns = new List<int>();

	public List<AudioClip> audioClips = new List<AudioClip>();

	public List<Sprite> ageSprites = new List<Sprite>();

	public AudioSource audioSource;

	public Image picture;

	public void NextTurn()
	{
		if (turn < 47)
		{
			year += 100;
		}
		else if (turn < 80)
		{
			year += 60;
		}
		else if (turn < 200)
		{
			year += 30;
		}
		else if (turn < 230)
		{
			year += 20;
		}
		else if (turn < 290)
		{
			year += 10;
		}
		else if (turn < 320)
		{
			year += 5;
		}
		else if (turn < 420)
		{
			year += 3;
		}
		else if (turn < 440)
		{
			year += 2;
		}
		else
		{
			year++;
		}
		turn++;
		lastTurn++;
		if (lastTurn > ageturns[backAge])
		{
			world.newAgeing = 1;
		}
		UpdateTime();
	}

	public void ShowAge()
	{
		lastTurn -= ageturns[backAge];
		backAge++;
		agePage.enabled = true;
		agePage.SetBool("disAppear", value: false);
		if (world.menuData.english == 0)
		{
			ageName.text = world.datas.ages[backAge].name;
			if (world.menuData.hard == 0)
			{
				ageDiscription.text = world.datas.ages[backAge].discription1;
			}
			else
			{
				ageDiscription.text = world.datas.ages[backAge].discription2;
			}
		}
		else
		{
			ageName.text = world.datas.ages[backAge].englishName;
			if (world.menuData.hard == 0)
			{
				ageDiscription.text = world.datas.ages[backAge].englishDis1;
			}
			else
			{
				ageDiscription.text = world.datas.ages[backAge].englishDis2;
			}
		}
		ageImage.sprite = ageSprites[backAge - 1];
		UnlockAchi();
	}

	public void UnlockAchi()
	{
		switch (backAge)
		{
		case 1:
			world.Achi("02");
			break;
		case 2:
			world.Achi("03");
			break;
		case 3:
			world.Achi("04");
			break;
		case 4:
			world.Achi("05");
			break;
		case 5:
			world.Achi("06");
			break;
		case 6:
			world.Achi("07");
			break;
		}
	}

	public void UpdateTime()
	{
		if (world.menuData.english == 0)
		{
			if (year < 10000)
			{
				yearText.text = "公元前" + (10000 - year) + "年";
			}
			else
			{
				yearText.text = "公元" + (year - 10000) + "年";
			}
			timeScore.fillAmount = (float)lastTurn * 1f / (float)ageturns[backAge];
			ageText.text = world.datas.ages[backAge].name;
		}
		else
		{
			if (year < 10000)
			{
				yearText.text = 10000 - year + "BC";
			}
			else
			{
				yearText.text = year - 10000 + "AD";
			}
			timeScore.fillAmount = (float)lastTurn * 1f / (float)ageturns[backAge];
			ageText.text = world.datas.ages[backAge].englishName;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && world.newAgeing > 120)
		{
			Time.timeScale = 1f;
			agePage.SetBool("disAppear", value: true);
			world.newAgeing = 0;
			CheckAge();
			UpdateTime();
			if (world.menuData.hard == 1)
			{
				world.chooseHero.OpenChoose();
			}
		}
	}

	public void CheckAge()
	{
		audioSource.clip = audioClips[backAge];
		audioSource.volume = 0.3f;
		audioSource.Play();
		picture.sprite = world.pictures[backAge];
		switch (backAge)
		{
		case 1:
			world.foodCost++;
			if (world.menuData.hard == 0)
			{
				world.datas.gailv[2]++;
			}
			world.buttomUI.ChangeResource(2, 1);
			break;
		case 2:
		{
			if (world.menuData.hard == 0)
			{
				world.datas.gailv[2]++;
			}
			else
			{
				world.datas.gailv[6]++;
			}
			world.buttomUI.ChangeResource(2, 1);
			world.lands[1].gameObject.SetActive(value: true);
			world.wideLimit = 10;
			for (int k = 0; k < 5; k++)
			{
				for (int l = 5; l < 10; l++)
				{
					world.freeBlocks.Add(world.blockCells[l, k]);
				}
			}
			for (int m = 0; m < 10; m++)
			{
				world.datas.NewBlock();
			}
			world.transform.localPosition = new Vector3(18f, 212.5f, 0f);
			world.datas.gailv[3]++;
			world.datas.gailv[6]++;
			world.datas.NewPoor();
			break;
		}
		case 3:
		{
			_ = world.menuData.hard;
			_ = 1;
			world.buttomUI.ChangeResource(2, 1);
			world.lands[2].gameObject.SetActive(value: true);
			world.lands[3].gameObject.SetActive(value: true);
			world.hightLimit = 10;
			for (int n = 5; n < 10; n++)
			{
				for (int num = 0; num < 10; num++)
				{
					world.freeBlocks.Add(world.blockCells[num, n]);
				}
			}
			for (int num2 = 0; num2 < 20; num2++)
			{
				world.datas.NewBlock();
			}
			world.transform.localPosition = new Vector3(18f, 40f, 0f);
			if (world.menuData.hard == 0)
			{
				world.datas.gailv[2]--;
				world.datas.gailv[3]++;
			}
			else
			{
				world.datas.gailv[2]--;
			}
			world.datas.NewPoor();
			break;
		}
		case 4:
			world.foodCost++;
			world.buttomUI.ChangeResource(2, 1);
			if (world.menuData.hard == 0)
			{
				world.datas.gailv[3]++;
				world.datas.gailv[3]++;
			}
			world.datas.NewPoor();
			break;
		case 5:
			world.datas.gailv[6] += 5;
			world.datas.gailv[7] += 6;
			world.datas.NewPoor();
			world.datas.pool[world.datas.poolNumber] = 7;
			break;
		case 6:
		{
			world.datas.gailv[6] = 0;
			world.datas.gailv[7] = 0;
			world.datas.NewPoor();
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (world.blockCells[j, i].kind == 6)
					{
						Block block = world.blockCells[j, i];
						block.number = 0;
						block.FreshBlock(newBlock: false);
						block.BeDestroy();
						world.wars.Remove(block);
					}
				}
			}
			world.buttomUI.skillNumbers.Remove(6);
			world.buttomUI.UpdateSkill();
			break;
		}
		case 7:
			break;
		}
	}
}
