using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    //Object to show-------------------------------
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    //-------------------------------

    public TextMeshPro CardnameText;

    //Contains Cards from this to the end;
    //  PreCards not included;
    private Card PreCard;
    private Card NextCard;

    public FCardData CardData;

    public Vector3 NextCardOffset = new Vector3(-1.5f, -0.5f, 0f);

    //[SerializeField]
    private Camera _mainCamera;

    public GameObject CardLayout;
    //private void LateUpdate()
    //{
    //    Vector3 cameraPosition = _mainCamera.transform.position;
    //    cameraPosition.y = transform.position.y;
    //    transform.LookAt(cameraPosition);
    //    transform.Rotate(0, 180, 0);
    //}

    public bool InitCarddataByID(int cardId)
    {
        FCardData TempCardData;
        if (CardDatas.Instance.Carddata_dic.TryGetValue(cardId, out TempCardData))
        {
            this.CardData = TempCardData;

            if (spriteRenderer)
            {
                spriteRenderer.sprite = this.CardData.SpriteRef;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;

        if (!InitCarddataByID(CardData.CardId))
        {
            Destroy(this.gameObject);
        }

		//animator = this.transform.parent.GetComponent<Animator>();
        if (CardDatas.Instance.Carddata_dic.TryGetValue(CardData.CardId, out CardData))
        { 
            if (CardnameText)
            {
                CardnameText.text = CardData.CardName;
            }
        }

        if (animator)
        {
            animator.Play("SpawnCardAnimator");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartMove()
    {
        CardLayout.GetComponent<MeshCollider>().enabled = false;
        if (PreCard)
        { 
            PreCard.NextCard = null;
            PreCard = null;
        }
    }

    public void Move(Vector3 InPostion)
    {
        this.transform.position = InPostion + NextCardOffset;
        if (NextCard)
        { 
            NextCard.Move(InPostion + NextCardOffset);
        }
        Debug.Log("moving card " + this.name + this.transform.position);
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

    public List<Card> GetCardList(Card InRootCard)
    {
        List<Card> TempCardList = new List<Card>();
        if (InRootCard)
        {
            TempCardList.Add(InRootCard);

            if (InRootCard.NextCard)
            {
                Card TempNextCard = InRootCard.NextCard;
                for (; TempNextCard;)
                {
                    if (TempNextCard)
                    {
                        TempCardList.Add(TempNextCard);
                    }
                    TempNextCard = TempNextCard.NextCard;
                }
                return TempCardList;
            }
            else 
            {
                return TempCardList;
            }
        }

        return null;
    }

    //private void RefreshCardList()
    //{
    //    if (NextCard)
    //    {
    //        //非链首 递归加入list
    //        NextCard.RefreshCardList();
    //    }
    //    else
    //    {
    //        //链首 清除list
    //        GetCardList().Clear();
    //    }
    //
    //    if (!GetCardList().Contains(this))
    //    {
    //        GetCardList().Remove(this);
    //        GetCardList().Add(this);
    //    }
    //}

    //增
    private void AddToParentListEnd(Card InParentCard)
    {
        if (InParentCard)
        {
            InParentCard.NextCard = this;
            this.PreCard = InParentCard;
        }
    }

    public void Drop(Card InPreCard, Vector3 InPosition)
    {
        CardLayout.GetComponent<MeshCollider>().enabled = true;

        if (InPreCard)
        {
            this.PreCard = InPreCard;
            transform.position = InPreCard.transform.position + NextCardOffset;
            InPreCard.NextCard = this;
            //AddToListEnd(InPreCard.CardList);
        }
        else
        {
            transform.position = InPosition;
        }

        Recipe matchRecipe = RecipeManager.Instance.MatchRecipe(GetCardList(GetListHead()));
        if (matchRecipe != null)
        {
            foreach (int outId in matchRecipe.Output_dic.Keys)
            {
                int CreateCardNum = matchRecipe.Output_dic[outId];
                while (CreateCardNum > 0)
                {
                    CardsManager.Instance.CreateCardById(outId, this.transform.parent.gameObject);
                    CreateCardNum--;
                }
            }
            //matchRecipe.inputs_items
            foreach (Card card in GetCardList(GetListHead()))
            {
                
                DestroyImmediate(card.gameObject);

            }
        }

    }

    public void LevelUp()
    { }

}
