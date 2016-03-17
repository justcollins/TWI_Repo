using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UltimateLaser : MonoBehaviour
{

    // Use this for initialization
    public GameObject laserObj;
    public GameObject bg;
    public GameObject WarningLight;
    public GameObject Text;

    private EngineMonitor EM;
    private KeyboardManager keyboard;

    float timer = 0f;
    float chillTimer = 0f;
    float elapsedTime = 0f;
    float OG = 1f;
    bool check = false;

    Image Overheat;
    Image Warning;

    Color Over;
    Color War;


    void Start()
    {
        EM = GameObject.FindObjectOfType<EngineMonitor>();
        keyboard = FindObjectOfType<KeyboardManager>();

        Overheat = Text.GetComponent<Image>();
        Warning = WarningLight.GetComponent<Image>();

        Over = Overheat.color;
        War = Warning.color;

        laserObj.SetActive(false);
    }

    void Update()
    {
        if (EM.getLaserState() == 1)
        {
            bg.transform.SetSiblingIndex(2);

            if (Input.GetKey("space") && check == false)
            {


                timer += Time.deltaTime;

                laserObj.SetActive(true);

                if (timer < 0)
                {
                    timer = 0f;
                }

                else if (timer < 1 && timer > 0)
                {

                    WarningLight.transform.localScale = new Vector3(OG / 100, OG / 100, 0);
                }

                else if (timer > 1 && timer < 3)
                {

                    WarningLight.transform.localScale = new Vector3(OG / 3, OG / 3, 0);

                }
                else if (timer > 3 && timer < 5)
                {


                    WarningLight.transform.localScale = new Vector3(OG / 1.5f, OG / 1.5f, 0);

                }
                else if (timer > 5 && timer < 7)
                {

                    WarningLight.transform.localScale = new Vector3(OG, OG, 0);

                    Over.a = 255;
                    Overheat.color = Over;
                }

                else
                {

                    WarningLight.transform.localScale = new Vector3(0, 0, 0);

                    Over.a = 0;
                    Overheat.color = Over;


                }
            }


            else if (check == false)
            {


                elapsedTime += Time.deltaTime;
                if (elapsedTime >= 0.1)
                {
                    timer = timer - 0.2f; //Change float to increase decrease speed
                    elapsedTime = 0;
                }

                laserObj.SetActive(false);
                if (timer < 0)
                {
                    timer = 0f;
                }

                else if (timer < 1 && timer > 0)
                {

                    WarningLight.transform.localScale = new Vector3(OG / 100, OG / 100, 0);
                }

                else if (timer > 1 && timer < 3)
                {

                    WarningLight.transform.localScale = new Vector3(OG / 3, OG / 3, 0);

                }
                else if (timer > 3 && timer < 5)
                {


                    WarningLight.transform.localScale = new Vector3(OG / 1.5f, OG / 1.5f, 0);

                    Over.a = 0;
                    Overheat.color = Over;

                }
                else if (timer > 5 && timer < 7)
                {

                    WarningLight.transform.localScale = new Vector3(OG, OG, 0);

                    Over.a = 255;
                    Overheat.color = Over;
                }
                else
                {

                    WarningLight.transform.localScale = new Vector3(0, 0, 0);

                    Over.a = 0;
                    Overheat.color = Over;


                }

            }
            if (timer > 7)
            {
                check = true;
                timer = 8;
                laserObj.SetActive(false);
                chillTimer += Time.deltaTime;
                if (chillTimer > 5)
                {
                    check = false;
                    chillTimer = 0;
                    laserObj.SetActive(true);
                    timer = 0;
                }
            }
        }

    }
}