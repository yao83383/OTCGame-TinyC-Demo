using UnityEngine;

public class TC2Rocket : MonoBehaviour
{
	public TC2World world;

	public int speed;

	public int setSpeed;

	public AudioClip fire;

	public AudioClip move;

	public bool fly;

	public bool vectory;

	private int flyCount;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (fly)
		{
			if (speed < setSpeed)
			{
				speed++;
			}
			base.transform.parent.position += new Vector3(0f, (float)speed * 0.15f, 0f);
			flyCount++;
			if (flyCount >= 1000)
			{
				flyCount = 0;
				speed = 0;
				base.transform.parent.gameObject.SetActive(value: false);
			}
		}
	}

	public void StartFly()
	{
		if (setSpeed == 0)
		{
			setSpeed = 100;
		}
		fly = true;
		if (!vectory)
		{
			world.buttomUI.ChangeResource(1, 50);
			world.SetFloatUI(base.transform.position, 1, 50);
		}
		speed = 0;
	}

	public void MoveSteel()
	{
		if (!vectory)
		{
			world.sound.PlaySound(move);
		}
	}

	public void StartFire()
	{
		if (!vectory)
		{
			world.sound.PlaySound(fire);
		}
	}
}
