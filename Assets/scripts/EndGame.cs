using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
	public Image endGame;

	public Image fill;

	public World world;

	public float fillAmount;

	public Text title;

	public Text text;

	private int endAmount;

	private void Start()
	{
		if (world.menuData.english == 1)
		{
			title.text = "Game Over";
			text.text = "Hold left button back to menu.";
		}
	}

	private void FixedUpdate()
	{
		if (endAmount == 0)
		{
			endAmount++;
			EndSound();
		}
		if (Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			fillAmount += 0.01f;
			fill.fillAmount = fillAmount;
			if (fillAmount > 1f)
			{
				SceneManager.LoadScene("Menu");
			}
		}
	}

	public void EndSound()
	{
		world.sound.PlaySound(world.sound.otherSounds[7]);
	}
}
