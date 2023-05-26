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
        //skillTextUI.GetComponentInChildren<TMP_Text>().text = CharSet.chosenDB.Skill.List[num].myTipText;
        var texts = skillTextUI.GetComponentsInChildren<TMP_Text>();
        texts[0].text = CharSet.chosenDB.Skill.List[num].myTipText;
        texts[1].text = CharSet.chosenDB.Skill.List[num].myTipText2;

        var sprites = skillTextUI.GetComponentsInChildren<Image>();

        sprites[0].sprite = CharSet.chosenDB.Skill.List[num].MySprite;
        sprites[2].sprite = CharSet.chosenDB.Skill.List[num].myTipSprite;
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
