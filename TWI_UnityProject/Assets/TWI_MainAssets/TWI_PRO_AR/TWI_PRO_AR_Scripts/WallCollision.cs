using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour {
	public Submarine_Resources subRes;
	public Collider subCol;
	[Range(0.0f, 5.0f)] public float wallPressure;

	void OnCollisionEnter(Collision col){
		if (col.collider == subCol) {
			subRes.setCabinPressure(wallPressure);
		}
	}
}
