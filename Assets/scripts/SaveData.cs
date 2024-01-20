using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
	public int number;

	public int hard;

	public int difficulty;

	public bool exist;

	public string age;

	public string year;

	public string time;

	public int Scoreturn;

	public int ScoreYear;

	public int ScoreAge;

	public int ScroeLastTurn;

	public List<int> unlockTechs = new List<int>();

	public int nowTech;

	public int nowAge;

	public int nextTechNeed;

	public List<int> limits = new List<int>();

	public List<int> lows = new List<int>();

	public List<int> gailv = new List<int>();

	public List<int> pool = new List<int>();

	public int poolNumber;

	public List<int> skillCounts = new List<int>();

	public int foodProduction;

	public int industryProduction;

	public int cultureProduction;

	public int combineCount;

	public int wonderBuild;

	public int AddReported;

	public int monsterKill;

	public int wars;

	public int nuclearBomb;

	public int hightLimit;

	public int wideLimit;

	public int speed;

	public List<BlockData> blockDatas = new List<BlockData>();

	public int food;

	public int rock;

	public int culture;

	public int foodCost;

	public List<int> skillNumbers = new List<int>();

	public List<int> heroNumbers = new List<int>();

	public List<int> canPickHeros = new List<int>();

	public List<int> showedHeros = new List<int>();
}
