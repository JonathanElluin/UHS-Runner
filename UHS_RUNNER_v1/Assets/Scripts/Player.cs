using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public CharacterController PlayerCtrl;
    public float Gravity;
    public float Speed;
    float StopSpeed;
    bool OnEvent = false;
    public float SpeedRot;
    public float SpeedJump;
    bool IsJumping = false;
    bool CanRotate = false;
    bool IsRotating = false;
    public Transform TurningPoint = null;
    Quaternion newRot;

	// Use this for initialization
	void Start () {
        PlayerCtrl = GetComponent<CharacterController>();
        newRot = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 _moveDir = Vector3.zero;

        if (PlayerCtrl.isGrounded)
        {
            //Recentre le joueur
            _moveDir = transform.forward;
            _moveDir *= Speed * Time.deltaTime;
            PlayerRotate();
            //if (CanRotate && transform.rotation != newRot)
            //{
            //    transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * SpeedRot);
            //}
        }

        else if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }

        
        
        _moveDir.y -= Gravity * Time.deltaTime;
        PlayerCtrl.Move(_moveDir);

        //y jump 
        //
    }

    private void Update()
    {

        if (!CanRotate)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                newRot = Quaternion.Euler(0, -90, 0) * transform.rotation;
                CanRotate = true;
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                newRot = Quaternion.Euler(0, 90, 0) * transform.rotation;
                CanRotate = true;

            }
        }

        if (TurningPoint && Mathf.Approximately(transform.position.y, TurningPoint.position.y))
        {
            if (CanRotate)
            {
                IsRotating = true;
            }
        }
        else
        {
            IsRotating = true;
        }
    }

    void PlayerRotate()
    {
        
        if (IsRotating)
        {
            if (transform.rotation != newRot)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * SpeedRot);
            }
            else
            {
                IsRotating = false;
                TurningPoint = null;
                CanRotate = false;
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}
