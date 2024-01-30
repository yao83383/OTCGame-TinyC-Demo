using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class TC2ImportJson : MonoBehaviour
{
	[HideInInspector]
	private TC2World world;

	private void Start()
	{
		world = GetComponent<TC2World>();
		ImportTechJson();
		ImportSkillJson();
		ImportAgeJson();
		ImportHeroJson();

		ImportBlockSlotJson();
		ImportBlockJson();
	}

	private void ImportBlockSlotJson()
	{
        world.datas.BlockSlotJson.Clear();
        string value = File.ReadAllText(Application.dataPath + "/Json/TC2BlockSlot.json");
        world.datas.BlockSlotJson = JsonConvert.DeserializeObject<List<TC2BlockSlotJson>>(value);
    }

    private void ImportBlockJson()
    {
        world.datas.BlockSlotJson.Clear();
        string value = File.ReadAllText(Application.dataPath + "/Json/TC2Block.json");
        world.datas.BlockJson = JsonConvert.DeserializeObject<List<TC2BlockJson>>(value);
    }

    private void ImportTechJson()
	{
		world.datas.newTechjsons.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/newTech.json");
		world.datas.newTechjsons = JsonConvert.DeserializeObject<List<TC2NewTechJson>>(value);
	}

	private void ImportSkillJson()
	{
		world.datas.skilljsons.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/skill.json");
		world.datas.skilljsons = JsonConvert.DeserializeObject<List<TC2SkillJson>>(value);
	}


	private void ImportAgeJson()
	{
		world.datas.ages.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/age.json");
		world.datas.ages = JsonConvert.DeserializeObject<List<TC2Age>>(value);
	}

	private void ImportHeroJson()
	{
		world.datas.heroJsons.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/hero.json");
		world.datas.heroJsons = JsonConvert.DeserializeObject<List<TC2HeroJson>>(value);
	}
}
