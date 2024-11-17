using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;  // ความเร็วในการเคลื่อนที่
    [SerializeField]
    private float collisionCheckDistance = 0.1f;  // ระยะห่างในการตรวจสอบการชน

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private BoxCollider2D boxCollider;

    // ตรวจสอบว่าในทิศทางที่ต้องการจะเคลื่อนที่มีการชนกับกำแพงหรือไม่
    private bool CanMove(Vector2 direction)
    {
        // ตรวจสอบการชนในทิศทางที่กำหนดโดยใช้ OverlapBox
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, collisionCheckDistance);
        return hit.collider == null;  // ถ้าไม่มีการชน (hit.collider เป็น null) ให้สามารถเคลื่อนที่ได้
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // ตรวจสอบว่ามีการชนในทิศทางที่ Player จะเคลื่อนที่หรือไม่
        Vector2 targetVelocity = movementInput * speed;

        if (movementInput.x != 0 && CanMove(Vector2.right * Mathf.Sign(movementInput.x)))
        {
            rb.velocity = new Vector2(targetVelocity.x, rb.velocity.y); // เคลื่อนที่ในแนว X
        }
        else if (movementInput.y != 0 && CanMove(Vector2.up * Mathf.Sign(movementInput.y)))
        {
            rb.velocity = new Vector2(rb.velocity.x, targetVelocity.y); // เคลื่อนที่ในแนว Y
        }
        else
        {
            rb.velocity = Vector2.zero;  // หยุดการเคลื่อนที่ถ้าชนกำแพง
        }
    }

    // รับค่าการเคลื่อนที่จาก Input System
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    // ตรวจสอบการชนเมื่อเข้าสู่การชนกับกำแพง
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // หยุดการเคลื่อนที่เมื่อชนกับกำแพง
            rb.velocity = Vector2.zero;
        }
    }

    // ตรวจสอบการชนแบบต่อเนื่อง
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // หยุดการเคลื่อนที่ในกรณีที่ Player ยังอยู่ในสถานะชน
            rb.velocity = Vector2.zero;
        }
    }

    // ตรวจสอบการออกจากการชน
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // เมื่อออกจากการชนกำแพง, อนุญาตให้ Player เคลื่อนที่ได้ตามปกติ
        }
    }
}
