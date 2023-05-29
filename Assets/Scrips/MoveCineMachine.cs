using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class MoveCineMachine : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float startFOV = 40f;
    public float targetFOV;
    public float[] targetsFOV = {38.0f, 40, 45.0f};
    public float transitionDuration = 0.5f;
    private float time = 0f;
    
    private void Start()
    {
        virtualCamera.m_Lens.FieldOfView = startFOV;
    }

    private void Update()
    {
        if (Time.time >= time)
        {   
            time = Time.time + Random.Range(8, 12);
            StartCoroutine(ChangeFOVCoroutine());
        }
    }

    private IEnumerator ChangeFOVCoroutine()
    {   
        targetFOV = targetsFOV[Random.Range(0, targetsFOV.Length)];
        float elapsedTime = 0f;
        float currentFOV = virtualCamera.m_Lens.FieldOfView;
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(currentFOV, targetFOV, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.FieldOfView = targetFOV;
    }
}
