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
                string[] values = line.Split(','); // ����CSVʹ�ö�����Ϊ�ָ���  
                data.Add(values);
            }

            // ��������...  
            foreach (string[] row in data)
            {
                // ��ӡ������  
                Debug.Log(string.Join(", ", row));
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + filePath);
        }
    }
}