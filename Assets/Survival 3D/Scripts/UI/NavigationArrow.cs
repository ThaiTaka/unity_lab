using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArrow : MonoBehaviour
{
    [Header("Settings")]
    public Transform player; // Player transform
    public float distanceFromPlayer = 2f; // Khoảng cách từ player
    public float heightOffset = 1.0f; // Độ cao (tầm bụng player, 1m từ mặt đất)
    public float rotationSpeed = 5f; // Tốc độ xoay mũi tên
    
    [Header("Arrow Visual")]
    public GameObject arrowObject; // 3D model mũi tên (Cube, Cylinder, hoặc Sprite)
    public bool rotateToTarget = true; // Xoay mũi tên về phía target
    
    [Header("Arrow Appearance")]
    public Color arrowColor = Color.red; // Màu mũi tên mặc định
    
    [Header("Arrow Rotation Fix")]
    public Vector3 arrowLocalPosition = new Vector3(0, -0.6f, 2.5f); // Position của arrow (Y=-0.6 cao hơn một chút)
    public Vector3 arrowRotationOffset = new Vector3(-90, 90, 90); // Rotation của arrow model (-90, 90, 90)
    public Vector3 arrowScale = new Vector3(1f, 0.7f, 0.7f); // Scale của arrow (phóng to/thu nhỏ)
    // Custom Stand Arrow: Position (0, -0.6, 2.5), Rotation (-90, 90, 90), Scale (1, 0.7, 0.7)
    
    [Header("Animation")]
    public bool enablePulse = true; // Hiệu ứng nhấp nháy
    public float pulseSpeed = 2f; // Tốc độ nháy (2 = vừa phải, 1 = chậm, 3 = nhanh)
    public float pulseScale = 0.2f; // Độ phóng to thu nhỏ
    
    [Header("Glow Effect")]
    public bool enableGlow = true; // Hiệu ứng sáng nháy
    public float glowSpeed = 1.5f; // Tốc độ nháy sáng (1.5 = vừa phải)
    public float glowMinIntensity = 0.5f; // Độ tối nhất (0.5 = 50% độ sáng)
    public float glowMaxIntensity = 1.5f; // Độ sáng nhất (1.5 = 150% độ sáng)
    public Color glowColorRed = new Color(1f, 0f, 0f, 1f); // Màu đỏ
    public Color glowColorYellow = new Color(1f, 1f, 0f, 1f); // Màu vàng
    
    [Header("Distance Display")]
    public TMPro.TextMeshProUGUI distanceText; // Hiển thị khoảng cách đến zombie
    public bool showDistance = true;
    public string distanceFormat = "{0:F1}m"; // Format: "15.5m"
    
    private Transform currentTarget;
    private Vector3 originalScale;
    private Renderer[] arrowRenderers; // Cache ALL renderers (Stand Arrow has multiple children)
    private Light arrowLight; // Light component for glow effect

    private void Start()
    {
        if (player == null)
        {
            player = PlayerController.instance.transform;
            Debug.Log($"🎯 NavigationArrow: Found player at {player.position}");
        }
        
        // Check if NavigationArrow is child of Player
        if (transform.parent == player)
        {
            Debug.Log($"✅ NavigationArrow is CHILD of Player - Using local positioning");
            // Set local position (relative to player) - FORCE correct height
            transform.localPosition = new Vector3(0, heightOffset, 0);
            transform.localRotation = Quaternion.identity; // Reset rotation
            Debug.Log($"🎯 NavigationArrow local position set to: {transform.localPosition}");
            Debug.Log($"🎯 NavigationArrow world position: {transform.position}");
        }
        else
        {
            // NavigationArrow is independent - teleport to player
            Debug.Log($"✅ NavigationArrow is INDEPENDENT - Teleporting to player");
            transform.position = player.position + Vector3.up * heightOffset;
            Debug.Log($"🎯 NavigationArrow world position set to: {transform.position}");
        }
        
        // Ensure NavigationArrow is NOT child of Canvas (must be in world space)
        if (transform.parent != null && transform.parent.GetComponent<Canvas>() != null)
        {
            Debug.LogError("❌ NavigationArrow is child of Canvas! It must be in World Space, not UI!");
            Debug.LogError("📋 FIX: Drag NavigationArrow OUT of Canvas in Hierarchy");
        }

        if (arrowObject != null)
        {
            // FORCE correct values (override Inspector if needed)
            arrowLocalPosition = new Vector3(0, -0.6f, 2.5f); // Cao hơn một chút
            arrowRotationOffset = new Vector3(-90, 90, 90); // X = -90
            arrowScale = new Vector3(1f, 0.7f, 0.7f);
            
            // Set arrow position, rotation, and scale
            arrowObject.transform.localPosition = arrowLocalPosition; // (0, -0.8, 2.5)
            arrowObject.transform.localRotation = Quaternion.Euler(arrowRotationOffset); // (0, 90, 90)
            arrowObject.transform.localScale = arrowScale; // (1, 0.7, 0.7)
            
            originalScale = arrowScale; // Save for pulse animation
            
            Debug.Log($"🎯 Arrow child configured:");
            Debug.Log($"   - Local Position: {arrowObject.transform.localPosition}");
            Debug.Log($"   - Local Rotation: {arrowObject.transform.localEulerAngles}");
            Debug.Log($"   - Local Scale: {arrowObject.transform.localScale}");
            Debug.Log($"   - World Position: {arrowObject.transform.position}");
            
            // Add Point Light for glow effect (DISABLED - we only want emission glow)
            arrowLight = arrowObject.GetComponent<Light>();
            if (arrowLight != null)
            {
                // Disable existing light (we don't want it to shine on ground)
                arrowLight.enabled = false;
                Debug.Log($"🔦 Disabled Light component - using only emission glow");
            }
            // NOTE: We do NOT add Light component - only emission glow for the model itself
            
            // Get ALL renderers (Stand Arrow model has multiple child objects with MeshRenderer)
            arrowRenderers = arrowObject.GetComponentsInChildren<Renderer>();
            
            if (arrowRenderers == null || arrowRenderers.Length == 0)
            {
                Debug.LogError("❌ Stand Arrow does NOT have any Renderer components in children!");
                Debug.LogError("📋 FIX: Make sure Stand Arrow model has MeshRenderer on its child objects");
            }
            else
            {
                Debug.Log($"✅ Found {arrowRenderers.Length} Renderer(s) in Stand Arrow children");
                
                // Setup emission for ALL renderers
                foreach (Renderer renderer in arrowRenderers)
                {
                    if (renderer == null || renderer.material == null) continue;
                    
                    Material mat = renderer.material;
                    
                    // Enable emission for INNER GLOW effect
                    mat.EnableKeyword("_EMISSION");
                    mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
                    
                    // Set emission color (BRIGHT for inner glow)
                    if (mat.HasProperty("_EmissionColor"))
                    {
                        // Set initial bright emission (red glow from inside)
                        mat.SetColor("_EmissionColor", glowColorRed * 3f); // Very bright red!
                        Debug.Log($"  ✅ Emission enabled on: {renderer.name}");
                    }
                    
                    // Ensure material is opaque
                    if (mat.HasProperty("_Mode"))
                    {
                        mat.SetFloat("_Mode", 0); // Opaque mode
                    }
                }
            }
            
            Debug.Log($"🎯 NavigationArrow: Arrow object assigned: {arrowObject.name}");
        }
        else
        {
            Debug.LogError("❌ NavigationArrow: Arrow Object is NOT assigned!");
            Debug.LogError("📋 HOW TO FIX:\n" +
                          "1. Hierarchy → Right-click NavigationArrow\n" +
                          "2. 3D Object → Cube (or Cylinder)\n" +
                          "3. Rename to 'Arrow'\n" +
                          "4. Scale: (0.3, 0.5, 1.5) for Cube\n" +
                          "5. Drag 'Arrow' into NavigationArrow script → Arrow Object field");
        }
        
        Debug.Log($"🎯 NavigationArrow initialized - Waiting for zombie to spawn...");
    }

    private void Update()
    {
        // Get current zombie position from WaveManager
        if (WaveManager.instance != null && WaveManager.instance.IsWaveActive())
        {
            currentTarget = WaveManager.instance.GetCurrentZombiePosition();
            
            // Debug first time we get target
            if (currentTarget != null && (arrowObject == null || !arrowObject.activeSelf))
            {
                Debug.Log($"🎯 NavigationArrow: Target acquired! Zombie at {currentTarget.position}");
            }
        }
        else
        {
            currentTarget = null;
        }

        if (currentTarget == null || player == null)
        {
            // Hide arrow if no target
            if (arrowObject != null && arrowObject.activeSelf)
            {
                arrowObject.SetActive(false);
                Debug.Log($"🎯 NavigationArrow: Hiding arrow (no target)");
            }
            // Hide distance text (optional feature)
            if (distanceText != null)
            {
                distanceText.gameObject.SetActive(false);
            }
            return;
        }
        
        // Show arrow
        if (arrowObject != null && !arrowObject.activeSelf)
        {
            arrowObject.SetActive(true);
            Debug.Log($"🎯 NavigationArrow: Showing arrow!");
        }
        
        // Show distance text if assigned
        if (showDistance && distanceText != null && !distanceText.gameObject.activeSelf)
        {
            distanceText.gameObject.SetActive(true);
        }

        // Position arrow (only if NOT child of player)
        if (transform.parent != player)
        {
            // Independent positioning - follow player
            Vector3 targetPosition = player.position + Vector3.up * heightOffset;
            transform.position = targetPosition;
        }
        else
        {
            // Child of player - use local position (already set in Start)
            // No need to update position, it follows player automatically
        }

        // Rotate arrow to point at zombie
        if (rotateToTarget)
        {
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            direction.y = 0; // Keep arrow horizontal
            
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                
                // If child of player, use local rotation
                if (transform.parent == player)
                {
                    // Convert world rotation to local rotation
                    Quaternion localRotation = Quaternion.Inverse(player.rotation) * targetRotation;
                    transform.localRotation = Quaternion.Slerp(
                        transform.localRotation, 
                        localRotation, 
                        rotationSpeed * Time.deltaTime
                    );
                }
                else
                {
                    // Use world rotation
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation, 
                        targetRotation, 
                        rotationSpeed * Time.deltaTime
                    );
                }
            }
        }
        
        // Re-apply arrow child rotation AFTER parent rotates (CRITICAL FIX for Stand Arrow model)
        if (arrowObject != null)
        {
            arrowObject.transform.localRotation = Quaternion.Euler(arrowRotationOffset);
        }
        
        // Pulse animation (scale effect)
        if (enablePulse && arrowObject != null)
        {
            float pulseValue = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseScale;
            arrowObject.transform.localScale = originalScale * pulseValue;
        }
        
        // Inner glow animation (EMISSION pulsing - ĐỎ ↔ VÀNG)
        if (enableGlow && arrowRenderers != null && arrowRenderers.Length > 0)
        {
            // Calculate color transition (0.0 = Red, 1.0 = Yellow)
            float colorTransition = (Mathf.Sin(Time.time * glowSpeed) + 1f) * 0.5f;
            
            // Lerp between Red and Yellow
            Color currentGlowColor = Color.Lerp(glowColorRed, glowColorYellow, colorTransition);
            
            // Calculate glow intensity (3.0 to 8.0 for VERY BRIGHT emission)
            float glowIntensity = Mathf.Lerp(3f, 8f, colorTransition);
            
            // Apply emission to ALL renderers in Stand Arrow
            foreach (Renderer renderer in arrowRenderers)
            {
                if (renderer == null || renderer.material == null) continue;
                
                Material mat = renderer.material;
                
                // Set emission color (ĐỎ → VÀNG transition)
                if (mat.HasProperty("_EmissionColor"))
                {
                    Color emissionColor = currentGlowColor * glowIntensity; // Red→Yellow 3x-8x brightness!
                    mat.SetColor("_EmissionColor", emissionColor);
                    
                    // Enable HDR emission for EXTRA brightness
                    mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
                }
            }
        }
        
        // NOTE: Light component is DISABLED - we only use emission glow (no light shining on ground)
        
        // Update distance text (optional - only if assigned)
        if (showDistance && distanceText != null)
        {
            float distance = Vector3.Distance(player.position, currentTarget.position);
            distanceText.text = string.Format(distanceFormat, distance);
        }
    }

    private void OnDrawGizmos()
    {
        // Debug visualization
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 arrowPos = player.position + Vector3.up * heightOffset;
            Gizmos.DrawWireSphere(arrowPos, 0.5f);
            
            if (currentTarget != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(arrowPos, currentTarget.position);
            }
        }
    }
}
