using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader
{
    public List<Recipe> LoadRecipeData()
    {
        RecipeManager.Instance.Recipedata.Clear();

        string filePath = Path.Combine(Application.streamingAssetsPath, "Recipes.csv");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string[]> data = new List<string[]>();

            for(int index = 1; index < lines.Length; ++ index)
            {
                string[] structData = lines[index].Split(','); // 假设CSV使用逗号作为分隔符  

                if (structData.Length == 5)
                {
                    Recipe recipe = new Recipe();

                    string[] input_items = structData[0].Split(';');
                    string[] input_nums = structData[1].Split(';');
                    for(int index_input = 0; index_input < input_items.Length; ++index_input)
                    {
                        RecipeElem tempElem;

                        tempElem.ItemId = int.Parse(input_items[index_input]);
                        tempElem.Num = int.Parse(input_nums[index_input]);

                        recipe.inputs_items.Add(tempElem.ItemId);
                        recipe.inputs_nums.Add(tempElem.Num);
                        recipe.inputs.Add(tempElem);
                    }

                    string[] output_items = structData[2].Split(',');
                    string[] output_nums = structData[3].Split(',');
                    for (int index_out = 0; index_out < output_items.Length; ++index_out)
                    {
                        RecipeElem tempElem;

                        tempElem.ItemId = int.Parse(output_items[index_out]);
                        tempElem.Num = int.Parse(output_nums[index_out]);

                        recipe.outputs_items.Add(tempElem.ItemId);
                        recipe.outputs_nums.Add(tempElem.Num);
                        recipe.outputs.Add(tempElem);
                    }
                    recipe.combinetime = float.Parse(structData[4]);
                    RecipeManager.Instance.Recipedata.Add(recipe);
                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + filePath);
        }

        return RecipeManager.Instance.Recipedata;
    }

    public List<ItemData> LoadItemData()
    {
        CardsManager.Instance.Itemdata.Clear();

        string filePath = Path.Combine(Application.streamingAssetsPath, "Items.csv");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string[]> data = new List<string[]>();

            for (int index = 1; index < lines.Length; ++index)
            {
                string[] structData = lines[index].Split(','); // 假设CSV使用逗号作为分隔符  

                if (structData.Length >= 2)
                {
                    //string[] ids = structData[0].Split(';');
                    //string[] names = structData[1].Split(';');
 
                    ItemData tempData;

                    tempData.ItemId = int.Parse(structData[0]);
                    tempData.ItemName = structData[1];

                    CardsManager.Instance.Itemdata.Add(tempData);
                    CardDatas.Instance.Itemdata_dic.Add(tempData.ItemId, tempData);
                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + filePath);
        }

        return CardsManager.Instance.Itemdata;
    }
}