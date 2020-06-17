using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_menu : MonoBehaviour
{
    public Text PanHS1;
    public Text PanHS2;
    public Text PanHS3;
    public Text LoudHS1;
    public Text LoudHS2;
    public Text LoudHS3;
    public Text CompHS1;
    public Text CompHS2;
    public Text CompHS3;
    public Text EqHS1;
    public Text EqHS2;
    public Text EqHS3;


    public void Update()
    {

        PanHS1.text = PlayerPrefs.GetInt("PanHS1", 0).ToString();
        PanHS2.text = PlayerPrefs.GetInt("PanHS2", 0).ToString();
        PanHS3.text = PlayerPrefs.GetInt("PanHS3", 0).ToString();
        LoudHS1.text = PlayerPrefs.GetInt("LoudHS1", 0).ToString();
        LoudHS2.text = PlayerPrefs.GetInt("LoudHS2", 0).ToString();
        LoudHS3.text = PlayerPrefs.GetInt("LoudHS3", 0).ToString();
        CompHS1.text = PlayerPrefs.GetInt("CompHS1", 0).ToString();
        CompHS2.text = PlayerPrefs.GetInt("CompHS2", 0).ToString();
        CompHS3.text = PlayerPrefs.GetInt("CompHS3", 0).ToString();
        EqHS1.text = PlayerPrefs.GetInt("EqHS1", 0).ToString();
        EqHS2.text = PlayerPrefs.GetInt("EqHS2", 0).ToString();
        EqHS3.text = PlayerPrefs.GetInt("EqHS3", 0).ToString();
    }

    public void RestScore()
    {
        PlayerPrefs.DeleteAll();
        PanHS1.text = "0";
        PanHS2.text = "0";
        PanHS3.text = "0";
        LoudHS1.text = "0";
        LoudHS2.text = "0";
        LoudHS3.text = "0";
        CompHS1.text = "0";
        CompHS2.text = "0";
        CompHS3.text = "0";
        EqHS1.text = "0";
        EqHS2.text = "0";
        EqHS3.text = "0";
    }

    public void OpenMenu()
    {
        
        SceneManager.LoadScene("Main_menu");

    }

    public void PlayPan()
    {
        SceneManager.LoadScene("Pandio");
    }

    public void PlayEQ()
    {
        SceneManager.LoadScene("EQ excercise");
    }

    public void PlayComp()
    {
        SceneManager.LoadScene("Compression excercise");
    }

    public void PlayLoud()
    {
        SceneManager.LoadScene("Loudness");
    }

    public void CompInfo()
    {
        SceneManager.LoadScene("Compression excercise info");
    }

    public void EQInfo()
    {
        SceneManager.LoadScene("EQ excercise instructions");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }    

}
