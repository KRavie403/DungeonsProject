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
        //메모리 중요( 중간 로딩 씬 필요 why : 씬 로딩시 이전 씬과 이후 씬의 메모리가 한 순간 같이 생기는 경우가 발생하기 때문)

    }

    IEnumerator Loading(int i)
    {
        yield return SceneManager.LoadSceneAsync(4);
        SceneLoaderText.Inst.Setting(i);
        Slider loadingSlider = FindAnyObjectByType<Slider>();  

        AsyncOperation op = SceneManager.LoadSceneAsync(i);     //return AsyncOperation operation;
        
        op.allowSceneActivation = false;    //씬 로딩이 끝나면 바로 해당 씬을 바로 활성화

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
