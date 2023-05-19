using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Instancing : MonoBehaviour
{
    public GameObject orgObj;

    private void Start()
    {
        for(int i = 0; i < 100; ++i)
        {
            Instantiate(orgObj, new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f)), Quaternion.identity);
        }
    }
}
