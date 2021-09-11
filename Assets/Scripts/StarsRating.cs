using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class StarsRating : MonoBehaviour
{

    Animator anim;

    ParticleSystem particle;

    AudioSource audioClip;

    void Start()
    {
        anim = GetComponent<Animator>();

        particle = GetComponentInChildren<ParticleSystem>();

        audioClip = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {


        Ranking(2);

    }


    private void Ranking(int to)
    {

        PlayAddition();

        anim.SetTrigger(to.ToString());

    }

    void k()
    {
        Debug.Log("音频停止");
    }

    Coroutine cor;

    private void PlayAddition()
    {
        particle.Play();

        audioClip.Play();

        if (cor != null)
        {
            StopCoroutine(cor);
        }

        cor = StartCoroutine(AudioCallBack(audioClip, k));
    }

    private IEnumerator AudioCallBack(AudioSource AudioObject, UnityAction action)
    {
        //yield return new WaitForSecondsRealtime(audioClip.clip.length);

        while (AudioObject.isPlaying)
        {
            yield return new WaitForSecondsRealtime(0.1f);//延迟零点一秒执行
        }
        action();
    }

}
