﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//EDITED LOCALLY
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
    private KeyboardManager keyboard;
    

    private int scannerState = 0;
    private int laserState = 2;
    private int spotState = 2;
    private int instState = 2;
    private int engState = 2;
    private int FwdRevState = 1;
    
	// Use this for initialization
	void Start () {
        baseRend.GetComponent<Renderer>();
        baseRend.enabled = true;
        baseRend.material = baseMat;
        keyboard = FindObjectOfType<KeyboardManager>();
        
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
//------------------------E----------------------------------
        if (Input.GetKeyDown(keyboard.LaserShield))
        {
            LR_Scan.SetActive(false);
            SR_Scan.SetActive(false);
            if (laserState != 1)
            {
                SC_Laser.SetActive(true);
                EF_Field.SetActive(false);
                setLaserState(1);
            }
            else
            {
                SC_Laser.SetActive(false);
                EF_Field.SetActive(true);
                setLaserState(0);
            }
            
        }
        
        
//------------------------Q-----------------------------------
        if (Input.GetKeyDown(keyboard.Scanner))
        {
            setLaserState(2);
            SC_Laser.SetActive(false);
            EF_Field.SetActive(false);
            if (scannerState == 0)
            {
                LR_Scan.SetActive(true);
                SR_Scan.SetActive(false);
                setScanState(1);
            }
            else
            {
                LR_Scan.SetActive(false);
                SR_Scan.SetActive(true);
                setScanState(0);
            }
            
        }
//-----------------------X------------------------------------
        if(Input.GetKeyDown(keyboard.SpotOn))
        {
            if (spotState != 1)
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
        if (Input.GetKeyDown(keyboard.InstOn))
        {
            if (instState != 1)
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
        if (Input.GetKeyDown(keyboard.EngineOn))
        {
            if (engState != 1)
            {
                Eng_Lite.SetActive(true);
                setEngState(1);
            }
            else
            {
                setEngState(0);
            }
        }

        if (Input.GetKeyDown(keyboard.MiddleMouse))
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

    public int getLaserState()
    {
        return laserState;
    }
    public void setLaserState(int lazState)
    {
        laserState = lazState;
    }

    public int getSpotState()
    {
        return spotState;
    }
    public void setSpotState(int sptState)
    {
        spotState = sptState;
    }

    public int getInstState()
    {
        return instState;
    }
    public void setInstState(int inState)
    {
        instState = inState;
    }

    public int getEngState()
    {
        return engState;
    }
    public void setEngState(int eState)
    {
        engState = eState;
    }

    public int getFwdRev()
    {
        return FwdRevState;
    }
    public void setFwdRev(int frState)
    {
        FwdRevState = frState;
    }
    
    
}
