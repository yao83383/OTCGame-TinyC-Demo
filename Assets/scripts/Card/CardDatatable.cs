using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatatable", menuName = "Prefabs/CardDatatable")]
public class CardDatatable : ScriptableObject
{
    public List<FCardData> item = new List<FCardData>();
}