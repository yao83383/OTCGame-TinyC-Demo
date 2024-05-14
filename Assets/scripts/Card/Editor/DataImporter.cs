using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class DataImporter
{
    [MenuItem("Custom/Import CardDataList from CSV")]
    public static void ImportCardData()
    {
        string csvCardDataFilePath = "Assets/Data_CSV_files/CardDataList.csv";
        

        CardDatatable table = ScriptableObject.CreateInstance<CardDatatable>();
        table.Cards = ReadCardDataCsvFile(csvCardDataFilePath);

        // 在编辑器中，使用AssetDatabase来保存ScriptableObject  
#if UNITY_EDITOR
        string filepath = "Assets/PData/CardDatatable.asset";
        if (!File.Exists(filepath))
        {
            AssetDatabase.CreateAsset(table, AssetDatabase.GenerateUniqueAssetPath(filepath));
        }
        else
        {
            AssetDatabase.DeleteAsset(filepath);
            AssetDatabase.CreateAsset(table, AssetDatabase.GenerateUniqueAssetPath(filepath));
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }

    public static List<FCardData> ReadCardDataCsvFile(string filePath)
    {
        string spritFile = "Assets/sprite/item/";
        string prefabFile = "Assets/Prefabs/";

        List<FCardData> cardDataList = new List<FCardData>();
        string[] lines = File.ReadAllLines(filePath);

        // 跳过标题行（第一行）  
        for (int i = 1; i < lines.Length; i++) // 从第二行开始循环  
        {
            string[] fields = lines[i].Split(','); // 假设字段由逗号分隔  

            // 创建一个新的CardData对象并设置其属性  
            FCardData cardData = new FCardData();
            cardData.CardId = int.Parse(fields[0].Trim()); // 假设第一个字段是CardId  
            cardData.CardName = fields[1].Trim(); // 假设第二个字段是CardName  
            cardData.CostGold = int.Parse(fields[2].Trim());
            cardData.spritename = fields[3].Trim();
            cardData.prefabname = fields[4].Trim();
            cardData.SpriteRef = LoadSpriteFromPathInEditor(spritFile + cardData.spritename);
            cardData.PrefabRef = LoadGameobjectFromPathInEditor(prefabFile + cardData.prefabname);

            // 将CardData对象添加到列表中  
            cardDataList.Add(cardData);
        }

        return cardDataList;
    }

    // 编辑器内加载Sprite资源  
    public static Sprite LoadSpriteFromPathInEditor(string assetPath)
    {
#if UNITY_EDITOR
        // 确保路径是以"Assets/"开头的完整路径  
        if (!assetPath.StartsWith("Assets/"))
        {
            assetPath = "Assets/" + assetPath;
        }
        assetPath += ".png";
        // 加载Sprite资产  
        Object asset = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
        return asset as Sprite;
#else
    throw new NotSupportedException("This method can only be used in the Unity Editor.");  
#endif
    }

    // 编辑器内加载Sprite资源  
    public static GameObject LoadGameobjectFromPathInEditor(string assetPath)
    {
#if UNITY_EDITOR
        // 确保路径是以"Assets/"开头的完整路径  
        if (!assetPath.StartsWith("Assets/"))
        {
            assetPath = "Assets/" + assetPath;
        }
        assetPath += ".prefab";
        // 加载Prefab资产  
        GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
        return asset as GameObject;
#else
    throw new NotSupportedException("This method can only be used in the Unity Editor.");  
#endif
    }

    [MenuItem("Custom/Import RecipeDataList from CSV")]
    public static void ImportRecipeData()
    {
        string csvRecipeDataFilePath = "Assets/Data_CSV_files/RecipesDataList.csv";


        RecipeDatatable table = ScriptableObject.CreateInstance<RecipeDatatable>();
        table.Recipes = ReadRecipeDataCsvFile(csvRecipeDataFilePath);

        // 在编辑器中，使用AssetDatabase来保存ScriptableObject  
#if UNITY_EDITOR
        string filepath = "Assets/PData/RecipesDataTable.asset";
        if (!File.Exists(filepath))
        {
            AssetDatabase.CreateAsset(table, AssetDatabase.GenerateUniqueAssetPath(filepath));
        }
        else 
        {
            AssetDatabase.DeleteAsset(filepath);
            AssetDatabase.CreateAsset(table, AssetDatabase.GenerateUniqueAssetPath(filepath));
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }

    public static List<Recipe> ReadRecipeDataCsvFile(string filePath)
    {
        List<Recipe> recipeDataList = new List<Recipe>();
        string[] lines = File.ReadAllLines(filePath);

        // 跳过标题行（第一行）  
        for (int i = 1; i < lines.Length; i++) // 从第二行开始循环  
        {
            string[] fields = lines[i].Split(','); // 假设字段由逗号分隔  

            Recipe recipe = new Recipe();

            string[] input_items = fields[2].Split(';');
            string[] input_nums = fields[3].Split(';');
            for (int index_input = 0; index_input < input_items.Length; ++index_input)
            {
                RecipeElem tempElem;

                tempElem.CardId = int.Parse(input_items[index_input]);
                tempElem.Num = int.Parse(input_nums[index_input]);

                recipe.inputs.Add(tempElem);
            }

            string[] output_items = fields[0].Split(',');
            string[] output_nums = fields[1].Split(',');
            for (int index_out = 0; index_out < output_items.Length; ++index_out)
            {
                RecipeElem tempElem;

                tempElem.CardId = int.Parse(output_items[index_out]);
                tempElem.Num = int.Parse(output_nums[index_out]);

                recipe.outputs.Add(tempElem);
            }
            recipe.combinetime = float.Parse(fields[4]);

            recipeDataList.Add(recipe);
        }

        return recipeDataList;
    }
}