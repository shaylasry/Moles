using UnityEngine;

public class RotationVibration : MonoBehaviour
{
    [SerializeField] private float _frequency = 1f; // Noise frequency
    [SerializeField] private float _amplitude = 15f; // Vibration amplitude in degrees
    [SerializeField] private float _speed = 1f; // Vibration speed

    private float _initialRotationZ;
    private float _noiseOffset;  

    private void Awake()
    {
        _initialRotationZ = transform.localEulerAngles.z;
        _noiseOffset = Random.Range(0f, 1000f); // Random offset to make each object's vibration unique
    }

    private void Update()
    {
        VibrateRotation();
    }

    private void VibrateRotation()
    {
        float noise = (Mathf.PerlinNoise(Time.time * _speed + _noiseOffset, 0) * 2 - 1) * _amplitude;
        float currentRotationZ = _initialRotationZ + noise;
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, currentRotationZ);
    }
}