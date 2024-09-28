using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class ModelBehaviour : MonoBehaviour
{
    public float speed;

    Rigidbody _rb;
    Vector3 _movement;

    public float currentAngle = 0f;
    private float targetAngle = 0f;
    private Vector3 currentVelocity = Vector3.zero;

    Camera _camera;

    public float smoothSpeed = 0.3f;
    float rotationSpeed = 30f;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        _camera = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        HandleCameraMovement();
    }


    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }

    void HandleCameraMovement()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetAngle -= 30f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetAngle += 30f;
        }
    }

    private void UpdateCameraPosition()
    {
        currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(gameObject.transform.rotation.x, currentAngle, gameObject.transform.rotation.z);
        Vector3 desiredPosition = rotation * gameObject.transform.position;

        //Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        gameObject.transform.rotation = rotation;
    }


    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A, D или стрелки влево/вправо
        float vertical = Input.GetAxis("Vertical");     // W, S или стрелки вверх/вниз

        // ѕолучаем передний и правый векторы камеры
        Vector3 camForward = _camera.transform.forward;
        Vector3 camRight = _camera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // ¬ычисл€ем итоговое направление движени€
        Vector3 desiredMoveDirection = camForward * vertical + camRight * horizontal;

        if (desiredMoveDirection.magnitude > 1f)
        {
            desiredMoveDirection.Normalize();
        }

        _movement = desiredMoveDirection;

        _rb.MovePosition(transform.position + desiredMoveDirection * speed * Time.deltaTime);

        //_rb.MovePosition(gameObject.transform.position + _movement * speed * Time.fixedDeltaTime);
    }
}
