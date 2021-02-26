using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class World_Gear : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public GameObject UI_Relative;
    
    Physics2DRaycaster camRaycaster;
    GameManager GM;

    void Awake () {
        camRaycaster = Camera.main.GetComponent<Physics2DRaycaster> ();
        GM = GameObject.FindObjectOfType<GameManager> ();
    }

    public void OnBeginDrag (PointerEventData eventData) {
        camRaycaster.eventMask = 311;
        GM.CheckGears (gameObject);
    }

    public void OnDrag (PointerEventData eventData) {
        var newPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        newPos.z = 0;
        transform.position = newPos;
    }

    public void OnEndDrag (PointerEventData eventData) {
        camRaycaster.eventMask = -1;

        var slot = eventData.pointerCurrentRaycast.gameObject;
        if (slot == null || slot.tag != "UI-Slot" || slot.transform == transform.parent || slot.transform.childCount > 0) {
            transform.localPosition = Vector2.zero;
        }

        GM.CheckGears ();
    }
}