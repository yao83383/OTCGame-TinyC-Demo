using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public class CardsManager : MonoBehaviour
{
    // 静态变量来保存Manager的实例  
    private static CardsManager _instance;

    //存储卡数据
    //public FCardData Carddata;

    //所有当前Card实例
    public List<FCardData> Carddata = new List<FCardData>();
    // 私有构造函数，防止外部通过new创建实例  
    private CardsManager() { }

    // 公共静态方法，用于获取Manager的实例  
    public static CardsManager Instance
    {
        get
        {
            // 如果_instance为空，则寻找场景中的Manager实例  
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardsManager>();

                // 如果没有找到，则创建一个新的Manager实例并添加到场景中  
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("CardsManager");
                    _instance = managerObject.AddComponent<CardsManager>();
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
    }

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
                //TempCard.transform.localScale = new Vector3(1, 1, 1);

                Card cardComp = TempCard.GetComponent<Card>();
                if (cardComp)
                { 
                    cardComp.InitCarddataByID(CardId);
                }
            }
        }
        return TempCard;
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