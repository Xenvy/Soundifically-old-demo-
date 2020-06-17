using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Data.SqlTypes;
using System.CodeDom.Compiler;
using UnityEditor;

public class Click_reste_button : MonoBehaviour
{
    //UnityEngine class instances
    public GameObject circelPrefab;
    public GameObject GameOverMenu;
    public GameObject WinningMenu;
    public GameObject SpeakerPoint;

    //Structure variables collecting the cursor position
    public Vector3 center;
    public Vector3 size;
    public Vector3 scaleChange;
    public Vector3 speakrePos;

    //Variables needed to run the exercise
    public int life = 100;
    public int score = 0;
    public int level = 1;
    public int shot = 0;

    //Text variables for displaying data in the GUI
    public Text scoreText;
    public Text scoreText2;
    public Text scoreText3;
    public Text lifeText;
    public Text levelText;

    //Variables used to control the sound engine
    public int sound_r = 0;
    public int sound_x = 0;
    public int marker = 1;

    //FMOD instance initiation
    [FMODUnity.EventRef]
    public string bul_sound = "event:/bul";
    FMOD.Studio.EventInstance bul_sound_instance;
    Rigidbody cachedRigidBody;

    void Start()
    {

        //FMOD parameters
        bul_sound_instance = FMODUnity.RuntimeManager.CreateInstance(bul_sound);
        bul_sound_instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
        bul_sound_instance.setParameterByName("sound_r", sound_r);
        bul_sound_instance.setParameterByName("sound_x", sound_x);
        bul_sound_instance.setParameterByName("Track", marker);
        bul_sound_instance.start();
    }



    void Update()
    {
        cachedRigidBody = GetComponent<Rigidbody>();

        //Getting cursor location
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        //Conditions of handling click the mouse on the appropriate fields of the board
        if (hit.collider == null && Input.GetMouseButtonDown(0))
        {
            life = life - 10;
            Console.WriteLine(life);
            lifeText.text = life.ToString();
            if (life == 0)
            {
                GameOver();
            }
        }
        else if (hit.collider != null)
        {

            if (hit.collider)
            {

                if (hit.collider.tag == "Reset_button")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        bul_sound_instance.release();
                        bul_sound_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                        score += level * life;
                        scoreText.text = score.ToString();
                        scoreText2.text = score.ToString();
                        scoreText3.text = score.ToString();
                        levelText.text = level.ToString();
                        shot += 1;
                        if (shot % 5 == 0)
                        {
                            scaleChange = new Vector3(-1.8f, -1.8f , 0);
                            circelPrefab.transform.localScale += scaleChange;
                            level += 1;
                            shot = 0;

                        }
                        if (level == 11)
                        {
                            Winning();
                        }
                        SpawnNextButton();
                    }
                }
                else if (hit.collider.tag == "Back" && Input.GetMouseButtonDown(0))
                {
                    bul_sound_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }

        }

    }

    //Function handling button generation
    void SpawnNextButton()
    {
        float x = UnityEngine.Random.Range(-3.65f, 3.65f);
        Double y = Math.Sqrt(Math.Pow(3.65f, 2) - Math.Pow(x, 2));
        float yf = Convert.ToSingle(y);
        float yy = UnityEngine.Random.Range(-yf, yf);
        int zz = UnityEngine.Random.Range(1, 11);
 
        Vector3 pos = center + new Vector3(x, yy, 0);
        speakrePos = center + new Vector3(x, yy, 0);
        SpeakerPoint.transform.localPosition = speakrePos;

        sound_r = Convert.ToInt32((Math.Sqrt(Math.Pow(yy, 2) + Math.Pow(x, 2))) * 10);
        sound_x = Convert.ToInt32((Math.Atan2(yy, x) * 10));
        marker = zz;

        Instantiate(circelPrefab, pos, Quaternion.identity);

        Destroy(gameObject);

    }

    //Losing board / passing the highest scores to the results board
    void GameOver()
    {
        bul_sound_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if (score > PlayerPrefs.GetInt("PanHS1", 0))
        {
            PlayerPrefs.SetInt("PanHS3", PlayerPrefs.GetInt("PanHS2", 0));
            PlayerPrefs.SetInt("PanHS2", PlayerPrefs.GetInt("PanHS1", 0));
            PlayerPrefs.SetInt("PanHS1", score);
        }
        else if (score < PlayerPrefs.GetInt("PanHS1", 0) && score > PlayerPrefs.GetInt("PanHS2", 0))
        {
            PlayerPrefs.SetInt("PanHS3", PlayerPrefs.GetInt("PanHS2", 0));
            PlayerPrefs.SetInt("PanHS2", score);
        }
        else if (score < PlayerPrefs.GetInt("PanHS2", 0) && score > PlayerPrefs.GetInt("PanHS3", 0))
        {
            PlayerPrefs.SetInt("PanHS3", score);
        }

        Destroy(gameObject);
        GameOverMenu.SetActive(true);
        SpeakerPoint.SetActive(true);


    }

    //Victory board / passing the highest scores to the results board
    void Winning()
    {

        if (score > PlayerPrefs.GetInt("PanHS1", 0))
        {
            PlayerPrefs.SetInt("PanHS3", PlayerPrefs.GetInt("PanHS2", 0));
            PlayerPrefs.SetInt("PanHS2", PlayerPrefs.GetInt("PanHS1", 0));
            PlayerPrefs.SetInt("PanHS1", score);
        }
        else if (score < PlayerPrefs.GetInt("PanHS1", 0) && score > PlayerPrefs.GetInt("PanHS2", 0))
        {
            PlayerPrefs.SetInt("PanHS3", PlayerPrefs.GetInt("PanHS2", 0));
            PlayerPrefs.SetInt("PanHS2", score);
        }
        else if (score < PlayerPrefs.GetInt("PanHS2", 0) && score > PlayerPrefs.GetInt("PanHS3", 0))
        {
            PlayerPrefs.SetInt("PanHS3", score);
        }

        bul_sound_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        WinningMenu.SetActive(true);
    }

   

}
