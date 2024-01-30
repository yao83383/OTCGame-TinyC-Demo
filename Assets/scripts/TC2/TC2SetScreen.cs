using UnityEngine;
using UnityEngine.UI;

public class TC2SetScreen : MonoBehaviour
{
	public TC2World world;

	public TC2MenuData menuData;

	public bool inMenu;

	public Image menuSelection;

	public Image menuShow;

	public TC2ButtomUI battleButtomUI;

	public Image battleUI;

	public Image battleWorld;

	private void Start()
	{
		menuData.firstOpen = PlayerPrefs.GetInt("firstOpen");
		if (menuData.firstOpen == 0)
		{
			int width = Screen.width;
			int height = Screen.height;
			PlayerPrefs.SetInt("thishight", height);
			PlayerPrefs.SetInt("thiswide", width);
			Debug.Log(width + "+" + height);
			if ((float)height * 1f / (float)width <= 0.5625f)
			{
				menuData.high = height;
				menuData.wide = width;
			}
			else
			{
				menuData.high = height;
				menuData.wide = Mathf.RoundToInt((float)height * 16f / 9f);
				menuData.window = 1;
			}
			PlayerPrefs.SetInt("hight", menuData.high);
			PlayerPrefs.SetInt("wide", menuData.wide);
			PlayerPrefs.SetInt("window", menuData.window);
			PlayerPrefs.SetInt("screenNumber", 10);
			menuData.firstOpen = 1;
			PlayerPrefs.SetInt("firstOpen", menuData.firstOpen);
		}
		else
		{
			menuData.high = PlayerPrefs.GetInt("hight");
			menuData.wide = PlayerPrefs.GetInt("wide");
			menuData.window = PlayerPrefs.GetInt("window");
		}
		ChangeResolution();
	}

	public void RetSetResolution()
	{
		int @int = PlayerPrefs.GetInt("thiswide");
		int int2 = PlayerPrefs.GetInt("thishight");
		if ((float)int2 * 1f / (float)@int <= 1.7777778f)
		{
			menuData.high = int2;
			menuData.wide = @int;
		}
		else
		{
			menuData.high = int2;
			menuData.wide = Mathf.RoundToInt((float)int2 * 9f / 16f);
			menuData.window = 1;
		}
	}

	public void ChangeResolution()
	{
		bool fullscreen = true;
		if (menuData.window == 1)
		{
			fullscreen = false;
		}
		Screen.SetResolution(menuData.wide, menuData.high, fullscreen);
		if (inMenu)
		{
			SetMainMenu();
		}
		else
		{
			SetBattle();
		}
	}

	public void SetMainMenu()
	{
		float num = (float)menuData.high * 1f / 1080f;
		menuSelection.transform.localScale = new Vector3(num, num, 1f);
		menuShow.transform.localScale = new Vector3(num, num, 1f);
	}

	public void SetBattle()
	{
		float num = (float)menuData.high * 1f / 1080f;
		float num2 = (float)menuData.wide * 1f / 1920f;
		battleButtomUI.transform.localScale = new Vector3(num2, num2, 1f);
		battleButtomUI.transform.localPosition = new Vector3(0f, 540f * (num2 - num), 0f);
		battleWorld.transform.localScale = new Vector3(num, num, 1f);
		battleUI.transform.localScale = new Vector3(num, num, 1f);
		battleButtomUI.esc.localPosition = new Vector3(battleButtomUI.esc.localPosition.x, 490f - 1080f * (num2 - num) / num2, 0f);
		battleButtomUI.heroFather.localPosition = new Vector3(battleButtomUI.heroFather.localPosition.x, 331f - 1080f * (num2 - num) / num2, 0f);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			menuData.firstOpen = 0;
			PlayerPrefs.SetInt("firstOpen", menuData.firstOpen);
		}
	}
}
