using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour
{
	public List<SaveCell> saveCells = new List<SaveCell>();

	public List<SaveData> saveDatas = new List<SaveData>();

	public Text title;

	public Text ifSure;

	public Text yesSure;

	public Text noSure;

	public SaveCell showingSaveCell;

	public bool save;

	public World world;

	public MainMenu mainMenu;

	public GameObject sure;

	private int nowCell;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Close();
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			AllLoad();
			Open(save);
			title.text = "重新加载存档成功";
		}
	}

	public void Open(bool ifsave)
	{
		base.gameObject.SetActive(value: true);
		save = ifsave;
		if (world.menuData.english == 1)
		{
			if (save)
			{
				title.text = "Save Game";
			}
			else
			{
				title.text = "Load Game";
			}
			ifSure.text = "Sure to Delete?";
			yesSure.text = "Yes";
			noSure.text = "No";
		}
		else
		{
			if (save)
			{
				title.text = "保存文明";
			}
			else
			{
				title.text = "读取文明";
			}
			ifSure.text = "确定删除";
			yesSure.text = "删除";
			noSure.text = "取消";
		}
		for (int i = 0; i < 3; i++)
		{
			ShowCell(i);
		}
	}

	public void Delete(int cell)
	{
		if (saveDatas[cell].exist)
		{
			sure.SetActive(value: true);
			nowCell = cell;
		}
	}

	public void CloseSure()
	{
		sure.SetActive(value: false);
	}

	public void ShowCell(int cell)
	{
		SaveCell saveCell = saveCells[cell];
		SaveData saveData = saveDatas[cell];
		if (saveData.exist)
		{
			saveCell.age.text = saveData.age;
			saveCell.year.text = saveData.year;
			saveCell.time.text = saveData.time;
			if (saveData.hard == 0)
			{
				saveCell.hard.text = null;
			}
			else if (world.menuData.english == 1)
			{
				saveCell.hard.text = "Hero Mode";
			}
			else
			{
				saveCell.hard.text = "英雄模式";
			}
			saveCell.difficulty.text = "存档失效";
			if (world.menuData.english == 0)
			{
				switch (saveData.difficulty)
				{
				case 1:
					saveCell.difficulty.text = "简单难度";
					break;
				case 2:
					saveCell.difficulty.text = "普通难度";
					break;
				case 3:
					saveCell.difficulty.text = "困难难度";
					break;
				case 4:
					saveCell.difficulty.text = "极难难度";
					break;
				}
			}
			else
			{
				switch (saveData.difficulty)
				{
				case 1:
					saveCell.difficulty.text = "Easy";
					break;
				case 2:
					saveCell.difficulty.text = "Normal";
					break;
				case 3:
					saveCell.difficulty.text = "Hard";
					break;
				case 4:
					saveCell.difficulty.text = "Hell";
					break;
				}
			}
		}
		else
		{
			saveCell.age.text = null;
			saveCell.year.text = null;
			saveCell.time.text = null;
			saveCell.hard.text = null;
			saveCell.difficulty.text = null;
		}
	}

	public void SureDelete()
	{
		saveDatas[nowCell].exist = false;
		ShowCell(nowCell);
		sure.SetActive(value: false);
		world.sound.PlaySound(world.sound.delect);
	}

	public void SaveOrLoad(int cell)
	{
		if (save)
		{
			Save(cell);
			Close();
			Debug.Log("save");
		}
		else if (saveDatas[cell].exist)
		{
			AllSave();
			world.menuData.loading = cell;
			SceneManager.LoadScene("SampleScene");
		}
	}

	public void Close()
	{
		AllSave();
		world.openMenu = null;
		base.gameObject.SetActive(value: false);
	}

	public void AllSave()
	{
		string value = JsonConvert.SerializeObject(saveDatas);
		StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/save.json");
		streamWriter.Write(value);
		streamWriter.Close();
		PlayerPrefs.SetInt("finishGuide", world.menuData.finishGuide);
	}

	public void AllLoad()
	{
		string path = Application.persistentDataPath + "/save.json";
		if (File.Exists(path))
		{
			StreamReader streamReader = new StreamReader(path);
			string value = streamReader.ReadToEnd();
			streamReader.Close();
			saveDatas = JsonConvert.DeserializeObject<List<SaveData>>(value);
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				saveDatas[i].exist = false;
			}
		}
	}

	public void Load(int cell)
	{
		SaveData saveData = saveDatas[cell];
		world.menuData.hard = saveData.hard;
		world.menuData.difficulty = saveData.difficulty;
		world.loading = true;
		TimeScore timeScore = world.buttomUI.timeScore;
		timeScore.backAge = saveData.ScoreAge;
		timeScore.turn = saveData.Scoreturn;
		timeScore.year = saveData.ScoreYear;
		timeScore.lastTurn = saveData.ScroeLastTurn;
		timeScore.UpdateTime();
		Datas datas = world.datas;
		datas.unlockTechs.Clear();
		foreach (int unlockTech in saveData.unlockTechs)
		{
			datas.unlockTechs.Add(unlockTech);
		}
		datas.limits.Clear();
		foreach (int limit in saveData.limits)
		{
			datas.limits.Add(limit);
		}
		datas.lows.Clear();
		foreach (int low in saveData.lows)
		{
			datas.lows.Add(low);
		}
		datas.gailv.Clear();
		foreach (int item in saveData.gailv)
		{
			datas.gailv.Add(item);
		}
		datas.pool.Clear();
		foreach (int item2 in saveData.pool)
		{
			datas.pool.Add(item2);
		}
		datas.skillCounts.Clear();
		foreach (int skillCount in saveData.skillCounts)
		{
			datas.skillCounts.Add(skillCount);
		}
		datas.nowTech = saveData.nowTech;
		datas.nowAge = saveData.nowAge;
		datas.nextTechNeed = saveData.nextTechNeed;
		datas.poolNumber = saveData.poolNumber;
		datas.NewPoor();
		datas.foodProduction = saveData.foodProduction;
		datas.industryProduction = saveData.industryProduction;
		datas.cultureProduction = saveData.cultureProduction;
		datas.combineCount = saveData.combineCount;
		datas.wonderBuild = saveData.wonderBuild;
		datas.monsterKill = saveData.monsterKill;
		datas.wars = saveData.wars;
		datas.nuclearBomb = saveData.nuclearBomb;
		datas.AddReported = saveData.AddReported;
		world.hightLimit = saveData.hightLimit;
		world.wideLimit = saveData.wideLimit;
		world.speed = saveData.speed;
		world.foodCost = saveData.foodCost;
		world.food = saveData.food;
		world.rock = saveData.rock;
		world.culture = saveData.culture;
		for (int i = 0; i < world.hightLimit; i++)
		{
			for (int j = 0; j < world.wideLimit; j++)
			{
				if (!world.freeBlocks.Contains(world.blockCells[j, i]))
				{
					world.freeBlocks.Add(world.blockCells[j, i]);
				}
			}
		}
		for (int k = 0; k < saveData.blockDatas.Count; k++)
		{
			BlockData blockData = saveData.blockDatas[k];
			Block block = world.blockCells[blockData.x, blockData.y];
			block.kind = blockData.kind;
			block.land = blockData.land;
			block.level = blockData.level;
			block.number = blockData.number;
			block.speed = blockData.speed;
			block.x = blockData.x;
			block.y = blockData.y;
			block.FreshBlock(newBlock: false);
			world.freeBlocks.Remove(block);
		}
		world.buttomUI.skillNumbers.Clear();
		foreach (int skillNumber in saveData.skillNumbers)
		{
			world.buttomUI.skillNumbers.Add(skillNumber);
		}
		world.buttomUI.heroNumbers.Clear();
		foreach (int heroNumber in saveData.heroNumbers)
		{
			world.buttomUI.heroNumbers.Add(heroNumber);
		}
		world.chooseHero.showedHeros.Clear();
		foreach (int showedHero in saveData.showedHeros)
		{
			world.chooseHero.showedHeros.Add(showedHero);
		}
		world.chooseHero.canPickHeros.Clear();
		foreach (int canPickHero in saveData.canPickHeros)
		{
			world.chooseHero.canPickHeros.Add(canPickHero);
		}
		world.buttomUI.UpdateSkill();
		world.buttomUI.UpdateHero();
		world.buttomUI.AllChange();
		timeScore.audioSource.clip = timeScore.audioClips[timeScore.backAge];
		timeScore.audioSource.Play();
		timeScore.picture.sprite = world.pictures[timeScore.backAge];
		if (timeScore.backAge > 1)
		{
			world.lands[1].gameObject.SetActive(value: true);
		}
		if (timeScore.backAge > 2)
		{
			world.lands[2].gameObject.SetActive(value: true);
			world.lands[3].gameObject.SetActive(value: true);
		}
		if (timeScore.backAge == 2)
		{
			world.transform.localPosition = new Vector3(18f, 212.5f, 0f);
		}
		else if (timeScore.backAge > 2)
		{
			world.transform.localPosition = new Vector3(18f, 40f, 0f);
		}
		world.loading = false;
	}

	public void Save(int cell)
	{
		world.sound.PlaySound(world.sound.skillSounds[2]);
		SaveData saveData = saveDatas[cell];
		saveData.exist = true;
		saveData.time = DateTime.Now.ToLocalTime().ToString();
		saveData.hard = world.menuData.hard;
		saveData.difficulty = world.menuData.difficulty;
		TimeScore timeScore = world.buttomUI.timeScore;
		saveData.ScoreAge = timeScore.backAge;
		saveData.Scoreturn = timeScore.turn;
		saveData.ScoreYear = timeScore.year;
		saveData.ScroeLastTurn = timeScore.lastTurn;
		saveData.age = timeScore.ageText.text;
		saveData.year = timeScore.yearText.text;
		Datas datas = world.datas;
		saveData.unlockTechs.Clear();
		foreach (int unlockTech in datas.unlockTechs)
		{
			saveData.unlockTechs.Add(unlockTech);
		}
		saveData.limits.Clear();
		foreach (int limit in datas.limits)
		{
			saveData.limits.Add(limit);
		}
		saveData.lows.Clear();
		foreach (int low in datas.lows)
		{
			saveData.lows.Add(low);
		}
		saveData.gailv.Clear();
		foreach (int item in datas.gailv)
		{
			saveData.gailv.Add(item);
		}
		saveData.pool.Clear();
		foreach (int item2 in datas.pool)
		{
			saveData.pool.Add(item2);
		}
		saveData.skillCounts.Clear();
		foreach (int skillCount in datas.skillCounts)
		{
			saveData.skillCounts.Add(skillCount);
		}
		saveData.nowTech = datas.nowTech;
		saveData.nowAge = datas.nowAge;
		saveData.nextTechNeed = datas.nextTechNeed;
		saveData.poolNumber = datas.poolNumber;
		saveData.nowTech = datas.nowTech;
		saveData.foodProduction = datas.foodProduction;
		saveData.industryProduction = datas.industryProduction;
		saveData.cultureProduction = datas.cultureProduction;
		saveData.combineCount = datas.combineCount;
		saveData.wonderBuild = datas.wonderBuild;
		saveData.monsterKill = datas.monsterKill;
		saveData.wars = datas.wars;
		saveData.nuclearBomb = datas.nuclearBomb;
		saveData.AddReported = datas.AddReported;
		saveData.hightLimit = world.hightLimit;
		saveData.wideLimit = world.wideLimit;
		saveData.speed = world.speed;
		saveData.foodCost = world.foodCost;
		saveData.food = world.food + world.buttomUI.food.numberWait;
		saveData.rock = world.rock + world.buttomUI.rock.numberWait;
		saveData.culture = world.culture + world.buttomUI.culture.numberWait;
		saveData.blockDatas.Clear();
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				Block block = world.blockCells[j, i];
				if (block.kind != 0)
				{
					BlockData blockData = new BlockData();
					blockData.kind = block.kind;
					blockData.land = block.land;
					blockData.level = block.level;
					blockData.number = block.number;
					blockData.speed = block.speed;
					blockData.x = block.x;
					blockData.y = block.y;
					saveData.blockDatas.Add(blockData);
				}
			}
		}
		saveData.skillNumbers.Clear();
		foreach (int skillNumber in world.buttomUI.skillNumbers)
		{
			saveData.skillNumbers.Add(skillNumber);
		}
		saveData.heroNumbers.Clear();
		foreach (int heroNumber in world.buttomUI.heroNumbers)
		{
			saveData.heroNumbers.Add(heroNumber);
		}
		saveData.canPickHeros.Clear();
		foreach (int canPickHero in world.chooseHero.canPickHeros)
		{
			saveData.canPickHeros.Add(canPickHero);
		}
		saveData.showedHeros.Clear();
		foreach (int showedHero in world.chooseHero.showedHeros)
		{
			saveData.showedHeros.Add(showedHero);
		}
	}
}
