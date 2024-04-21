using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public TextMeshPro CardnameText;

    //Contains Cards from this to the end;
    //  PreCards not included;
    [HideInInspector]
    private List<Card> CardList = new List<Card>();
    private string CardName;

    private Card PreCard;
    private Card NextCard;
    private GameObject NextEmptyCard;

    public ItemData CardData;
    public int CardId;

    [HideInInspector]
    public Vector3 NextCardOffset = new Vector3(0f, 0.5f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        if (CardDatas.Instance.Itemdata_dic.TryGetValue(CardId, out CardData))
        { 
            if (CardnameText)
            {
                CardnameText.text = CardData.ItemName;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartMove()
    {
        GetComponent<MeshCollider>().enabled = false;
        if (PreCard)
        { 
            PreCard.NextCard = null;
            PreCard = null;
        }
    }

    public void Move(Vector3 InPostion)
    {
        this.transform.position = InPostion;
        if (NextCard)
        { 
            NextCard.Move(InPostion - NextCardOffset);
        }
    }

    private Card GetListHead()
    {
        if (this.PreCard)
        {
            return PreCard.GetListHead();
        }
        return this;
    }

    public Card GetListEnd()
    {
        if (this.NextCard)
        {
            return this.NextCard.GetListEnd();
        }
        else
        {
            return this;
        }
    }

    public List<Card> GetCardList()
    {
        if (this.PreCard)
        {
            return this.PreCard.GetCardList();
        }
        else
        {
            return this.CardList;
        }
    }

    private void RefreshCardList()
    {
        if (NextCard)
        {
            //非链首 递归加入list
            NextCard.RefreshCardList();
        }
        else
        {
            //链首 清除list
            GetCardList().Clear();
        }

        if (!GetCardList().Contains(this))
        {
            GetCardList().Remove(this);
            GetCardList().Add(this);
        }
    }

    public void Drop(Card InPreCard, Vector3 InPosition)
    {
        this.PreCard = InPreCard;
        GetComponent<MeshCollider>().enabled = true;
        if (InPreCard)
        {
            transform.position = InPreCard.transform.position - NextCardOffset;
            InPreCard.NextCard = this;
        }
        else
        {
            transform.position = InPosition;
        }
        RefreshCardList();
        CardsManager.Instance.MatchRecipe(this.CardList);
    }

    public void LevelUp()
    { }
}
