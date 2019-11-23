using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private float zoomDepth;

    private float _originalZoom;
    
    #region Singleton
    public static CameraController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
        _originalZoom = mainCamera.m_Lens.FieldOfView;
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
                if (mainCamera.m_Lens.FieldOfView > zoomDepth)
                {
                    mainCamera.m_Lens.FieldOfView -= increment;
                    yield return new WaitForSeconds(0.0001f);
                }
                else zoomIn = false;
            }
            else
            {
                if (mainCamera.m_Lens.FieldOfView < _originalZoom)
                {
                    mainCamera.m_Lens.FieldOfView += increment;
                    yield return new WaitForSeconds(0.0001f);
                }
                else active = false;
            }
        }
        
        yield return null;
    }
}
