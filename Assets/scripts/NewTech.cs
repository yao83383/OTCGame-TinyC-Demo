using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTech : MonoBehaviour
{
	public Text title;

	public Text discription;

	public Text other;

	public Text ageName;

	public Text ageDiscription;

	public World world;

	public Animator animator;

	public bool canClick;

	public bool newAge;

	public List<TechSelection> techSelections = new List<TechSelection>();

	public Transform agePage;

	public List<NewTechJson> canUsedTechs = new List<NewTechJson>();

	public List<Sprite> techSprites = new List<Sprite>();

	private void Start()
	{
	}

	private void Update()
	{
		if (newAge && Input.GetMouseButtonDown(0))
		{
			newAge = false;
			animator.enabled = true;
		}
	}

	public void ShowTech()
	{
		if (world.menuData.english == 1)
		{
			title.text = "New Technology";
		}
		if (Time.timeScale == 0f)
		{
			return;
		}
		Time.timeScale = 0f;
		base.gameObject.SetActive(value: true);
		animator.enabled = true;
		canUsedTechs.Clear();
		//read all canusetech to list
		for (int i = 0; i < world.datas.newTechjsons.Count; i++)
		{
			if (!world.datas.unlockTechs.Contains(i) && world.datas.newTechjsons[i].age == world.datas.nowAge)
			{
				canUsedTechs.Add(world.datas.newTechjsons[i]);
			}
		}
		//age tech all updated then to new age
		if (canUsedTechs.Count < 1)
		{
			world.datas.nowAge++;
			for (int j = 0; j < world.datas.newTechjsons.Count; j++)
			{
				if (!world.datas.unlockTechs.Contains(j) && world.datas.newTechjsons[j].age == world.datas.nowAge)
				{
					canUsedTechs.Add(world.datas.newTechjsons[j]);
				}
			}
		}
		//show leading 3 tech or less if canuseTechs less than 3
		for (int k = 0; k < 3; k++)
		{
			if (k < canUsedTechs.Count)
			{
				TechSelection techSelection = techSelections[k];
				techSelection.gameObject.SetActive(value: true);
				techSelection.number = canUsedTechs[k].number;
				techSelection.image.sprite = techSprites[techSelection.number - 1];
				if (world.menuData.english == 0)
				{
					techSelection.techName.text = canUsedTechs[k].name;
					techSelection.discription.text = canUsedTechs[k].discription;
				}
				else
				{
					techSelection.techName.text = canUsedTechs[k].englishName;
					techSelection.discription.text = canUsedTechs[k].englishDis;
				}
			}
			else
			{
				techSelections[k].gameObject.SetActive(value: false);
			}
		}
		//?
		if (world.menuData.hard == 0)
		{
			if (world.datas.nowTech == 7)
			{
				world.datas.gailv[2] += 2;
			}
			if (world.datas.nowTech == 17)
			{
				world.datas.gailv[2] -= 2;
			}
		}
	}

	public void StopAppear()
	{
		animator.enabled = false;
		animator.SetBool("appear", value: false);
		canClick = true;
	}

	public void DisAppear()
	{
		animator.enabled = false;
		animator.SetBool("appear", value: true);
		canClick = false;
		Time.timeScale = 1f;
	}

	public void SelectTech(int number)
	{
		world.datas.unlockTechs.Add(number);
		world.datas.CheckTech(number);
		world.culture -= world.datas.nextTechNeed;
		world.datas.nowTech++;
		switch (world.menuData.difficulty)
		{
		case 1:
			world.datas.nextTechNeed = world.datas.nowTech + 7 + 4 * world.datas.nowAge;
			if (world.datas.nowAge == 6 && canUsedTechs.Count == 1)
			{
				world.datas.nextTechNeed = 300;
			}
			break;
		case 2:
			world.datas.nextTechNeed = world.datas.nowTech * 2 + 6 + 5 * world.datas.nowAge;
			if (world.datas.nowAge == 6 && canUsedTechs.Count == 1)
			{
				world.datas.nextTechNeed = 300;
			}
			break;
		case 3:
			world.datas.nextTechNeed = world.datas.nowTech * 4 + 10;
			if (world.datas.nowAge == 6 && canUsedTechs.Count == 1)
			{
				world.datas.nextTechNeed = 800;
			}
			break;
		case 4:
			world.datas.nextTechNeed = world.datas.nowTech * 5 + 10 + 5 * world.datas.nowAge;
			if (world.datas.nowAge == 6 && canUsedTechs.Count == 1)
			{
				world.datas.nextTechNeed = 800;
			}
			break;
		}
		world.buttomUI.culture.ChangeText();
		if (canUsedTechs.Count == 1)
		{
			world.datas.nowAge++;
		}
		animator.enabled = true;
		world.buttomUI.techAwake.SetActive(value: false);
		canClick = false;
	}

	public void Close()
	{
		if (canClick)
		{
			animator.enabled = true;
		}
	}
}
