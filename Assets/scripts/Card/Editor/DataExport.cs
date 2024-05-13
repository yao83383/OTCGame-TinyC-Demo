using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataExporter
{
    [MenuItem("Custom/Export CardDataList to CSV")]
    public static void ExportDataList()
    {
        // 假设你已经有了一个 List<MyData> 类型的变量  
        CardDatatable cardDataAsset = AssetDatabase.LoadAssetAtPath<CardDatatable>("Assets/PData/CardDatatable.asset");

        // 填充你的数据列表  
        // myDataList.Add(new MyData { myInt = 1, myFloat = 2.0f, myString = "Three" });  
        // myDataList.Add(new MyData { myInt = 4, myFloat = 5.0f, myString = "Six" });  
        // ...  

        // 指定 CSV 文件的保存路径  
        string csvFilePath = "Assets/Data_CSV_files/CardDataList.csv";

        // 写入 CSV 文件的头部（可选）  
        using (StreamWriter writer = new StreamWriter(csvFilePath))
        {
            writer.WriteLine("cardid,cardname,costgold,sprite,prefab"); // 假设这是你的 CSV 文件的列标题  

            // 遍历列表中的每个 MyData 对象  
            foreach (FCardData data in cardDataAsset.Cards)
            {
                // 将数据格式化为 CSV 行的形式
                string csvLine = $"{data.CardId},{data.CardName},{data.CostGold},{data.SpriteRef.name},{data.PrefabRef.name}";
            
                // 写入 CSV 文件的当前行  
                writer.WriteLine(csvLine);
            }
        }

        // 刷新编辑器以显示新创建的文件  
        AssetDatabase.Refresh();

        // 在导出完成后，你可以选择打开这个文件  
        EditorUtility.RevealInFinder(csvFilePath);
    }
}