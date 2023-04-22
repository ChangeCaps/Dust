using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // The initial deviation in degrees
    public float maxDeviation;
    public float minDeviation;
    // The time it takes for the player to aim.
    public float aimingTime;
    public GameObject bullet;

    LineRenderer lineRenderer;
    bool isAiming;
    float currentDeviation;

    // Start is called before the first frame update
    void Start() {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        this.ClearLineRenderer();
        this.currentDeviation = this.maxDeviation;
    }

    // Update is called once per frame
    void Update() {
        if (!this.isAiming && Input.GetMouseButtonDown(0)) {
            this.currentDeviation = this.maxDeviation;
            this.isAiming = true;
        }

        if (this.isAiming) {
            float range = this.maxDeviation - this.minDeviation;

            this.currentDeviation -= Time.deltaTime * range / this.aimingTime;
            this.currentDeviation = Mathf.Max(this.currentDeviation, this.minDeviation);
        }

        if (this.isAiming && Input.GetMouseButtonUp(0)) {
            this.Fire();
            this.isAiming = false;
        }

        if (this.isAiming) {
            this.DrawDeviationCone();
        }
    }

    void Fire() {
        this.ClearLineRenderer();
        Transform transform = this.GetComponent<Transform>();
        Vector3 forward = transform.forward;

        GameObject bullet = Instantiate(this.bullet, transform);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        float deviationRadians = this.currentDeviation * Mathf.Deg2Rad;
        float minAngle = Mathf.Atan2(forward.z, forward.x) - deviationRadians / 2.0f;
        float maxAngle = minAngle + deviationRadians;

        float angle = Random.Range(minAngle, maxAngle);

        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        Vector3 direction = new Vector3(cos, 0.0f, sin);
        rb.AddForce(direction * 10000.0f);
    }

    void DrawDeviationCone() {
        this.lineRenderer.positionCount = 52;

        Transform transform = this.GetComponent<Transform>();
        Vector3 forward = transform.forward;

        Vector3[] points = new Vector3[52];
        points[0] = transform.position;
        points[51] = transform.position;

        float deviationRadians = this.currentDeviation * Mathf.Deg2Rad;
        float startAngle = Mathf.Atan2(forward.z, forward.x) - deviationRadians / 2.0f;

        for (int i = 1; i <= 50; i++) {
            float angle = startAngle + i / 50.0f * deviationRadians;

            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);
            Vector3 point = new Vector3(cos * 10.0f, 1.0f, sin * 10.0f);
            points[i] = transform.position + point;
        }

        this.lineRenderer.SetPositions(points);
    }

    void ClearLineRenderer() {
        this.lineRenderer.positionCount = 0;
    }
}
