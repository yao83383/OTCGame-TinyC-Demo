using UnityEngine;
using UnityEngine.UI;

public class FloatUI : MonoBehaviour
{
	public Image image;

	public Text text;

	public Animator animator;

	public void EndAnimator()
	{
		base.gameObject.SetActive(value: false);
	}
}
