using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

[System.Serializable]
public class CardSettings
{
    public CardSettings(int IncardId, string InImageName, string InprefabName)
    {
        cardId = IncardId;
        ImageName = InImageName;
        prefabName = InprefabName;
    }
    public int cardId;
    public string ImageName;
    public string prefabName;
}

public class CardsManager : MonoBehaviour
{
    // ��̬����������Manager��ʵ��  
    private static CardsManager _instance;

    // ˽�й��캯������ֹ�ⲿͨ��new����ʵ��  
    private CardsManager() { }

    // ������̬���������ڻ�ȡManager��ʵ��  
    public static CardsManager Instance
    {
        get
        {
            // ���_instanceΪ�գ���Ѱ�ҳ����е�Managerʵ��  
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardsManager>();

                // ���û���ҵ����򴴽�һ���µ�Managerʵ������ӵ�������  
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("CardsManager");
                    _instance = managerObject.AddComponent<CardsManager>();
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
    }


    public GameObject InstantiateCard(int cardId)
    {
        if (cardsDictionary.ContainsKey(cardId))
        {
            string prefabPath = "prefabs/" + cardsDictionary[cardId].prefabName;
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            GameObject newCard = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            LoadCardImage(cardId, newCard.GetComponent<Card>());
            return newCard;
        }
        else
        {
            Debug.LogError("Card prefab not found for ID: " + cardId);
            return null;
        }
    }

    public void LoadCardImage(int cardId, Card InCard)
    {
        string imagePath = "sprites/" + cardsDictionary[cardId].ImageName;
        Texture2D cardImage = Resources.Load<Texture2D>(imagePath);
        if (cardImage == null)
        {
            Debug.LogError("Card image not found for ID: " + cardId);
        }
        else 
        {
            //Material newMat = new Material(Shader.Find("Standard"));
            //newMat.mainTexture = cardImage;

            MeshRenderer meshRenderer = InCard.GetComponent<MeshRenderer>();
            //meshRenderer.materials[1] = newMat;
            //meshRenderer.material = newMat;
            meshRenderer.materials[1].mainTexture = cardImage;
            meshRenderer.materials[2].mainTexture = cardImage;
        }
    }
    //-------------------------------------------------------------
    //CardImage:---------------------------------------------------
    // ʹ��List<MyDictionaryItem>��ģ��Dictionary�Ĺ��ܣ���ΪList������Inspector�б༭  
    public List<CardSettings> CardDatas = new List<CardSettings>();

    // ����Ҫ��ʱ������Խ����Listת��ΪDictionary��ʹ��  
    private Dictionary<int, CardSettings> cardsDictionary;

    void Start()
    {
        // ����JSON�ļ�����Resources�ļ�����  
        string jsonPath = Path.Combine(Application.streamingAssetsPath, "cards.json");
        string jsonData = File.ReadAllText(jsonPath);
        CardDatas = JsonConvert.DeserializeObject<List<CardSettings>>(jsonData); //JsonUtility.FromJson<List<Recipe>>(jsonData);
        // ��ʼ��Dictionary��ʹ��List�е�����  
        cardsDictionary = new Dictionary<int, CardSettings>();
        foreach (var item in CardDatas)
        {
            CardSettings TempCardData = new CardSettings(item.cardId, item.ImageName, item.prefabName);
            cardsDictionary.Add(item.cardId, TempCardData);
        }

        // ���������ʹ��myDictionary��������������  
        InstantiateCard(1);
    }
    //CardImage:---------------------------------------------------
    //-------------------------------------------------------------
}