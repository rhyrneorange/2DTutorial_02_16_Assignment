using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform m_firePos;
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] Inventory m_myInven;
    [Header("주인공 이동 속도")]
    [SerializeField] float m_speed;
    float m_jumpPower = 8f;
    [Space(10f)]

    [SerializeField] Animator m_animator; //멤버 객체 m
    //[SerializeField] SpriteRenderer m_sprRenderer;
    [SerializeField] Rigidbody2D m_rigidbody;

    Vector3 m_dir; //입력에 따른 방향
    int m_jumpCount;
    bool m_isFall;
    bool m_isGrounded;

    void Start()
    {
        //m_sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_animator = gameObject.GetComponent<Animator>();
        m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        m_rigidbody.velocity += (Vector2)m_dir * m_speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //바닥에 닿았을 때
        if (collision.CompareTag("Ground"))
        {
            m_isFall = false;
            m_isGrounded = true;
            m_animator.SetInteger("JumpState", 0);
            m_jumpCount = 0;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //바닥을 벗어났을 때
        if (collision.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }
    void CreatBullet()
    {
        var obj = Instantiate(m_bulletPrefab);
        var bullet = obj.GetComponent<BulletController>();
        bullet.SetBullet(m_firePos.position, transform.eulerAngles.y == 180f ? Vector3.right : Vector3.left);
    }

    void Update()
    {
        //인벤토리
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!m_myInven.gameObject.activeSelf)
            {
                m_myInven.ShowUI();
            }
            else
            {
                m_myInven.HideUI();
            }
        }

        //스페이스 바
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_animator.SetBool("IsFire", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_animator.SetBool("IsFire", true);
        }

        //좌우 방향키
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_animator.SetBool("IsMove", false);
            m_dir = Vector3.zero; //입력에 따라 방향 전달
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_animator.SetBool("IsMove", true);
            transform.rotation = Quaternion.identity;
            m_dir = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_animator.SetBool("IsMove", true);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            m_dir = Vector3.right;
        }

        //왼쪽 Ctrl
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //점프
            m_jumpCount++;
            if (m_jumpCount > 2)
            {
                m_jumpCount = 2;
                return;
            }
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, 0f); //현재 속도 초기화
            m_rigidbody.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
            m_animator.SetInteger("JumpState", 1);
        }

        //낙하 중일 때
        if (!m_isGrounded)
        {
            if (m_rigidbody.velocity.y < -1f)
            {
                if (!m_isFall)
                {
                    m_animator.SetInteger("JumpState", 2);
                    m_isFall = true;
                }
            }
        }
    }
}
