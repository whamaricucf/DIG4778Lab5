using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;
    public Player player;
    public bool gameOver = false;
    private PlayerInputActions _playerInputActions;
    private CinemachineVirtualCamera virtualCamera;
    private float minZoom, maxZoom, zoomSpeed;


    public int meteorCount = 0;
    public int bigMeteorCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        player = GameObject.FindAnyObjectByType<Player>();
        virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = player.transform;
        minZoom = 60f;
        maxZoom = 80f;
        zoomSpeed = 5f;
        InvokeRepeating(nameof(SpawnMeteor), 1f, 2f);
    }

    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActions();

        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            CancelInvoke();
        }

        if (_playerInputActions.Player.Restart.triggered && gameOver)
        {
            SceneManager.LoadScene("Week5Lab");
        }

        if (meteorCount == 5)
        {
            BigMeteor();
        }
        if (bigMeteorCount > 0)
        {
            float fov = virtualCamera.m_Lens.FieldOfView;
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, maxZoom, zoomSpeed * Time.deltaTime);
        }
        else
        {
            float fov = virtualCamera.m_Lens.FieldOfView;
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, minZoom, zoomSpeed * Time.deltaTime);
        }
    }

    void SpawnMeteor()
    {
        Instantiate(meteorPrefab, new Vector3(player.transform.position.x + Random.Range(-10, 10), player.transform.position.y + 8f, 0), Quaternion.identity);
    }

    void BigMeteor()
    {
        meteorCount = 0;
        bigMeteorCount++;
        Instantiate(bigMeteorPrefab, new Vector3(player.transform.position.x + Random.Range(-10, 10), player.transform.position.y + 8f, 0), Quaternion.identity);
        
    }
}
