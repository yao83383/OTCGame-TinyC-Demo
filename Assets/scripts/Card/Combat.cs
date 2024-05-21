using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private bool isPlayerTurn = true;

    //��ҿ���List��
    [HideInInspector]
    public List<Card> PlayerCards;
    //���˿���List:
    [HideInInspector]
    public List<Card> EnemyCards;
    // Start is called before the first frame update
    void Start()
    {

        //��������

        //���Ʒ���λ��

        CalcTurn();
    }

    private bool inited = false;
    public void InitCombat(List<Card> from, List<Card> to)
    {
        PlayerCards = from;
        EnemyCards = to;

        inited = true;
    }

    public void CalcTurn()
    {
        //����ж������� > ����
        if (PlayerCards[0].CardData.ActionPreority > EnemyCards[0].CardData.ActionPreority)
        { 
            isPlayerTurn = true;
        }
        else 
        {
            isPlayerTurn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inited) return;
        //�غϿ���
        if (isPlayerTurn)
        {
            //PlayerTurn:
            Attack(PlayerCards, EnemyCards);
        }
        else
        {
            //EnemyTurn��
            Attack(EnemyCards, PlayerCards);
        }
    }

    void Attack(List<Card> from, List<Card> to)
    {
        foreach (Card card in from)
        {
            if (to.Count <= 0)
            {
                CombatOver();
                return;
            }

            card.animator.Play("attack");

            if (to[0].CardData.Health >= 0)
            {
                to[0].CardData.Health -= card.CardData.AttackPoint;
                Debug.Log(card.name + "attacked" + to[0].name + ", and Deals" + card.CardData.AttackPoint + "damage");
                if (to[0].CardData.Health < 0)
                {
                    to.RemoveAt(0);
                }
            }
            
        }
        isPlayerTurn = !isPlayerTurn;

        if (isPlayerTurn)
        {
            Debug.Log("switch to player turning");
        }
        else
        {
            Debug.Log("switch to enemy turning");
        }
    }

    void CombatOver()
    {
        Debug.Log("Combat over");
    }
}
