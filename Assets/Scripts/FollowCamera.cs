using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform _followTarget;
    [SerializeField] Vector3 _cameraOffset = new Vector3(0, 4f, -8f); 
    [SerializeField] float _followSpeed = 5f;    
    [SerializeField] float _lookSpeed = 5f;

    void LateUpdate()
    {
        if (_followTarget == null) return;

        // Desired position
        Vector3 desiredPosition = _followTarget.position + _followTarget.TransformDirection(_cameraOffset);

        // Smooth position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _followSpeed * Time.deltaTime);

        // Smooth rotation
        Quaternion targetRotation = Quaternion.LookRotation(_followTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lookSpeed * Time.deltaTime);
    }
}
