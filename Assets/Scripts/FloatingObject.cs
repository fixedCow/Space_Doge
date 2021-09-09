using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FloatingObject : MonoBehaviour
{
    public Rigidbody2D rb2d;

    [SerializeField] protected bool isGuided;
    [SerializeField] protected float speed;
    [SerializeField] protected float turnSpeed;             // 1~5;
    protected float timer;
    protected Vector2 direction;

    protected virtual void Update()
    {
        CheckSelfDestroyedCondition();
        GuidedCheck();
    }
    public void SetIsGuided(bool b) => isGuided = b;
    public void SetSpeed(float value) => speed = value;
    public void SetTurnSpeed(float value) => turnSpeed = value;

    public void Shoot()
    {
        direction = (DogeController.inst.transform.position - transform.position).normalized;
        rb2d.velocity = direction * speed;
    }
    protected virtual void Deactivate()
    {
        gameObject.SetActive(false);
        rb2d.velocity = Vector2.zero;
        SetIsGuided(false);
        timer = 0f;
    }
    protected virtual void CheckSelfDestroyedCondition()
    {
        if (Vector2.Distance(DogeController.inst.transform.position, transform.position) > GameManager.inst.distance)
            Deactivate();
        if (isGuided) timer += Time.deltaTime;
        if (timer > 12f)
            Deactivate();
    }
    protected virtual void GuidedCheck()
    {
        if (isGuided)
            Homing();
    }
    protected virtual void Homing()
    {
        Vector2 dir = (Vector2)DogeController.inst.transform.position - (Vector2)transform.position;

        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.up).z;

        rb2d.angularVelocity = -rotateAmount * turnSpeed * 25;
        rb2d.velocity = transform.up * speed;
    }
}
