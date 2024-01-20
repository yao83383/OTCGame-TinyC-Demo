using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Esc : MonoBehaviour
{
	public AudioMixer audioMixer;

	public Slider sliderMusic;

	public Slider sliderSound;

	public World world;

	public int wide;

	public int hight;

	public int window;

	public Text screen;

	public Text full;

	public Text language;

	public MenuData menuData;

	public List<Vector2Int> screens = new List<Vector2Int>();

	public int screenNumber;

	public Text title;

	public Text music;

	public Text sound;

	public Text load;

	public Text exist;

	public Text back;

	public bool inBattle;

	public Text startGame;

	public Text loadGame;

	public Text heroMode;

	public Text setting;

	public Text credit;

	public Text exitGame;

	public Text easy;

	public Text normal;

	public Text hard;

	public Text hell;

	public void ChangeMusic(float f)
	{
		if (sliderMusic.value < -20f)
		{
			audioMixer.SetFloat("musicSound", -100f);
		}
		else
		{
			audioMixer.SetFloat("musicSound", sliderMusic.value);
		}
	}

	public void ChangeSound(float f)
	{
		if (sliderSound.value < -20f)
		{
			audioMixer.SetFloat("soundSound", -100f);
		}
		else
		{
			audioMixer.SetFloat("soundSound", sliderSound.value);
		}
	}

	public void CloseEsc()
	{
		PlayerPrefs.SetInt("screenNumber", screenNumber);
		PlayerPrefs.SetInt("hight", menuData.high);
		PlayerPrefs.SetInt("wide", menuData.wide);
		PlayerPrefs.SetInt("window", menuData.window);
		PlayerPrefs.SetFloat("sound", sliderSound.value);
		PlayerPrefs.SetFloat("music", sliderMusic.value);
		world.openMenu = null;
		base.gameObject.SetActive(value: false);
	}

	public void CloseGame()
	{
		PlayerPrefs.SetInt("screenNumber", screenNumber);
		PlayerPrefs.SetInt("hight", menuData.high);
		PlayerPrefs.SetInt("wide", menuData.wide);
		PlayerPrefs.SetInt("window", menuData.window);
		PlayerPrefs.SetFloat("sound", sliderSound.value);
		PlayerPrefs.SetFloat("music", sliderMusic.value);
		Application.Quit();
	}

	public void OpenSave()
	{
		PlayerPrefs.SetInt("screenNumber", screenNumber);
		PlayerPrefs.SetInt("hight", menuData.high);
		PlayerPrefs.SetInt("wide", menuData.wide);
		PlayerPrefs.SetInt("window", menuData.window);
		PlayerPrefs.SetFloat("sound", sliderSound.value);
		PlayerPrefs.SetFloat("music", sliderMusic.value);
		world.saveGame.Open(ifsave: false);
	}

	private void Awake()
	{
		sliderMusic.value = PlayerPrefs.GetFloat("music");
		sliderSound.value = PlayerPrefs.GetFloat("sound");
		screenNumber = PlayerPrefs.GetInt("screenNumber");
		UpdateLanguage();
	}

	public void ChangeNumber(int add)
	{
		screenNumber += add;
		if (screenNumber > 10)
		{
			screenNumber = 0;
		}
		if (screenNumber < 0)
		{
			screenNumber = 10;
		}
		if (screenNumber == 10)
		{
			world.setScreen.RetSetResolution();
		}
		else
		{
			menuData.wide = screens[screenNumber].x;
			menuData.high = screens[screenNumber].y;
		}
		UpdateScreen();
	}

	public void ChangeFull()
	{
		if (menuData.window == 1)
		{
			menuData.window = 0;
		}
		else
		{
			menuData.window = 1;
		}
		UpdateScreen();
	}

	public void ChangeEnglish()
	{
		if (menuData.english == 1)
		{
			menuData.english = 0;
		}
		else
		{
			menuData.english = 1;
		}
		PlayerPrefs.SetInt("english", menuData.english);
		UpdateLanguage();
	}

	public void UpdateLanguage()
	{
		if (menuData.english == 1)
		{
			language.text = "English";
			title.text = "Setting";
			music.text = "Music";
			sound.text = "Sound";
			load.text = "Load Game";
			exist.text = "Exit";
			back.text = "Back to menu";
		}
		else
		{
			language.text = "中文";
			title.text = "设置";
			music.text = "音乐";
			sound.text = "音效";
			load.text = "读取游戏";
			exist.text = "退出游戏";
			back.text = "退回主菜单";
		}
		if (!inBattle)
		{
			world.mainMenu.ChangeMenu();
		}
		UpdateScreen();
	}

	public void BackToMenu()
	{
		PlayerPrefs.SetInt("screenNumber", screenNumber);
		PlayerPrefs.SetInt("hight", menuData.high);
		PlayerPrefs.SetInt("wide", menuData.wide);
		PlayerPrefs.SetInt("window", menuData.window);
		PlayerPrefs.SetFloat("sound", sliderSound.value);
		PlayerPrefs.SetFloat("music", sliderMusic.value);
		SceneManager.LoadScene("Menu");
	}

	private void UpdateScreen()
	{
		screen.text = menuData.wide + "*" + menuData.high;
		if (menuData.window == 0)
		{
			if (menuData.english == 0)
			{
				full.text = "全屏幕";
			}
			else
			{
				full.text = "Full Screen";
			}
		}
		else if (menuData.english == 0)
		{
			full.text = "窗口化";
		}
		else
		{
			full.text = "Window";
		}
		world.setScreen.ChangeResolution();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			CloseEsc();
		}
	}
}
