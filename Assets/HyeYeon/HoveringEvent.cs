using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class HoveringEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//, IPointerMoveHandler
{

    public GameObject skillTextUI;      //��ų ���� UI
    public CharcterSet CharSet;
    public int num = 0;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        skillTextUI.SetActive(true);
        skillTextUI.GetComponentInChildren<TMP_Text>().text = CharSet.chosenDB.Skill.List[num].myTipText;
        //skillTextUI.GetComponentInChildren<Image>().sprite = chosenDB.Skill.List[n].MySprite;
        // ��ų�� ���� ���� UI skillTextUI.gameObject.SetActive(true)�� ���ش�.

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ��ų�� ���� ���� UI skillTextUI.gameObject.SetActive(false)�� ���ش�.
        skillTextUI.SetActive(false);
    }

    /*public void OnPointerMove(PointerEventData eventData)
    {
        // ��ų�� ���� ���� UI skillTextUI�� ���콺 ��ġ�� �̵�
    }*/

}
