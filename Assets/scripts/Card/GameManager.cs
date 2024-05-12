using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 静态变量来保存Manager的实例  
    private static GameManager _instance;

    //存储卡数据
    public GlobalData _globaldata;
    public TextMeshProUGUI GoldNumText;
    // 私有构造函数，防止外部通过new创建实例  
    private GameManager() { }

    // 公共静态方法，用于获取Manager的实例  
    public static GameManager Instance
    {
        get
        {
            // 如果_instance为空，则寻找场景中的Manager实例  
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                // 如果没有找到，则创建一个新的Manager实例并添加到场景中  
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    _instance = managerObject.AddComponent<GameManager>();
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
        GoldNumText.text = "所剩金钱: " + _globaldata.SpriteStone;
    }

    public void BuyCardById(int cardId)
    {
        FCardData findData = CardsManager.Instance.GetCardDataByid(cardId);

        if (findData.CardId != 0)
        { 
            GameObject cardObject = CardsManager.Instance.CreateCardById(cardId, this.gameObject);
            _globaldata.SpriteStone -= findData.CostGold;
            GoldNumText.text = "所剩金钱: " + (_globaldata.SpriteStone - findData.CostGold);
        }
    }
}