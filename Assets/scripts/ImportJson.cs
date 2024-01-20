using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ImportJson : MonoBehaviour
{
	public World world;

	private void Start()
	{
		ImportTechJson();
		ImportSkillJson();
		ImportAgeJson();
		ImportHeroJson();
	}

	private void Update()
	{
	}

	private void ImportTechJson()
	{
		world.datas.newTechjsons.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/newTech.json");
		world.datas.newTechjsons = JsonConvert.DeserializeObject<List<NewTechJson>>(value);
	}

	private void ImportSkillJson()
	{
		world.datas.skilljsons.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/skill.json");
		world.datas.skilljsons = JsonConvert.DeserializeObject<List<SkillJson>>(value);
	}

	private void ImportAgeJson()
	{
		world.datas.ages.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/age.json");
		world.datas.ages = JsonConvert.DeserializeObject<List<Age>>(value);
	}

	private void ImportHeroJson()
	{
		world.datas.heroJsons.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/hero.json");
		world.datas.heroJsons = JsonConvert.DeserializeObject<List<HeroJson>>(value);
	}
}
