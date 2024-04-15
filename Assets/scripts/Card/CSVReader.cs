using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader
{
    public List<Recipe> LoadRecipeData()
    {
        List<Recipe> tempRecipes = new List<Recipe>();
        string filePath = Path.Combine(Application.streamingAssetsPath, "Recipes.csv");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string[]> data = new List<string[]>();

            for(int index = 1; index < lines.Length; ++ index)
            {
                string[] structData = lines[index].Split(','); // 假设CSV使用逗号作为分隔符  

                if (structData.Length == 2)
                {
                    Recipe recipe = new Recipe();

                    string[] inputElems = structData[0].Split(';');
                    foreach (string elem in inputElems)
                    {
                        RecipeElem tempElem;

                        string[] values = elem.Split('|');

                        tempElem.ItemId = int.Parse(values[0]);
                        tempElem.Num = int.Parse(values[1]);

                        recipe.inputs.Add(tempElem);
                    }

                    string[] outputElems = structData[1].Split(';');
                    foreach (string elem in outputElems)
                    {
                        RecipeElem tempElem;

                        string[] values = elem.Split('|');

                        tempElem.ItemId = int.Parse(values[0]);
                        tempElem.Num = int.Parse(values[1]);

                        recipe.outputs.Add(tempElem);
                    }
                    tempRecipes.Add(recipe);
                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + filePath);
        }

        return tempRecipes;
    }
}