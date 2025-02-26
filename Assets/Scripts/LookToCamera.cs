using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private Transform defaultCameraTransform;
    private void LateUpdate()
    {
        if (defaultCameraTransform == null)
            defaultCameraTransform = Camera.main.transform;

        transform.eulerAngles = defaultCameraTransform.eulerAngles;
    }
}