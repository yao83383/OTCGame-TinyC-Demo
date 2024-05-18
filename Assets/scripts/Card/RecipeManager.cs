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

    void Start()
    {
        // ����JSON�ļ�����Resources�ļ�����  
        //string jsonPath = Path.Combine(Application.streamingAssetsPath, "recipes.json");
        //string jsonData = File.ReadAllText(jsonPath);
        //recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonData); //JsonUtility.FromJson<List<Recipe>>(jsonData);
    }
    public Recipe MatchRecipe(List<Card> ToMatchCardList)
    {
        Dictionary<int, int> idList = new Dictionary<int, int>();
        //������ǰ���� id ���ֱ����
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

        //ƥ��id������recipe
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
    //������ȫƥ��
    public Recipe FullMatchRecipe(List<Card> ToMatchCardList)
    {
        
        return null;
    }
}
