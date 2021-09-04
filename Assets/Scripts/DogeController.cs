using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ECharacterState
{
    Idle,
    Invincible,
    CantInteract
}

public class DogeController : MonoBehaviour
{
    [SerializeField] private ECharacterState state = ECharacterState.Idle;
    public FloatingJoystick joystick;
    public Rigidbody2D rb2d;

    public float speed;

    private void Update()
    {
        if (state == ECharacterState.CantInteract) return;

        Move();
    }
    private void Move()
    {
        if (!joystick.gameObject.activeSelf) return;
        rb2d.velocity = joystick.Direction * speed;
    }
}
