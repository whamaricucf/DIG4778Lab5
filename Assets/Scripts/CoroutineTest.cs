// Hi, I have no idea why but waiting in the Screenshaker coroutine doesn't work as expected.
// The AmplitudeGain is set to 3 but never reset to 0 after the wait.
// However, if I call Stopshaker inside the coroutine, it works fine.
// The code in here is a little messy as a result of my attempts to debug it.
// I truly have no idea why waiting in Screenshaker just ends the coroutine prematurely.

using Cinemachine;
using UnityEngine;
using System.Collections;

public class CoroutineTest : MonoBehaviour
{
    public GameManager gameManager;
    private CinemachineBasicMultiChannelPerlin noise;

    // Start is called before the first frame update
    void Start()
    {
        var gmObj = GameObject.Find("GameManager");
        if (gmObj == null)
        {
            Debug.LogError("GameManager GameObject not found in scene!");
            return;
        }
        gameManager = gmObj.GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager component not found on GameManager GameObject!");
            return;
        }
        if (gameManager.virtualCamera == null)
        {
            Debug.LogError("virtualCamera is not assigned on GameManager!");
            return;
        }
        noise = gameManager.virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (noise == null)
        {
            Debug.LogWarning("CinemachineBasicMultiChannelPerlin component not found on the virtual camera!");
        }
    }
    public IEnumerator Screenshaker()
    {
        Debug.Log("Screenshaker " + this.GetInstanceID());
        if (noise == null)
        {
            Debug.LogWarning("CinemachineBasicMultiChannelPerlin not found!");
            yield break;
        }
        SetAmplitudeTo3();
        StartCoroutine(Stopshaker());
        Debug.Log("AmplitudeGain set to 3");
        yield return new WaitForSecondsRealtime(0.3f);
        Debug.Log("After WaitForSeconds");
        SetAmplitudeTo0();
        Debug.Log("Resetting AmplitudeGain to 0");
    }

    public IEnumerator Stopshaker()
    {
        Debug.Log("Stopshaker " + this.GetInstanceID());
        yield return new WaitForSeconds(0.3f);
        SetAmplitudeTo0();
    }

    private void SetAmplitudeTo3()
    {
        noise.m_AmplitudeGain = 3f;
    }

    private void SetAmplitudeTo0()
    {
        noise.m_AmplitudeGain = 0f;
    }
}