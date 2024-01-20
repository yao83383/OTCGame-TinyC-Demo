using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
	public Block block;

	public Skill skill;

	public Transform information;

	public Text inforName;

	public Text inforDiscrption;

	private void Start()
	{
		Cursor.visible = false;
	}

	private void Update()
	{
		base.transform.position = Input.mousePosition;
	}
}
