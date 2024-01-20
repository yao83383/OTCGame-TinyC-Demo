using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ImportJsonTest : MonoBehaviour
{
    public List<NewTechJson> newTechjsons = new List<NewTechJson>();

    public List<SkillJson> skilljsons = new List<SkillJson>();

    public List<HeroJson> heroJsons = new List<HeroJson>();

    public List<Age> ages = new List<Age>();
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
		newTechjsons.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/newTech.json");
		newTechjsons = JsonConvert.DeserializeObject<List<NewTechJson>>(value);

        print("当前解析的数据：" + newTechjsons[0].name);
        print("当前解析的数据：" + newTechjsons[0].number);
        print("当前解析的数据：" + newTechjsons[0].age);
        print("当前解析的数据：" + newTechjsons[0].culture);
        print("当前解析的数据：" + newTechjsons[0].discription);
        print("当前解析的数据：" + newTechjsons[0].englishName);
        print("当前解析的数据：" + newTechjsons[0].englishDis);


    }

    private void ImportSkillJson()
	{
		skilljsons.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/skill.json");
		skilljsons = JsonConvert.DeserializeObject<List<SkillJson>>(value);
	}

	private void ImportAgeJson()
	{
		ages.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/age.json");
		ages = JsonConvert.DeserializeObject<List<Age>>(value);
	}

	private void ImportHeroJson()
	{
		heroJsons.Clear();
		string value = File.ReadAllText(Application.dataPath + "/Json/hero.json");
		heroJsons = JsonConvert.DeserializeObject<List<HeroJson>>(value);
	}
}
