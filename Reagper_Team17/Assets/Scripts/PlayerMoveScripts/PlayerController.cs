using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sr;

    public float movementSpeed = 3.0f;
    public float jumpPower = 30f;
    public GameObject condiBar; //캐릭터의 체력바를 위한 선언
    public bool condiZero = false; //컨디션BAr.. 너무 달려서 체력이 0이 됨.
    bool isJumping = false; //캐릭터가 점프를 하고있는지 아닌지..
    public int playerPos_Floor = 1;//캐릭터의 위치_층별_ 1층 _2층
    
    public int playerPos_Room = 0;//캐릭터의 위치_층별_ 1층 _2층


    public bool isLadder = false; //사다리를 타고 있는지 아닌지 여부
    public bool wantDown; //아래로 내려가고싶은지 여부
    //====================================
    //아이템 인벤토리
    public Inventory inventory;
    public GameObject _item;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();

        if (isLadder && Input.GetKey(KeyCode.X))
        {
            //만약 사다리를 타고 있다면...?
            float v = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 0; //사다리를 타고있을땐, 중력 없게
            rigid.velocity = new Vector2(rigid.velocity.x, v * movementSpeed);
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                //만약 스페이스 바를 눌렀고, 점프가 안되있을 경우!.. 점프!
                Jump(); //사다리를 타고있지 않을 땐, 중력 있게
            }
            rigid.gravityScale = 3;
        }


    }
    private void FixedUpdate()
    {
        //착지와 캐릭터가 위치한 방의 위치파악을 위해..
        LayerMask mask = LayerMask.GetMask("1F") | LayerMask.GetMask("2F");

        if (rigid.velocity.y < 0) //캐릭터가 점프해서 velocity.y가 높아졌을 때만
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); //에디터 상에서만 레이를 그려준다
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 3, mask);
            if (rayHit.collider != null) // 바닥 감지를 위해서 레이저를 쏜다! 
            {
                isJumping = false;
                Debug.Log(rayHit.collider.name);

                if (rayHit.collider.CompareTag("1F_Floor"))
                {
                    playerPos_Floor = 1;
                    Debug.Log("현재 층 : " + playerPos_Floor);
                }
                else if (rayHit.collider.CompareTag("2F_Floor"))
                {
                    playerPos_Floor = 2;
                    Debug.Log("현재 층 : " + playerPos_Floor);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            //사다리에 닿였는지
            isLadder = true;
        }
        for(int i=1;i<5;i++)
        {
            //플레이어가 있는 방 위치 파악
            if (collision.CompareTag("room"+i.ToString()))
            {
                playerPos_Room = i;
            }
        }

        if(collision.CompareTag("key"))
        {
            //만약 플레이어와 닿아있는 Key에서 shift를 누르면.. 인벤토리에 저장
            _item = collision.gameObject;
            inventory.AddItem(_item.gameObject, _item.gameObject.GetComponent<Item>());

            /*if (Input.GetKey(KeyCode.LeftShift))
            {
                inventory.AddItem(_item.gameObject, _item.gameObject.GetComponent<Item>());
            }*/
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }


}
