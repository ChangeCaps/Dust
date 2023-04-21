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
    public LineRenderer l;
    float ran;
    void Awake()
    {
       
        r = this.gameObject.GetComponent<Rigidbody>();
        indian = GameObject.Find("Enemy");
        cowboy = GameObject.Find("Cowboy");
        l = cowboy.GetComponent<LineRenderer>();
        dir = indian.transform.position - cowboy.transform.position;
        ran = Random.Range(l.GetPosition(2).x, l.GetPosition(1).x);

        dir = new Vector3(ran, dir.y, dir.z);
       

    }

    // Update is called once per frame
    void Update()
    {
       
        r.AddForce(dir.normalized);
     
    }
}
