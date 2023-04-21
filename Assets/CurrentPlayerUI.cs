using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayerUI : MonoBehaviour
{
    List<GameObject> Skills;
    List<GameObject> Items;

    void Awake()
    {
        Skills = new List<GameObject>(8);
        Items = new List<GameObject>(8);
    }
    
}
