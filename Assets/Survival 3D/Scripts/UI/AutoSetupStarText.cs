using UnityEngine;
using TMPro;

/// <summary>
/// Script tự động setup TextMeshPro cho Star display
/// Attach vào GameObject có TextMeshProUGUI component
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class AutoSetupStarText : MonoBehaviour
{
    [Header("Text Settings")]
    public float fontSize = 40f;
    public Color textColor = Color.white;
    public TMPro.FontStyles fontStyle = TMPro.FontStyles.Bold;
    
    [Header("Alignment")]
    public TMPro.TextAlignmentOptions alignment = TMPro.TextAlignmentOptions.Center;
    
    [Header("Shadow/Outline (Optional)")]
    public bool addOutline = true;
    public Color outlineColor = Color.black;
    public float outlineWidth = 0.2f;
    
    private TextMeshProUGUI textComponent;
    
    private void Awake()
    {
        SetupText();
    }
    
    [ContextMenu("Setup Text")]
    public void SetupText()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        
        if (textComponent == null)
        {
            Debug.LogError("TextMeshProUGUI component not found!");
            return;
        }
        
        // Basic settings
        textComponent.fontSize = fontSize;
        textComponent.color = textColor;
        textComponent.fontStyle = fontStyle;
        textComponent.alignment = alignment;
        
        // Enable auto-sizing nếu text quá dài
        textComponent.enableAutoSizing = true;
        textComponent.fontSizeMin = 20;
        textComponent.fontSizeMax = fontSize;
        
        // Wrapping
        textComponent.enableWordWrapping = false;
        textComponent.overflowMode = TextOverflowModes.Overflow;
        
        // Outline cho dễ đọc
        if (addOutline)
        {
            textComponent.outlineWidth = outlineWidth;
            textComponent.outlineColor = outlineColor;
        }
        
        // Margin
        textComponent.margin = new Vector4(10, 10, 10, 10);
        
        Debug.Log($"✅ TextMeshPro setup complete for {gameObject.name}");
    }
    
    // Reset về default nếu cần
    [ContextMenu("Reset to Default")]
    public void ResetToDefault()
    {
        fontSize = 40f;
        textColor = Color.white;
        fontStyle = TMPro.FontStyles.Bold;
        alignment = TMPro.TextAlignmentOptions.Center;
        addOutline = true;
        outlineColor = Color.black;
        outlineWidth = 0.2f;
        
        SetupText();
    }
}
