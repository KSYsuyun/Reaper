using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PlayerController
    //5월 20일: EunBin 캐릭터 이동 구현

    Rigidbody2D rigid;
    SpriteRenderer sr;

    public float movementSpeed = 3.0f;

    public GameObject condiBar; //캐릭터의 체력바를 위한 선언
    public bool condiZero = false; //컨디션BAr.. 너무 달려서 체력이 0이 됨.

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        /*        if(!condiZero)//false일때, 즉 체력이 바닥 나지않았을때.. 실행
                {
                    //만약 condiZero가 true이면, 움직이지못한다.
                    Move();
                }
                else if (condiZero) //true일때, 즉 체력이 바닥 났을때.. 실행
                {
                    //힘들어하는 애니메이션 추가
                    //!!
                }*/
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



        if (!condiZero)//false일때, 즉 체력이 바닥 나지않았을때.. 실행
        {
            if (Input.GetKey(KeyCode.Z))
            {
                movementSpeed = 7;
                condiBar.GetComponent<ConditionBar>().currentHP -= 0.5f;
            }
            else
            {
                movementSpeed = 3;

                condiBar.GetComponent<ConditionBar>().currentHP += 0.2f;
            }
        }
        else if (condiZero) //true일때, 즉 체력이 바닥 났을때.. 실행
        {
            //힘들어하는 애니메이션 추가
            movementSpeed = 2;//느려진 스피드..

            condiBar.GetComponent<ConditionBar>().currentHP += 0.2f;
        }


        
        

        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }
}
