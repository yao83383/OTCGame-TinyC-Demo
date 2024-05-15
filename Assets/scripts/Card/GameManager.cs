using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ��̬����������Manager��ʵ��  
    private static GameManager _instance;

    //Console Cheat
    public GameObject codeInputBox;
    public TextMeshProUGUI codeText;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (codeInputBox.activeInHierarchy)
            {
                CheckText(codeText.text);
                codeInputBox.SetActive(false);
            }
            else
            {
                codeInputBox.SetActive(true);
            }
        }
    }

    private void CheckText(string text)
    {
        text += " end";
        string[] fields = text.Split(' ');

        if (fields.Length <= 3)
        {
            Debug.Log("ָ�����");
            return;
        }

        if (fields[0].Contains("/give"))
        {
            if (fields[1].Contains("@"))
            {
                int cardNum = int.Parse(fields[2]);
                string[] cardIdStr = fields[1].Split('@');
                int cardId = int.Parse(cardIdStr[cardIdStr.Length - 1]);
              
                //int.TryParse(cardIdNum[cardIdNum.Length - 1], out cardNum);
                for (int index = 0; index < cardNum; ++index)
                {
                    BuyCardById(cardId);
                }
            }
        }
    }

    public void BuyCardById(int cardId)
    {
        FCardData findData = CardsManager.Instance.GetCardDataByid(cardId);

        if (findData.CardId != 0)
        { 
            GameObject cardObject = CardsManager.Instance.CreateCardById(cardId, this.gameObject);
            _globaldata.SpriteStone -= findData.CostGold;
            GoldNumText.text = "��ʣ��Ǯ: " + (_globaldata.SpriteStone);
        }
    }
}