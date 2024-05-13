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

        // �ڱ༭���У�ʹ��AssetDatabase������ScriptableObject  
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

        // ���������У���һ�У�  
        for (int i = 1; i < lines.Length; i++) // �ӵڶ��п�ʼѭ��  
        {
            string[] fields = lines[i].Split(','); // �����ֶ��ɶ��ŷָ�  

            // ����һ���µ�CardData��������������  
            FCardData cardData = new FCardData();
            cardData.CardId = int.Parse(fields[0].Trim()); // �����һ���ֶ���CardId  
            cardData.CardName = fields[1].Trim(); // ����ڶ����ֶ���CardName  
            cardData.CostGold = int.Parse(fields[2].Trim());
            cardData.spritename = fields[3].Trim();
            cardData.prefabname = fields[4].Trim();
            cardData.SpriteRef = LoadSpriteFromPathInEditor(spritFile + cardData.spritename);
            cardData.PrefabRef = LoadGameobjectFromPathInEditor(prefabFile + cardData.prefabname);

            // ��CardData������ӵ��б���  
            cardDataList.Add(cardData);
        }

        return cardDataList;
    }

    // �༭���ڼ���Sprite��Դ  
    public static Sprite LoadSpriteFromPathInEditor(string assetPath)
    {
#if UNITY_EDITOR
        // ȷ��·������"Assets/"��ͷ������·��  
        if (!assetPath.StartsWith("Assets/"))
        {
            assetPath = "Assets/" + assetPath;
        }
        assetPath += ".png";
        // ����Sprite�ʲ�  
        Object asset = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
        return asset as Sprite;
#else
    throw new NotSupportedException("This method can only be used in the Unity Editor.");  
#endif
    }

    // �༭���ڼ���Sprite��Դ  
    public static GameObject LoadGameobjectFromPathInEditor(string assetPath)
    {
#if UNITY_EDITOR
        // ȷ��·������"Assets/"��ͷ������·��  
        if (!assetPath.StartsWith("Assets/"))
        {
            assetPath = "Assets/" + assetPath;
        }
        assetPath += ".prefab";
        // ����Prefab�ʲ�  
        GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
        return asset as GameObject;
#else
    throw new NotSupportedException("This method can only be used in the Unity Editor.");  
#endif
    }
}