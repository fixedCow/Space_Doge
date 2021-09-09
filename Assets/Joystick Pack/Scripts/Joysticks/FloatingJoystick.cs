using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (background.gameObject.activeSelf && Direction != new Vector2(0, 0))
        {
            DogeController.inst.Dash();
        }
        else
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            background.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.15f).From(new Vector3(0, 0, 0), true);
            base.OnPointerDown(eventData);
        }

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}