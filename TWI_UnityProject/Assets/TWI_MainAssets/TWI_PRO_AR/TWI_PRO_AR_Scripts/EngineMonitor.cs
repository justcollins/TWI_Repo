using UnityEngine;
using System.Collections;

public class EngineMonitor : MonoBehaviour {

    public GameObject[] battery;
    public GameObject SR_Scan;
    public GameObject LR_Scan;
    public GameObject SC_Laser;
    public GameObject EF_Field;
    public GameObject Fwd_Lite;
    public GameObject Rev_Lite;
    public GameObject Eng_Lite;
    public GameObject Inst_Lite;
    public GameObject Spot_Lite;
    public GameObject Oxy_Warn;
    public GameObject Pres_Warn;
    public GameObject Speed_Indic;
    public Renderer baseRend;
    public Material baseMat;
    public Submarine_Resources sub;

    private KeyCode key;
    private int scannerState = 0;
    private int laserState = 0;
    
	// Use this for initialization
	void Start () {
        baseRend.GetComponent<Renderer>();
        baseRend.enabled = true;
        baseRend.material = baseMat;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 10; i++)
        {
            if (sub.getShipEnergy() >= (i) * 10 + 1)
            {
                battery[i].SetActive(true);
            }
            else
                battery[i].SetActive(false);
        }
//------------------------Q----------------------------------
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (scannerState == 0)
            {
                LR_Scan.SetActive(true);
                setScanState(1);
            }
            else if (scannerState == 1)
            {
                LR_Scan.SetActive(false);
                SR_Scan.SetActive(true);
                setScanState(2);
            }
            else
            {
                SR_Scan.SetActive(false);
                setScanState(0);
            } 
        }
//------------------------E-----------------------------------
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            if (laserState == 0)
            {
                SC_Laser.SetActive(true);
                setLaserState(1);
            }
            else if (laserState == 1)
            {
                SC_Laser.SetActive(false);
                EF_Field.SetActive(true);
                setLaserState(2);
            }
            else
            {
                EF_Field.SetActive(false);
                setLaserState(0);
            }
        }
	}

    public void setScanState(int scanState)
{
        scannerState = scanState;
    }
    public void setLaserState(int lazState)
    {
        laserState = lazState;
    }
}
