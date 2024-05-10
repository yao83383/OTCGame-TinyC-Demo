using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

using System;  
using System.Security.Cryptography;  
using System.Text;  
  
// ������䷽��  
[System.Serializable]
public class Recipe  
{  
    public Dictionary<int, int> Ingredients { get; set; } // ԭ�Ϻ�����  
    public List<int> Output { get; set; } // �����Ʒ  
  
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
    // �������ܵ����ԣ���ϳ�������������ϳɳɹ��ʵ�
    //public float SuccessRate = 1;

}  


public class RecipeManager : MonoBehaviour
{
	//Create HashMap to save Recipe---------------------------------------------------------------------
	private Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();  
  
    // ����䷽����ϣ����  
    public void AddRecipe(Recipe recipe)  
    {  
        string hash = GenerateMD5Hash(SerializeIngredients(recipe.Ingredients));  
        recipes[hash] = recipe;  
    }  
  
    // ���Һϳ��䷽  
    public Recipe FindRecipe(Dictionary<int, int> ingredients)  
    {  
        string hash = GenerateMD5Hash(SerializeIngredients(ingredients));  
        if (recipes.ContainsKey(hash))  
        {  
            return recipes[hash];  
        }  
        return null;  
    }  
  
    // ���л�ԭ���ֵ�Ϊ�ַ������Ա����ɹ�ϣֵ  
    private string SerializeIngredients(Dictionary<int, int> ingredients)  
    {  
		// ��ԭ���б��������,ȷ������md5����Ĳ�����ֵ���䷽˳���޹�  
		Ingredients.Sort(); 
		
        StringBuilder sb = new StringBuilder();  
        foreach (var pair in ingredients)  
        {  
            sb.Append(pair.Key);  
            sb.Append(":");  
            sb.Append(pair.Value);  
            sb.Append(",");  
        }  
        // �Ƴ����һ������  
        if (sb.Length > 0) sb.Length--;  
        return sb.ToString();  
    }  
  
    // ʹ��MD5���ɹ�ϣֵ  
    private string GenerateMD5Hash(string input)  
    {  
        // ����һ��MD5����  
        using (MD5 md5Hash = MD5.Create())  
        {  
            // ���������ַ����Ĺ�ϣֵ  
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));  
  
            // ����һ��StringBuilder���ռ��ֽڲ������ַ���  
            StringBuilder sBuilder = new StringBuilder();  
  
            // ѭ��������ϣ���ݵ�ÿ���ֽڣ���ʹ��String.Format�����ʽ��Ϊʮ�������ַ���  
            for (int i = 0; i < data.Length; i++)  
            {  
                sBuilder.Append(data[i].ToString("x2"));  
            }  
  
            // ����ʮ�������ַ���  
            return sBuilder.ToString();  
        }  
    }  
	//---------------------------------------------------------------------
	
    // ��̬����������Manager��ʵ��  
    private static RecipeManager _instance;

    // ˽�й��캯������ֹ�ⲿͨ��new����ʵ��  
    private RecipeManager() { }

    // ������̬���������ڻ�ȡManager��ʵ��  
    public static RecipeManager Instance
    {
        get
        {
            // ���_instanceΪ�գ���Ѱ�ҳ����е�Managerʵ��  
            if (_instance == null)
            {
                _instance = FindObjectOfType<RecipeManager>();

                // ���û���ҵ����򴴽�һ���µ�Managerʵ������ӵ�������  
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("RecipeManager");
                    _instance = managerObject.AddComponent<RecipeManager>();
                }
            }

            return _instance;
        }
    }

    // ��ʼ��������������������ӳ�ʼ������  
    void Awake()
    {
        // ȷ��Manager�ǵ���  
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        // ��������ӳ�ʼ������...  
    }

    public List<Recipe> Recipedata = new List<Recipe>();
    void Start()
    {
        // ����JSON�ļ�����Resources�ļ�����  
        //string jsonPath = Path.Combine(Application.streamingAssetsPath, "recipes.json");
        //string jsonData = File.ReadAllText(jsonPath);
        //recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonData); //JsonUtility.FromJson<List<Recipe>>(jsonData);
    }
}
