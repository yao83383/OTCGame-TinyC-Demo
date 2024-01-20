using UnityEngine;
using UnityEngine.UI;

public class HeroSelection : MonoBehaviour
{
	public int number;

	public Image image;

	public Text heroName;

	public Text discription;

	public ChooseHero chooseHero;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void HightLight()
	{
		base.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
		chooseHero.world.sound.PlaySound(chooseHero.world.sound.selection);
	}

	public void NoHightLight()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void SelectThis()
	{
		chooseHero.SelectThis(number);
	}
}
