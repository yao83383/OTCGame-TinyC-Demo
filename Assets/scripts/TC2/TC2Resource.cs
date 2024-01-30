using UnityEngine;
using UnityEngine.UI;

public class TC2Resource : MonoBehaviour
{
	public ButtomUI buttomUI;

	public int numberWait;

	public int kind;

	public Animator animator;

	public Text text;

	public string textName;

	public string discription;

	public string englishName;

	public string englishDis;

	public bool bestright;

	public Icon icon;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ChangeText()
	{
		if (numberWait <= 0)
		{
			switch (kind)
			{
			case 1:
				buttomUI.world.food += numberWait;
				break;
			case 2:
				buttomUI.world.rock += numberWait;
				break;
			case 3:
				buttomUI.world.culture += numberWait;
				break;
			}
			numberWait = 0;
		}
		else if (numberWait > 0)
		{
			switch (kind)
			{
			case 1:
				if (numberWait >= 24)
				{
					buttomUI.world.food += 5;
					numberWait -= 5;
				}
				else if (numberWait >= 18)
				{
					buttomUI.world.food += 4;
					numberWait -= 4;
				}
				else if (numberWait >= 12)
				{
					buttomUI.world.food += 3;
					numberWait -= 3;
				}
				else if (numberWait >= 6)
				{
					buttomUI.world.food += 2;
					numberWait -= 2;
				}
				else
				{
					buttomUI.world.food++;
					numberWait--;
				}
				break;
			case 2:
				if (numberWait >= 24)
				{
					buttomUI.world.rock += 5;
					numberWait -= 5;
				}
				else if (numberWait >= 18)
				{
					buttomUI.world.rock += 4;
					numberWait -= 4;
				}
				else if (numberWait >= 12)
				{
					buttomUI.world.rock += 3;
					numberWait -= 3;
				}
				else if (numberWait >= 6)
				{
					buttomUI.world.rock += 2;
					numberWait -= 2;
				}
				else
				{
					buttomUI.world.rock++;
					numberWait--;
				}
				break;
			case 3:
				if (buttomUI.world.culture >= buttomUI.world.datas.nextTechNeed)
				{
					return;
				}
				if (numberWait >= 24)
				{
					buttomUI.world.culture += 5;
					numberWait -= 5;
				}
				else if (numberWait >= 18)
				{
					buttomUI.world.culture += 4;
					numberWait -= 4;
				}
				else if (numberWait >= 12)
				{
					buttomUI.world.culture += 3;
					numberWait -= 3;
				}
				else if (numberWait >= 6)
				{
					buttomUI.world.culture += 2;
					numberWait -= 2;
				}
				else
				{
					buttomUI.world.culture++;
					numberWait--;
				}
				if (buttomUI.world.culture >= buttomUI.world.datas.nextTechNeed)
				{
					icon.animator.enabled = true;
					buttomUI.techAwake.SetActive(value: true);
					buttomUI.world.sound.PlaySound(buttomUI.world.sound.newTech);
				}
				break;
			}
		}
		switch (kind)
		{
		case 1:
			text.text = buttomUI.world.food + "(-" + buttomUI.world.foodCost + ")";
			if (buttomUI.world.food <= buttomUI.world.foodCost * 3)
			{
				text.color = new Color(1f, 0.25f, 0.25f, 1f);
				icon.animator.enabled = true;
				buttomUI.hungry.gameObject.SetActive(value: true);
			}
			else
			{
				text.color = new Color(1f, 1f, 1f, 1f);
				buttomUI.hungry.gameObject.SetActive(value: false);
			}
			break;
		case 2:
			text.text = buttomUI.world.rock.ToString();
			break;
		case 3:
			text.text = buttomUI.world.culture + "/" + buttomUI.world.datas.nextTechNeed;
			buttomUI.cultureFill.fillAmount = (float)buttomUI.world.culture * 1f / (float)buttomUI.world.datas.nextTechNeed;
			break;
		}
	}

	public void EndChange()
	{
		if (numberWait == 0)
		{
			animator.enabled = false;
		}
	}

	public void HightLight()
	{
		buttomUI.world.mouse.information.gameObject.SetActive(value: true);
		if (buttomUI.world.menuData.english == 0)
		{
			buttomUI.world.mouse.inforName.text = textName;
			buttomUI.world.mouse.inforDiscrption.text = discription;
		}
		else
		{
			buttomUI.world.mouse.inforName.text = englishName;
			buttomUI.world.mouse.inforDiscrption.text = englishDis;
		}
		if (bestright)
		{
			buttomUI.world.mouse.information.GetChild(0).localPosition = new Vector3(-180f, 60f, 0f);
		}
	}

	public void NoHightLight()
	{
		buttomUI.world.mouse.information.gameObject.SetActive(value: false);
		if (bestright)
		{
			buttomUI.world.mouse.information.GetChild(0).localPosition = new Vector3(192f, 60f, 0f);
		}
	}

	public void OpenTech()
	{
		if (buttomUI.world.culture >= buttomUI.world.datas.nextTechNeed)
		{
			buttomUI.world.newTech.ShowTech();
		}
	}
}
