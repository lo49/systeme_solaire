using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject cameraOri;
	private bool camSuivActive=false;
	private float speed;
	private float speedRot;
	private float posCurseurX;

	void Start () {
		name = "MainCamera";
		speed = 15f;
		speedRot = 1.7f;
		transform.position = new Vector3(0f, 3000f, 0f);
		transform.eulerAngles = new Vector3 (90, 0, 0) ;
		camera.farClipPlane = 100000f;
		camera.nearClipPlane = 0.01f;
		camera.fieldOfView = 35f;
		cameraOri = new GameObject ();
		cameraOri.name = "CameraOri";
	}


	public void deplacerCamera ()
	{
		float mouvX = speedRot * Input.GetAxis ("Mouse X");
		float mouvY = speedRot * Input.GetAxis ("Mouse Y");
		if (camSuivActive) {
			transform.RotateAround (cameraOri.transform.position, transform.right*mouvY, speedRot);
			transform.RotateAround (cameraOri.transform.position, -transform.up*mouvX, speedRot);
		}else{
			transform.Translate(-cameraOri.transform.up * mouvY * speed);
			transform.Translate(-cameraOri.transform.right * mouvX * speed);
		}
	}

	public void avanceCamera(){
		if (camSuivActive){
			transform.localPosition -= transform.localPosition*1.04f*Input.GetAxis("Mouse ScrollWheel");
		}else{
			transform.Translate(cameraOri.transform.forward * Input.GetAxis("Mouse ScrollWheel")*90f*speed);
		}
	}

	public void rotateZ(){
		transform.Rotate(new Vector3(0,0,-speedRot * Input.GetAxis("Mouse X")));
	}

	public void rotateXYCamera()
	{
		transform.Rotate(new Vector3(-speedRot * Input.GetAxis("Mouse Y"), 0, 0));
		transform.Rotate(new Vector3(0, speedRot * Input.GetAxis("Mouse X"),0));
	}

	public GameObject getCameraOri()
	{
		return cameraOri;
	}

	public void setCamSuivActive(bool ca)
	{
		camSuivActive = ca;
		if (camSuivActive) {
						transform.parent = cameraOri.transform;
						transform.LookAt (cameraOri.transform.position);
				} else {
						transform.parent = null;
				}
	//	transform.localPosition = 1.12f*followAstre.getTailleEchelle()*(transform.position-followAstre.transform.position).normalized;
	}

}
