using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    [Header("카메라 옵션")]
    [Tooltip("카메라 속도")]
    public float CamSpeed;
    [Tooltip("카메라 오프셋")]
    public Vector3 CamOffset;
    [Tooltip("카메라 초기위치")]
    public GameObject StartPosition;

    public GameObject TargetObject;

    Vector3 TargetVector;

    Vector3 velvec;

    Ray ray;
    RaycastHit hit;

    public Vector3 Target;

    void Start()
    {
        //TargetObject = StartPosition;
    }

    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100000, Color.blue, 0.3f); 
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 0.3f);

        if (Physics.Raycast(ray, out hit, 100000))
        {
            Target = hit.point;
        }
    }

    void FixedUpdate()
    {   
    }

    private void LateUpdate()
    {
        CamUpdate();
    }

    void CamUpdate()
    {
        TargetVector = TargetObject.transform.position + CamOffset;

        transform.position = Vector3.SmoothDamp(transform.position, TargetVector, ref velvec, CamSpeed);
    }


}
