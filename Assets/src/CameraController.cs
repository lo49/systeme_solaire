using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private SystemeController sc;
	private GameObject deplacement; //déplacement de la caméra depuis la dernière frame
	private Vector3 posAstreSuivi; // correspond à la distance entre l'astre suivi et la caméra.
	private float distMinAstreSuivi;
	private bool camSuivActive=true;
	private bool depEnCours=false;
	private float speed;  // vitesse de translation de la caméra
	private float speedRot;  // vitesse de rotation
	private GameObject farCamera;
	
	void Awake () {
		name = "NearCamera";
		deplacement = new GameObject ();
		deplacement.transform.position = new Vector3 (0f, 3000f, 0f); //correspond déplacement initial, donc à la position initiale de la caméra
		deplacement.transform.eulerAngles = new Vector3 (90f, 0f, 0f); // rotation initiale (regarde vers le bas)
		posAstreSuivi = Vector3.zero;
		speed = 15f;
		speedRot = 1.7f;
		camera.nearClipPlane = 0.01f; // distance minimale des objets que la caméra représente
		camera.farClipPlane = 10000f;  // distance maximale des objets que la caméra représente. Vrai max = 
		camera.fieldOfView = 60f; // angle de vision de la caméra. vaut 60 par défault.
		camera.clearFlags = CameraClearFlags.Depth;
		camera.depth = 2;
		camera.renderingPath = RenderingPath.VertexLit;

		farCamera = new GameObject();
		farCamera.AddComponent<Camera> ();
		farCamera.name = "FarCamera";
		farCamera.transform.position = transform.position;
		farCamera.transform.eulerAngles = transform.eulerAngles;
		farCamera.transform.parent = transform;
		farCamera.camera.nearClipPlane = 8400f;
		farCamera.camera.farClipPlane = 20000000000f;
		farCamera.camera.fieldOfView = 60f;
		farCamera.camera.clearFlags = CameraClearFlags.Skybox;
		farCamera.camera.depth = 1;
		farCamera.camera.renderingPath = RenderingPath.VertexLit;
	}


	public void gererDeplacementCamera() // appelé dans Update dans SystemeController
	{
		if (Input.GetMouseButton (2)) { // si middle clic
				onMiddleClic ();
		}
		if (Input.GetMouseButton (0)) { // si left clic
				rotateZ ();
		}
		if (Input.GetMouseButton (1)) { // si right clic
				rotateXY ();
		}
		avanceCamera ();
		if (depEnCours)
			smoothDep();
	}

	private void smoothDep(){
		Vector3 target = distMinAstreSuivi * posAstreSuivi.normalized;
		//deplacement.transform.position -= Vector3.Lerp (posAstreSuivi, target, 2f);
		deplacement.transform.position -= 0.04f * (posAstreSuivi - target);
		if ((posAstreSuivi+deplacement.transform.position).sqrMagnitude < target.sqrMagnitude*9f)
			depEnCours = false;
	}

	private void onMiddleClic () // Quand on appuie sur le bouton du milieu
	{
		float mouvX = speedRot * Input.GetAxis ("Mouse X"); // donne le mouvement de la souris en X
		float mouvY = speedRot * Input.GetAxis ("Mouse Y"); // et Y.
		if (camSuivActive) { // si la caméra suit un objet, on lui tourne autour
			deplacement.transform.RotateAround (-posAstreSuivi, deplacement.transform.right, speedRot*mouvY);
			deplacement.transform.RotateAround (-posAstreSuivi, -deplacement.transform.up, speedRot*mouvX);

		} else { // sinon on se déplace suivant le plan perpendiculaire à la direction de la caméra.
			deplacement.transform.Translate(-deplacement.transform.up * mouvY * speed, Space.World);
			deplacement.transform.Translate(-deplacement.transform.right * mouvX * speed, Space.World);
		}
	}

	public void avanceCamera() // Si on fait tourner la molette de la souris
	{
		if (camSuivActive){ // si la caméra suit un objet on se rapproche de l'objet
			deplacement.transform.position -= 1.04f*Input.GetAxis("Mouse ScrollWheel")*(posAstreSuivi - (posAstreSuivi.normalized * distMinAstreSuivi));
		} else { // sinon on se déplace suivant la direction de la caméra.
			deplacement.transform.Translate(deplacement.transform.forward * Input.GetAxis("Mouse ScrollWheel")*90f*speed, Space.World);
		}
	}

	public void rotateZ() // Quand on appuie sur le bouton gauche
	{
		deplacement.transform.Rotate(new Vector3(0,0,-speedRot * Input.GetAxis("Mouse X"))); // on tourne autour de la direction de la caméra
	}

	public void rotateXY()// Quand on appuie sur le bouton droit
	{
		deplacement.transform.Rotate(new Vector3(-speedRot * Input.GetAxis("Mouse Y"), 0, 0));
		deplacement.transform.Rotate(new Vector3(0, speedRot * Input.GetAxis("Mouse X"),0));
	} // fait tourner la caméra

	public Vector3 getPosAstreSuivi()
	{
		if ((posAstreSuivi + deplacement.transform.position).sqrMagnitude <= distMinAstreSuivi*distMinAstreSuivi){
			deplacement.transform.position = distMinAstreSuivi * 2f * posAstreSuivi.normalized;
			depEnCours=false;
		}
		posAstreSuivi += deplacement.transform.position;
		transform.rotation = deplacement.transform.rotation;
		deplacement.transform.position = Vector3.zero;
		return posAstreSuivi;
	}
	
	public void setTailleAstreSuivi (float taille){
		distMinAstreSuivi = taille/2f + camera.nearClipPlane*2f;
	}

	public void changeTaille(float ancTaille, float nouvTaille)
	{
		deplacement.transform.position = (nouvTaille / ancTaille - 1) * posAstreSuivi;
		setTailleAstreSuivi (nouvTaille);
		depEnCours=false;
	}

	public void deplaceCamera(Vector3 pos){
		deplacement.transform.position += pos;
		depEnCours = true;
	}

	public void followAstre(bool ca)// appelé dans SystemeController
	{
		camSuivActive = ca;
	}

	public bool getCamSuivActive()
	{
		return camSuivActive;
	}


}
