using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressionExcercise : MonoBehaviour
{
    private int sample1;
    private int sample2;
    private bool sample1_correct;
    public AudioClip[] samples;
    private AudioSource audio_source;
    GameObject text;
    GameObject text2;
    GameObject text3;
    private void ChooseRandomSample()
    {
        sample1 = Random.Range(0, samples.Length);
        if(sample1 % 2 == 0)
        {
            sample2 = sample1 + 1;
            sample1_correct = false;
        }
        else
        {
            sample2 = sample1 - 1;
            sample1_correct = true;
        }
    }
    void Awake()
    {
        audio_source = FindObjectOfType<AudioSource>();
        ChooseRandomSample();
        text = GameObject.Find("Correct");
        text2 = GameObject.Find("Incorrect");
        text3 = GameObject.Find("NextButton");
        text.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
    }
    public void PlaySample1()
    {
        audio_source.clip = samples[sample1];
        audio_source.Play();
    }
    public void Playsample2()
    {
        audio_source.clip = samples[sample2];
        audio_source.Play();
    }
    public void ChooseSample1()
    {
        if(sample1_correct==true)
        {
            text.SetActive(true);
        }
        else
        { 
            text2.SetActive(true);
        }
        text3.SetActive(true);
    }

    public void ChooseSample2()
    {
        if (sample1_correct == false)
        {
            text.SetActive(true);
        }
        else
        {
            text2.SetActive(true);
        }
        text3.SetActive(true);
    }

    public void Next()
    {
        text.SetActive(false);
        text2.SetActive(false);
        ChooseRandomSample();
        text3.SetActive(false);
    }
}
