using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public LineRenderer l;
    public static float t = 0;
    float minimum = 10;
    float maximum = 0;
    float minimum1 = -10;
    float maximum1 = 0;
    public GameObject prefab;
    public bool reload = false;
    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        l.SetPosition(1, new Vector3(Mathf.Lerp(minimum, maximum, t), 1, 0));
     
        l.SetPosition(2, new Vector3(Mathf.Lerp(minimum1, maximum1, t), 1, 0));
        //t1 += 0.1f * Time.deltaTime;
        t = Mathf.Clamp(t + 0.1f * Time.deltaTime, 0, 10);
        if (Input.GetMouseButton(0) && reload == false) {

            Instantiate(prefab, this.gameObject.transform);
           // Debug.Log(this.gameObject.transform.position);
            reload = true;
            StartCoroutine("Reload");
        }

    }

    IEnumerator Reload() {

        yield return new WaitForSecondsRealtime(1f);
        reload = false;
    
    }


}
