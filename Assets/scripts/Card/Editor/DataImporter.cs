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
        AssetDatabase.CreateAsset(table, AssetDatabase.GenerateUniqueAssetPath("Assets/PData/CardDatatable.asset"));
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
}