using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public List<Image> images = new List<Image>();

	public int selection;

	//游戏设置
	public MenuData menuData;

	//读取游戏
	public SaveGame saveGame;

	//制作人员
	public GameObject credit;

	//退出游戏
	public GameObject esc;

	//开始文明
	public GameObject eazySelect;

	//英雄模式
	public GameObject hardSelect;

	public World world;

	public Text creditTitle;

	//制作人员——Text
	public Text maker;
	
	public Text startGame;

	public Text loadGame;

	public Text heroMode;

	public Text setting;

	public Text credit1;

	public Text exitGame;

	public Text easy;

	public Text normal;

	public Text hard;

	public Text hell;

	public Image menutitle;

	public Sprite chineseTitle;

	public Sprite englishTitle;

	private void Start()
	{
		if (menuData.canHard == 0)
		{
			images[2].transform.GetChild(0).GetComponent<Text>().color = new Color(0.3f, 0.25f, 0.2f, 1f);
		}
		else
		{
			images[2].transform.GetChild(0).GetComponent<Text>().color = new Color(0.78f, 0.58f, 0.3f, 1f);
		}
		selection = 100;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			menuData.canHard = 1;
			PlayerPrefs.SetInt("canHard", 1);
			images[2].transform.GetChild(0).GetComponent<Text>().color = new Color(0.78f, 0.58f, 0.3f, 1f);
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			menuData.finishGuide = 0;
			PlayerPrefs.SetInt("finishGuide", 0);
			menuData.finishHardGuide = 0;
			PlayerPrefs.SetInt("finishHardGuide", 0);
			PlayerPrefs.SetInt("firstOpen", 0);
		}
	}

	public void CloseCredit()
	{
		credit.SetActive(value: false);
	}

	public void HightLight(int n)
	{
		if (n != 2 || menuData.canHard != 0)
		{
			selection = n;
			images[n].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
			world.sound.PlaySound(world.sound.selection);
		}
	}

	public void NoHightLight()
	{
		images[selection].transform.localScale = new Vector3(1f, 1f, 1f);
		selection = 100;
	}

	//Hover 播放声音
	public void HoverHightLight()
	{
		world.sound.PlaySound(world.sound.selection);
	}

	//开始游戏按钮
	public void ClickEasyMode()
	{
		eazySelect.SetActive(value: true);
	}

	//保存、读取
    public void ClickSaveGame()
    {
		saveGame.Open(ifsave: false);
		ClickEmptySpace();
	}

	//英雄模式按钮
    public void ClickHardMode()
    {
		hardSelect.SetActive(value: true);
	}

	//退出
    public void ClickEsc()
    {
		esc.SetActive(value: true);
		ClickEmptySpace();
	}

	//制作人员
    public void ClickCredit()
    {
        credit.SetActive(value: true);
        UpdateCredit();
		ClickEmptySpace();
	}

	//确定退出
    public void ClickQuit()
    {
		Application.Quit();
		ClickEmptySpace();
	}

	//简单 按钮
    public void Clickdifficulty1()
    {
        menuData.hard = 0;
        menuData.difficulty = 1;
        SceneManager.LoadScene("SampleScene");
		ClickEmptySpace();
	}
	//普通 按钮
    public void Clickdifficulty2()
    {
        menuData.hard = 0;
        menuData.difficulty = 2;
        SceneManager.LoadScene("SampleScene");
		ClickEmptySpace();
	}

	//英雄 简单
    public void Clickdifficulty3()
    {
        menuData.hard = 1;
        menuData.difficulty = 3;
        SceneManager.LoadScene("SampleScene");
		ClickEmptySpace();
	}

	//英雄 困难
    public void Clickdifficulty4()
    {
        menuData.hard = 1;
        menuData.difficulty = 4;
        SceneManager.LoadScene("SampleScene");
    }

	public void ClickEmptySpace()
    {
		//if (selection != 0)
		{
			eazySelect.SetActive(value: false);
		}
		//if (selection != 2)
		{
		    hardSelect.SetActive(value: false);
		}
	}


    //public void Click()
	//{
	//	if (selection == 100)
	//	{
	//		eazySelect.SetActive(value: false);
	//		hardSelect.SetActive(value: false);
	//		return;
	//	}
	//	saveGame.world.sound.PlaySound(saveGame.world.sound.openMenu);
	//	switch (selection)
	//	{
	//	case 0:
	//		eazySelect.SetActive(value: true);
	//		break;
	//	case 1:
	//		saveGame.Open(ifsave: false);
	//		break;
	//	case 2:
	//		hardSelect.SetActive(value: true);
	//		break;
	//	case 3:
	//		esc.SetActive(value: true);
	//		break;
	//	case 4:
	//		credit.SetActive(value: true);
	//		UpdateCredit();
	//		break;
	//	case 5:
	//		Application.Quit();
	//		break;
	//	case 11:
	//		menuData.hard = 0;
	//		menuData.difficulty = 1;
	//		SceneManager.LoadScene("SampleScene");
	//		break;
	//	case 12:
	//		menuData.hard = 0;
	//		menuData.difficulty = 2;
	//		SceneManager.LoadScene("SampleScene");
	//		break;
	//	case 13:
	//		menuData.hard = 1;
	//		menuData.difficulty = 3;
	//		SceneManager.LoadScene("SampleScene");
	//		break;
	//	case 14:
	//		menuData.hard = 1;
	//		menuData.difficulty = 4;
	//		SceneManager.LoadScene("SampleScene");
	//		break;
	//	}
	//	if (selection != 0)
	//	{
	//		eazySelect.SetActive(value: false);
	//	}
	//	if (selection != 2)
	//	{
	//		hardSelect.SetActive(value: false);
	//	}
	//}

	public void UpdateCredit()
	{
		if (world.menuData.english == 0)
		{
			creditTitle.text = "制作人员";
			maker.text = "策划&程序：OTC\r\n 美术：OTC，catiscute";
		}
		else
		{
			creditTitle.text = "Credit";
			maker.text = "Game design&Code:OTC\r\n Art:OTC，Catiscute";
		}
	}

	public void ChangeMenu()
	{
		if (menuData.english == 1)
		{
			startGame.text = "Start Game";
			loadGame.text = "Load Game";
			heroMode.text = "Hero Mode";
			setting.text = "Setting";
			exitGame.text = "Exit";
			credit1.text = "Credit";
			easy.text = "Easy";
			normal.text = "Normal";
			hard.text = "Hard";
			hell.text = "Hell";
			menutitle.sprite = englishTitle;
		}
		else
		{
			startGame.text = "开始文明";
			loadGame.text = "读取游戏";
			heroMode.text = "英雄模式";
			setting.text = "游戏设置";
			exitGame.text = "退出游戏";
			credit1.text = "制作人员";
			easy.text = "容易";
			normal.text = "普通";
			hard.text = "困难";
			hell.text = "极难";
			menutitle.sprite = chineseTitle;
		}
	}
}
