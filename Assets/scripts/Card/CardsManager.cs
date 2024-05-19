using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using AStar.Utils.DesignPattern.Singleton;

public class CardsManager : SingletonMonoBase<CardsManager>
{
    //存储卡数据
    //public FCardData Carddata;

    public Vector3 NextCardOffset = new Vector3(0f, 0.5f, -1.5f);

    //所有当前Card实例
    [HideInInspector]
    public List<FCardData> Carddata = new List<FCardData>();

    public GameObject CreateCardById(int CardId, GameObject parent)
    {
        GameObject TempCard =null;
        FCardData TempCardData;
        if (CardDatas.Instance.Carddata_dic.TryGetValue(CardId, out TempCardData))
        {
            if (TempCardData.PrefabRef)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                TempCard = Instantiate(TempCardData.PrefabRef, new Vector3(0, 0, 0), rotation, parent.transform);
                TempCard.GetComponent<Card>().InitCarddataByID(CardId);
                //TempCard.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        return TempCard;
    }

    public FCardData GetCardDataByid(int cardId)
    {
        FCardData tempData = new FCardData();
        foreach (FCardData cdata in CardDatas.Instance.BaseCardDataTable.Cards)
        {
            if (cdata.CardId == cardId)
            {
                tempData = cdata;
                break;
            }
            else
            {
                tempData.CardId = 0;
                tempData.CardName = "";
                tempData.CostGold = 0;
                tempData.PrefabRef = null;
                tempData.SpriteRef = null;
                continue;
            }
        }
        return tempData;
    }

    public void LoadCardImage(int cardId, Card InCard)
    {
        //string imagePath = "sprites/" + cardsDictionary[cardId].ImageName;
        //Texture2D cardImage = Resources.Load<Texture2D>(imagePath);
        //if (cardImage == null)
        //{
        //    Debug.LogError("Card image not found for ID: " + cardId);
        //}
        //else 
        //{
        //    //Material newMat = new Material(Shader.Find("Standard"));
        //    //newMat.mainTexture = cardImage;
        //
        //    MeshRenderer meshRenderer = InCard.GetComponent<MeshRenderer>();
        //    //meshRenderer.materials[1] = newMat;
        //    //meshRenderer.material = newMat;
        //    meshRenderer.materials[1].mainTexture = cardImage;
        //    meshRenderer.materials[2].mainTexture = cardImage;
        //}
    }
    //-------------------------------------------------------------
    //CardImage:---------------------------------------------------
    // 使用List<MyDictionaryItem>来模拟Dictionary的功能，因为List可以在Inspector中编辑  
    //public List<CardSettings> CardDatas = new List<CardSettings>();

    // 在需要的时候，你可以将这个List转换为Dictionary来使用  
   // private Dictionary<int, CardSettings> cardsDictionary;

    void Start()
    {
        //// 假设JSON文件放在Resources文件夹下  
        //string jsonPath = Path.Combine(Application.streamingAssetsPath, "cards.json");
        //string jsonData = File.ReadAllText(jsonPath);
        //CardDatas = JsonConvert.DeserializeObject<List<CardSettings>>(jsonData); //JsonUtility.FromJson<List<Recipe>>(jsonData);
        //// 初始化Dictionary，使用List中的数据  
        //cardsDictionary = new Dictionary<int, CardSettings>();
        //foreach (var item in CardDatas)
        //{
        //    CardSettings TempCardData = new CardSettings(item.cardId, item.ImageName, item.prefabName);
        //    cardsDictionary.Add(item.cardId, TempCardData);
        //}
        //
        //// 现在你可以使用myDictionary进行其他操作了  
        //InstantiateCard(1);
    }
    //CardImage:---------------------------------------------------
    //-------------------------------------------------------------

    
}