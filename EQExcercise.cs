using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EQExcercise : MonoBehaviour
{
    private int sample1;
    private int sample2;
    public double[] frequencies;
    public AudioClip[] samples;
    private AudioSource audio_source;
    private double correct_frequency;
    private bool is_boosted;
    public Slider frequency_slider;
    public TextMeshProUGUI current_frequency;
    public TextMeshProUGUI CorrectFrequency;
    private double slider_value;
    GameObject text;
    GameObject text2;
    GameObject text3;
    GameObject text4;
    GameObject text5;
    GameObject text6;
    GameObject text7;
    GameObject text8;
    GameObject text9;
    GameObject text10;
    private void ChooseRandomSample()
    {
        sample1 = 9*Random.Range(0, samples.Length/9);
        sample2 = Random.Range(0,9);
        if (sample2 < 5)
        {
            is_boosted = true;
        }
        else
        {
            is_boosted = false;
        }
        sample2 += sample1;
        correct_frequency = frequencies[sample2];
        CorrectFrequency.text = correct_frequency + " Hz";
    }
    void Awake()
    {
        audio_source = FindObjectOfType<AudioSource>();
        ChooseRandomSample();
        text = GameObject.Find("Title");
        text2 = GameObject.Find("Title2");
        text3 = GameObject.Find("Perfect");
        text4 = GameObject.Find("Excellent");
        text5 = GameObject.Find("Good");
        text6 = GameObject.Find("Close");
        text7 = GameObject.Find("TryAgain");
        text8 = GameObject.Find("NextButton");
        text9 = GameObject.Find("CheckButton");
        text10 = GameObject.Find("CorrectFrequency");
        if(is_boosted == false)
        {
            text.SetActive(false);
        }
        else
        {
            text2.SetActive(false);
        }
        text3.SetActive(false);
        text4.SetActive(false);
        text5.SetActive(false);
        text6.SetActive(false);
        text7.SetActive(false);
        text8.SetActive(false);
        text10.SetActive(false);
    }
    private void Update()
    {
        current_frequency.text = (int)(20 * Mathf.Pow(2, frequency_slider.value)) + " Hz";
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
    public void Check_correct()
    {
        slider_value = 20 * Mathf.Pow(2, frequency_slider.value);
        if (correct_frequency / slider_value > 0.95 && correct_frequency / slider_value < 1.05)
        {
            text3.SetActive(true);
        }
        else if(correct_frequency / slider_value > 0.90 && correct_frequency / slider_value < 1.1)
        {
            text4.SetActive(true);
        }
        else if(correct_frequency / slider_value > 0.8 && correct_frequency / slider_value < 1.2)
        {
            text5.SetActive(true);
        }
        else if(correct_frequency / slider_value > 0.7 && correct_frequency / slider_value < 1.3)
        {
            text6.SetActive(true);
        }
        else
        {
            text7.SetActive(true);
        }
        audio_source.Stop();
        text9.SetActive(false);
        text8.SetActive(true);
        text10.SetActive(true);
    }
    public void Next()
    {
        text.SetActive(false);
        text2.SetActive(false);
        ChooseRandomSample();
        if (is_boosted == true)
        {
            text.SetActive(true);
        }
        else
        {
            text2.SetActive(true);
        }
        text3.SetActive(false);
        text4.SetActive(false);
        text5.SetActive(false);
        text6.SetActive(false);
        text7.SetActive(false);
        text8.SetActive(false);
        text10.SetActive(false);
        text9.SetActive(true);
    }
}
