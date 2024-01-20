using UnityEngine;
using UnityEngine.UI;

public class SaveCell : MonoBehaviour
{
	public Text age;

	public Text year;

	public Text time;

	public Text hard;

	public Text difficulty;

	public SaveGame saveGame;

	public int number;

	public Outline outline;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Delete()
	{
		saveGame.Delete(number);
	}

	public void SaveOrLoad()
	{
		saveGame.SaveOrLoad(number);
	}

	public void HightLight()
	{
		base.transform.localScale = new Vector3(1.05f, 1.05f, 1f);
		outline.enabled = true;
		saveGame.showingSaveCell = this;
		saveGame.world.sound.PlaySound(saveGame.world.sound.selection);
	}

	public void NoHightLight()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		outline.enabled = false;
		saveGame.showingSaveCell = null;
	}
}
