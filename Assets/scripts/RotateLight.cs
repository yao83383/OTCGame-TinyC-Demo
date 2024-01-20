using UnityEngine;
using UnityEngine.UI;

public class RotateLight : MonoBehaviour
{
	public Image image1;

	public Image image2;

	private Vector3 v1 = new Vector3(0.8f, 0.68f, 0.59f);

	private Vector3 v2 = new Vector3(0.79f, 0.74f, 0.54f);

	public int disAppear;

	public World world;

	public bool stay;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
		if (stay)
		{
			return;
		}
		disAppear--;
		if (disAppear < 51)
		{
			image1.color = new Color(v1.x, v1.y, v1.z, (float)disAppear * 0.01f);
			image2.color = new Color(v2.x, v2.y, v2.z, (float)disAppear * 0.01f);
			if (disAppear == 0)
			{
				world.buttomUI.UpdateSkill();
				base.gameObject.SetActive(value: false);
				disAppear = 51;
			}
		}
		else if (disAppear < 75)
		{
			image1.color = new Color(v1.x, v1.y, v1.z, (float)(75 - disAppear) * 0.02f);
			image2.color = new Color(v2.x, v2.y, v2.z, (float)(75 - disAppear) * 0.02f);
		}
	}

	public void StartLight(bool ifDisAppear)
	{
		base.gameObject.SetActive(value: true);
		image1.color = new Color(v1.x, v1.y, v1.z, 0.5f);
		image2.color = new Color(v2.x, v2.y, v2.z, 0.5f);
		if (ifDisAppear)
		{
			disAppear = 50;
		}
		else
		{
			disAppear = 51;
		}
	}

	public void Build()
	{
		base.gameObject.SetActive(value: true);
		image1.color = new Color(v1.x, v1.y, v1.z, 0f);
		image2.color = new Color(v2.x, v2.y, v2.z, 0f);
		disAppear = 80;
	}
}
