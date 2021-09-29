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
    public SpriteRenderer sr;
    public Rigidbody2D rb2d;
    public CircleCollider2D col2d;
    public Sprite[] sprs;
    private Sprite defaultSprite;
    public TrailRenderer dashTrail;

    public GameObject boom;
    private IEnumerator invincibleCo;

    private bool hasShield;
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
    }
    private void Update()
    {
        if (state == ECharacterState.CantInteract) return;

        CheckState();
    }
    public ECharacterState GetState() { return state; }
    public void Move(int dir)
    {
        switch (dir)
        {
            case 0:
                rb2d.velocity = Vector2.zero;
                break;
            case 1:             // left
                rb2d.velocity = Vector2.left * 5;
                break;
            case 2:
                rb2d.velocity = Vector2.right * 5;
                break;
        }
    }
    private void CheckState()
    {
        if (state == ECharacterState.Invincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer > 0f)
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
            SetInvincibility(true, 1.5f);
        }
        else
        {
            // Gameover();
        }
    }
    public void SetInvincibility(bool b, float time = 0.5f)
    {
        if (b)
        {
            invincibleTimer = time;
            state = ECharacterState.Invincible;
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
        //state = ECharacterState.CantInteract;
        //Camera.main.GetComponent<ProCamera2DPixelPerfect>().enabled = false;
        rb2d.velocity = Vector2.zero;
        GameManager.inst.SetTimeScale(0.2f);
        //Camera.main.GetComponent<ProCamera2DPixelPerfect>().Zoom = 2;
    }
}
