using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
	public int number;

	public Image image;

	public World world;

	public bool holding;

	private void Start()
	{
	}

	private void Update()
	{
		if (world.showingHero == this && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
		{
			StartDrag();
			holding = true;
		}
		if (holding && (Input.GetMouseButtonUp(0) || Input.touchCount == 0))
		{
			EndDrag();
			holding = false;
		}
	}

	public void UpdateHero(int n)
	{
		number = n;
		image.sprite = world.datas.heroSprites[number];
	}

	public void StartDrag()
	{
		image.sprite = world.datas.heroSprites[31];
		world.PickUpHero(number);
		world.kuanRange.number = number;
		world.kuanRange.hero = true;
	}

	public void EndDrag()
	{
		image.sprite = world.datas.heroSprites[number];
		world.mouse.skill.gameObject.SetActive(value: false);
		world.kuanRange.hero = false;
		if (world.showingBlock != null)
		{
			world.PutDownHero(number);
			world.buttomUI.heroNumbers.Remove(number);
			world.buttomUI.UpdateHero();
		}
		else
		{
			image.sprite = world.datas.heroSprites[number];
		}
	}

	public void HightLight()
	{
		base.transform.localScale = new Vector3(1.07f, 1.07f, 1f);
		world.showingHero = this;
		world.mouse.information.gameObject.SetActive(value: true);
		world.mouse.information.localPosition = new Vector3(0f, -150f, 0f);
		if (world.menuData.english == 0)
		{
			world.mouse.inforName.text = world.datas.heroJsons[number].name;
			world.mouse.inforDiscrption.text = world.datas.heroJsons[number].discription;
		}
		else
		{
			world.mouse.inforName.text = world.datas.heroJsons[number].englishName;
			world.mouse.inforDiscrption.text = world.datas.heroJsons[number].englishDis;
		}
	}

	public void NoHighLight()
	{
		world.mouse.information.localPosition = new Vector3(0f, 0f, 0f);
		world.showingHero = null;
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		world.mouse.information.gameObject.SetActive(value: false);
	}
}
