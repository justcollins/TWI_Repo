using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldAndLazer : MonoBehaviour
{

   
    public GameObject laserObj;
    public GameObject shieldObj;
    public GameObject bg;
    public GameObject WarningLight;
    public GameObject Text;
    
    public Submarine_Resources sub;
    private EngineMonitor EM;
    private KeyboardManager keyboard;
	private SoundManager Sound;

    private float timer = 0f;
    private float chillTimer = 0f;
    private float elapsedTime = 0f;
    private float OG = 1f;

    private float enTimer = 0.0f;
    private float enMaxTime = 0.1f;

    private bool check = false;

    private Image Overheat;

    private Color Over;

	public bool PlayLazer = false;

    void Start()
    {
        EM = GameObject.FindObjectOfType<EngineMonitor>();
        keyboard = FindObjectOfType<KeyboardManager>();
		Sound = FindObjectOfType<SoundManager> ();

        Overheat = Text.GetComponent<Image>();      

        Over = Overheat.color;

        shieldObj.SetActive(false);
        laserObj.SetActive(false);
    }

    void Update()
    {

        if (EM.getLaserState() == 1 || EM.getLaserState() == 0)
        {
            bg.transform.SetSiblingIndex(2);

            if (Input.GetKey(keyboard.LeftMouse) && check == false)
            {

                enTimer += Time.deltaTime;
                timer += Time.deltaTime;
			               

                if (EM.getLaserState() == 1)
                {
                    shieldObj.SetActive(false);
                    laserObj.SetActive(true);
					PlayLazer = true;


                    if (enTimer >= enMaxTime)
                    {
                        sub.setEnergyLevel(-0.3f);
                        enTimer = 0.0f;
                    }
                }
                else if (EM.getLaserState() == 0) 
                {
                    laserObj.SetActive(false);
                    shieldObj.SetActive(true);
                    if (enTimer >= enMaxTime)
                    {
                        sub.setEnergyLevel(-0.5f);
                        enTimer = 0.0f;
                    }
                }

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
				PlayLazer = false;
				Sound.PlayLas = false;
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= 0.1)
                {
                    timer = timer - 0.2f;
                    elapsedTime = 0;
                }
                if (EM.getLaserState() == 1)
                {
                    laserObj.SetActive(false);
                }
                else if (EM.getLaserState() == 0)
                {
                    shieldObj.SetActive(false);
                }

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
            if (timer > 7 && EM.getLaserState() == 1)
            {             
                timer = TooHot(check, timer, WarningLight, OG, Over, Overheat, laserObj);
                timer = TooHot(check, timer, WarningLight, OG, Over, Overheat, shieldObj);

                chillTimer += Time.deltaTime;

                if (chillTimer > 5)
                {
                    check = false;
                    chillTimer = 0;
                    timer = 0;
                }
            }
            if (timer > 7 && EM.getLaserState() == 0)
            {
                timer = TooHot(check, timer, WarningLight, OG, Over, Overheat, shieldObj);
                timer = TooHot(check, timer, WarningLight, OG, Over, Overheat, laserObj);

                chillTimer += Time.deltaTime;

                if (chillTimer > 5)
                {
                    check = false;
                    chillTimer = 0;
                    timer = 0;                 
                }
            }
        }

    }

    public float TooHot(bool check, float timer, GameObject WarningLight,float OG, Color Over, Image Overheat, GameObject Obj) 
    {
        check = true;
        timer = 8;

        WarningLight.transform.localScale = new Vector3(OG, OG, 0);

        Over.a = 255;
        Overheat.color = Over;
        Obj.SetActive(false);
       
        return timer;
    }

public void SetWeapons(bool set)
	{
		check = set;
	}
	public bool GetWeapons()
	{
		return check;
	}
}