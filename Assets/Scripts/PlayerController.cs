using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
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
    public GameObject autofireButton;

    private float nextFire;
    private bool autofire = true;

    public void SwitchAutofire() {
        autofire = !autofire;
        autofireButton.GetComponentInChildren<Text>().text = string.Format("Autofire: {0}", autofire ? "on" : "off");
    }

    void FixedUpdate() {
        // TODO: move to PC only -->
        //float inX = Input.GetAxis("Horizontal");
        //float inZ = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(inX, 0.0f, inZ);
        // <--

        Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3(
             Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }

    void Update() {
        bool ready = autofire || fireArea.CanFire();

        if (ready && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }
}
