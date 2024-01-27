using System;
using System.Collections.Generic;
//using Steamworks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TC2World : MonoBehaviour
{
	[Serializable]
	public class WaitingBlock
	{
		public int x;

		public int y;

		public int kind;

		public int level;

		public int land;
	}

	public bool inMenu;

	public int hightLimit;

	public int wideLimit;

	public int speed;
	[HideInInspector]
	public List<TC2Block> freeBlocks = new List<TC2Block>();
	[HideInInspector]
	public TC2Block[,] blockCells = new TC2Block[10, 10];
	[HideInInspector]
	public Image[,] shadows = new Image[10, 10];
	[HideInInspector]
	public List<Image> lands = new List<Image>();
	[HideInInspector]
	public List<Sprite> sprites = new List<Sprite>();
	[HideInInspector]
	public List<Sprite> pictures = new List<Sprite>();
	[HideInInspector]
	public List<Image> freeShadows = new List<Image>();

	public MenuData menuData;

	public ButtomUI buttomUI;
	//鼠标当前悬浮的block，用于显示block信息
	[HideInInspector]
	public TC2Block showingBlock;
	//被拖动的第一块
	[HideInInspector]
	public TC2Block movingBlock;

	public Hero showingHero;

	public Skill showingSkill;

	public Mouse mouse;

	public Datas datas;

	public NewTech newTech;

	public EndGame endGame;

	public Sound sound;

	public SaveGame saveGame;

	public StartGame startGame;

	public RotateLight light1;

	public Vectory vectory;

	public Esc esc;

	public AudioMixer audioMixer;

	public SetScreen setScreen;

	public Buff buff;

	public HeroShow heroShow;

	public ChooseHero chooseHero;

	public ChangeEnglish changeEnglish;

	public MainMenu mainMenu;

	public KuanRange kuanRange;
	[HideInInspector]
	public List<WaitingBlock> appearBlocks = new List<WaitingBlock>();
	[HideInInspector]
	public List<TC2Block> movingBlocks = new List<TC2Block>();
	[HideInInspector]
	public List<TC2Block> combiningBlocks = new List<TC2Block>();
	[HideInInspector]
	public List<FloatUI> floatUIs = new List<FloatUI>();
	[HideInInspector]
	public List<Boom> booms = new List<Boom>();
	[HideInInspector]
	public List<Transform> rockets = new List<Transform>();
	//TC2
    [HideInInspector]
    public Dictionary<Vector2, TC2BlockSlot> BlockSlots = new Dictionary<Vector2, TC2BlockSlot>();
	private int appearCount;

	private int checkDead;
	[HideInInspector]
	public List<TC2Block> wars = new List<TC2Block>();
	[HideInInspector]
	public List<TC2Block> buildings = new List<TC2Block>();

	[HideInInspector]
	public bool combining;
	[HideInInspector]
	public bool dead;
	[HideInInspector]
	public bool loading;
	[HideInInspector]
	public GameObject openMenu;
	[HideInInspector]
	public int food;
	[HideInInspector]
	public int rock;
	[HideInInspector]
	public int culture;
	[HideInInspector]
	public int foodCost;
	[HideInInspector]
	public int shakeCount;
	[HideInInspector]
	public int newAgeing;
	[HideInInspector]
	public int showBlockCount;

	public List<Vector3> worldShakes = new List<Vector3>();
	/*
	private void Awake()
	{
		menuData.firstOpen = PlayerPrefs.GetInt("firstOpen");
		if (menuData.firstOpen == 0)
		{
			string text = Application.systemLanguage.ToString();
			if (text.CompareTo("ChineseSimplified") == 0 || text.CompareTo("ChineseTranditional") == 0 || text.CompareTo("Chinese") == 0)
			{
				Debug.Log(text);
				PlayerPrefs.SetInt("english", 0);
			}
			else
			{
				Debug.Log(text);
				PlayerPrefs.SetInt("english", 1);
			}
		}
		AwakeLoadData();
		saveGame.AllLoad();
		if (!inMenu)
		{
			SetAllBlock();
		}
		else
		{
			mainMenu.ChangeMenu();
		}
		Application.runInBackground = true;
	}

	private void AwakeLoadData()
	{
		menuData.canHard = PlayerPrefs.GetInt("canHard");
		menuData.finishGuide = PlayerPrefs.GetInt("finishGuide");
		menuData.finishHardGuide = PlayerPrefs.GetInt("finishHardGuide");
		menuData.english = PlayerPrefs.GetInt("english");
	}

	public void Achi(string s)
	{
		//if (SteamManager.Initialized)
		//{
		//	SteamUserStats.SetAchievement(s);
		//	SteamUserStats.StoreStats();
		//}
	}

	private void Start()
	{
		float @float = PlayerPrefs.GetFloat("music");
		float float2 = PlayerPrefs.GetFloat("sound");
		if (float2 < -20f)
		{
			audioMixer.SetFloat("soundSound", -100f);
		}
		else
		{
			audioMixer.SetFloat("soundSound", float2);
		}
		if (@float < -20f)
		{
			audioMixer.SetFloat("musicSound", -100f);
		}
		else
		{
			audioMixer.SetFloat("musicSound", @float);
		}
		if (inMenu)
		{
			return;
		}
		if (menuData.loading == 100)
		{
			NewBlock(0, 2, 1, 1);
			NewBlock(4, 3, 2, 1);
			NewBlock(1, 4, 3, 1);
			datas.AddReported = 1;
			if (menuData.hard == 1)
			{
				food = 20;
				rock = 8;
				foodCost = 2;
				datas.nextTechNeed = 15;
				if (menuData.finishHardGuide == 1)
				{
					chooseHero.PickTwoHero();
					chooseHero.OpenChoose();
				}
			}
			buttomUI.AllChange();
			datas.NewPoor();
			if (menuData.finishGuide == 0 && menuData.hard == 0)
			{
				startGame.gameObject.SetActive(value: true);
			}
			if (menuData.finishHardGuide == 0 && menuData.hard == 1)
			{
				startGame.gameObject.SetActive(value: true);
			}
		}
		else
		{
			saveGame.Load(menuData.loading);
			menuData.loading = 100;
		}
		Achi("01");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && openMenu == null)
		{
			OpenEsc();
		}
		if (!inMenu && Input.GetMouseButtonDown(1) && showingBlock != null && showingBlock.kind != 0)
		{
			ShowInformation();
		}
	}

	public void OpenEsc()
	{
		esc.gameObject.SetActive(value: true);
		openMenu = esc.gameObject;
		esc.UpdateLanguage();
	}

	public void SetFloatUI(Vector3 v, int kind, int number)
	{
		FloatUI floatUI = floatUIs[0];
		floatUIs.Remove(floatUI);
		floatUIs.Add(floatUI);
		floatUI.transform.position = v + new Vector3(0f, 20f, 0f);
		switch (kind)
		{
		case 1:
			floatUI.image.sprite = sprites[74];
			break;
		case 2:
			floatUI.image.sprite = sprites[72];
			break;
		case 3:
			floatUI.image.sprite = sprites[73];
			break;
		}
		floatUI.text.text = number.ToString();
		floatUI.gameObject.SetActive(value: true);
	}

	public void NewBlock(int x, int y, int kind, int level)
	{
		WaitingBlock waitingBlock = new WaitingBlock();
		waitingBlock.kind = kind;
		waitingBlock.level = level;
		waitingBlock.x = x;
		waitingBlock.y = y;
		appearBlocks.Add(waitingBlock);
	}

	private void FixedUpdate()
	{
		if (inMenu)
		{
			return;
		}
		if (showingBlock != null && showingBlock.kind != 0)
		{
			showBlockCount++;
			if (showBlockCount == 25 && datas.blockGuides[showingBlock.kind].number < 2)
			{
				ShowInformation();
			}
			else if (showBlockCount == 100)
			{
				ShowInformation();
			}
		}
		if (buttomUI.food.numberWait + food < 0)
		{
			checkDead++;
		}
		else
		{
			checkDead = 0;
		}
		if (checkDead > 50)
		{
			endGame.gameObject.SetActive(value: true);
			dead = true;
		}
		if (appearBlocks.Count > 0 && movingBlocks.Count == 0 && combiningBlocks.Count == 0 && !dead)
		{
			if (newAgeing != 0)
			{
				if (newAgeing == 1)
				{
					buttomUI.timeScore.ShowAge();
				}
				newAgeing++;
				if (newAgeing < 120)
				{
					buttomUI.timeScore.audioSource.volume -= 0.0025f;
				}
				return;
			}
			appearCount++;
			if (appearCount >= 20)
			{
				if (freeBlocks.Count > 0)
				{
					WaitingBlock waitingBlock = appearBlocks[0];
					bool flag = false;
					if (waitingBlock.x < 99 && freeBlocks.Contains(blockCells[waitingBlock.x, waitingBlock.y]))
					{
						flag = true;
						Block block = blockCells[waitingBlock.x, waitingBlock.y];
						freeBlocks.Remove(block);
						block.kind = waitingBlock.kind;
						block.level = waitingBlock.level;
						block.FreshBlock(newBlock: true);
					}
					if (!flag && freeBlocks.Count > 0)
					{
						Block block2 = freeBlocks[UnityEngine.Random.Range(0, freeBlocks.Count)];
						freeBlocks.Remove(block2);
						block2.kind = waitingBlock.kind;
						block2.level = waitingBlock.level;
						waitingBlock.x = block2.x;
						waitingBlock.y = block2.y;
						block2.FreshBlock(newBlock: true);
					}
				}
				else
				{
					appearBlocks.Clear();
				}
				appearCount = 0;
			}
		}
		WorldShake();
	}

	private void ShowInformation()
	{
		mouse.information.gameObject.SetActive(value: true);
		datas.blockGuides[showingBlock.kind].number++;
		if (menuData.english == 0)
		{
			mouse.inforName.text = datas.blockGuides[showingBlock.kind].name;
			mouse.inforDiscrption.text = datas.blockGuides[showingBlock.kind].discription;
			if (showingBlock.kind == 4)
			{
				mouse.inforName.text = datas.wonderNames[showingBlock.level].name;
			}
		}
		else
		{
			mouse.inforName.text = datas.blockGuides[showingBlock.kind].englishName;
			mouse.inforDiscrption.text = datas.blockGuides[showingBlock.kind].englishDis;
			if (showingBlock.kind == 4)
			{
				mouse.inforName.text = datas.wonderNames[showingBlock.level].english;
			}
		}
	}

	private void SetAllBlock()
	{
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				blockCells[j, i] = freeBlocks[j + i * 10];
				blockCells[j, i].x = j;
				blockCells[j, i].y = i;
				shadows[j, i] = freeShadows[j + i * 10];
			}
		}
		freeBlocks.Clear();
		for (int k = 0; k < hightLimit; k++)
		{
			for (int l = 0; l < wideLimit; l++)
			{
				freeBlocks.Add(blockCells[l, k]);
			}
		}
	}

	public void StartDrag(TC2Block b)
	{
		sound.PlaySound(sound.moveSounds[5]);
		movingBlock = b;
		mouse.block.gameObject.SetActive(value: true);
		mouse.block.kind = b.kind;
		mouse.block.number = b.number;
		mouse.block.land = b.land;
		mouse.block.level = b.level;
		mouse.block.FreshBlock(newBlock: false);
		mouse.block.transform.localPosition = new Vector3(0f, 0f, 0f);
		for (int i = 0; i < hightLimit; i++)
		{
			for (int j = 0; j < wideLimit; j++)
			{
				int num = i - b.y;
				if (num < 0)
				{
					num = -num;
				}
				int num2 = j - b.x;
				if (num2 < 0)
				{
					num2 = -num2;
				}
				if ((float)(num + num2) > (float)b.speed + 0.1f)
				{
					shadows[j, i].enabled = true;
				}
			}
		}
	}

	public void EndDrag()
	{
		mouse.block.gameObject.SetActive(value: false);
		for (int i = 0; i < hightLimit; i++)
		{
			for (int j = 0; j < wideLimit; j++)
			{
				shadows[j, i].enabled = false;
			}
		}
	}

	public void StartSkill(int number)
	{
		sound.PlaySound(sound.moveSounds[5]);
		mouse.skill.gameObject.SetActive(value: true);
		mouse.skill.UpdateSkill(number);
		mouse.skill.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	public void PickUpHero(int n)
	{
		sound.PlaySound(sound.moveSounds[5]);
		mouse.skill.gameObject.SetActive(value: true);
		mouse.skill.text.text = null;
		mouse.skill.image.sprite = datas.heroSprites[n];
	}

	public void PutDownHero(int n)
	{
		heroShow.transform.position = showingBlock.transform.position;
		heroShow.number = n;
		heroShow.image.sprite = datas.heroSprites[n];
		heroShow.gameObject.SetActive(value: true);
		heroShow.x = showingBlock.x;
		heroShow.y = showingBlock.y;
		sound.PlaySound(sound.otherSounds[6]);
	}

	public void NextYear()
	{
		buttomUI.ChangeResource(2, -foodCost);
		datas.NewBlock();
		for (int i = 0; i < wars.Count; i++)
		{
			wars[i].number--;
			wars[i].numberText.text = wars[i].number.ToString();
			if (wars[i].number <= 0)
			{
				wars[i].BeDestroy();
				wars.RemoveAt(i);
			}
		}
		for (int j = 0; j < buildings.Count; j++)
		{
			SetFloatUI(buildings[j].transform.position, 1, 1);
			buttomUI.ChangeResource(1, 1);
		}
		buttomUI.timeScore.NextTurn();
		if (menuData.hard == 0)
		{
			return;
		}
		for (int k = 0; k < hightLimit; k++)
		{
			for (int l = 0; l < wideLimit; l++)
			{
				TC2Block block = blockCells[l, k];
				if (block.kind > 0 && block.kind < 4 && block.number > block.level && (!block.moving || block.moveTime <= 0.15f) && !block.combining && !block.skill)
				{
					block.number--;
					if (block.number == block.level)
					{
						block.numberText.color = new Color(0.25f, 0.15f, 0.15f, 1f);
						block.outline1.effectColor = new Color(1f, 1f, 1f, 1f);
						block.outline2.effectColor = new Color(1f, 1f, 1f, 1f);
					}
					block.numberText.text = block.number.ToString();
				}
			}
		}
	}

	public bool canSkill(int cost)
	{
		if (rock >= cost)
		{
			buttomUI.ChangeResource(3, -cost);
			return true;
		}
		buttomUI.rock.icon.animator.enabled = true;
		return false;
	}

	public void BoomHere(Vector3 v)
	{
		Boom boom = booms[0];
		boom.transform.position = v + new Vector3(0f, 40f, 0f);
		boom.gameObject.SetActive(value: true);
		booms.RemoveAt(0);
		booms.Add(boom);
		shakeCount = 1;
		datas.nuclearBomb++;
		sound.PlaySound(sound.otherSounds[0]);
	}

	public void RocketHere(Vector3 v)
	{
		Transform transform = rockets[0];
		rockets.RemoveAt(0);
		rockets.Add(transform);
		transform.transform.position = v;
		transform.gameObject.SetActive(value: true);
	}

	public void WorldShake()
	{
		if (shakeCount != 0)
		{
			base.transform.position += worldShakes[shakeCount - 1];
			shakeCount++;
			if (shakeCount > worldShakes.Count)
			{
				shakeCount = 0;
			}
		}
	}
	*/
}
