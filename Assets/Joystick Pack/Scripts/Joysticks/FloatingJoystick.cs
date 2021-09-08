using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FloatingJoystick : Joystick
{
    public bool alreadyActivated;

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (alreadyActivated)
        {

        }
        else
        {
            alreadyActivated = true;
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            background.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.15f).From(new Vector3(0, 0, 0), true);
            base.OnPointerDown(eventData);
        }

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        alreadyActivated = false;
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}