using UnityEngine;

public class SafeAreaAdjuster : MonoBehaviour
{
    private void Start() {
        var panel = GetComponent<RectTransform>();
        var area = Screen.safeArea;
        
        var anchorMin = area.position;
        var anchorMax = area.position + area.size;

        anchorMin.x /= Screen.width;
        anchorMax.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.y /= Screen.height;

        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
    }
}