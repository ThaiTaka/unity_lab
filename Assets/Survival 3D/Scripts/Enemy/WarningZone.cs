using System.Collections;
using UnityEngine;

/// <summary>
/// Vùng cảnh báo đỏ trước khi meteor rơi
/// </summary>
public class WarningZone : MonoBehaviour
{
    [Header("Visual")]
    public Color warningColor = new Color(1f, 0f, 0f, 0.5f); // Đỏ semi-transparent
    public float pulseSpeed = 2f; // Tốc độ nhấp nháy
    
    private MeshRenderer meshRenderer;
    private Material material;
    private float alpha = 0.5f;
    
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
        if (meshRenderer != null)
        {
            material = meshRenderer.material;
            material.color = warningColor;
        }
        
        StartCoroutine(PulseEffect());
    }
    
    private IEnumerator PulseEffect()
    {
        while (true)
        {
            // Nhấp nháy alpha từ 0.3 → 0.7
            alpha = Mathf.PingPong(Time.time * pulseSpeed, 0.4f) + 0.3f;
            
            if (material != null)
            {
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            }
            
            yield return null;
        }
    }
}
