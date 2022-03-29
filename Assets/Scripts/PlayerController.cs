﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    InGameViewPresenter presenter;

    public float speed = 10.0f;
    public float gravity = 9.81f;
    public float preage = 1000;
    public ReactiveProperty<float> currentAge = new ReactiveProperty<float>();
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask bamboolayer;

    private Vector3 moveDirection = Vector3.zero;
    public GameObject[] specilattackpoints;
    private int equipment;
    [SerializeField]
    private ScoreManager scoremanager;

    Transform mainCamera;

    CharacterController controller;

    public void AttackBambooLogic(GenericBamboo hitBamboo)
    {
        scoremanager.AddBamboo(hitBamboo.GetBambooType());

        var isGot = presenter.PickUpItem(out var itemEntity);
        if (isGot)
        {
            scoremanager.AddItem(itemEntity);
        }
        hitBamboo.AttackAction();
    }

    void Start()
    {
        currentAge.Value = preage;
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
        LookForward();

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeSpecialAttack();
        }
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            SpecialAttack();
        }
    }
    void FixedUpdate()
    {
        if(speed < 30) speed = preage - currentAge.Value + speed;
        Age();
    }

    void Age()
    {
        if (currentAge.Value > 20)
        {
            int sumbamboo = scoremanager.currentEntity.KaguyaNum + scoremanager.currentEntity.NormalNum + scoremanager.currentEntity.ShineNum;
            currentAge.Value = preage - sumbamboo / 10;
        }
    }

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
    void LookForward()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }
        var camRot = mainCamera.rotation.eulerAngles;
        var cameraYaw = new Vector3(0, camRot.y, 0);
        transform.localEulerAngles = cameraYaw;
    }

    void Attack()
    {
        Collider[] hitBamboos = Physics.OverlapSphere(attackPoint.position, attackRadius, bamboolayer);
        foreach(Collider hitBamboo in hitBamboos)
        {
            if (hitBamboo.TryGetComponent<GenericBamboo>(out var genericBamboo))
            {
                AttackBambooLogic(genericBamboo);
            }
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
