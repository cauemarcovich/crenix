using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler {
    SlotType type;
    GameManager GM;

    void Awake () {
        type = GetComponent<RectTransform> () != null ? SlotType.UI : SlotType.World;
        GM = GameObject.FindObjectOfType<GameManager> ();
    }

    public void OnDrop (PointerEventData eventData) { DropOnSlot (eventData.pointerDrag); }

    void DropOnSlot (GameObject draggedObject) {
        if (transform.childCount > 0) return;
        if (type == SlotType.UI) {
            if (draggedObject.HasComponent<RectTransform> ())
                DropUIToUI (draggedObject);
            else
                DropWorldToUI (draggedObject);
        } else {
            if (draggedObject.HasComponent<RectTransform> ())
                DropUIToWorld (draggedObject);
            else
                DropWorldToWorld (draggedObject);
        }
    }

    void DropUIToWorld (GameObject draggedObject) {
        var image = draggedObject.GetComponent<Image> ();
        GM.CreateGearOnWorld (transform, draggedObject, image.color);
        draggedObject.SetActive (false);

        GM.CheckGears ();
    }

    void DropUIToUI (GameObject draggedObject) {
        draggedObject.transform.SetParent (transform);
        draggedObject.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
    }

    void DropWorldToWorld (GameObject draggedObject) {
        draggedObject.transform.parent = transform;
        draggedObject.transform.localPosition = Vector2.zero;
    }

    void DropWorldToUI (GameObject draggedObject) {
        var gearWorld = draggedObject.GetComponent<World_Gear> ();
        gearWorld.UI_Relative.SetActive (true);
        gearWorld.UI_Relative.transform.SetParent (transform);
        gearWorld.UI_Relative.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;

        Destroy (draggedObject);
        Camera.main.GetComponent<Physics2DRaycaster> ().eventMask = -1;
        GM.CheckGears (draggedObject);
    }

    enum SlotType { UI, World }
}