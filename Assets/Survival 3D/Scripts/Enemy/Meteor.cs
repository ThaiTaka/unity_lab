using UnityEngine;

/// <summary>
/// Meteor rơi từ trên xuống, gây damage khi chạm đất
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Meteor : MonoBehaviour
{
    [Header("Settings")]
    public float fallSpeed = 20f;
    public float damage = 20f;
    public float explosionRadius = 3f;
    
    [Header("Effects")]
    public GameObject explosionEffect;
    public AudioClip explosionSound;
    
    private Rigidbody rb;
    private bool hasExploded = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.down * fallSpeed;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (hasExploded) return;
        
        hasExploded = true;
        
        // Spawn explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        
        // Play sound
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1.0f);
        }
        
        // Gây damage cho player trong bán kính
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerNeeds player = hit.GetComponent<PlayerNeeds>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    Debug.Log($"💥 Meteor hit player! Damage: {damage}");
                }
            }
        }
        
        // Destroy meteor
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        // Vẽ explosion radius trong editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
