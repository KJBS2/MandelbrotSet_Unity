using UnityEngine;
using System.Collections;

public class MakeCubes : MonoBehaviour {

	public GameObject Cube;
	Calculate cal;

	double[,] value;
	
	void Start() {

		GameObject g = GameObject.Find("Calculate");
		cal = g.GetComponent<Calculate> ();

		value = new double[cal.X, cal.Y];
		for (int i=0; i<160; i++) {
			for (int j=0; j<120; j++) {
				value [i, j] = -1;
			}
		}
		StartCoroutine (countTime(1.0f) );
	}
	
	IEnumerator countTime(float delaytime) {
		yield return new WaitForSeconds (delaytime);
		
		cal.draw ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
