using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundMgr : MonoBehaviour
{
    public static SoundMgr instance; //이 클래스를 어디서든 쉽게 접근, 참조가능하게 스테틱변수로만듦

    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSound;
    [SerializeField] Sound[] sfxSounds; 

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        PlayRandomBGM();
    }

    public void PlaySE(string _soundName) 
    {
        for(int i = 0; i < sfxSounds.Length; i++) //등록한 사운드의 개수만큼 반복하여 사운드찾기
        {
            if(_soundName == sfxSounds[i].soundName) // 동일한 이름의 효과음이 있다면 재생
            {
                for(int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (!sfxPlayer[x].isPlaying) //재생중이지 않은 x번째 플레이어에 전 조건문에서 찾아낸 i 번째 mp3를 넣어줌
                    {
                        sfxPlayer[x].clip = sfxSounds[i].clip; //넣어줌
                        sfxPlayer[x].Play(); //재생
                        return; // 반복문 강제로 빠져나감
                    }
                }
                Debug.Log("모든 효과음 플레이어가 사용중입니다"); //함수가 끝나지 않았을 때 뜸
                return; // 이 함수를 끝내줌
            }
        }
        Debug.Log("등록된 효과음이 없습니다");
    }

    public void PlayRandomBGM()
    {
        int random = Random.Range(0, 2); //브금 두 개면 최소값이랑 최대값 중 랜덤으로 실행되게
        bgmPlayer.clip = bgmSound[random].clip; //클립이 들어감
        bgmPlayer.Play();
    }
}
