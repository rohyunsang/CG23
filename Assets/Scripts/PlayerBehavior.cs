using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float runSpeed = 10.0f;  // 달리기 속도를 기본 속도의 2배로 설정
    public float jumpForce = 10.0f;
    public float gravity = 9.81f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector3 moveDirection;
    private CharacterController controller;
    private Animator anim;
    public LayerMask _fieldLayer;  // 필드 레이어 설정

    //Weapon weapon;



    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();
        Attack();
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        // shift 키가 눌렸는지 확인
        float currentSpeed = (Input.GetKey(KeyCode.LeftShift)) ? runSpeed : moveSpeed;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            anim.SetBool("isMove", true);
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        if (IsGroundedUsingRay() && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
            anim.SetTrigger("Jump");
        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
    private bool IsGroundedUsingRay()
    {
        // CharacterController.IsGrounded가 true라면 Raycast를 사용하지 않고 판정 종료
        if (controller.isGrounded) return true;

        // 발사하는 광선의 초기 위치와 방향
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);

        // 탐색 거리
        var maxDistance = 1.5f;

        // 광선 디버그 용도
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);

        // Raycast의 hit 여부로 판정
        return Physics.Raycast(ray, maxDistance, _fieldLayer);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int randomSwing = Random.Range(0, 2); // 0 또는 1을 반환합니다.

            if (randomSwing == 0)
            {
                anim.SetTrigger("Swing_0");
            }
            else
            {
                anim.SetTrigger("Swing_1");
            }

        }

    }


}