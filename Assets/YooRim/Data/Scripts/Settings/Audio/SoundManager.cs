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
    
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) //����� ��� �޼ҵ�
    {
        for(int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name) // ���� �̸��� �Ű������� ���� �� �� ����
                BgSoundPlay(bglist[i]);

        }
        // ���� �̸��� Ŭ���� �̸��� �������� ȣ��
    }

    public void BgSoundVolume(float val) //����
    {
        mixer.SetFloat("BgSoundVolume", val); 
        //�ͼ��� �Ķ���Ͱ��� �Ǽ��� ���� �־��� �̶� �Ѱ��ִ� ���� ������ ���� �Ǵµ� �ͼ��� ������ �α׽������� ��������� ������ ���� ���ؼ� ��ȯ�� ���� �Ѱ������ ����� ���� ��ȭ�� �̷����
        //�����̴��� �����ϰ� �ּҰ��� 0.0001�� �ٲ��� -> On Value Changed�� �� �޼��带 �߰����� ->���� �� �����̴��� �����غ��� ����� ���� ������.

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
        GameObject go = new GameObject(sfxName + "Sound"); //������Ʈ �����ϰ� �̸��� ����� �ٲ���
        AudioSource audiosource = go.AddComponent<AudioSource>(); //���� ����� ���� ������ҽ�������Ʈ �߰� ��
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0]; //�ͼ����� �׷��� ã�ƿ� �־��ذ� �ͼ����� ���������ϸ� ������ ��
        audiosource.clip = clip; // ����� Ŭ���� �Ű������� �޾Ƽ� ����
        audiosource.Play(); //�Լ�ȣ��

        Destroy(go, clip.length); //��������� �����ߴ� ������Ʈ �ı�
            // �� ��° ���ڰ����� Ŭ���� ���̸� �־��ָ� ����� ������ ���� ������Ʈ�� �ı���
            // ����Ʈ�� ����Ǵ� �ڵ忡 -> ����Ŵ���.�� �Լ� ȣ��("�̸�", �����Ŭ��)�� �Ѱ���, �����Ŭ���� �������� public AudioClip clip;
            // ����Ƽ���� ����Ʈ�� ����� ��ũ��Ʈ�� �ִ� �ν����� â�� ��������� Ŭ�� â�� Ŭ���� �߰� => ���: ������Ʈ �����ǰ� ȿ���������
    }

    public void BgSoundPlay(AudioClip clip) //������Լ�
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BgSound")[0];
        bgSound.clip = clip; //Ŭ���־��ְ�
        bgSound.loop = true; // �ݺ����
        bgSound.volume = 0.1f;
        bgSound.Play(); // �÷����Լ� ȣ��
    }
}
