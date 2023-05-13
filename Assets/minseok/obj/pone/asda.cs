using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asda : MonoBehaviour
{
    Coroutine move = null;
    float Range = 1.0f;
    Vector3 startPos, destPos;
    float t = 0.0f;
    float Dir = 0.5f;
    void Start()
    {
        t = 0.5f;
        destPos = startPos = transform.position;
        startPos.y -= Range / 1.0f;
        destPos.y += Range / 1.0f;

        move = StartCoroutine(Moving());
    }
    void Update()
    {
        
    }
    Vector3 targetPos;
    public float Velocity = 2.0f;
    IEnumerator Moving()
    {
        while (true)
        {
            t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 0.5f);
            if (Mathf.Approximately(t, 0.0f) || Mathf.Approximately(t, 0.5f))
            {
                Dir *= -1.0f;
            }
            transform.position = Vector3.Lerp(startPos, destPos, t);
            yield return null;
            targetPos = transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPos, Velocity * Time.deltaTime);
        }
        
    }
}
