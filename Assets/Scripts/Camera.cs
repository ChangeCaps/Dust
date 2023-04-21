using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Vector3 offset;
    public float speed;
    public float maxDistance;
    public GameObject player;
    public GameObject enemy;

    Vector3 targetPosition;

    static Vector3 ExponentialSmoothing(
        Vector3 current, 
        Vector3 target, 
        float alpha, 
        float delta
    ) {
        float factor = 1.0f - Mathf.Exp(-delta * alpha);
        return current + factor * (target - current);
    }

    // Update is called once per frame
    void Update() {
        if (this.ShouldTargetEnemy()) {
            this.TargetEnemy();
        } else if (this.player != null) {
            this.TargetPlayer();
        }

        this.UpdatePosition();
    }

    float EnemyDistance() {
        if (this.player == null || this.enemy == null) {
            return 0.0f;
        }

        Transform player = this.player.GetComponent<Transform>();
        Transform enemy = this.enemy.GetComponent<Transform>();

        return Vector3.Distance(player.position, enemy.position);
    }

    bool ShouldTargetEnemy() {
        bool isValid = this.player != null && this.enemy != null;
        bool inRange = this.EnemyDistance() < this.maxDistance * 2.0;

        return isValid && inRange;
    }

    void UpdatePosition() {
        Transform transform = this.GetComponent<Transform>();

        Vector3 newPosition = ExponentialSmoothing(
            transform.position,
            this.targetPosition + this.offset, 
            this.speed,
            Time.deltaTime
        );
        transform.position = newPosition;
    }

    void TargetPlayer() {
        this.targetPosition = this.player.GetComponent<Transform>().position;
    }

    void TargetEnemy()  {
        Transform player = this.player.GetComponent<Transform>();
        Transform enemy = this.enemy.GetComponent<Transform>();

        this.targetPosition = (player.position + enemy.position) / 2.0f;
    }
}
