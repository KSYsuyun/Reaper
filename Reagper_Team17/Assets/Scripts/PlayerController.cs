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
    public float jumpPower = 10f;
    public GameObject condiBar; //캐릭터의 체력바를 위한 선언
    public bool condiZero = false; //컨디션BAr.. 너무 달려서 체력이 0이 됨.
    bool isJumping = false; //캐릭터가 점프를 하고있는지 아닌지..
    public int playerPos_Floor = 1;//캐릭터의 위치_층별_ 1층 _2층
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        
        if (Input.GetButtonDown("Jump"))
        {
            //만약 스페이스 바를 눌렀고, 점프가 안되있을 경우!.. 점프!
            Jump();
        }
    }
    private void FixedUpdate()
    {
        //착지와 캐릭터가 위치한 방의 위치파악을 위해..
        LayerMask mask = LayerMask.GetMask("1F") | LayerMask.GetMask("2F");

        if (rigid.velocity.y < 0) //캐릭터가 점프해서 velocity.y가 높아졌을 때만
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); //에디터 상에서만 레이를 그려준다
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, mask);
            if (rayHit.collider != null) // 바닥 감지를 위해서 레이저를 쏜다! 
            {
                isJumping = false;
                if (rayHit.collider.tag == "1F")
                {
                    playerPos_Floor = 1;
                }
                else if (rayHit.collider.tag == "1F")
                {
                    playerPos_Floor = 2;
                }

            }
        }
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
    void Jump()
    {
        if(isJumping)
        {
            //만약 점프를 하는 중이면.. true;
            return;
        }

        rigid.velocity = Vector3.zero;

        Vector3 jumpVelocity = new Vector3(0, jumpPower, 0);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = true;
    }
}
