using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataExporter
{
    [MenuItem("Custom/Export CardDataList to CSV")]
    public static void ExportDataList()
    {
        // �������Ѿ�����һ�� List<MyData> ���͵ı���  
        CardDatatable cardDataAsset = AssetDatabase.LoadAssetAtPath<CardDatatable>("Assets/PData/CardDatatable.asset");

        // �����������б�  
        // myDataList.Add(new MyData { myInt = 1, myFloat = 2.0f, myString = "Three" });  
        // myDataList.Add(new MyData { myInt = 4, myFloat = 5.0f, myString = "Six" });  
        // ...  

        // ָ�� CSV �ļ��ı���·��  
        string csvFilePath = "Assets/Data_CSV_files/CardDataList.csv";

        // д�� CSV �ļ���ͷ������ѡ��  
        using (StreamWriter writer = new StreamWriter(csvFilePath))
        {
            writer.WriteLine("cardid,cardname,costgold,sprite,prefab"); // ����������� CSV �ļ����б���  

            // �����б��е�ÿ�� MyData ����  
            foreach (FCardData data in cardDataAsset.Cards)
            {
                // �����ݸ�ʽ��Ϊ CSV �е���ʽ
                string csvLine = $"{data.CardId},{data.CardName},{data.CostGold},{data.SpriteRef.name},{data.PrefabRef.name}";
            
                // д�� CSV �ļ��ĵ�ǰ��  
                writer.WriteLine(csvLine);
            }
        }

        // ˢ�±༭������ʾ�´������ļ�  
        AssetDatabase.Refresh();

        // �ڵ�����ɺ������ѡ�������ļ�  
        EditorUtility.RevealInFinder(csvFilePath);
    }
}