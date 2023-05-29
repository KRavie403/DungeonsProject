using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer mixer;
    public AudioSource bgSound;
    public AudioClip[] bglist;


    [SerializeField] private AudioSource _musicSource, _effectsSource;
    
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) //배경음 재생 메소드
    {
        for(int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name) // 씬의 이름은 매개변수를 통해 알 수 있음
                BgSoundPlay(bglist[i]);

        }
        // 씬의 이름과 클립의 이름이 같은것을 호출
    }

    public void BgSoundVolume(float val) //볼륨
    {
        mixer.SetFloat("BgSoundVolume", val); 
        //믹서에 파라메터값과 실수형 값을 넣어줌 이때 넘겨주는 값이 볼륨의 값이 되는데 믹서의 볼륨은 로그스케일을 사용함으로 간단한 식을 통해서 변환한 값을 넘겨줘야지 제대로 음량 변화가 이루어짐
        //슬라이더를 생성하고 최소값을 0.0001로 바꿔줌 -> On Value Changed에 이 메서드를 추가해줌 ->실행 후 슬라이더를 조절해보면 배경음 볼륨 조절됨.

    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SFXPlay(string sfxName, AudioClip clip) 
    {
        GameObject go = new GameObject(sfxName + "Sound"); //오브젝트 생성하고 이름을 사운드로 바꿔줌
        AudioSource audiosource = go.AddComponent<AudioSource>(); //사운드 재생을 위해 오디오소스컴포넌트 추가 후
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0]; //믹서에서 그룹을 찾아와 넣어준것 믹서에서 볼륨조절하면 영향을 줌
        audiosource.clip = clip; // 오디오 클립도 매개변수로 받아서 적용
        audiosource.Play(); //함수호출

        Destroy(go, clip.length); //재생끝나면 생성했던 오브젝트 파괴
            // 두 번째 인자값으로 클립의 길이를 넣어주면 재생이 끝나고 나서 오브젝트가 파괴됨
            // 이펙트가 실행되는 코드에 -> 사운드매니저.이 함수 호출("이름", 재생할클립)을 넘겨줌, 오디오클립도 선언해줌 public AudioClip clip;
            // 유니티에서 이펙트가 실행될 스크립트가 있는 인스펙터 창에 선언해줬던 클립 창에 클립을 추가 => 결과: 오브젝트 생성되고 효과음재생됨
    }

    public void BgSoundPlay(AudioClip clip) //배경음함수
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BgSound")[0];
        bgSound.clip = clip; //클립넣어주고
        bgSound.loop = true; // 반복재생
        bgSound.volume = 0.1f;
        bgSound.Play(); // 플레이함수 호출
    }
}
