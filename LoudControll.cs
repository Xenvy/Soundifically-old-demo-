using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoudControll : MonoBehaviour
{

    public int life = 100;
    public int fScore = 0;
    public int score = 0;
    public int level = 1;
    public int startFlag = 0;
    public int sideFlag = 0;
    public int shot = 0;
    public int fakeVol = 0;
    public int fakeVolCor = 0;
    public int trackFlag = 0;

    public int volume = 0;
    public int volumeCor = 0;
    public int track = 0;

    public Text scoreText;
    public Text scoreText2;
    public Text scoreText3;
    public Text lifeText;
    public Text levelText;
    public Text num1;
    public Text num2;
    
    public GameObject wrongText;
    public GameObject rightText;
    public GameObject WinningText;
    public GameObject LoseingText;
    public GameObject ButtonA1;
    public GameObject ButtonA2;
    public GameObject ButtonB1;
    public GameObject ButtonB2;

    [FMODUnity.EventRef]
    public string bul_sound = "event:/Loud";
    FMOD.Studio.EventInstance loud_instance;
    Rigidbody cachedRigidBody;

    void Start()
    {
        //FMOD passing parameters
        loud_instance = FMODUnity.RuntimeManager.CreateInstance(bul_sound);
        loud_instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
        loud_instance.setParameterByName("Volume", volume);
        loud_instance.setParameterByName("Track", track);
    }


    void Update()
    {
        cachedRigidBody = GetComponent<Rigidbody>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        scoreText.text = score.ToString();
        scoreText2.text = score.ToString();
        scoreText3.text = score.ToString();
        levelText.text = level.ToString();
        lifeText.text = life.ToString();

        if (sideFlag == 0)
        {
            num1.text = volumeCor.ToString();
            num2.text = fakeVolCor.ToString();
        }
        else
        {
            num1.text = fakeVolCor.ToString();
            num2.text = volumeCor.ToString();
        }

        if (score > fScore)
        {
            fScore = score;
        }
    }

    public void StartGame()
    {
        loud_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        volume = Random.Range(-30, 11);
        track = Random.Range(0, 11);
        fakeVol = volume + Random.Range((8-level), (10/level));
        fakeVolCor = fakeVol + 10;
        volumeCor = volume + 10;
        sideFlag = Random.Range(0, 2);
        startFlag = 1;
        wrongText.SetActive(false);
        rightText.SetActive(false);

        if (sideFlag == 0)
        {
            ButtonA1.SetActive(true);
            ButtonA2.SetActive(false);
            ButtonB1.SetActive(false);
            ButtonB2.SetActive(true);
            num1.text = volumeCor.ToString();
            num2.text = fakeVolCor.ToString();
        }
        else
        {
            ButtonA1.SetActive(false);
            ButtonA2.SetActive(true);
            ButtonB1.SetActive(true);
            ButtonB2.SetActive(false);
            num1.text = fakeVolCor.ToString();
            num2.text = volumeCor.ToString();
        }

    }

    public void OrginalSound()
    {
        if (startFlag == 1)
        {
            loud_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            loud_instance.setParameterByName("Volume", -10);
            loud_instance.setParameterByName("Track", track);
            loud_instance.start();
        }

    }
    public void AdjustedSound()
    {
        if (startFlag == 1)
        {
            loud_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            loud_instance.setParameterByName("Volume", volume);
            loud_instance.setParameterByName("Track", track);
            loud_instance.start();
        }
    }

    public void GoodAnsw()
    {
        loud_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        rightText.SetActive(true);

        if (shot == 5)
        {
            level++;
            shot = 0;
            score += level * life;
        }
        else
        {
            shot += 1;
            score += level * life;
        }
    }

    public void WrongAnsw() 
    {
        loud_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        if (life == 10)
        {
            LoseingText.SetActive(true);
        }
        else
        {
            wrongText.SetActive(true);
            ButtonA1.SetActive(false);
            ButtonA2.SetActive(false);
            ButtonB1.SetActive(false);
            ButtonB2.SetActive(false);
            life -= 10;
        }

    }

    public void BackToMain()
    {
        if (fScore > PlayerPrefs.GetInt("LoudHS1", 0))
        {
            PlayerPrefs.SetInt("LoudHS3", PlayerPrefs.GetInt("LoudHS2", 0));
            PlayerPrefs.SetInt("LoudHS2", PlayerPrefs.GetInt("LoudHS1", 0));
            PlayerPrefs.SetInt("LoudHS1", score);
            
        }
        else if (fScore < PlayerPrefs.GetInt("LoudHS1", 0) && fScore > PlayerPrefs.GetInt("LoudHS2", 0))
        {
            PlayerPrefs.SetInt("LoudHS3", PlayerPrefs.GetInt("LoudHS2", 0));
            PlayerPrefs.SetInt("LoudHS2", score);
        }
        else if (fScore < PlayerPrefs.GetInt("LoudHS2", 0) && fScore > PlayerPrefs.GetInt("LoudHS3", 0))
        {
            PlayerPrefs.SetInt("LoudHS3", score);
        }
    }
}
