using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

	double[,] value;

	void Start() {
		value = new double[40, 30];
		for (int i=0; i<40; i++) {
			for (int j=0; j<30; j++) {
				value [i, j] = -1;
			}
		}
		StartCoroutine (countTime(1.0f) );
	}

	IEnumerator countTime(float delaytime) {
		yield return new WaitForSeconds (delaytime);

		GameObject g = GameObject.Find("Calculate");
		
		Calculate CScript = g.GetComponent<Calculate> ();


		for (int i=0; i<40; i++) {
			for(int j=0; j<30; j++) {
				if(CScript.get_value(i, j) < 1000) {
//					Debug.Log(i.ToString("000") + " " + j.ToString("000") );					
				}

				value[i,j] = CScript.get_value(i,j);
			}
		}

	}

	void Update() {
		/*
		GameObject g = GameObject.Find("Calculate");
		
		Calculate CScript = g.GetComponent<Calculate> ();
		
		for (int i=0; i<40; i++) {
			for(int j=0; j<40; j++) {
				if(CScript.get_value(i, j) < 1000) {
					Debug.Log(i.ToString("000") + " " + j.ToString("000") );

				}
			}
		}
		*/
	}

	private static Material mat;
	void Awake() {
		mat = new Material( "Shader \"Lines/Colored Blended\" {" +
		                   "SubShader { Pass { " +
		                   "    Blend SrcAlpha OneMinusSrcAlpha " +
		                   "    ZWrite Off Cull Off Fog { Mode Off } " +
		                   "    BindChannels {" +
		                   "      Bind \"vertex\", vertex Bind \"color\", color }" +
		                   "} } }" );
		mat.hideFlags = HideFlags.HideAndDontSave;
		mat.shader.hideFlags = HideFlags.HideAndDontSave;
	}

	void OnPostRender() {

		for (int i=0; i<40; i++) {
			for(int j=0; j<30; j++) {
				if(value[i,j] == -1) continue; 
				if(value[i,j] < 1000) {
					Vector3 d1 = new Vector3(i+0, 0, j+0);
					Vector3 d2 = new Vector3(i+1, 0, j+0);
					Vector3 d3 = new Vector3(i+1, 0, j+1);
					Vector3 d4 = new Vector3(i+0, 0, j+1);

					float k = (float) value[i,j]*10.0f;

					Vector3 u1 = new Vector3(i+0, k, j+0);
					Vector3 u2 = new Vector3(i+1, k, j+0);
					Vector3 u3 = new Vector3(i+1, k, j+1);
					Vector3 u4 = new Vector3(i+0, k, j+1);

					GL4(d1, d2, d3, d4);
					GL4(u1, u2, d2, d1);
					GL4(u2, u3, d3, d2);
					GL4(u3, u4, d4, d3);
					GL4(u4, u1, d1, d4);
					GL4(u4, u3, u2, u1);
				}
			}
		}

	}

	void GL4(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
		
		GL.PushMatrix ();
		mat.SetPass (0);
		GL.Begin (GL.TRIANGLES);
		GL.Color(new Color(0.5f, 0.0f, 0.0f, 0.5f));
		GL.Vertex (v1);
		GL.Vertex (v2);
		GL.Vertex (v3);
		GL.End ();
		GL.PopMatrix ();


		GL.PushMatrix ();
		mat.SetPass (0);
		GL.Begin (GL.TRIANGLES);
		GL.Color(new Color(0.5f, 0.0f, 0.0f, 0.5f));
		GL.Vertex (v3);
		GL.Vertex (v4);
		GL.Vertex (v1);
		GL.End ();
		GL.PopMatrix ();
	}
	void OnApplicationQuit() {
		DestroyImmediate(mat);
	}
}
