using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    public GameManager gameManager;
    private int hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 0.5f);

        if (transform.position.y < -11f + gameManager.player.transform.position.y)
        {
            Destroy(this.gameObject);
            gameManager.bigMeteorCount--;
        }

        if (hitCount >= 5)
        {
            Destroy(this.gameObject);
            gameManager.bigMeteorCount--;
            gameManager.virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3f; // Add a coroutine to reset this after a short duration
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.CompareTag("Player"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
            Destroy(whatIHit.gameObject);
        }
        else if (whatIHit.CompareTag("Laser"))
        {
            hitCount++;
            Destroy(whatIHit.gameObject);
        }
    }
}
