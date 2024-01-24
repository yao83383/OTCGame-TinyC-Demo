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

        print("��ǰ���������ݣ�" + newTechjsons[0].name);
        print("��ǰ���������ݣ�" + newTechjsons[0].number);
        print("��ǰ���������ݣ�" + newTechjsons[0].age);
        print("��ǰ���������ݣ�" + newTechjsons[0].culture);
        print("��ǰ���������ݣ�" + newTechjsons[0].discription);
        print("��ǰ���������ݣ�" + newTechjsons[0].englishName);
        print("��ǰ���������ݣ�" + newTechjsons[0].englishDis);


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

	private void CSVLoad()
	{ 
		// TODO - loadFile 

		// TODO - deserialize to TechJson or agetJson type etc.
	}

	private void ImportTechCSV()
	{ 
		// TODO - read from csv file

		// TODO - write to TechJson
	}

	private void ImportSkillCSV()
	{ 
		// TODO - read from csv file

		// TODO - write to skillJsons
	}

	private void ImportAgeCSV()
    {
		// TODO - read from csv file

		// TODO - write to ageJsons
    }

	private void ImportHeroCSV()
	{ 
		// TODO - read from csv file

		// TODO - write to heroJsons
	}


}
