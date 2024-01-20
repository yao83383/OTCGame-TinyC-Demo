using UnityEngine;
using UnityEngine.UI;

public class TechSelection : MonoBehaviour
{
	public Text techName;

	public Text discription;

	public Image image;

	public int number;

	public NewTech newTech;

	public Outline outline;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void HightLight()
	{
		base.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
		outline.enabled = true;
		newTech.world.sound.PlaySound(newTech.world.sound.selection);
		image.color = new Color(0.8f, 0.8f, 0.8f, 1f);
	}

	public void NoHightLight()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		outline.enabled = false;
		image.color = new Color(0.3f, 0.3f, 0.3f, 1f);
	}

	public void SetetThis()
	{
		if (newTech.canClick)
		{
			newTech.SelectTech(number);
			newTech.world.sound.PlaySound(newTech.world.sound.newTech);
		}
	}
}
