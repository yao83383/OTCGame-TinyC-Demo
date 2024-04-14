using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader
{
    public void LoadRecipeData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Recipes.csv");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string[]> data = new List<string[]>();

            foreach (string line in lines)
            {
                string[] values = line.Split(','); // 假设CSV使用逗号作为分隔符  
                data.Add(values);
            }

            // 处理数据...  
            foreach (string[] row in data)
            {
                // 打印行数据  
                Debug.Log(string.Join(", ", row));
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + filePath);
        }
    }
}