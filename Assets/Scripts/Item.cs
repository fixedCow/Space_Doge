using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemGroup
{
    coin = 0,
    meteor = 1
}
public class Item : MonoBehaviour
{
    public SpriteRenderer sr;
    public Collider2D col2d;
    public Sprite[] sprs;

    public EItemGroup itemGroup;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DogeController>() != null)
        {

        }
    }
    private void SetItemGroup(int num)
    {
        switch (num)
        {
            case 0:
                sr.sprite = sprs[0];
                itemGroup = EItemGroup.coin;
                break;
            case 1:
                sr.sprite = sprs[1];
                itemGroup = EItemGroup.meteor;
                break;
            case 2:
                sr.sprite = sprs[2];
                itemGroup = EItemGroup.meteor;
                break;
            case 3:
                sr.sprite = sprs[3];
                itemGroup = EItemGroup.meteor;
                break;
        }
    }
    private void Init()
    {
        if (Random.Range(0, 10) < 2)
        {
            SetItemGroup(0);
        }
        else
        {

        }
    }
}
