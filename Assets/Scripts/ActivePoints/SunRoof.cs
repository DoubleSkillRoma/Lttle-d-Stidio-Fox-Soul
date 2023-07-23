using System.Collections;
using UnityEngine;

public enum DoorState
{
    Open,
    Opening,
    Closed,
    Closing
}

public class SunRoof : MonoBehaviour
{
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _closeAngle = 0f;
    [SerializeField] private float _rotationSpeed = 65f;
    [SerializeField] private Animator _animator;

    private DoorState currentState = DoorState.Closed;
    private float _targetAngle;
    private bool _isRotating = false;

    private readonly float _rotationDelay = 1f;

    public event System.Action OnDoorOpened;
    public event System.Action OnDoorClosed;

    private void Start()
    {
        _targetAngle = _closeAngle;
    }

    public void Open()
    {
        if (currentState != DoorState.Opening && currentState != DoorState.Open)
        {
            _targetAngle = _openAngle;
            currentState = DoorState.Opening;

            if (!_isRotating)
            {
                StartCoroutine(RotateDoor());
            }
        }
    }

    public void Close()
    {
        if (currentState != DoorState.Closing && currentState != DoorState.Closed)
        {
            _targetAngle = _closeAngle;
            currentState = DoorState.Closing;

            if (!_isRotating)
            {
                StartCoroutine(RotateDoor());
            }
        }
    }

    private IEnumerator RotateDoor()
    {
        _isRotating = true;
        yield return new WaitForSeconds(_rotationDelay);

        while (true)
        {
            float currentAngle = transform.localRotation.eulerAngles.z;
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, _targetAngle, _rotationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, newAngle);

            if (currentAngle != _targetAngle)
            {
                _animator.SetBool("IsOpen", true);
            }
            else if (currentAngle == _targetAngle)
            {
                _animator.SetBool("IsOpen", false);
                if (currentState == DoorState.Opening)
                {
                    currentState = DoorState.Open;
                    OnDoorOpened?.Invoke(); 
                }
                else if (currentState == DoorState.Closing)
                {
                    currentState = DoorState.Closed;
                    OnDoorClosed?.Invoke();
                }
                _isRotating = false;
                yield break;
            }

            yield return null;
        }
    }
}
