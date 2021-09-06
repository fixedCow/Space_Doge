using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBlinking : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        StarReposition();
    }
    private void StarReposition()
    {
        if (gameObject.layer == LayerMask.NameToLayer("P1"))
        {
            spriteRenderer.color = new Color32(255, 255, 255, (byte)Random.Range(150, 220));
            animator.SetFloat("mult", Random.Range(0.2f, 0.5f));
        }
        else
        {
            spriteRenderer.color = new Color32(255, 255, 255, (byte)Random.Range(120, 180));
            animator.SetFloat("mult", Random.Range(0.1f, 0.4f));
        }
    }
    private void OnBecameInvisible()
    {
        StarReposition();
        transform.position = new Vector2(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1));
    }
}
