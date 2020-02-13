using System;
using UnityEngine;


[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public SimpleTouchPad touchPad;
    public TouchArea fireArea;

    private float nextFire;
    private bool autofire;

    private void Awake() {
        autofire = Options.Autofire;
    }

    Vector3 PCGetMovement() {
        return new Vector3(
            Input.GetAxis("Horizontal"),
            0.0f,
            Input.GetAxis("Vertical"));
    }

    Vector3 AndroidGetMovement() {
        Vector2 direction = touchPad.GetDirection();
        return new Vector3(direction.x, 0.0f, direction.y);
    }

    Vector3 GetMovement() {
#if UNITY_STANDALONE
        return PCGetMovement();
#elif UNITY_ANDROID
        return AndroidGetMovement();
#else
        throw new Exception("Unsupported platform.");
#endif
    }

    private void FixedUpdate() {
        Vector3 movement = GetMovement();
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3(
             Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }

    private void Update() {
        bool ready = autofire || fireArea.CanFire();

        if (ready && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }
}
