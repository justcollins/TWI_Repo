﻿using UnityEngine;
using UnityEngine.UI;
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
    public Slider Speed_Indic;
    public Renderer baseRend;
    public Material baseMat;
    public Submarine_Resources sub;
    public SubControl subCon;

    private int scannerState = 0;
    private int laserState = 0;
    private int spotState = 0;
    private int instState = 0;
    private int engState = 0;
    private int FwdRevState = 1;
    
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
//-----------------------X------------------------------------
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (spotState == 0)
            {
                Spot_Lite.SetActive(true);
                setSpotState(1);
            }
            else
            {
                Spot_Lite.SetActive(false);
                setSpotState(0);
            }
        }
//-----------------------C------------------------------------
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (instState == 0)
            {
                Inst_Lite.SetActive(true);
                setInstState(1);
            }
            else
            {
                Inst_Lite.SetActive(false);
                setInstState(0);
            }
        }
//-----------------------ENG ON------------------------------------
        if (Input.GetKeyDown(subCon.ENGINE_ON))
        {
            if (engState == 0)
            {
                Eng_Lite.SetActive(true);
                setEngState(1);
            }
            else
            {
                setEngState(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (FwdRevState == 1)
                setFwdRev(-1);
            else
                setFwdRev(1);
            
        }

        if (sub.getShipEnergy() <= 0)
        {
            setEngState(0);
        }

//---------------------FWDREV
        if (engState == 1)
        {
            if (subCon.getForBack())
            {
                Fwd_Lite.SetActive(true);
                Rev_Lite.SetActive(false);
            }
            else
            {
                Fwd_Lite.SetActive(false);
                Rev_Lite.SetActive(true);
            }
        }
        else
        {
            Fwd_Lite.SetActive(false);
            Rev_Lite.SetActive(false);
            Eng_Lite.SetActive(false);
        }
        

//---------------------Speed Handle

        Speed_Indic.value = Mathf.Abs(subCon.thrust);

//---------------------Oxygen

        if (sub.getOxygenLevel() < 11)
        {
            Oxy_Warn.SetActive(true);
        }
        else
        {
            Oxy_Warn.SetActive(false);
        }

//---------------------Pressure

        if (sub.getCabinPressure() >75)
        {
            Pres_Warn.SetActive(true);
        }
        else
        {
            Pres_Warn.SetActive(false);
        }
	    
        
    
    }//END UPDATE

    public void setScanState(int scanState)
{
        scannerState = scanState;
    }
    public void setLaserState(int lazState)
    {
        laserState = lazState;
    }
    public void setSpotState(int sptState)
    {
        spotState = sptState;
    }
    public void setInstState(int inState)
    {
        instState = inState;
    }
    public void setEngState(int eState)
    {
        engState = eState;
    }
    public void setFwdRev(int frState)
    {
        FwdRevState = frState;
    }
}