using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 9.81f;
    public float age, preage = 1000;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask bamboolayer;

    private Vector3 moveDirection = Vector3.zero;
    public GameObject[] specilattackpoints;
    private int equipment;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        equipment = 0;
        foreach(GameObject specilattackpoint in specilattackpoints)
        {
            specilattackpoint.SetActive(false);
            specilattackpoint.GetComponent<Collider>().enabled = false;
        }
        specilattackpoints[equipment].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeSpecialAttack();
        }
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }else if(Input.GetMouseButtonDown(1))
        {
            SpecialAttack();
        }
    }
    void FixedUpdate()
    {
        if(speed < 30) speed = preage - age + speed;
        // Age();
    }
    // void Age()
    // {
    //     if (age > 20)
    //     {
    //         age -= hoge / 10;
    //     }
    // }

    void Movement()
    {
        if(controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    void Attack()
    {
        Collider[] hitBamboos = Physics.OverlapSphere(attackPoint.position, attackRadius, bamboolayer);
        foreach(Collider hitBamboo in hitBamboos)
        {
            Debug.Log("攻撃");
        }
    }

    void SpecialAttack()
    {
        foreach(GameObject specilattackpoint in specilattackpoints)
        {
            if(specilattackpoint.activeSelf)
            {
                specilattackpoint.GetComponent<Collider>().enabled = true;
                Observable.Timer(TimeSpan.FromSeconds(0.1f)).Subscribe(_ => specilattackpoint.GetComponent<Collider>().enabled = false);
            }
        }
    }

    void ChangeSpecialAttack()
    {
        equipment++;
        if(equipment >= specilattackpoints.Length)
        {
            equipment = 0;
        }
        //技の入れ替え
        for(var i = 0; i < specilattackpoints.Length; i++)
        {
            if(i == equipment)
            {
                specilattackpoints[i].SetActive(true);
            }else
            {
                specilattackpoints[i].SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
