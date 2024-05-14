using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeDatatable", menuName = "Prefabs/RecipeDatatable")]
public class RecipeDatatable : ScriptableObject
{
    public List<Recipe> Recipes = new List<Recipe>();
}

// ������䷽��  
[System.Serializable]
public class Recipe
{
    public Dictionary<int, int> Ingredients = new Dictionary<int, int>(); // ԭ�Ϻ�����  
    public Dictionary<int, int> Output_dic = new Dictionary<int, int>();// �����Ʒ������

    [HideInInspector]
    public List<int> inputs_items = new List<int>();
    [HideInInspector]
    public List<int> inputs_nums = new List<int>();
    [HideInInspector]
    public List<int> outputs_items = new List<int>();
    [HideInInspector]

    public List<int> outputs_nums = new List<int>();
    public float combinetime;

    public List<RecipeElem> inputs = new List<RecipeElem>();
    public List<RecipeElem> outputs = new List<RecipeElem>();
}

[System.Serializable]
public struct RecipeElem
{
    public int CardId;
    public int Num;
}