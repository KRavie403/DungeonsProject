using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayerUI : MonoBehaviour
{
    GameObject[] Skills;
    GameObject[] Items;

    void Awake()
    {
        Skills = new GameObject[10];
        Items = new GameObject[10];
    }
    
}
