using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatatable", menuName = "Prefabs/CardDatatable")]
public class CardDatatable : ScriptableObject
{
    public List<FCardData> Cards = new List<FCardData>();
}

[System.Serializable]
public struct FCardData
{
    public int CardId; // ��Ʒ��Ψһ��ʶ��  
    public string CardName; // ��Ʒ����
    public int CostGold;

    public Sprite SpriteRef;
    public GameObject PrefabRef;

    [HideInInspector]
    public string spritename;
    [HideInInspector]
    public string prefabname;
}