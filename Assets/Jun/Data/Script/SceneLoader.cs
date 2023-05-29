using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeScene(int i)
    {
        StartCoroutine(Loading(i));
        //�޸� �߿�( �߰� �ε� �� �ʿ� why : �� �ε��� ���� ���� ���� ���� �޸𸮰� �� ���� ���� ����� ��찡 �߻��ϱ� ����)

    }

    IEnumerator Loading(int i)
    {
        yield return SceneManager.LoadSceneAsync(4);
        SceneLoaderText.Inst.Setting(i);
        Slider loadingSlider = FindAnyObjectByType<Slider>();  

        AsyncOperation op = SceneManager.LoadSceneAsync(i);     //return AsyncOperation operation;
        
        op.allowSceneActivation = false;    //�� �ε��� ������ �ٷ� �ش� ���� �ٷ� Ȱ��ȭ

        while (!op.isDone)
        {
            yield return new WaitForSeconds(0.5f);
            loadingSlider.value = op.progress / 0.9f;  //(0~0.9)
            Debug.Log($"{loadingSlider.value}");

            if (Mathf.Approximately(loadingSlider.value, 1.0f))
            {
                op.allowSceneActivation = true;
            }
            //yield return new WaitForSeconds(0.5f);

        }



    }

}
