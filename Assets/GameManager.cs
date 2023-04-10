using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Players;

    // Start is called before the first frame update
    void Awake()
    {
        Players = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
