using UnityEngine;

public class Boom : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void EndBoom()
	{
		base.gameObject.SetActive(value: false);
	}
}
