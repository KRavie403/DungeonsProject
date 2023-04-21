using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSet : MonoBehaviour
{
    public GameObject Map1;
    public GameObject Map2;
    public GameObject Quit;
    public GameObject CharacterSet;
    public GameObject Map;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseCharacter() //Settings
    {
        CharacterSet.SetActive(true);
    }

    public void CloseMapSet()
    {
        Map.SetActive(false);
    }
}
