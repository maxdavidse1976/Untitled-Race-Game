using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] float _acceleration = 800f;
    [SerializeField] float _maxSpeed = 20f;
    [SerializeField] float _turnSpeed = 100f;

    [Header("Wheel transforms")]
    [SerializeField] Transform _leftFrontWheel;
    [SerializeField] Transform _rightFrontWheel;
    [SerializeField] Transform _leftRearWheel;
    [SerializeField] Transform _rightRearWheel;

    [Header("Exhaust Trails")]
    [SerializeField] ParticleSystem _exhaustTrailLeft;
    [SerializeField] ParticleSystem _exhaustTrailRight;

    [Header("Exhaust settings")]
    [SerializeField] float _exhaustTreshold = 0.1f;

    [Header("Sound effects")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _engineHumming;
    [SerializeField] bool _playEngineSound = false;

    Rigidbody _rigidbody;
    float _moveInput;
    float _turnInput;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _moveInput = Input.GetAxis("Vertical");
        _turnInput = Input.GetAxis("Horizontal");

        RotateWheels();
    }

    void FixedUpdate()
    {
        // Limit the speed
        if (_rigidbody.angularVelocity.magnitude < _maxSpeed)
        {
            _rigidbody.AddForce(-transform.forward * _moveInput * _acceleration * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        // Check if we should play the engine sound based on the speed
        if (_rigidbody.angularVelocity.magnitude != 0)
        {
            _playEngineSound = true;
        }
        else
        {
            _playEngineSound = false;
        }

        if (_playEngineSound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }

        // Turning
        float turn = _turnInput * _turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);

        HandleExhaustFumes();
    }

    void RotateWheels()
    {
        float wheelSpeed = _moveInput * _acceleration * Time.fixedDeltaTime;


        // Rotate the wheels on local X axis
        if (_leftFrontWheel) _leftFrontWheel.Rotate(-Vector3.forward, wheelSpeed);
        if (_rightFrontWheel) _rightFrontWheel.Rotate(-Vector3.forward, wheelSpeed);
        if (_leftRearWheel) _leftRearWheel.Rotate(-Vector3.forward, wheelSpeed);
        if (_rightRearWheel) _rightRearWheel.Rotate(-Vector3.forward, wheelSpeed);
    }

    void HandleExhaustFumes()
    {
        bool isEmitting = _rigidbody.angularVelocity.magnitude > _exhaustTreshold;

        if (isEmitting == false)
        {
            _exhaustTrailLeft.Play();
            _exhaustTrailRight.Play();
        }
        else
        {
            _exhaustTrailLeft.Stop();
            _exhaustTrailRight.Stop();
        }
    }
}
