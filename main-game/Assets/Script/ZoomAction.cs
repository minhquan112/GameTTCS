using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZoomAction : MonoBehaviour
{
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 6f;
    private float maxZoom = 18f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;
    [SerializeField] Cinemachine.CinemachineVirtualCamera _virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        minZoom = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        _virtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(_virtualCamera.m_Lens.OrthographicSize, zoom, ref velocity, smoothTime);
    }
}
