using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEmbedding : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float frontOffset;
    private Vector3 forward;
    private Vector3 up;
    // Update is called once per frame

    void OnPreCull() => GL.Clear(true, true, Color.black);

    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 10); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }

    void Update()
    {
        forward = target.forward;
        forward.Normalize();
        up = target.up;
        up.Normalize();

        transform.position = target.position + forward * offset.z + up * offset.y;
        
        transform.LookAt(target.position + forward * frontOffset);
    }
}
