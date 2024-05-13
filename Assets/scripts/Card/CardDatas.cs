using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatas : MonoBehaviour
{
    // 静态变量来保存Manager的实例  
    private static CardDatas _instance;

    // 私有构造函数，防止外部通过new创建实例  
    private CardDatas() { }

    //基础卡包数据
    public CardDatatable BaseCardDataTable;

    //储存所有Card数据
    [HideInInspector]
    public Dictionary<int, FCardData> Carddata_dic = new Dictionary<int, FCardData>();

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
        //csvreader.LoadCardData();
        LoadCardData();

    }

    public static Dictionary<int, FCardData> LoadCardData()
    {
        CardDatas.Instance.Carddata_dic.Clear();

        foreach (FCardData card in CardDatas.Instance.BaseCardDataTable.Cards)
        {
            FCardData tempData;

            tempData = CardsManager.Instance.GetCardDataByid(card.CardId);

            if (tempData.CardId != 0)
            {
                CardDatas.Instance.Carddata_dic.Add(tempData.CardId, tempData);
            }
        }
        
        return CardDatas.Instance.Carddata_dic;
    }
}
