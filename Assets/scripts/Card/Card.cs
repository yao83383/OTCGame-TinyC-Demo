using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Contains Cards from this to the end;
    //  PreCards not included;
    [HideInInspector]
    public List<Card> CardList;
    private string CardName;
    //private Attac

    private Card PreCard;
    private Card NextCard;
    private GameObject NextEmptyCard;
    [HideInInspector]
    public Vector3 NextCardOffset = new Vector3(0f, 0.5f, 0f);


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
            return NextCard.GetListEnd();
        }
        else
        {
            return this;
        }
    }

    private void RefreshListHead()
    { 
        
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
    }
}
