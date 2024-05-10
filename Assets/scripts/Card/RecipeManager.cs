using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

using System;  
using System.Security.Cryptography;  
using System.Text;  
  
// 假设的配方类  
[System.Serializable]
public class Recipe  
{  
    public Dictionary<int, int> Ingredients { get; set; } // 原料和数量  
    public List<int> Output { get; set; } // 输出物品  
  
    public Recipe(Dictionary<int, int> ingredients, List<int> output)  
    {  
        Ingredients = ingredients;  
        Output = output;  
    }  

    public List<int> inputs_items = new List<int>();
    public List<int> inputs_nums = new List<int>();
    public List<int> outputs_items = new List<int>();
    public List<int> outputs_nums = new List<int>();
    public float combinetime;

    public List<RecipeElem> inputs = new List<RecipeElem>();
    public List<RecipeElem> outputs = new List<RecipeElem>();
    // 其他可能的属性，如合成所需的数量、合成成功率等
    //public float SuccessRate = 1;

}  


public class RecipeManager : MonoBehaviour
{
	//Create HashMap to save Recipe---------------------------------------------------------------------
	private Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();  
  
    // 添加配方到哈希表中  
    public void AddRecipe(Recipe recipe)  
    {  
        string hash = GenerateMD5Hash(SerializeIngredients(recipe.Ingredients));  
        recipes[hash] = recipe;  
    }  
  
    // 查找合成配方  
    public Recipe FindRecipe(Dictionary<int, int> ingredients)  
    {  
        string hash = GenerateMD5Hash(SerializeIngredients(ingredients));  
        if (recipes.ContainsKey(hash))  
        {  
            return recipes[hash];  
        }  
        return null;  
    }  
  
    // 序列化原料字典为字符串，以便生成哈希值  
    private string SerializeIngredients(Dictionary<int, int> ingredients)  
    {  
		// 对原料列表进行排序,确保传入md5计算的产出数值与配方顺序无关  
		Ingredients.Sort(); 
		
        StringBuilder sb = new StringBuilder();  
        foreach (var pair in ingredients)  
        {  
            sb.Append(pair.Key);  
            sb.Append(":");  
            sb.Append(pair.Value);  
            sb.Append(",");  
        }  
        // 移除最后一个逗号  
        if (sb.Length > 0) sb.Length--;  
        return sb.ToString();  
    }  
  
    // 使用MD5生成哈希值  
    private string GenerateMD5Hash(string input)  
    {  
        // 创建一个MD5对象  
        using (MD5 md5Hash = MD5.Create())  
        {  
            // 计算输入字符串的哈希值  
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));  
  
            // 创建一个StringBuilder来收集字节并创建字符串  
            StringBuilder sBuilder = new StringBuilder();  
  
            // 循环遍历哈希数据的每个字节，并使用String.Format将其格式化为十六进制字符串  
            for (int i = 0; i < data.Length; i++)  
            {  
                sBuilder.Append(data[i].ToString("x2"));  
            }  
  
            // 返回十六进制字符串  
            return sBuilder.ToString();  
        }  
    }  
	//---------------------------------------------------------------------
	
    // 静态变量来保存Manager的实例  
    private static RecipeManager _instance;

    // 私有构造函数，防止外部通过new创建实例  
    private RecipeManager() { }

    // 公共静态方法，用于获取Manager的实例  
    public static RecipeManager Instance
    {
        get
        {
            // 如果_instance为空，则寻找场景中的Manager实例  
            if (_instance == null)
            {
                _instance = FindObjectOfType<RecipeManager>();

                // 如果没有找到，则创建一个新的Manager实例并添加到场景中  
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("RecipeManager");
                    _instance = managerObject.AddComponent<RecipeManager>();
                }
            }

            return _instance;
        }
    }

    // 初始化方法，可以在这里添加初始化代码  
    void Awake()
    {
        // 确保Manager是单例  
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        // 在这里添加初始化代码...  
    }

    public List<Recipe> Recipedata = new List<Recipe>();
    void Start()
    {
        // 假设JSON文件放在Resources文件夹下  
        //string jsonPath = Path.Combine(Application.streamingAssetsPath, "recipes.json");
        //string jsonData = File.ReadAllText(jsonPath);
        //recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonData); //JsonUtility.FromJson<List<Recipe>>(jsonData);
    }
}
