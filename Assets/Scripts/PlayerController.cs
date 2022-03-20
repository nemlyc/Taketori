using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 9.81f;
    public float age, preage = 1000;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask bamboolayer;

    private Vector3 moveDirection = Vector3.zero;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
