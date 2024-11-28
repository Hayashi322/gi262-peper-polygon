using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // ความเร็วในการเคลื่อนที่
    [SerializeField] private float collisionCheckDistance = 0.1f; // ระยะห่างในการตรวจสอบการชน
    [SerializeField] private LayerMask wallLayer; // เลเยอร์สำหรับตรวจสอบกำแพง

    private Rigidbody2D rb;
    private Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = movementInput * speed;

        if (movementInput != Vector2.zero) // มีการกดปุ่มเคลื่อนที่
        {
            // ตรวจสอบการชนในแกน X และ Y
            bool canMoveX = movementInput.x != 0 && CanMove(Vector2.right * Mathf.Sign(movementInput.x));
            bool canMoveY = movementInput.y != 0 && CanMove(Vector2.up * Mathf.Sign(movementInput.y));

            // กำหนดความเร็วเฉพาะแกนที่สามารถเคลื่อนที่ได้
            float moveX = canMoveX ? targetVelocity.x : 0;
            float moveY = canMoveY ? targetVelocity.y : 0;

            rb.velocity = new Vector2(moveX, moveY);
        }
        else
        {
            rb.velocity = Vector2.zero; // หยุดการเคลื่อนที่เมื่อไม่มี input
        }
    }

    private bool CanMove(Vector2 direction)
    {
        // ใช้ Raycast เพื่อตรวจสอบการชนในทิศทางที่กำหนด
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, collisionCheckDistance, wallLayer);
        return hit.collider == null; // หากไม่มีการชน ให้เคลื่อนที่ได้
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))
        {
            Debug.Log("!!Get Item!!");
            Destroy(collision.gameObject); // ลบไอเทมออกจากเกม
        }
    }
}
