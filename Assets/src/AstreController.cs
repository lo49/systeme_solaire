using UnityEngine;
using System.Collections;

public class AstreController : MonoBehaviour {
	
	private Astre astre;
	private GameObject astreReel;

	
	void Awake (){
		astreReel = gameObject; //// astreReel est le GameObject auquel ce script est attaché
		Destroy (astreReel.GetComponent<Renderer>());
		Destroy (astreReel.GetComponent<MeshFilter>());
		astreReel.collider.isTrigger = true;
		astreReel.AddComponent<Rigidbody>();
		astreReel.rigidbody.useGravity = false;
		astreReel.renderer.enabled = false;
		//rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		// la ligne du dessus sera nécessaire pour les collisions dans le cas où l'astre ou le temps est très rapide.
	}

	void Start () {
		astreReel.name = astre.getNom();
	}
	
	public void ajoutForce(Vector3 force)
	{
		astreReel.rigidbody.AddForce (force);
	}
	
	public void setNom(string nom){
		astreReel.name = nom;
	}

	public void setAstre(Astre a)
	{
		astre = a;
	}

	public void setPosition(Vector3 pos){
		astreReel.transform.position = pos;
	}

	public void setVitesse(Vector3 vit){
		astreReel.rigidbody.velocity = vit;
	}

	public void setMasse(float m){
		astreReel.rigidbody.mass = m;
	}

	public void setTaille(float taille){
		astreReel.transform.localScale = Vector3.one * taille;
	}
	public void OnTriggerEnter(Collider other){
		if(other.rigidbody != null){
			// On récupère les données des de planète en collision.
			// Vitesse : 
			Vector3 prin_v1 = astreReel.rigidbody.velocity;
			Vector3 other_v1 = other.rigidbody.velocity;
			float angle = piAngle(Vector3.Angle(prin_v1,other_v1));
			if(astre.getMasse() > other.rigidbody.mass){
				float inertie = (other.rigidbody.mass + astre.getMasse() * (other.rigidbody.velocity - astreReel.rigidbody.velocity).magnitude) / 100f;
				float pAstreDetruit1 = inertie / astre.getMasse();
				float pAstreDetruit2 = other.rigidbody.mass;
			}
			else if (astre.getMasse() == other.rigidbody.mass){
				print("Erreur !!! Les masses sont pareil on c'est pas quoi faire !");
			}

		}
	}
	private float piAngle(float angle){
		if(angle < 180){
			return angle;
		}else if (angle <= 360){
			return Mathf.Abs(angle - 360);
		}else{
			return -1;
		}
	}
	public GameObject getAstreReel()
	{
		return astreReel;
	}
}
