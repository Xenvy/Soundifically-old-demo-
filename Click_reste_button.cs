using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Click_reste_button : MonoBehaviour
{
    public GameObject circelPrefab;

    public Vector3 center;
    public Vector3 size;

    public int life = 100;
    public int score = 0;
    public int level = 1;

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
                            score += level*life;
                            scoreText.text = score.ToString();
                            levelText.text = level.ToString();
                            SpawnNextButton();
                        }
                    }
                }
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
        sound_x = Math.Abs(Convert.ToInt32((Math.Atan(yy / x)) * 10));
        Instantiate(circelPrefab, pos, Quaternion.identity);

        Destroy(gameObject);

    }

    void GameOver()
    {

    }

    void OnMouseEnter()
    {
        //Cursor.visible = false;
        //Cursor.SetCursor(gameCoursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
