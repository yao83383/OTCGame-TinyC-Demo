using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public class CardsManager : MonoBehaviour
{
    // ��̬����������Manager��ʵ��  
    private static CardsManager _instance;

    //�洢������
    //public FCardData Carddata;

    //���е�ǰCardʵ��
    public List<FCardData> Carddata = new List<FCardData>();
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
    // ʹ��List<MyDictionaryItem>��ģ��Dictionary�Ĺ��ܣ���ΪList������Inspector�б༭  
    //public List<CardSettings> CardDatas = new List<CardSettings>();

    // ����Ҫ��ʱ������Խ����Listת��ΪDictionary��ʹ��  
   // private Dictionary<int, CardSettings> cardsDictionary;

    void Start()
    {
        //// ����JSON�ļ�����Resources�ļ�����  
        //string jsonPath = Path.Combine(Application.streamingAssetsPath, "cards.json");
        //string jsonData = File.ReadAllText(jsonPath);
        //CardDatas = JsonConvert.DeserializeObject<List<CardSettings>>(jsonData); //JsonUtility.FromJson<List<Recipe>>(jsonData);
        //// ��ʼ��Dictionary��ʹ��List�е�����  
        //cardsDictionary = new Dictionary<int, CardSettings>();
        //foreach (var item in CardDatas)
        //{
        //    CardSettings TempCardData = new CardSettings(item.cardId, item.ImageName, item.prefabName);
        //    cardsDictionary.Add(item.cardId, TempCardData);
        //}
        //
        //// ���������ʹ��myDictionary��������������  
        //InstantiateCard(1);
    }
    //CardImage:---------------------------------------------------
    //-------------------------------------------------------------

    
}