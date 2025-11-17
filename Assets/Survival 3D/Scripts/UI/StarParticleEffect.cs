using System.Collections;
using UnityEngine;

/// <summary>
/// Script này tạo hiệu ứng particle khi zombie chết và rơi sao
/// Attach vào prefab sao 3D để tạo hiệu ứng bay lên UI
/// </summary>
public class StarParticleEffect : MonoBehaviour
{
    [Header("Animation Settings")]
    public float floatSpeed = 2f;
    public float rotationSpeed = 360f;
    public float scaleSpeed = 2f;
    public float lifetime = 2f;
    
    [Header("Color Animation")]
    public Gradient colorGradient;
    public bool animateColor = true;
    
    private Renderer starRenderer;
    private float elapsedTime = 0f;
    private Vector3 startPosition;
    private Vector3 startScale;
    
    private void Start()
    {
        starRenderer = GetComponent<Renderer>();
        startPosition = transform.position;
        startScale = transform.localScale;
        
        // Setup default gradient if not set
        if (colorGradient == null || colorGradient.colorKeys.Length == 0)
        {
            colorGradient = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[3];
            colorKeys[0] = new GradientColorKey(Color.yellow, 0f);
            colorKeys[1] = new GradientColorKey(Color.white, 0.5f);
            colorKeys[2] = new GradientColorKey(Color.yellow, 1f);
            
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0] = new GradientAlphaKey(1f, 0f);
            alphaKeys[1] = new GradientAlphaKey(0f, 1f);
            
            colorGradient.SetKeys(colorKeys, alphaKeys);
        }
        
        // Auto destroy after lifetime
        Destroy(gameObject, lifetime);
    }
    
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / lifetime;
        
        // Float upward
        transform.position = startPosition + Vector3.up * (floatSpeed * elapsedTime);
        
        // Rotate
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right, rotationSpeed * 0.5f * Time.deltaTime);
        
        // Scale animation (pulse then shrink)
        float scale = 1f;
        if (t < 0.3f)
        {
            // Pulse grow
            scale = Mathf.Lerp(0f, 1.2f, t / 0.3f);
        }
        else
        {
            // Shrink
            scale = Mathf.Lerp(1.2f, 0f, (t - 0.3f) / 0.7f);
        }
        transform.localScale = startScale * scale;
        
        // Color animation
        if (animateColor && starRenderer != null)
        {
            Color color = colorGradient.Evaluate(t);
            starRenderer.material.color = color;
        }
    }
}
