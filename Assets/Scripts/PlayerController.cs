using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public Animator anim;

    public float pickupRange = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        Debug.Log(moveInput);

        // 消除斜向移动速度变快的问题
        moveInput.Normalize();
        // * Time.deltaTime 消除帧率带来的速度差
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (moveInput != Vector3.zero) {
            anim.SetBool("isMoving", true);
        } else {
            anim.SetBool("isMoving", false);
        }
    }
}
