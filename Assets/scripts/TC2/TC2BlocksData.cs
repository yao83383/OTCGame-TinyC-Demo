using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TC2BlocksData", menuName = "Prefabs/TC2BlocksData")]
public class TC2BlocksData : ScriptableObject
{
    public List<string> kindNames;
	public List<GameObject> kindPrefab;
	public List<Sprite> blockImage;

	private void Start()
	{
	}

	private void Update()
	{
	}
}
