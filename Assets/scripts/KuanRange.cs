using UnityEngine;
using UnityEngine.UI;

public class KuanRange : MonoBehaviour
{
	public World world;

	public bool skill;

	public bool hero;

	public int number;

	public Image image;

	private int left;

	private int right;

	private int up;

	private int down;

	private int wide;

	private int hight;

	private int x;

	private int y;

	private int range;

	public RectTransform rectTransform;

	private void Start()
	{
	}

	private void Update()
	{
		if (world.showingBlock != null && (skill || hero) && world.showingBlock.x > -1 && world.showingBlock.x < world.wideLimit && world.showingBlock.y > -1 && world.showingBlock.y < world.hightLimit)
		{
			x = world.showingBlock.x;
			y = world.showingBlock.y;
			CheckBiggestRange();
			image.enabled = true;
			SetKuan();
		}
		else
		{
			image.enabled = false;
		}
	}

	public void SetKuan()
	{
		if (range == 0)
		{
			base.transform.position = world.showingBlock.transform.position;
			rectTransform.sizeDelta = new Vector2(95f, 95f);
		}
		else
		{
			base.transform.position = (world.blockCells[left, up].transform.position + world.blockCells[left, down].transform.position + world.blockCells[right, up].transform.position + world.blockCells[right, down].transform.position) / 4f;
			rectTransform.sizeDelta = new Vector2(87 * wide, 87 * hight);
		}
	}

	public void CheckBiggestRange()
	{
		range = 0;
		if (skill)
		{
			switch (number)
			{
			case 8:
				range = 1;
				break;
			case 14:
				range = 2;
				break;
			case 9:
				range = 2;
				if (x < 5)
				{
					x = 2;
				}
				else
				{
					x = 7;
				}
				if (y < 5)
				{
					y = 2;
				}
				else
				{
					y = 7;
				}
				break;
			}
		}
		if (hero)
		{
			switch (number)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 17:
			case 18:
			case 19:
				range = 2;
				break;
			case 5:
			case 7:
			case 10:
			case 20:
				range = 3;
				break;
			}
		}
		for (int i = x; i <= x + range; i++)
		{
			if (i < world.wideLimit)
			{
				right = i;
			}
		}
		for (int num = x; num >= x - range; num--)
		{
			if (num > -1)
			{
				left = num;
			}
		}
		for (int j = y; j <= y + range; j++)
		{
			if (j < world.hightLimit)
			{
				up = j;
			}
		}
		for (int num2 = y; num2 >= y - range; num2--)
		{
			if (num2 > -1)
			{
				down = num2;
			}
		}
		wide = right - left + 1;
		hight = up - down + 1;
	}
}
