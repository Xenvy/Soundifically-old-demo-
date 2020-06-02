using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class Click_reste_button : MonoBehaviour
{
    public GameObject circelPrefab;

    public Vector3 center;
    public Vector3 size;

    public int life = 100;
    public int score = 0;
    public int level = 1;
    public float vol = 50;
    public int marker = 1;

    public Text scoreText;
    public Text lifeText;
    public Text levelText;

    public Texture2D gameCoursor;

    public int sound_r = 0;
    public int sound_x = 0;

    //FMOD instance initiation
    [FMODUnity.EventRef]
    public string bul_sound = "event:/bul";
    FMOD.Studio.EventInstance bul_sound_instance;
    Rigidbody cachedRigidBody;

    void Start()
    {
        //FMOD passing parameters
        bul_sound_instance = FMODUnity.RuntimeManager.CreateInstance(bul_sound);
        bul_sound_instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
        bul_sound_instance.setParameterByName("sound_r", sound_r);
        bul_sound_instance.setParameterByName("sound_x", sound_x);
        bul_sound_instance.setParameterByName("Track", marker);
        bul_sound_instance.setVolume(5f);
        bul_sound_instance.start();
    }



    void Update()
    {
        cachedRigidBody = GetComponent<Rigidbody>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);


        if (hit.collider == null && Input.GetMouseButtonDown(0))
        {
            life = life - 10;
            Console.WriteLine(life);
            lifeText.text = life.ToString();
        }
        else if (hit.collider != null)
        {
            if (life > 0)
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
                            levelText.text = level.ToString();
                            SpawnNextButton();
                        }
                    }
                    else if (hit.collider.tag == "VolumeMenu" && Input.GetMouseButtonDown(0))
                    {
                        SetVolume(vol);
                    }
                    else if (hit.collider.tag == "Back" && Input.GetMouseButtonDown(0))
                    {
                        bul_sound_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    }
                }

            }
            else
            {
                GameOver();
            }
        }
        else
        {

        }

    }

    void SpawnNextButton()
    {
        float x = UnityEngine.Random.Range(-3.65f, 3.65f);
        Double y = Math.Sqrt(Math.Pow(3.65f, 2) - Math.Pow(x, 2));
        float yf = Convert.ToSingle(y);
        float yy = UnityEngine.Random.Range(-yf, yf);
        float zz = UnityEngine.Random.Range(1, 3);

        Vector3 pos = center + new Vector3(x, yy, 0);
        sound_r = Convert.ToInt32((Math.Sqrt(Math.Pow(yy, 2) + Math.Pow(x, 2))) * 10);
        sound_x = Convert.ToInt32((Math.Atan2(yy, x) * 10));
        marker = Convert.ToInt32(zz);
        Instantiate(circelPrefab, pos, Quaternion.identity);

        Destroy(gameObject);

    }

    void GameOver()
    {
        cachedRigidBody = GetComponent<Rigidbody>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        bul_sound_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        if (hit.collider.tag == "VolumeMenu" && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void MusicStop()
    {
        bul_sound_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetVolume(float newVolume)
    {
        vol = newVolume;
        bul_sound_instance.setVolume(vol);
    }


    void OnMouseEnter()
    {
        //Cursor.visible = false;
        //Cursor.SetCursor(gameCoursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
