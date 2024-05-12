using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ��̬����������Manager��ʵ��  
    private static GameManager _instance;

    //�洢������
    public GlobalData _globaldata;
    public TextMeshProUGUI GoldNumText;
    // ˽�й��캯������ֹ�ⲿͨ��new����ʵ��  
    private GameManager() { }

    // ������̬���������ڻ�ȡManager��ʵ��  
    public static GameManager Instance
    {
        get
        {
            // ���_instanceΪ�գ���Ѱ�ҳ����е�Managerʵ��  
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                // ���û���ҵ����򴴽�һ���µ�Managerʵ������ӵ�������  
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    _instance = managerObject.AddComponent<GameManager>();
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
        GoldNumText.text = "��ʣ��Ǯ: " + _globaldata.SpriteStone;
    }

    public void BuyCardById(int cardId)
    {
        FCardData findData = CardsManager.Instance.GetCardDataByid(cardId);

        if (findData.CardId != 0)
        { 
            GameObject cardObject = CardsManager.Instance.CreateCardById(cardId, this.gameObject);
            _globaldata.SpriteStone -= findData.CostGold;
            GoldNumText.text = "��ʣ��Ǯ: " + (_globaldata.SpriteStone - findData.CostGold);
        }
    }
}