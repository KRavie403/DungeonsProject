using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{
    public LayerMask pickMask;
    public UnityEvent<Vector3> clickAction = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
            {
                if ((1 << hit.transform.gameObject.layer & pickMask) != 0)
                {
                    clickAction?.Invoke(hit.transform.position);
                }

            }
        }

    }

}