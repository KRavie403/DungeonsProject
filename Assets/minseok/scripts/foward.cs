using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 10.0f * Time.deltaTime);
        }
    }
}
