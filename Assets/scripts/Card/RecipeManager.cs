using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

using System;  
using System.Security.Cryptography;  
using System.Text;
using System.Linq;
using AStar.Utils.DesignPattern.Singleton;

public class RecipeManager : SingletonMonoBase<RecipeManager>
{
	//Create HashMap to save Recipe---------------------------------------------------------------------
	private Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();
    public List<Recipe> Recipedata = new List<Recipe>();
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
        var sortedByKeys = ingredients.OrderBy(entry => entry.Key);
        foreach (var pair in sortedByKeys)
        {
            Debug.Log("Key = " + pair.Key + ", Value = " + pair.Value);
        }
		
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

    void Start()
    {
        // 假设JSON文件放在Resources文件夹下  
        //string jsonPath = Path.Combine(Application.streamingAssetsPath, "recipes.json");
        //string jsonData = File.ReadAllText(jsonPath);
        //recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonData); //JsonUtility.FromJson<List<Recipe>>(jsonData);
    }
    public Recipe MatchRecipe(List<Card> ToMatchCardList)
    {
        Dictionary<int, int> idList = new Dictionary<int, int>();
        //遍历当前卡牌 id 并分别计数
        foreach (Card card in ToMatchCardList)
        {
            if (idList.ContainsKey(card.CardData.CardId))
            {
                int num;
                if (idList.TryGetValue(card.CardData.CardId, out num))
                {
                    ++num;
                    idList[card.CardData.CardId] = num;
                }
                else
                {
                    idList.Add(card.CardData.CardId, 1);
                }
            }
            else 
            {
                idList.Add(card.CardData.CardId, 1);
            }
        }
        return FindRecipe(idList);

        //匹配id，返回recipe
        //foreach (Recipe recipe in RecipeManager.Instance.Recipedata)
        //{
        //    bool Dirtyflag = true;
        //    foreach (int itemid in recipe.inputs_items)
        //    {
        //        int tempNum = 0;
        //        if (idList.TryGetValue(itemid, out tempNum))
        //        {
        //
        //        }
        //        else
        //        {
        //            Dirtyflag = false;
        //            break;
        //        }
        //    }
        //
        //    if (!Dirtyflag)
        //    {
        //        return recipe;
        //    }
        //    else
        //    {
        //        continue;
        //    }
        //}
        //return null;
    }
    //必须完全匹配
    public Recipe FullMatchRecipe(List<Card> ToMatchCardList)
    {
        
        return null;
    }
}
