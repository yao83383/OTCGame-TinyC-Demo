using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtomUI : MonoBehaviour
{
	public Resource food;

	public Resource rock;

	public Resource culture;

	public World world;
	[HideInInspector]
	public List<Skill> skills = new List<Skill>();
	[HideInInspector]
	public List<int> skillNumbers = new List<int>();
	[HideInInspector]
	public List<Hero> heroes = new List<Hero>();
	[HideInInspector]
	public List<int> heroNumbers = new List<int>();

	public Image cultureFill;

	public Image hungry;

	public TimeScore timeScore;

	public GameObject techAwake;

	public Transform heroFather;

	public Transform esc;

	public Text hungryText;

	private void Start()
	{
		UpdateSkill();
		UpdateHero();
		timeScore.UpdateTime();
		if (world.menuData.english == 0)
		{
			hungryText.text = "食物告急！";
		}
		else
		{
			hungryText.text = "Hungry!";
		}
	}

	private void Update()
	{
	}

	public void UpdateHero()
	{
		for (int i = 0; i < heroes.Count; i++)
		{
			if (i >= heroNumbers.Count)
			{
				heroes[i].gameObject.SetActive(value: false);
				continue;
			}
			heroes[i].gameObject.SetActive(value: true);
			heroes[i].UpdateHero(heroNumbers[i]);
		}
	}

	public void UpdateSkill()
	{
		for (int i = 0; i < skills.Count; i++)
		{
			if (i >= skillNumbers.Count)
			{
				skills[i].gameObject.SetActive(value: false);
				continue;
			}
			skills[i].gameObject.SetActive(value: true);
			skills[i].UpdateSkill(skillNumbers[i]);
		}
	}

	public void ChangeResource(int kind, int number)
	{
		switch (kind)
		{
		case 2:
			food.numberWait += number;
			if (number > 0)
			{
				world.datas.foodProduction += number;
			}
			food.animator.enabled = true;
			break;
		case 3:
			rock.numberWait += number;
			if (number > 0)
			{
				world.datas.industryProduction += number;
			}
			rock.animator.enabled = true;
			break;
		case 1:
			culture.numberWait += number;
			if (number > 0)
			{
				world.datas.cultureProduction += number;
			}
			culture.animator.enabled = true;
			break;
		}
	}

	public void AllChange()
	{
		food.text.text = world.food + "(-" + world.foodCost + ")";
		if (world.food <= world.foodCost * 5)
		{
			food.text.color = new Color(1f, 0.25f, 0.25f, 1f);
			food.icon.animator.enabled = true;
			hungry.gameObject.SetActive(value: true);
		}
		else
		{
			food.text.color = new Color(1f, 1f, 1f, 1f);
			hungry.gameObject.SetActive(value: false);
		}
		rock.text.text = world.rock.ToString();
		culture.text.text = world.culture + "/" + world.datas.nextTechNeed;
		cultureFill.fillAmount = (float)world.culture * 1f / (float)world.datas.nextTechNeed;
		if (world.culture == world.datas.nextTechNeed)
		{
			culture.icon.animator.enabled = true;
		}
	}
}
