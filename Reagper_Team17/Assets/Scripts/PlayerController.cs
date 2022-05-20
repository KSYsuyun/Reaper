using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PlayerController
    //5월 20일: EunBin 캐릭터 이동 구현

    public GameObject condiBar; //캐릭터의 체력바를 위한 선언

    Rigidbody2D rigid;
    SpriteRenderer sr;

    public float movementSpeed = 3.0f;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        Move();
    }
    
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        // left
        if (Input.GetAxisRaw("Horizontal") < 0)  //왼쪽
        {
            moveVelocity = Vector3.left;

            sr.flipX = true;
        }
        // right
        else if (Input.GetAxisRaw("Horizontal") > 0) //오른쪽
        {
            moveVelocity = Vector3.right;

            sr.flipX = false;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            movementSpeed = 7;
            condiBar.GetComponent<ConditionBar>().currentHP -= 1f;
        }
        else
        {
            movementSpeed = 3;

            condiBar.GetComponent<ConditionBar>().currentHP += 1f;
        }
        

        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }
}
