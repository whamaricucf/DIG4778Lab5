using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FacePlayer : MonoBehaviour
{
    // Variables
    private Transform player;

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

    // Get angle between enemy and player then rotate
    void Update()
    {
        if (player == null) return; // Prevents NullReferenceException

        Vector3 directionDifferent = player.position - transform.position;
        float signedAngle = Vector3.SignedAngle(transform.up, directionDifferent, Vector3.forward);
        Vector3 rotation = new Vector3(0, 0, signedAngle + 90);

        transform.Rotate(rotation * Time.deltaTime * 10);
    }
}
