using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
   
 // ���ǵ� ���� ����
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    private float applySpeed;

    // ���� ����
    [SerializeField]
    private float jumpForce;

    // �ɾ��� �� �󸶳� ������ �����ϴ� ����
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    // ���� ����
    private bool isRun = false;
    private bool isGround = true;
    private bool isCrouch = false;

    // �ΰ���
    [SerializeField]
    private float lookSensitivity;

    // ī�޶� �Ѱ�
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;



    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �Ҵ�
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();

        // �ʱ�ȭ
        applySpeed = walkSpeed;

        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }

    // Update is called once per frame
    void Update()
    {
        //IsGround();
        //TryJump();
        //TryRun();
        //TryCrouch();
        //Move();
        //CameraRotation();
        //CharacterRotation();
    }
    // ���� üũ
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    // ���� �õ�
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }
    // ����
    private void Jump()
    {
        if (isCrouch)
            //Crouch();

        myRigid.velocity = transform.up * jumpForce;
    }

    // �޸��� �õ�
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }
    // �޸���
    private void Running()
    {
        if (isCrouch)
           // Crouch();

        isRun = true;
        applySpeed = runSpeed;
    }

    // �޸��� ���
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }
}
