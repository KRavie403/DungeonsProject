using System.Collections;
using System.Collections.Generic;
using System;
using System.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSet : MonoBehaviour
{
    public GameObject CharacterSet;
    public GameObject Caution;
    public CharacterDB currentCharacterDB;

    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void backCharacterSelection() //Settings
    {
        CharacterSet.SetActive(false);
    }

    void CallCaution()
    {
        GameObject obj = Instantiate(Caution, this.transform);
        Destroy(obj, 2);
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2.0f);
    }

    public void StartGame()
    {
        if(currentCharacterDB.characterList.Count < 4)
        {
            CallCaution();
        }
        else
        {
            SceneLoader.Inst.ChangeScene(1);
            this.gameObject.SetActive(false); //스테이지 비활
        }
    }
}


