using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FCardData
{
    public int CardId; // ��Ʒ��Ψһ��ʶ��  
    public string CardName; // ��Ʒ����

    public Sprite SpriteRef;
    public GameObject PrefabRef;
}

public class CardDatas : MonoBehaviour
{
    // ��̬����������Manager��ʵ��  
    private static CardDatas _instance;

    // ˽�й��캯������ֹ�ⲿͨ��new����ʵ��  
    private CardDatas() { }

    //������������
    public CardDatatable BaseCardDataTable;

    //��������Card����
    [HideInInspector]
    public Dictionary<int, FCardData> Carddata_dic = new Dictionary<int, FCardData>();

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
        csvreader.LoadCardData();
    }
}
