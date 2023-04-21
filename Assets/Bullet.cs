using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody r;
    public GameObject indian;
    public GameObject cowboy;
    Vector3 dir;
    float ran;
    void Awake()
    {
        r = this.gameObject.GetComponent<Rigidbody>();
        indian = GameObject.Find("Indian");
        cowboy = GameObject.Find("Cowboy");
        dir = indian.transform.position - cowboy.transform.position;
        ran = Random.Range(Mathf.Clamp(dir.x -(10-Aim.t),-10,0), Mathf.Clamp(dir.x + (10-Aim.t),0,10));
        Debug.Log(Mathf.Clamp(dir.x - (10 - Aim.t), -10, 0)+ " "+ Mathf.Clamp(dir.x + (10 - Aim.t), 0, 10));

        dir = new Vector3(ran, dir.y, dir.z);
       

    }

    // Update is called once per frame
    void Update()
    {
       
        r.AddForce(dir.normalized);
     
    }
}
