using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;
    [SerializeField] private float zoomDepth;
    [SerializeField] private CameraShaker shaker;
    [SerializeField] private CameraShaker.Properties standardShake;

    private float _originalZoom;
    
    public CameraShaker Shaker { get; private set; }
    
    #region Singleton
    public static CameraController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
        _originalZoom = mainCamera.fieldOfView;
        Shaker = shaker;
    }
    #endregion

    public void PlayerHitEffect() => StartCoroutine(HitAndZoom());

    private IEnumerator HitAndZoom()
    {
        var active = true;
        var zoomIn = true;
        var increment = 0.5f;

        while (active)
        {
            if (zoomIn)
            {
                if (mainCamera.fieldOfView > zoomDepth)
                {
                    mainCamera.fieldOfView -= increment;
                    yield return new WaitForSeconds(0.0001f);
                }
                else zoomIn = false;
            }
            else
            {
                if (mainCamera.fieldOfView < _originalZoom)
                {
                    mainCamera.fieldOfView += increment;
                    yield return new WaitForSeconds(0.0001f);
                }
                else active = false;
            }
        }
        
        yield return null;
    }
}
