using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    // 자기 자신을 인스턴스화 시키는 코드!!!
    public static SoundManager instance;

    [SerializeField] Sound[] effectSounds;
    [SerializeField] AudioSource[] effectPlayer;

    [SerializeField] Sound[] bgmSounds;
    [SerializeField] AudioSource bgmPlayer;

    [SerializeField] AudioSource voicePlayer;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void PlayBGM(string p_name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if(p_name == bgmSounds[i].name)
            {
                bgmPlayer.clip = bgmSounds[i].clip;
                bgmPlayer.Play();
                return;
            }
        }
        Debug.LogError(p_name + "에 해당되는 BGM이 없습니다.");
    }

    void StopBGM()
    {
        bgmPlayer.Stop();
    }

    void PauseBGM()
    {
        bgmPlayer.Pause();
    }

    void UnPauseBGM()
    {
        bgmPlayer.UnPause();
    }

    void PlayEffectSound(string p_name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(p_name == effectSounds[i].name)
            {
                for (int j = 0; j < effectPlayer.Length; j++)
                {
                    if(!effectPlayer[j].isPlaying)
                    {
                        effectPlayer[j].clip = effectSounds[i].clip;
                        effectPlayer[j].Play();
                        return;
                    }
                }
                Debug.LogError("모든 효과음 플레이어가 사용중입니다.");
                return;
            }
        }
        Debug.LogError(p_name + "에 해당되는 효과음 사운드가 없습니다.");
    }

    void StopAllEffectSound()
    {
        for (int i = 0; i < effectPlayer.Length; i++)
        {
            effectPlayer[i].Stop();
        }
    }

    void PlayVoiceSound(string p_name)
    {
        AudioClip _clip = Resources.Load<AudioClip>("Sounds/Voice/" + p_name);
        if(_clip != null)
        {
            voicePlayer.clip = _clip;
            voicePlayer.Play();
        }
        else
        {
            Debug.LogError(p_name + "에 해당되는 보이스 사운드가 없습니다.");
        }
    }

    ///
    ///  p_Type : 0 -> 브금 재생
    ///  p_Type : 1 -> 효과음 재생
    ///  p_Type : 2 -> 보이스 재생
    ///
    public void PlaySound(string p_name, int p_Type)
    {
        if (p_Type == 0) PlayBGM(p_name);
        else if (p_Type == 1) PlayEffectSound(p_name);
        else PlayVoiceSound(p_name);
    }
}
