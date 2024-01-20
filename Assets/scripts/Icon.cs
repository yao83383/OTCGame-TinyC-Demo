using UnityEngine;

public class Icon : MonoBehaviour
{
	public Animator animator;

	public int kind;

	public World world;

	public void AnimatorEnd()
	{
		switch (kind)
		{
		case 1:
			if (world.food > world.foodCost * 5)
			{
				animator.enabled = false;
			}
			break;
		case 2:
			animator.enabled = false;
			break;
		case 3:
			if (world.culture < world.datas.nextTechNeed)
			{
				animator.enabled = false;
			}
			break;
		}
	}
}
