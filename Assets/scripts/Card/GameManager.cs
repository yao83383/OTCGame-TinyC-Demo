using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AStar.Utils.DesignPattern.Singleton;
using UnityEngine.SceneManagement;
using System.Transactions;
public class GameManager : SingletonMonoBase<GameManager>
{

    //Console Cheat
    public GameObject codeInputBox;
    public TextMeshProUGUI codeText;

    //�洢������
    public GlobalData _globaldata;
    public TextMeshProUGUI GoldNumText;

    // ��ʼ��������������������ӳ�ʼ������  
    public override void Awake()
    {
        base.Awake();
        // ��������ӳ�ʼ������...  
        GoldNumText.text = "��ʣ��Ǯ: " + _globaldata.SpriteStone;
    }

    private bool isInCombatScene = false;

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