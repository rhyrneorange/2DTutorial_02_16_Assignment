using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Camera m_mainCam;
    //float m_time;
    Vector3 m_dir = Vector3.left;
    Vector3 m_prevPos;
    Rigidbody2D m_rigidbody;
    [SerializeField] float m_speed = 10f;

    /// <summary>
    /// 총알의 위치와 방향
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="dir"></param>
    public void SetBullet(Vector3 pos, Vector3 dir)
    {
        m_dir = dir;
        transform.position = pos;
        //m_rigidbody.AddForce(m_dir * m_speed, ForceMode2D.Impulse);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            RemoveBullet();
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            RemoveBullet();
        }
    }

    /*void OnBecameInvisible() //씬 카메라도 포함된다
    {
        Destroy(gameObject);
    }*/
    void RemoveBullet()
    {
        Destroy(gameObject);
    }
    
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //m_mainCam = Camera.main;
        //m_time = Time.time;

        Invoke("RemoveBullet", 3f); //매개변수가 없는 함수만 호출 가능
    }

    // Update is called once per frame
    void Update()
    {
        m_prevPos = transform.position;
        gameObject.transform.position += m_dir * m_speed * Time.deltaTime;
        var dir =transform.position - m_prevPos;
        var hit = Physics2D.Raycast(m_prevPos, dir.normalized, m_speed * Time.deltaTime, 1 << LayerMask.NameToLayer("Wall"));
        if (hit.collider != null)
        {
            transform.position = hit.point;
        }
        /*if (Time.time > m_time + 3f)
        {
            Destroy(gameObject);
        }*/

        /*m_time += Time.deltaTime;
        if (m_time > 3f)
        {
            Destroy(gameObject);
        }*/

        /* var viewPoint = m_mainCam.WorldToViewportPoint(transform.position);
        if (viewPoint.x < 0f || viewPoint.x > 1f)
        {
            Destroy(gameObject);
        }*/
    }
}
