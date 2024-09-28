using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 playerDistance, initRotation;

    [SerializeField] GameObject _playerModel;
    ModelBehaviour modelBehaviour;

    Vector3 _position;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        modelBehaviour = GetComponent<ModelBehaviour>();
    }


    private void Update()
    {
        FollowPlayer();
    }



    void FollowPlayer()
    {
        //// ������ �������� ������ ������ �� ������������ ��� (Y)
        //Quaternion rotation = Quaternion.Euler(initRotation.x, modelBehaviour.currentAngle, initRotation.z);

        //// ��������� �������� ������� ������

        Vector3 desiredPosition = _playerModel.transform.position + playerDistance;

        //// ������ ���������� ������ � �������� �������
        //transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, modelBehaviour.smoothSpeed);

        //// ������������, ����� ������ ������ �������� �� ������
        //transform.LookAt(_playerModel.transform.position + Vector3.left * 1.5f);

        //Vector3 desiredPosition = _playerModel.transform.position + playerDistance;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //transform.position = _playerModel.transform.position + playerDistance;
        transform.position = desiredPosition;
    }
}
