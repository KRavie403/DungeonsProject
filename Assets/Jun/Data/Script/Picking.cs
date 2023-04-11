using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : CharactorMovement
{
    public LayerMask pickMask;
    public UnityEvent<Vector2Int> clickAction = null;

    private Vector2Int currentHover;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
        {
            Player.STATE _curState = this.GetComponent<Player>().GetState();
            if (_curState == Player.STATE.MOVE)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if ((1 << hit.transform.gameObject.layer & pickMask) != 0)
                    {
                        Debug.Log($"Hit Layer : {hit.transform.gameObject.layer}");
                        clickAction?.Invoke(GMMap.GetTileIndex(hit.transform.gameObject));
                    }

                }
                else
                {
                    //Debug.Log(GB.GetTileIndex(hit.transform.gameObject));
                    Vector2Int hitPos = GMMap.GetTileIndex(hit.transform.gameObject);
                    if (currentHover == -Vector2Int.one)
                    {
                        currentHover = hitPos;
                        GMMap.tiles[hitPos.x, hitPos.y].layer = 8;
                    }
                    if (currentHover != hitPos)
                    {
                        if (GMMap.CheckTileVisited(currentHover.x, currentHover.y) == -1)
                            GMMap.tiles[currentHover.x, currentHover.y].layer = 3;
                        else
                            GMMap.tiles[currentHover.x, currentHover.y].layer = 9;
                        currentHover = hitPos;
                        GMMap.tiles[hitPos.x, hitPos.y].layer = 8;
                    }
                }
            }
        }
        else
        {
            if (currentHover != -Vector2Int.one)
            {
                GMMap.tiles[currentHover.x, currentHover.y].layer = 3;
                currentHover = -Vector2Int.one;
            }
        }

    }
}