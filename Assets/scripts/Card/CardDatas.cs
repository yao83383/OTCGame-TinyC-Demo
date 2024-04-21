using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public int ItemId; // ��Ʒ��Ψһ��ʶ��  
    public string ItemName; // ��Ʒ����
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
    // �������ܵ����ԣ���ϳ�������������ϳɳɹ��ʵ�
    //public float SuccessRate = 1;
}

public class CardDatas : MonoBehaviour
{
    // ��̬����������Manager��ʵ��  
    private static CardDatas _instance;

    // ˽�й��캯������ֹ�ⲿͨ��new����ʵ��  
    private CardDatas() { }

    public Dictionary<int, ItemData> Itemdata_dic = new Dictionary<int, ItemData>();
    public Dictionary<int, Recipe> Recipedata_dic = new Dictionary<int, Recipe>();

    // ������̬���������ڻ�ȡManager��ʵ��  
    public static CardDatas Instance
    {
        get
        {
            // ���_instanceΪ�գ���Ѱ�ҳ����е�Managerʵ��  
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardDatas>();

                // ���û���ҵ����򴴽�һ���µ�Managerʵ������ӵ�������  
                if (_instance == null)
                {
                    GameObject cardDataObject = new GameObject("CardDatas");
                    _instance = cardDataObject.AddComponent<CardDatas>();
                }
            }

            return _instance;
        }
    }

    // ��ʼ��������������������ӳ�ʼ������  
    void Awake()
    {
        // ȷ��Manager�ǵ���  
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        // ��������ӳ�ʼ������...  
        CSVReader csvreader = new CSVReader();
        csvreader.LoadRecipeData();
        csvreader.LoadItemData();
    }
}
