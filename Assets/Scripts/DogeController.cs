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
    public static DogeController inst;

    [SerializeField] private ECharacterState state = ECharacterState.Idle;
    public FloatingJoystick joystick;
    public Rigidbody2D rb2d;

    public GameObject boom;

    private bool hasShield;
    public float speed;

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
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
    public void Hit()
    {
        if (hasShield)
        {

        }
        else
        {
            // Gameover();
        }
    }
    private void Gameover()
    {
        state = ECharacterState.CantInteract;
    }
    private void SetInvincible(float time)
    {

    }
    private IEnumerator SetInvincibleCo(float time)
    {
        state = ECharacterState.Invincible;
        yield return new WaitForSeconds(time);
        state = ECharacterState.Idle;
    }
}
