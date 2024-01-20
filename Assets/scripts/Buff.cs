using UnityEngine;

public class Buff : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void CloseBuff()
	{
		base.gameObject.SetActive(value: false);
	}
}
