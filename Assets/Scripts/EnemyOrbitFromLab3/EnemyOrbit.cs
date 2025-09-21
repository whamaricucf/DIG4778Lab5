using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbit : MonoBehaviour
{
    public Transform player;
    public float speedMultiplier = 1f;
    public float exponent = 2f;           // Exponential factor for speed scaling
    public float minSpeed = 0.5f;
    public float maxSpeed = 10f;
    public float maxDistance = 20f;      // Maximum allowed distance from player
    public float minDistance = 2f;       // Minimum allowed distance from player
    public float pullSpeed = 1f;         // How quickly the enemy is pulled back
    public float pushSpeed = 1f;         // How quickly the enemy is pushed away

    public void GetPlayer()
    {
        if (player == null)
        {
            Player playerComponent = GameObject.FindAnyObjectByType<Player>();
            if (playerComponent != null)
            {
                player = playerComponent.transform;
            }
        }
    }


    private void Update()
    {
        if (player == null) return;

        // Direction from enemy to player
        Vector2 toPlayer = (player.position - transform.position).normalized;

        // Get the perpendicular direction (tangent to the orbit)
        Vector2 perpendicular = new Vector2(-toPlayer.y, toPlayer.x); // 90 degrees CCW

        // Calculate distance to player
        float distance = Vector2.Distance(transform.position, player.position);

        // Exponential speed scaling
        float speed = Mathf.Clamp(Mathf.Pow(distance, exponent) * speedMultiplier, minSpeed, maxSpeed);

        // Move perpendicularly to the player (orbit)
        Vector2 move = perpendicular * speed * Time.deltaTime;

        // Following logic ensures the enemy maintains a reasonable distance from the player 
        // and acts as collision avoidance between an enemy and the player

        // If too far, add a gentle pull toward the player
        if (distance >= maxDistance)
        {
            move += toPlayer * pullSpeed * Time.deltaTime;
        }
        // If too close, push away from the player
        else if (distance <= minDistance)
        {
            move -= toPlayer * pushSpeed * Time.deltaTime;
        }

        transform.position += (Vector3)move;
    }
}
