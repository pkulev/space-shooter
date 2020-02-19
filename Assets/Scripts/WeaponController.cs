using UnityEngine;


public class WeaponController : MonoBehaviour {

    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate;
    public float delay;

    private void Start () {
        InvokeRepeating("Fire", delay, fireRate);
    }

    private void Fire() {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }
}
