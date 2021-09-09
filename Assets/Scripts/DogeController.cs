using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Com.LuisPedroFonseca.ProCamera2D;

public enum ECharacterState
{
    Idle,
    Invincible,
    Dash,
    CantInteract
}

public class DogeController : MonoBehaviour
{
    public static DogeController inst;

    [SerializeField] private ECharacterState state = ECharacterState.Idle;
    public FloatingJoystick joystick;
    public SpriteRenderer sr;
    public Rigidbody2D rb2d;
    public Sprite[] sprs;
    private Sprite defaultSprite;
    public TrailRenderer dashTrail;

    public GameObject boom;
    private IEnumerator invincibleCo;

    private bool hasShield;
    private float dashTimer;
    private float invincibleTimer;
    public float speed;

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
    private void Start()
    {
        defaultSprite = sprs[0];
        dashTimer = 0f;
    }
    private void Update()
    {
        if (state == ECharacterState.CantInteract) return;

        Move();
        CheckState();
    }
    public ECharacterState GetState() { return state; }
    private void Move()
    {
        if (!joystick.gameObject.activeSelf || state == ECharacterState.Dash)
            return;
        rb2d.velocity = joystick.Direction * speed;
    }
    public void Dash()
    {
        if (!ChargeGaugeController.inst.CheckDashPossibility() || state == ECharacterState.CantInteract || state == ECharacterState.Dash)
            return;

        state = ECharacterState.Dash;
        GameManager.inst.InstantiateSonicboomEffect(transform.position);
        sr.color = new Color32(255, 255, 255, 255);
        defaultSprite = sr.sprite;
        sr.sprite = sprs[2];
        dashTrail.gameObject.SetActive(true);
        rb2d.velocity = joystick.Direction.normalized * speed * 5f;
    }
    private void CheckState()
    {
        if (state == ECharacterState.Dash)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer > 0.25f)
            {
                dashTimer = 0f;
                rb2d.velocity = Vector2.zero;
                sr.sprite = defaultSprite;
                dashTrail.gameObject.SetActive(false);
                dashTrail.Clear();
                SetInvincibility(true);
            }
        }
        else if (state == ECharacterState.Invincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0f)
            {
                if (invincibleTimer % 0.2f < 0.1f)
                    sr.color = new Color32(255, 255, 255, 150);
                else
                    sr.color = new Color32(255, 255, 255, 50);
            }
            else
            {
                SetInvincibility(false);
            }
        }
    }
    public void Hit()
    {
        if (hasShield)
        {
            SetShield(false);
            SetInvincibility(true, 1f);
        }
        else
        {
            // Gameover();
        }
    }
    public void SetInvincibility(bool b, float time = 0.7f)
    {
        if (b)
        {
            state = ECharacterState.Invincible;
            invincibleTimer = time;
        }
        else
        {
            state = ECharacterState.Idle;
            invincibleTimer = time;
            sr.color = new Color32(255, 255, 255, 255);
        }
    }
    public void SetShield(bool b)
    {
        if (b)
        {
            hasShield = true;
            sr.sprite = sprs[1];
        }
        else
        {
            hasShield = false;
            sr.sprite = sprs[0];
        }

    }
    private void Gameover()
    {
        state = ECharacterState.CantInteract;
    }
}
