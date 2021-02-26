using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public static class ExtensionMethods {
    public static bool HasComponent<T> (this GameObject obj) where T : Component {
        return obj.GetComponent<T> () != null;
    }
}