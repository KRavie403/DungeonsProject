using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharcterSet : MonoBehaviour
{
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

    public void OpenMapSet() //Settings
    {
        Map.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
