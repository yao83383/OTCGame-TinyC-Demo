using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct CardSettings
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
    // 静态变量来保存Manager的实例  
    private static CardsManager _instance;

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


    public GameObject InstantiateCard(int cardId)
    {
        //if (cardsDictionary.ContainsKey(cardId))
        //{
        //    string prefabPath = "prefabs/" + cardsDictionary[cardId].prefabName;
        //    GameObject prefab = Resources.Load<GameObject>(prefabPath);
        //    GameObject newCard = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        //    LoadCardImage(cardId, newCard.GetComponent<Card>());
        //    return newCard;
        //}
        //else
        //{
        //    Debug.LogError("Card prefab not found for ID: " + cardId);
            return null;
        //}
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

    public List<ItemData> Itemdata = new List<ItemData>();
    public Recipe MatchRecipe(List<Card> ToMatchCardList)
    {
        Dictionary<int, int> idList = new Dictionary<int, int>();
        //遍历当前卡牌 id 并分别计数
        foreach (Card card in ToMatchCardList)
        {
            if (idList.ContainsKey(card.CardId))
            {
                int num = 0;
                if (idList.TryGetValue(card.CardId, out num))
                {
                    ++num;
                    idList[card.CardId] = num;
                }
                else
                {
                    idList.Add(card.CardId, 1);
                }
            }
        }

        //匹配id，返回recipe
        foreach (Recipe recipe in RecipeManager.Instance.Recipedata)
        {
            bool Dirtyflag = true;
            foreach (int itemid in recipe.inputs_items)
            {
                int tempNum = 0;
                if (idList.TryGetValue(itemid, out tempNum))
                { 
                    
                }
                else
                {
                    Dirtyflag = false;
                    break;
                }
            }

            if (!Dirtyflag)
            {
                return recipe;
            }
            else
            {
                continue;
            }
        }
        return null;
    }

    //必须完全匹配
    public Recipe FullMatchRecipe(List<Card> ToMatchCardList)
    {
        Dictionary<int, int> idList = new Dictionary<int, int>();
        //遍历当前卡牌 id 并分别计数
        foreach (Card card in ToMatchCardList)
        {
            if (idList.ContainsKey(card.CardId))
            {
                int num = 0;
                if (idList.TryGetValue(card.CardId, out num))
                {
                    ++num;
                    idList[card.CardId] = num;
                }
                else
                {
                    idList.Add(card.CardId, 1);
                }
            }
        }

        //匹配id，只有完全匹配，返回recipe
        foreach (Recipe recipe in RecipeManager.Instance.Recipedata)
        {
            bool Dirtyflag = true;
            for (int index = 0; index < recipe.inputs_items.Count; ++index)
            {
                int tempNum = 0;
                if (idList.TryGetValue(recipe.inputs_items[index], out tempNum))
                {
                    //如果配方内，此 物品对应数量 不与 idList内该物品数量 相当，不可合成；
                    if (recipe.inputs_nums[index] == tempNum)
                    {
                        continue;
                    }
                    else
                    {
                        Dirtyflag = false;
                        break;
                    }
                }
                else
                {
                    Dirtyflag = false;
                    break;
                }
            }

            //如果此recipe内所有物品数量都与idlist内物品数量 相当，则找到对应recipe；
            if (Dirtyflag)
            {
                return recipe;
            }
            else
            {
                continue;
            }
        }
        return null;
    }

    public void OnRecipeFullMatch(List<Card> ToMatchCardList)
    { 
        
    }
}