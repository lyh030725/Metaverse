using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    void Start()
    {
        
    }

    public AudioClip audioClip1;
    public AudioClip audioClip2;

    void Update()
    {
        
    }

    int i = 0;

    private void OnTriggerEnter(Collider other)
    {
        i++;

        if(i%2 == 0)
        {
            Managers.Sound.Play(audioClip1, Define.Sound.Bgm);
        }
        else
        {
            Managers.Sound.Play(audioClip2, Define.Sound.Effect);
        }
    }
}
