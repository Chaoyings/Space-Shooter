using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource ac;
	private float nextFire;

	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform showSpawn;
	public float fireRate;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		ac = GetComponent<AudioSource> ();
		nextFire = 1.0f;
	}

	void Update() {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, showSpawn.position, showSpawn.rotation);
			ac.Play ();
		}
		
	}

	//FixedUpdate is called before each fixed physics steps
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal"); //Value between 0 and 1
		float moveVertical = Input.GetAxis ("Vertical"); //Value between 0 and 1
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
