using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

    public float smoothing;

    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;

    void Awake() {
        direction = Vector2.zero;
        touched = false;
    }

    public void OnDrag(PointerEventData eventData) {
        if (eventData.pointerId == pointerID) {
            Vector2 currentPosition = eventData.position;
            direction = (currentPosition - origin).normalized;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (!touched) {
            touched = true;
            pointerID = eventData.pointerId;
            origin = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (eventData.pointerId == pointerID) {
            direction = Vector2.zero;
            touched = false;
        }
    }

    public Vector2 GetDirection() {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
        return smoothDirection;
    }
}
