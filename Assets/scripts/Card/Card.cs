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

    void ChangeAllChildCardsCollision(bool IsEnable)
    {
        CardLayout.GetComponent<MeshCollider>().enabled = IsEnable;

        if (NextCard)
        {
            NextCard.ChangeAllChildCardsCollision(IsEnable);
        }
    }

    public void StartMove()
    {
        ChangeAllChildCardsCollision(false);
        if (PreCard)
        { 
            PreCard.NextCard = null;
            PreCard = null;
        }
    }

    private Card GetListHead()
    {
        if (PreCard)
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

    public void Move(Vector3 InPostion)
    {
        //Vector3 TempPostion = InPostion;
        //四舍五入取cell对齐；
        //TempPostion = new Vector3((int)((TempPostion.x + TerrainGrid.Instance.cellSize / 2) / TerrainGrid.Instance.cellSize) * TerrainGrid.Instance.cellSize, TempPostion.y, (int)((TempPostion.z + TerrainGrid.Instance.cellSize / 2) / TerrainGrid.Instance.cellSize) * TerrainGrid.Instance.cellSize);
        this.transform.position = InPostion;
        if (NextCard)
        {
            NextCard.Move(InPostion + CardsManager.Instance.NextCardOffset);
        }
        Debug.Log("moving card " + this.name + this.transform.position);
    }

    //Drop位置有上一张Card时
    public void Drop(Card InPreCard)
    {
        ChangeAllChildCardsCollision(true);

        if (InPreCard)
        {
            this.PreCard = InPreCard;
            transform.position = InPreCard.transform.position + CardsManager.Instance.NextCardOffset;
            InPreCard.NextCard = this;
            //AddToListEnd(InPreCard.CardList);
        }

        MatchRecipe();
    }

    public void Drop(Vector3 InPosition)
    {
        ChangeAllChildCardsCollision(true);

        transform.position = InPosition;

        MatchRecipe();
    }

    private void MatchRecipe()
    {
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
