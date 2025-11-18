using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;


public enum AIType
{
    Passive,
    Scared,
    Aggresive
}

public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Fleeing
}
public class NPC : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemDatabase[] dropOnDeath;

    [Header("AI")] 
    public AIType aiType;
    private AIState aiState;
    public float detectDistance;
    public float safeDistance;

    [Header("Wandering")] 
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")] 
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;
    private float playerDistance;

    [Header("Sound")] 
    public AudioSource audioSource;
    
    [Header("Events")]
    // Death event for wave system - UnityEvent cho phép config trong Inspector
    public UnityEngine.Events.UnityEvent onDeath = new UnityEngine.Events.UnityEvent();
    
    //get components

    private NavMeshAgent agent;
    private Animator anim;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        // Đợi NavMeshAgent sẵn sàng trước khi wandering
        StartCoroutine(InitializeAI());
    }
    
    private IEnumerator InitializeAI()
    {
        // Đợi 1 frame để NavMeshAgent được đặt trên NavMesh
        yield return new WaitForEndOfFrame();
        
        // Kiểm tra NavMeshAgent đã sẵn sàng
        if (agent != null && agent.isOnNavMesh)
        {
            SetState(AIState.Wandering);
            // Bắt đầu wandering ngay lập tức
            WanderToNewLocation();
            Debug.Log($"🧟 {gameObject.name} initialized - Starting to wander");
        }
        else
        {
            Debug.LogWarning($"❌ {gameObject.name}: NavMeshAgent not on NavMesh! Check placement.");
        }
    }

   
    private void Update()
    {
        //get Player distance
        playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        anim.SetBool("Moving",aiState != AIState.Idle);
       
       
        
        
        switch (aiState)
        {
            case AIState.Idle: PassiveUpdate();
                break;
            case AIState.Wandering: PassiveUpdate();
                break;
            case AIState.Attacking: AttackingUpdate();
                break;
            case AIState.Fleeing: FleeingUpdate();
                break;
        }
    }

    void PassiveUpdate()
    {
        // Kiểm tra agent có sẵn sàng và có path hợp lệ trước khi check remainingDistance
        if (aiState == AIState.Wandering && agent != null && agent.isOnNavMesh && agent.hasPath && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation",Random.Range(minWanderWaitTime,maxWanderWaitTime));
        }
        
        //begin the attack to player if npc detect him
        if (aiType == AIType.Aggresive && playerDistance < detectDistance)
        {
            
            
            SetState(AIState.Attacking);
        }
        //run away from the player if we detect him
        else if (aiType == AIType.Scared && playerDistance < detectDistance)
        {
            SetState(AIState.Fleeing);
            agent.SetDestination(GetFleeLocation());
        }
        
    }

    void AttackingUpdate()
    {
        // Kiểm tra agent có sẵn sàng trước khi sử dụng
        if (agent == null || !agent.isOnNavMesh) return;
        
        if (playerDistance > attackDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(PlayerController.instance.transform.position);
        }
        else
        {
            agent.isStopped = true;
         
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                PlayerController.instance.GetComponent<IDamagable>().TakePhysicDamage(damage);
                
                anim.SetTrigger("Attack");
                
            }
            
        }
    }

    void FleeingUpdate()
    {
        // Kiểm tra agent có sẵn sàng và có path hợp lệ trước khi check remainingDistance
        if (agent != null && agent.isOnNavMesh && agent.hasPath && playerDistance < safeDistance && agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(GetFleeLocation());
        }
        else if (playerDistance > safeDistance)
        {
            SetState(AIState.Wandering);
        }

    }

    void SetState(AIState newState)
    {
        // Kiểm tra agent tồn tại và đã được đặt trên NavMesh
        if (agent == null || !agent.isOnNavMesh)
        {
            Debug.LogWarning($"{gameObject.name}: NavMeshAgent not ready or not on NavMesh!");
            return;
        }
        
        aiState = newState;
        switch (aiState)
        {
            case AIState.Idle:
            {
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            }
            case AIState.Wandering:
            {
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
            }
            case AIState.Attacking:
            {
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
            }
                
            case AIState.Fleeing:
            {
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
            }
                
            
        }
        
    }

    void WanderToNewLocation()
    {
        // if npc is not in idle state dont call for new destination
        if (aiState != AIState.Idle)
        {
            Debug.Log($"⚠️ {gameObject.name}: Can't wander, not in Idle state (current: {aiState})");
            return;
        }
        
        if (agent == null || !agent.isOnNavMesh)
        {
            Debug.LogError($"❌ {gameObject.name}: Agent not ready! agent={agent}, isOnNavMesh={agent?.isOnNavMesh}");
            return;
        }
        
        SetState(AIState.Wandering);
        Vector3 destination = GetWanderLocation();
        agent.SetDestination(destination);
        
        Debug.Log($"🚶 {gameObject.name}: Started wandering to {destination}");
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        
        // Tìm vị trí ngẫu nhiên quanh NPC
        Vector3 randomDirection = Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance);
        randomDirection += transform.position;
        
        // Sample vị trí trên NavMesh
        bool found = NavMesh.SamplePosition(randomDirection, out hit, maxWanderDistance, NavMesh.AllAreas);
        
        if (!found)
        {
            Debug.LogWarning($"⚠️ {gameObject.name}: No valid NavMesh position found nearby! Using current position.");
            return transform.position;
        }

        int i = 0;
        // Đảm bảo không đi quá gần player
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            randomDirection = Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance);
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
            {
                Debug.LogWarning($"⚠️ {gameObject.name}: Couldn't find wander position away from player after 30 tries!");
                break;
            }
        }

        Debug.Log($"🎯 {gameObject.name}: Wandering to {hit.position} (distance: {Vector3.Distance(transform.position, hit.position):F1}m)");
        return hit.position;
    }

    Vector3 GetFleeLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, safeDistance,
            NavMesh.AllAreas);
        int i = 0;
        while (GetDestinationAngel(hit.position) > 90 || playerDistance < safeDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, safeDistance,
                NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;

    }

    float GetDestinationAngel(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - PlayerController.instance.transform.position,
            transform.position + targetPos);
    }
    
    
    
    public void TakePhysicDamage(int damageAmount)
    {
        // Kiểm tra cheat One Hit Kill (không áp dụng cho Boss Anti T1)
        bool isBoss = gameObject.GetComponent<BossAntiT1>() != null;
        
        if (CheatCodeManager.IsOneHitKillActive() && !isBoss)
        {
            // One hit kill - zombie chết ngay lập tức
            health = 0;
            Debug.Log($"⚔️ ONE HIT KILL: {gameObject.name} killed instantly!");
        }
        else
        {
            // Damage bình thường
            health -= damageAmount;
        }

        if (health <= 0)
            Die();

        StartCoroutine(DamageFlash());
        if (aiType == AIType.Passive)
            SetState(AIState.Fleeing);
    }

    private float delay = 0.0f;

    void Die()
    {
        for (int x = 0; x < dropOnDeath.Length; x++)
        {
            Instantiate(dropOnDeath[x].dropPrefab, transform.position, Quaternion.identity);
            
        }
        anim.SetTrigger("Die");
        
        // Trigger death event for wave system
        // WaveManager sẽ handle việc add star, không cần gọi ở đây!
        onDeath?.Invoke();
        
        Destroy(gameObject,this.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length+delay);
    }

    IEnumerator DamageFlash()
    {
        
        audioSource.Play();
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = new Color(1.0f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = Color.white;

    }
}
