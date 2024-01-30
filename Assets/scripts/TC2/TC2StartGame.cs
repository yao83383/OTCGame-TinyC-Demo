using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TC2StartGame : MonoBehaviour
{
	public World world;

	public Text title;

	public List<Transform> texts = new List<Transform>();

	public List<Transform> hardTexts = new List<Transform>();

	private int count;

	private void Start()
	{
		AllPosition();
		if (world.menuData.english == 1)
		{
			ChangeEnglish();
		}
	}

	private void ChangeEnglish()
	{
		title.text = "Right click to continue.";
		texts[0].GetComponent<Text>().text = "You can drag block or switch with another block.  If there are 3 same blocks together,they will combine and gather resources.   Every move or combine will make a new block.  You can not move now because the game hasn't started yet.";
		texts[1].GetComponent<Text>().text = "Food will be consumed every move.  The game ends when the food is less than zero.";
		texts[2].GetComponent<Text>().text = "Industry will be consumed after using skills.";
		texts[3].GetComponent<Text>().text = "The accumulation of culture will discover new technology.";
		texts[4].GetComponent<Text>().text = "Drag the skill onto block to use.";
		texts[5].GetComponent<Text>().text = "Please lead humans to Victory!";
		hardTexts[0].GetComponent<Text>().text = "There are rich resources when the block appear,\r\n but become less every turn.";
		hardTexts[1].GetComponent<Text>().text = "It costs more resources in Hero Mode,\r\n so plan more carefully pleace.";
		hardTexts[2].GetComponent<Text>().text = "You can choose heroes in every new Era,\r\n they will make a huge difference if be used appropriatly";
		hardTexts[3].GetComponent<Text>().text = "Please lead humans to Victory!";
	}

	private void Update()
	{
		if (!Input.GetMouseButtonDown(1))
		{
			return;
		}
		if (world.menuData.hard == 0)
		{
			texts[count].gameObject.SetActive(value: false);
			count++;
			if (count < texts.Count)
			{
				texts[count].gameObject.SetActive(value: true);
				return;
			}
			base.gameObject.SetActive(value: false);
			world.menuData.finishGuide = 1;
			PlayerPrefs.SetInt("finishGuide", world.menuData.finishGuide);
			return;
		}
		hardTexts[count].gameObject.SetActive(value: false);
		count++;
		if (count == 2)
		{
			world.chooseHero.PickTwoHero();
			world.chooseHero.OpenChoose();
		}
		if (count < hardTexts.Count)
		{
			hardTexts[count].gameObject.SetActive(value: true);
			return;
		}
		base.gameObject.SetActive(value: false);
		world.menuData.finishHardGuide = 1;
		PlayerPrefs.SetInt("finishHardGuide", world.menuData.finishHardGuide);
	}

	private void AllPosition()
	{
		if (world.menuData.hard == 0)
		{
			texts[0].gameObject.SetActive(value: true);
		}
		else
		{
			hardTexts[0].gameObject.SetActive(value: true);
		}
		float num = (float)world.menuData.high * 1f / 1080f;
		float num2 = (float)world.menuData.wide * 1f / 1920f;
		texts[0].transform.localPosition = new Vector3(0f, 300f, 0f) * num2 / num;
		texts[1].transform.localPosition = (new Vector3(0f, 200f, 0f) + world.buttomUI.food.transform.parent.localPosition + world.buttomUI.transform.localPosition) * num2 / num;
		texts[2].transform.localPosition = (new Vector3(0f, 200f, 0f) + world.buttomUI.rock.transform.parent.localPosition + world.buttomUI.transform.localPosition) * num2 / num;
		texts[3].transform.localPosition = (new Vector3(-100f, 200f, 0f) + world.buttomUI.culture.transform.parent.localPosition + world.buttomUI.transform.localPosition) * num2 / num;
		texts[4].transform.localPosition = (new Vector3(-100f, 100f, 0f) + world.buttomUI.skills[0].transform.parent.localPosition + world.buttomUI.transform.localPosition) * num2 / num;
		hardTexts[0].transform.localPosition = new Vector3(0f, 300f, 0f) * num2 / num;
		hardTexts[1].transform.localPosition = (new Vector3(100f, 200f, 0f) + world.buttomUI.food.transform.parent.localPosition + world.buttomUI.transform.localPosition) * num2 / num;
	}
}
