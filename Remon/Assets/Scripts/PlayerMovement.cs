using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.EventSystems.PointerEventData;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 2f;
    public float speed = 3f;
    public float horizontal = 0f;
    public float vertical = 0f;
    private bool isButtonActive = false;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!isButtonActive)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = Define.horizontal;
            vertical = Define.vertical;
            
        }
        print(horizontal + ", "+ vertical);

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        m_Movement = transform.TransformVector(m_Movement);

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }


        //transform.position += m_Movement * 1 * Time.deltaTime;
        //transform.LookAt(transform.position + m_Movement);
        if (vertical >= 0)
        {
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation(desiredForward);
        }
    }

    //public void w()
    //{
    //    StartCoroutine(ButtonVertical(1)); 
    //    isButtonActive = true;
    //}
    //public void s()
    //{
    //    StartCoroutine(ButtonVertical(-1));
    //    isButtonActive = true;
    //}
    //public void a()
    //{
    //    StartCoroutine(ButtonHorizontal(-1));
    //    isButtonActive = true;
    //}
    //public void d()
    //{
    //    StartCoroutine(ButtonHorizontal(1));
    //    isButtonActive = true;
    //}

    //IEnumerator ButtonVertical(int dir)
    //{
    //    if (dir == 1)
    //    {
    //        vertical = 9.0f;
    //    }
    //    else if (dir == -1)
    //    {
    //        vertical = -9.0f;
    //    }
    //    yield return new WaitForSeconds(0.3f);
    //    vertical = 0f;
    //}
    //IEnumerator ButtonHorizontal(int dir)
    //{
    //    if (dir == 1)
    //    {
    //        horizontal = 9.0f;
    //    }
    //    else if (dir == -1)
    //    {
    //        horizontal = -9.0f;
    //    }
    //    yield return new WaitForSeconds(0.3f);
    //    horizontal = 0f;
    //}



    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed );
        m_Rigidbody.MoveRotation(m_Rotation);

    }

}
