using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
    GameObject GameArea;
    GameObject UI;
    TextMeshProUGUI Nugget;

    int totalGearsToPlace;

    void Awake () {
        GameArea = GameObject.Find ("GameArea");
        UI = GameObject.Find ("Inventory");
        Nugget = GameObject.Find ("Nugget/Baloon/Text").GetComponent<TextMeshProUGUI> ();

        var totalGears = UI.GetComponentsInChildren<UI_Gear> (true).Length;
        var totalWorldSlots = GameArea.GetComponentsInChildren<Slot> ().Length;
        totalGearsToPlace = Mathf.Min (totalGears, totalWorldSlots);
    }

    public void CreateGearOnWorld (Transform slot, GameObject UI_Relative, Color color) {
        var prefab = UI_Relative.GetComponent<UI_Gear> ().Prefab;

        var newGear = Instantiate (prefab, slot.position, Quaternion.identity);
        newGear.transform.SetParent (slot);

        var spriteRenderer = newGear.GetComponent<SpriteRenderer> ();
        spriteRenderer.color = color;

        var newGearWorldComponent = newGear.GetComponent<World_Gear> ();
        newGearWorldComponent.UI_Relative = UI_Relative;
    }

    public void CheckGears (GameObject objectToExclude = null) {
        var allGears = GameArea.GetComponentsInChildren<World_Gear> ();
        var gears = objectToExclude == null ? allGears : allGears.Where (g => g.gameObject != objectToExclude).ToArray ();

        if (gears.Length >= totalGearsToPlace) {
            SetSpinners (allGears, true);
            Nugget.text = "YAY, PARABÉNS,\nTASK CONCLUÍDA!";
        } else {
            SetSpinners (allGears, false);
            Nugget.text = "ENCAIXE AS ENGRENAGENS EM QUALQUER ORDEM!";
        }
    }

    void SetSpinners (World_Gear[] gears, bool enable) {
        foreach (var gear in gears)
            gear.GetComponent<Spinner> ().enabled = enable;
    }

    public void ResetGame () {
        foreach (var worldGear in GameArea.GetComponentsInChildren<World_Gear> ())
            Destroy (worldGear.gameObject);

        foreach (var gear in UI.GetComponentsInChildren<UI_Gear> (true)) {
            gear.gameObject.SetActive (true);

            var slot = UI.transform.GetChild (gear.UI_main_slot);
            gear.transform.SetParent (slot);
            gear.transform.localPosition = Vector2.zero;

            Nugget.text = "ENCAIXE AS ENGRENAGENS EM QUALQUER ORDEM!";
        }
    }
}