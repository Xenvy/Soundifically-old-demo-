using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Click_reste_button : MonoBehaviour
{
    public GameObject circelPrefab;

    public GameObject MamasAndPapas;

    public Vector3 center;
    public Vector3 size;

    public int life = 100;
    public int score = 0;


    [FMODUnity.EventRef]
    public string bul_sound = "event:/bul";
    FMOD.Studio.EventInstance bul_sound_instance;

    public float health = 0;

    Rigidbody cachedRigidBody;

    void Start()
    {
        bul_sound_instance = FMODUnity.RuntimeManager.CreateInstance(bul_sound);

        bul_sound_instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
        bul_sound_instance.setParameterByName("Sound_set", health);
        bul_sound_instance.start();
    }


    // Update is called once per frame
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
        }
        else if (hit.collider != null)
        {
            if(life > 0) 
            {
                if (hit.collider)
                {

                    if (hit.collider.tag == "Reset_button")
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            bul_sound_instance.release();
                            bul_sound_instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                            score += 10;
                            health += 20;
                            Console.WriteLine(score);
                            Console.WriteLine(health);
                            SpawnNextButton();
                        }
                    }
                }
            }
        }
        
    }

    void SpawnNextButton()
    {
        float xx = UnityEngine.Random.Range(-3.65f, 3.65f);
        float yy = UnityEngine.Random.Range(-3.65f, 3.65f);
        float zz = UnityEngine.Random.Range(1, 3);

        Vector3 pos = center + new Vector3(xx, yy, 0);






        Instantiate(circelPrefab, pos, Quaternion.identity);

        Destroy(gameObject);
       
    }

    void OnDestroy()
    {
       GetComponent<FMODUnity.StudioEventEmitter>().Stop();
    }

}
