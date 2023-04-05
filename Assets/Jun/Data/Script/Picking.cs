using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{
    public LayerMask pickMask;
    public UnityEvent<Vector3> clickAction = null;

    private GameBoard GB = null;
    private Vector2Int currentHover;
    // Start is called before the first frame update
    void Start()
    {
        GB = GameObject.FindGameObjectWithTag("GameMapManager").GetComponent<GameBoard>();
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if ((1 << hit.transform.gameObject.layer & pickMask) != 0)
                {
                    Debug.Log($"Hit Layer : {hit.transform.gameObject.layer}");
                    clickAction?.Invoke(hit.transform.position);
                }

            }
            else
            {
                //Debug.Log(GB.GetTileIndex(hit.transform.gameObject));
                Vector2Int hitPos = GB.GetTileIndex(hit.transform.gameObject);
                if (currentHover == -Vector2Int.one)
                {
                    currentHover = hitPos;
                    GB.tiles[hitPos.x, hitPos.y].layer = 8;
                }
                if (currentHover != hitPos)
                {
                    GB.tiles[currentHover.x, currentHover.y].layer = 3;
                    currentHover = hitPos;
                    GB.tiles[hitPos.x, hitPos.y].layer = 8;
                }
            }
        }
        else
        {
            if (currentHover != -Vector2Int.one)
            {
                GB.tiles[currentHover.x, currentHover.y].layer = 3;
                currentHover = -Vector2Int.one;
            }
        }

    }
}