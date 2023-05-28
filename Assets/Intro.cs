using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    Animator animator;
    public GameObject IntroPanel;
    void Start()
    {
        animator = IntroPanel.GetComponentInChildren<Animator>();

        StartCoroutine(CheckingAnimation());

    }
    private IEnumerator CheckingAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("LogoAnimation"))
            SceneManager.LoadSceneAsync(1);
    }
}