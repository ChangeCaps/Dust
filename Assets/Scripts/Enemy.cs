using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 target = new Vector3(0,0,0);
    Vector3 vel = new Vector3(0,0,0);
    Vector3 acc = new Vector3(0,0,0);
    public GameObject player;
    bool detected = false;
    public float maxSpeed = 20.1f;
    public float maxForce = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
       
        
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!detected) {
            Wander(target);
        } else {
            Seek(player.transform.position);
        }
        vel = vel + acc;
        Vector3.ClampMagnitude(vel, maxSpeed);
        this.gameObject.transform.Translate(vel*Time.deltaTime); 
        acc*=0;

    }

    void Seek(Vector3 t) {
        Debug.Log("Seeking");
        Vector3 desired = t - this.gameObject.transform.position;
        desired = desired.normalized;
        desired *= maxSpeed;
        Vector3 steer = desired - vel;
        //Vector3.ClampMagnitude(steer, maxForce);
        ApplyForce(steer);
    }

    void ApplyForce(Vector3 force) {
        acc = acc + force;
    }

    void Wander(Vector3 d) {
        Debug.Log("Wander");
        float temp = Random.Range(0, Mathf.PI * 2);
        float x = 1000 * Mathf.Cos(temp);
        float z = 1000 * Mathf.Sin(temp);
       d -= new Vector3(x, 0, z);
       // target = d;
       // Debug.Log(d);
        Vector3 desired = d - this.gameObject.transform.position;
        
        desired = desired.normalized;
        desired *= maxSpeed;
        Debug.Log("w" + desired);
        Vector3 steer = desired - vel;
        //Vector3.ClampMagnitude(steer, maxForce);
        Debug.Log("S"+steer);
        ApplyForce(steer);
    }
}
