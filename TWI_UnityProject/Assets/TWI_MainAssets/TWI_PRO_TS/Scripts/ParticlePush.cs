using UnityEngine;
using System.Collections;

public class ParticlePush : MonoBehaviour {

    private WindZone wind;
    public float WaitTime;
    public float Timer;
    

    void Start()
    {
        Timer = 0;
        wind = GetComponent<WindZone>();
    }

	void Update () 
    {
        Timer += Time.deltaTime;
        if (Timer >= WaitTime)
        {
            wind.windPulseMagnitude = (Mathf.PingPong(Time.time, Random.Range(0.2f, 6.0f)));
            Timer = 0;
          
        }
         
	}
}
