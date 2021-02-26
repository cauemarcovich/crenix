using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Gear : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public GameObject Prefab;
    [HideInInspector] public int UI_main_slot { get; set; }

    CanvasGroup group;
    RectTransform rect;
    Transform lastSlot;

    void Awake () {
        UI_main_slot = transform.parent.GetSiblingIndex ();

        group = GetComponent<CanvasGroup> ();
        rect = GetComponent<RectTransform> ();
    }

    void OnEnable () {
        group.blocksRaycasts = true;
    }

    public void OnBeginDrag (PointerEventData eventData) {
        group.blocksRaycasts = false;
        lastSlot = transform.parent;
        transform.SetParent (transform.parent.parent);
    }

    public void OnDrag (PointerEventData eventData) {
        var newPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        newPos.z = 0;
        transform.position = newPos;
    }

    public void OnEndDrag (PointerEventData eventData) {
        group.blocksRaycasts = true;

        var slot = eventData.pointerCurrentRaycast.gameObject;
        if (slot == null || slot.tag == "Gear") {
            transform.SetParent (lastSlot);
            rect.anchoredPosition = Vector2.zero;
        }
    }
}