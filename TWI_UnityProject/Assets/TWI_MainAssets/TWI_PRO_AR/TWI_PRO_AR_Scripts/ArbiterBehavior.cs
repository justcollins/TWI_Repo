using UnityEngine;
using System.Collections;

public class ArbiterBehavior : MonoBehaviour {
	
	public GameObject spawnOfSatan;
	public Transform spawnLoc;

	public GameObject motherSpawn;
	public ActiveEnvironments activeEnvCheck;

    public GameObject damageSphere1, damageSphere2, damageSphere3;
    private int curPoint1, curPoint2, curPoint3;
    public Transform[] sphere1Travel;
    public Transform[] sphere2Travel;
    public Transform[] sphere3Travel;

	private int spawnTotal;
	private int spawnMax = 50;
	private float maxTime;
	private float longTime = 0.0f;
	private float shortTime = 0.0f;
    private float moveSpeed = 20.0f;

	// Use this for initialization
	void Start () {
		Vector3 spawn = new Vector3 (spawnLoc.position.x, spawnLoc.position.y, spawnLoc.position.z);
		activeEnvCheck = FindObjectOfType<ActiveEnvironments>();
		for(int i = 0; i < 4; i++){
			Instantiate (spawnOfSatan, spawn, Quaternion.identity);
			spawnTotal++;
		}

        curPoint1 = 0;
        curPoint2 = 0;
        curPoint3 = 0;
        damageSphere1.transform.position = sphere1Travel[curPoint1].position;
        damageSphere2.transform.position = sphere2Travel[curPoint2].position;
        damageSphere3.transform.position = sphere3Travel[curPoint3].position;
        
	}

    // Update is called once per frame
    void Update()
    {
        attackProtocol();
		if(spawnTotal < spawnMax){
			if(activeEnvCheck.checkActive(motherSpawn)){
				shortSpawn();
                
			}
			else{
				longSpawn();
			}
		}
	}

	void shortSpawn(){
		//Debug.Log ("Short");
		Vector3 spawn = new Vector3 (spawnLoc.position.x, spawnLoc.position.y, spawnLoc.position.z);
		maxTime = 4.0f;
		shortTime += Time.deltaTime;
		//Debug.Log (shortTime);
		if (shortTime >= maxTime) {
			Instantiate (spawnOfSatan, spawn, Quaternion.identity);
			shortTime = 0;
		}
	}

	void longSpawn(){
		//Debug.Log ("Long");
		Vector3 spawn = new Vector3 (spawnLoc.position.x, spawnLoc.position.y, spawnLoc.position.z);
		maxTime = 8.0f;
		//Debug.Log (longTime);
		longTime += Time.deltaTime;
		if (longTime >= maxTime) {
			Instantiate (spawnOfSatan, spawn, Quaternion.identity);
			longTime = 0;
		}
	}

    void attackProtocol()
    {
        if (damageSphere1.transform.position == sphere1Travel[curPoint1].position)
        {
            if (curPoint1 == 0)
            {
                curPoint1 = 1;
            }
            if (curPoint1 < sphere1Travel.Length-1)
            {
                curPoint1++;
            }
            else
            {
                curPoint1 = 0;
            }      
        }
        damageSphere1.transform.position = Vector3.MoveTowards(damageSphere1.transform.position, sphere1Travel[curPoint1].position, moveSpeed * Time.deltaTime);
        if (damageSphere2.transform.position == sphere2Travel[curPoint2].position)
        {
            if (curPoint2 == 0)
            {
                curPoint2 = 1;
            }
            if (curPoint2 < sphere2Travel.Length - 1)
            {
                curPoint2++;
            }
            else
            {
                curPoint2 = 0;
            }
            
        }
        damageSphere2.transform.position = Vector3.MoveTowards(damageSphere2.transform.position, sphere2Travel[curPoint2].position, moveSpeed * Time.deltaTime);
        if (damageSphere3.transform.position == sphere3Travel[curPoint3].position)
        {
            if (curPoint3 == 0)
            {
                curPoint3 = 1;
            }
            if (curPoint3 < sphere3Travel.Length - 1)
            {
                curPoint3++;
            }
            else
            {
                curPoint3 = 0;
            }
            
        }
        damageSphere3.transform.position = Vector3.MoveTowards(damageSphere3.transform.position, sphere3Travel[curPoint3].position, moveSpeed * Time.deltaTime);
    }
}
