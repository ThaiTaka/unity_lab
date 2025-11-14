# 🎮 HƯỚNG DẪN SETUP WAVE SYSTEM + STAR RATING + NAVIGATION ARROW

## 📋 TÓM TẮT HỆ THỐNG

✅ **Wave System**: Zombie spawn theo thứ tự 1→2→3→4→5  
✅ **Star Rating**: Đánh bại zombie tăng sao (1★→6★)  
✅ **Navigation Arrow**: Mũi tên chỉ đường đến zombie  

---

## 🛠️ BƯỚC 1: TẠO SPAWN POSITIONS

### **Tạo Empty GameObjects Để Spawn Zombie:**

```
Hierarchy → Click phải → Create Empty
Đặt tên: "ZombieSpawnPoints"

Trong đó tạo 5 children:
├─ SpawnPoint_1 (Position: x, y, z)
├─ SpawnPoint_2 (Position: x, y, z)
├─ SpawnPoint_3 (Position: x, y, z)
├─ SpawnPoint_4 (Position: x, y, z)
└─ SpawnPoint_5 (Position: x, y, z)
```

**Đặt vị trí cách nhau (ví dụ):**
- SpawnPoint_1: (10, 0, 10)
- SpawnPoint_2: (20, 0, 15)
- SpawnPoint_3: (30, 0, 20)
- ...

---

## 🎯 BƯỚC 2: SETUP WAVE MANAGER

### **Tạo WaveManager GameObject:**

```
1. Hierarchy → Create Empty
2. Đặt tên: "WaveManager"
3. Add Component → WaveManager (script)
```

### **Configure WaveManager:**

```
Inspector → WaveManager:

Wave Settings:
├─ Size: 5 ← Số lượng waves
│
├─ Wave 0:
│  ├─ Wave Name: "Wave 1"
│  ├─ Zombie Prefab: Enemy_Zombie (kéo từ Enemies folder)
│  └─ Spawn Position: SpawnPoint_1
│
├─ Wave 1:
│  ├─ Wave Name: "Wave 2"
│  ├─ Zombie Prefab: Enemy_Zombie_2
│  └─ Spawn Position: SpawnPoint_2
│
├─ Wave 2:
│  ├─ Wave Name: "Wave 3"
│  ├─ Zombie Prefab: Enemy_Zombie_3
│  └─ Spawn Position: SpawnPoint_3
│
├─ Wave 3:
│  ├─ Wave Name: "Wave 4"
│  ├─ Zombie Prefab: Enemy_Zombie_4
│  └─ Spawn Position: SpawnPoint_4
│
└─ Wave 4:
   ├─ Wave Name: "Wave 5"
   ├─ Zombie Prefab: Enemy_Zombie_5
   └─ Spawn Position: SpawnPoint_5
```

---

## ⭐ BƯỚC 3: TẠO STAR UI

### **Tạo Star Prefab:**

```
1. Hierarchy → UI → Image
2. Đặt tên: "Star"
3. Inspector:
   ├─ Source Image: (star sprite/icon)
   ├─ Width: 50
   ├─ Height: 50
   └─ Color: Yellow
4. Kéo vào Project → Tạo Prefab "StarUI"
5. Xóa khỏi Hierarchy
```

### **Tạo Star Container:**

```
1. Hierarchy → Canvas → UI → Panel
2. Đặt tên: "StarContainer"
3. Inspector:
   ├─ Anchor: Top-Right
   ├─ Width: 400
   ├─ Height: 80
   └─ Add Component: Horizontal Layout Group
      ├─ Spacing: 10
      ├─ Child Alignment: Middle Right
      └─ Padding: 10
```

### **Configure WaveManager Star Settings:**

```
WaveManager Inspector:

Star System:
├─ Current Stars: 0
├─ Star Prefab: StarUI (kéo prefab vào)
└─ Star Container: StarContainer (kéo UI object vào)
```

---

## 🎯 BƯỚC 4: TẠO NAVIGATION ARROW

### **Tạo Arrow 3D Model:**

**Option 1: Dùng 3D Model Arrow**
```
1. Project → Create → 3D Object → Cube (hoặc import arrow model)
2. Scale: (2, 0.2, 0.5) - Hình mũi tên dài
3. Material: Màu đỏ/vàng
4. Tạo Prefab: "ArrowModel"
```

**Option 2: Dùng Sprite (đơn giản hơn)**
```
1. Hierarchy → 3D Object → Quad
2. Material: Unlit/Transparent
3. Texture: Arrow sprite
4. Rotation: X: 90 (nằm ngang)
5. Scale: (1, 1, 1)
```

### **Tạo NavigationArrow GameObject:**

```
1. Hierarchy → Create Empty
2. Đặt tên: "NavigationArrow"
3. Add Component → NavigationArrow (script)
4. Kéo ArrowModel vào làm child
```

### **Configure NavigationArrow:**

```
Inspector → NavigationArrow:

Settings:
├─ Player: (Auto tìm, hoặc kéo Player vào)
├─ Distance From Player: 2
├─ Height Offset: -0.5 (dưới chân)
└─ Rotation Speed: 5

Arrow Visual:
├─ Arrow Object: ArrowModel (child object)
└─ Rotate To Target: ✓

Animation:
├─ Enable Pulse: ✓
├─ Pulse Speed: 2
└─ Pulse Scale: 0.2
```

---

## 📱 BƯỚC 5: TẠO WAVE UI TEXT

```
1. Hierarchy → Canvas → UI → TextMeshPro
2. Đặt tên: "WaveText"
3. Inspector:
   ├─ Text: "Wave 1/5"
   ├─ Font Size: 36
   ├─ Alignment: Center
   ├─ Anchor: Top-Center
   └─ Position: (0, -50, 0)
4. WaveManager → UI → Wave Text: (kéo WaveText vào)
```

---

## 🎮 BƯỚC 6: TEST

### **Chuẩn Bị:**

```
✅ 5 Zombie prefabs có NPC component
✅ 5 Spawn points đã đặt vị trí
✅ WaveManager configured
✅ NavigationArrow active
✅ StarContainer ready
```

### **Play Game:**

```
1. Play (▶️)
2. Zombie 1 spawn → Mũi tên chỉ đến zombie
3. Đánh bại Zombie 1 → 1 sao xuất hiện
4. Zombie 2 spawn (sau 2 giây) → Mũi tên chỉ mới
5. Lặp lại đến Zombie 5
6. Hoàn thành → "Complete! ⭐5"
```

---

## 🎨 CUSTOMIZATION

### **Thay Đổi Màu Mũi Tên Theo Khoảng Cách:**

Thêm vào `NavigationArrow.cs`:

```csharp
[Header("Distance Color")]
public Color nearColor = Color.green;   // Gần
public Color farColor = Color.red;      // Xa
public float maxDistance = 50f;

private MeshRenderer arrowRenderer;

void Start()
{
    arrowRenderer = arrowObject.GetComponent<MeshRenderer>();
}

void Update()
{
    // ... existing code ...
    
    // Color based on distance
    if (arrowRenderer != null && currentTarget != null)
    {
        float distance = Vector3.Distance(player.position, currentTarget.position);
        float t = Mathf.Clamp01(distance / maxDistance);
        arrowRenderer.material.color = Color.Lerp(nearColor, farColor, t);
    }
}
```

### **Thêm Hiệu Ứng Particles Khi Zombie Chết:**

Thêm vào `WaveManager.cs`:

```csharp
[Header("Effects")]
public GameObject deathParticles; // Particle effect

private void OnZombieDeath()
{
    // ... existing code ...
    
    // Spawn particles
    if (deathParticles != null && currentZombie != null)
    {
        Instantiate(deathParticles, currentZombie.transform.position, Quaternion.identity);
    }
}
```

### **Thêm Sound Effects:**

```csharp
[Header("Audio")]
public AudioClip zombieDeathSound;
public AudioClip starSound;
public AudioClip waveCompleteSound;
private AudioSource audioSource;

void Start()
{
    audioSource = GetComponent<AudioSource>();
}

private void AddStar()
{
    // ... existing code ...
    
    if (audioSource && starSound)
    {
        audioSource.PlayOneShot(starSound);
    }
}
```

---

## 🏆 BƯỚC 7: ADVANCED FEATURES

### **1. Difficulty Scaling:**

```csharp
// Trong WaveManager
[Header("Difficulty")]
public float healthMultiplier = 1.2f; // Mỗi wave +20% máu
public float damageMultiplier = 1.1f; // Mỗi wave +10% damage

void StartNextWave()
{
    // ... spawn zombie ...
    
    NPC zombie = currentZombie.GetComponent<NPC>();
    if (zombie != null)
    {
        zombie.health = Mathf.RoundToInt(zombie.health * Mathf.Pow(healthMultiplier, currentWaveIndex));
        zombie.damage = Mathf.RoundToInt(zombie.damage * Mathf.Pow(damageMultiplier, currentWaveIndex));
    }
}
```

### **2. Wave Countdown Timer:**

```csharp
[Header("Timer")]
public float waveDelay = 3f;
public TMPro.TextMeshProUGUI countdownText;

IEnumerator StartNextWaveWithCountdown()
{
    for (int i = (int)waveDelay; i > 0; i--)
    {
        countdownText.text = $"Next wave in {i}...";
        yield return new WaitForSeconds(1f);
    }
    countdownText.text = "";
    StartNextWave();
}
```

### **3. Distance Display:**

```csharp
// Trong NavigationArrow
public TMPro.TextMeshProUGUI distanceText;

void Update()
{
    // ... existing code ...
    
    if (distanceText != null && currentTarget != null)
    {
        float dist = Vector3.Distance(player.position, currentTarget.position);
        distanceText.text = $"{dist:F1}m";
    }
}
```

---

## 📊 HIERARCHY STRUCTURE

```
Scene
├─ Player
├─ Canvas
│  ├─ StarContainer (Panel)
│  │  └─ (Stars spawn here)
│  ├─ WaveText (TextMeshPro)
│  └─ DistanceText (TextMeshPro)
├─ WaveManager (Empty + Script)
├─ NavigationArrow (Empty + Script)
│  └─ ArrowModel (3D Object)
└─ ZombieSpawnPoints
   ├─ SpawnPoint_1
   ├─ SpawnPoint_2
   ├─ SpawnPoint_3
   ├─ SpawnPoint_4
   └─ SpawnPoint_5
```

---

## 🎯 CHECKLIST HOÀN THIỆN

**Setup:**
- [ ] WaveManager created với 5 waves
- [ ] 5 Spawn points positioned
- [ ] NavigationArrow created
- [ ] StarContainer UI setup
- [ ] WaveText UI created
- [ ] Star prefab created

**Testing:**
- [ ] Wave 1 spawn OK
- [ ] Arrow points to zombie
- [ ] Zombie death triggers next wave
- [ ] Stars appear correctly (1→5)
- [ ] Final wave shows "Complete!"

**Polish:**
- [ ] Arrow color/animation
- [ ] Death particles
- [ ] Sound effects
- [ ] UI animations

---

## 🆘 TROUBLESHOOTING

### **Zombie không spawn:**
```
Check:
- WaveManager → Waves → Zombie Prefab assigned?
- WaveManager → Waves → Spawn Position assigned?
- Console có errors?
```

### **Arrow không chỉ đúng:**
```
Check:
- NavigationArrow → Player assigned?
- WaveManager.instance tồn tại?
- Zombie có spawn không?
```

### **Sao không xuất hiện:**
```
Check:
- WaveManager → Star Prefab assigned?
- WaveManager → Star Container assigned?
- StarContainer có Horizontal Layout Group?
```

### **Wave không tự động tiếp:**
```
Check:
- NPC.cs có onDeath event?
- WaveManager → OnZombieDeath được gọi?
- Console log "Zombie defeated!"?
```

---

## 🎉 KẾT QUẢ

**Game flow:**
```
1. Start game → Zombie 1 spawns
2. Arrow chỉ đến Zombie 1
3. Player follow arrow
4. Defeat Zombie 1 → ⭐ (1 star)
5. Wait 2s → Zombie 2 spawns
6. Arrow chỉ đến Zombie 2
7. Defeat Zombie 2 → ⭐⭐ (2 stars)
8. ... lặp lại ...
9. Defeat Zombie 5 → ⭐⭐⭐⭐⭐ (5 stars)
10. "Complete! ⭐5"
```

---

**🎮 Làm theo từng bước và test nhé! Nếu có lỗi thì báo cho tôi!**
