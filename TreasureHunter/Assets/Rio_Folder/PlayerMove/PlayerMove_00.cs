using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_00 : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            speed = 6.0F;
            if (Input.GetKey(KeyCode.LeftShift)) //달리기 구현
            {
                speed = 12.0F;
            }
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    } 
}
// Update is called once per frame
    //void Update()
    //{
    //    float h = Input.GetAxisRaw("Horizontal");
    //    float v = Input.GetAxisRaw("Vertical");
    //    Vector3 Dir = transform.right * h + transform.forward * v;
    //    Dir.Normalize();
    //    cc.SimpleMove(Dir * speed);

    //}