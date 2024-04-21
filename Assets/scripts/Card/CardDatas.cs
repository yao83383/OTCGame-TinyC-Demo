using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public int ItemId; // 物品的唯一标识符  
    public string ItemName; // 物品数量
}

[System.Serializable]
public struct RecipeElem
{
    public int ItemId;
    public int Num;
}

[System.Serializable]
public class Recipe
{
    public List<int> inputs_items = new List<int>();
    public List<int> inputs_nums = new List<int>();
    public List<int> outputs_items = new List<int>();
    public List<int> outputs_nums = new List<int>();
    public float combinetime;

    public List<RecipeElem> inputs = new List<RecipeElem>();
    public List<RecipeElem> outputs = new List<RecipeElem>();
    // 其他可能的属性，如合成所需的数量、合成成功率等
    //public float SuccessRate = 1;
}

public class CardDatas : MonoBehaviour
{
    // 静态变量来保存Manager的实例  
    private static CardDatas _instance;

    // 私有构造函数，防止外部通过new创建实例  
    private CardDatas() { }

    public Dictionary<int, ItemData> Itemdata_dic = new Dictionary<int, ItemData>();
    public Dictionary<int, Recipe> Recipedata_dic = new Dictionary<int, Recipe>();

    // 公共静态方法，用于获取Manager的实例  
    public static CardDatas Instance
    {
        get
        {
            // 如果_instance为空，则寻找场景中的Manager实例  
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardDatas>();

                // 如果没有找到，则创建一个新的Manager实例并添加到场景中  
                if (_instance == null)
                {
                    GameObject cardDataObject = new GameObject("CardDatas");
                    _instance = cardDataObject.AddComponent<CardDatas>();
                }
            }

            return _instance;
        }
    }

    // 初始化方法，可以在这里添加初始化代码  
    void Awake()
    {
        // 确保Manager是单例  
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        // 在这里添加初始化代码...  
        CSVReader csvreader = new CSVReader();
        csvreader.LoadRecipeData();
        csvreader.LoadItemData();
    }
}
