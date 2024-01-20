using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHero : MonoBehaviour
{
	public World world;

	public Datas datas;

	public List<int> canPickHeros = new List<int>();

	public List<int> showedHeros = new List<int>();

	public List<HeroSelection> heroSelections = new List<HeroSelection>();

	public GameObject big;

	public GameObject small;

	public Animator animator;

	public bool canPick;

	public Text title;

	public Text title2;

	private void Start()
	{
		if (world.menuData.english == 1)
		{
			title.text = "Choose Hero";
			title2.text = "Choose Hero";
		}
	}

	private void Update()
	{
	}

	public void ShowBig()
	{
		big.gameObject.SetActive(value: true);
		small.gameObject.SetActive(value: false);
	}

	public void ShowSmall()
	{
		big.gameObject.SetActive(value: false);
		small.gameObject.SetActive(value: true);
	}

	public void PickTwoHero()
	{
		int num = world.buttomUI.timeScore.backAge + 1;
		if (canPickHeros.Count == 0)
		{
			num = 0;
		}
		canPickHeros.Clear();
		List<HeroJson> list = new List<HeroJson>();
		for (int i = 0; i < datas.heroJsons.Count; i++)
		{
			if (datas.heroJsons[i].startAge <= num && datas.heroJsons[i].endAge >= num && !showedHeros.Contains(datas.heroJsons[i].number))
			{
				list.Add(datas.heroJsons[i]);
			}
		}
		int index = Random.Range(0, list.Count);
		canPickHeros.Add(list[index].number);
		list.RemoveAt(index);
		int index2 = Random.Range(0, list.Count);
		canPickHeros.Add(list[index2].number);
		list.RemoveAt(index2);
	}

	public void UpdateHero()
	{
		for (int i = 0; i < 2; i++)
		{
			heroSelections[i].number = canPickHeros[i];
			heroSelections[i].image.sprite = datas.heroSprites[canPickHeros[i]];
			if (world.menuData.english == 0)
			{
				heroSelections[i].heroName.text = datas.heroJsons[canPickHeros[i]].name;
				heroSelections[i].discription.text = datas.heroJsons[canPickHeros[i]].discription;
			}
			else
			{
				heroSelections[i].heroName.text = datas.heroJsons[canPickHeros[i]].englishName;
				heroSelections[i].discription.text = datas.heroJsons[canPickHeros[i]].englishDis;
			}
		}
	}

	public void SelectThis(int n)
	{
		if (canPick)
		{
			world.buttomUI.heroNumbers.Add(n);
			world.buttomUI.UpdateHero();
			world.sound.PlaySound(world.sound.newTech);
			showedHeros.Add(n);
			PickTwoHero();
			UpdateHero();
			base.gameObject.SetActive(value: false);
		}
	}

	public void OpenChoose()
	{
		base.gameObject.SetActive(value: true);
		animator.enabled = true;
		canPick = false;
		ShowBig();
		UpdateHero();
	}

	public void EndAnimator()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		animator.enabled = false;
		canPick = true;
	}
}
