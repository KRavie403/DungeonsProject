using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{
    LayerMask targetmask;
    UnityAction<Vector3> Actions = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Vector3.forward);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, targetmask)){
                Actions?.Invoke(hit.transform.position);
            }
        }
    }
}
