using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin noise;
    private Coroutine shakeRoutine;

    public void Init()
    {
        virtualCam = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        noise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float amplitude, float duration, float frequency = 3)
    {
        if(shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
            shakeRoutine = null;
        }

        shakeRoutine ??= StartCoroutine(ShakeRoutine(amplitude, frequency, duration));
    }

    private IEnumerator ShakeRoutine(float amplitude, float frequency, float duration)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;

        yield return new WaitForSeconds(duration);

        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }
}
