# 🎮 Hướng Dẫn Setup Boss Anti T1 Fight System

## 📋 Tổng Quan
Hệ thống boss fight với Anti T1 bao gồm:
- ✅ Video trigger khi đạt 6 sao
- ✅ Dialogue Anti với typing effect
- ✅ Boss spawn với thanh máu fill animation
- ✅ 3 phases: Attack (skill AOE) → Vulnerable → Repeat
- ✅ Victory video + Credits

---

## 📊 Flow Toàn Bộ Game

```
Player đánh 6 zombie
   ↓ (6 sao)
Video #1 phát
   ↓
Dialogue Anti:
  - "Tất cả chỉ là quảng bá thôi..."
  - "Tê liệt thì mãi là ..... TÊ..... LIỆTTTTTTTTTT" (chữ to + đỏ)
   ↓
Zombie gầm TO
   ↓
Boss Anti T1 spawn
  - Text "Anti T1" trên đầu
  - Thanh máu fill từ 0 → 100% (3 giây)
   ↓
Phase 1: Attack (15 giây)
  - Boss bất tử
  - Tung meteor/skill liên tục
  - Vùng cảnh báo đỏ trước khi damage
   ↓
Boss gầm và dừng lại
   ↓
Phase 2: Vulnerable
  - Player đánh 1 hit → Mất 1/3 HP
  - Boss gầm lại
   ↓
Repeat Phase 1 & 2 (3 lần total)
   ↓
Boss die (HP = 0)
   ↓
Video #2 phát (victory)
   ↓
"Thanks For Playing"
   ↓
Credits scroll
   ↓
Press ESC → Quay về Menu
```

---

## 🛠️ Bước 1: Setup Video Triggers

### 1.1. Tạo VideoTriggerManager GameObject
```
Hierarchy → Create Empty
Đặt tên: "VideoTriggerManager"
```

### 1.2. Assign VideoTriggerManager Script
```
Inspector → Add Component → VideoTriggerManager

Video Player:
├── Video Player: (tạo GameObject + VideoPlayer component)
├── Video Display: (Raw Image trên Canvas)
└── Video Canvas Group: (Canvas chứa video)

Anti Dialogue After Video:
├── Anti Dialogue Canvas: (Canvas chứa dialogue Anti)
├── Anti Dialogue Text: (TextMeshPro text)
└── Black Screen: (Image đen toàn màn hình)

Audio:
├── Typing Audio Source: (AudioSource component)
├── Typing Sound: (AudioClip typing sound)
└── Zombie Roar Sound: (AudioClip tiếng gầm TO)

Settings:
├── Typing Speed: 0.05
├── Loud Typing Speed: 0.08 (chữ to gõ chậm hơn)
├── Normal Font Size: 30
└── Loud Font Size: 60 (cho "TÊ... LIỆT")
```

---

## 🎬 Bước 2: Setup Video Player

### 2.1. Tạo Video Player GameObject
```
Hierarchy → Create Empty
Đặt tên: "VideoPlayer1"

Add Component → Video Player
├── Source: Video Clip
├── Video Clip: Kéo video file vào (.mp4)
├── Play On Awake: ❌ UNCHECKED
├── Loop: ❌ UNCHECKED
├── Render Mode: Render Texture
└── Target Texture: (tạo RenderTexture mới)
```

### 2.2. Tạo Video Canvas
```
Right-click Hierarchy → UI → Canvas
Đặt tên: "VideoCanvas"

Inspector:
├── Render Mode: Screen Space - Overlay
├── Sort Order: 100 (hiển thị trên cùng)
└── Add Component: Canvas Group
```

### 2.3. Tạo Video Display (Raw Image)
```
Right-click VideoCanvas → UI → Raw Image
Đặt tên: "VideoDisplay"

RectTransform:
├── Anchor: Stretch All
└── Texture: (RenderTexture từ VideoPlayer)

Color: White (255, 255, 255, 255)
```

---

## 👹 Bước 3: Setup Boss Anti T1

### 3.1. Tạo Boss Prefab
```
Hierarchy → 3D Object → Cube (hoặc model của bạn)
Đặt tên: "BossAntiT1"

Scale: (5, 5, 5) - Lớn hơn zombie thường
Tag: "Enemy"
Layer: Enemy
```

### 3.2. Add Boss Components
```
Add Component → BossAntiT1

Boss Stats:
└── Max Health Segments: 3 (3 đoạn máu)

UI:
├── Health Bar: (Slider trên đầu boss)
├── Boss Name Text: (TextMeshPro "Anti T1")
└── Boss Canvas: (Canvas con trên đầu boss)

Audio:
├── Audio Source: (AudioSource component)
├── Roar Sound: (AudioClip tiếng gầm)
└── Skill Sound: (AudioClip cast skill)

Phase Settings:
├── Attack Phase Duration: 15 (giây)
├── Vulnerable Phase Wait Time: 3 (giây)
└── Health Bar Fill Duration: 3 (giây)

Skills:
├── Meteor Prefab: (Prefab thiên thạch)
├── Warning Zone Prefab: (Prefab vùng đỏ)
└── Skill Cast Interval: 2 (giây)
```

### 3.3. Tạo UI Trên Đầu Boss
```
Right-click BossAntiT1 → UI → Canvas
Đặt tên: "BossCanvas"

Canvas:
├── Render Mode: World Space
├── Width: 300, Height: 100
├── Scale: 0.01 (để nhỏ lại)
└── Position: (0, 6, 0) (trên đầu boss)

Tạo UI elements:
├── BossNameText (TextMeshPro):
│   ├── Text: "Anti T1"
│   ├── Font Size: 30
│   ├── Alignment: Center
│   └── Color: Red
│
└── HealthBar (Slider):
    ├── Min Value: 0
    ├── Max Value: 1
    ├── Value: 0 (bắt đầu)
    ├── Width: 250, Height: 20
    └── Fill Color: Red
```

---

## 💥 Bước 4: Tạo Meteor & Warning Zone

### 4.1. Meteor Prefab
```
Create → 3D Object → Sphere
Đặt tên: "Meteor"

Scale: (2, 2, 2)
Material: Màu cam/đỏ sáng

Add Component:
├── Rigidbody:
│   ├── Use Gravity: ❌ OFF
│   └── Mass: 10
│
├── Sphere Collider
│
└── Meteor Script:
    ├── Fall Speed: 20
    ├── Damage: 20
    ├── Explosion Radius: 3
    ├── Explosion Effect: (Particle System)
    └── Explosion Sound: (AudioClip boom)

Drag vào Assets để tạo Prefab
```

### 4.2. Warning Zone Prefab
```
Create → 3D Object → Cylinder
Đặt tên: "WarningZone"

Scale: (6, 0.1, 6) - Mỏng và rộng
Rotation: (90, 0, 0) - Nằm ngang

Material:
├── Shader: Transparent/Diffuse
├── Color: Red (255, 0, 0, 128) - Semi-transparent
└── Rendering Mode: Transparent

Add Component → WarningZone Script

Drag vào Assets để tạo Prefab
```

---

## 🏆 Bước 5: Setup Victory Manager

### 5.1. Tạo VictoryManager GameObject
```
Hierarchy → Create Empty
Đặt tên: "VictoryManager"

Add Component → VictoryManager
```

### 5.2. Setup Victory Video
```
Victory Video:
├── Victory Video Player: (VideoPlayer cho video #2)
├── Video Display: (Raw Image)
└── Video Canvas Group: (Canvas chứa video)
```

### 5.3. Setup Credits Canvas
```
Right-click Hierarchy → UI → Canvas
Đặt tên: "CreditsCanvas"

Add:
├── Background (Image - Black)
└── CreditsText (TextMeshPro):
    ├── Alignment: Center + Top
    ├── Font Size: 24
    ├── Color: White
    ├── Width: 1200
    └── Overflow: Overflow (để scroll)
```

### 5.4. Setup Thank You Canvas
```
Right-click Hierarchy → UI → Canvas
Đặt tên: "ThankYouCanvas"

Add:
├── Background (Image - Black)
└── ThankYouText (TextMeshPro):
    ├── Text: "THANKS FOR PLAYING"
    ├── Font Size: 60
    ├── Alignment: Center + Middle
    └── Color: White
```

---

## 🎯 Bước 6: Setup Boss Spawner

### 6.1. Tạo BossSpawner GameObject
```
Hierarchy → Create Empty
Đặt tên: "BossSpawner"

Position: Vị trí giữa map (nơi boss sẽ spawn)

Add Component → BossAntiT1Spawner
├── Boss Anti T1 Prefab: (Kéo prefab boss vào)
└── Spawn Point: (Transform của BossSpawner)
```

---

## 🔗 Bước 7: Kết Nối Tất Cả

### 7.1. StarCollectionSystem
- Đảm bảo game có script `StarCollectionSystem` để đếm sao
- VideoTriggerManager sẽ check `starSystem.GetStarCount() >= 6`

### 7.2. Player Attack Boss
Trong script `PlayerController` hoặc `EquipTool`, thêm:
```csharp
void OnAttackHit(Collider hit)
{
    if (hit.CompareTag("Enemy"))
    {
        BossAntiT1 boss = hit.GetComponent<BossAntiT1>();
        if (boss != null)
        {
            boss.TakeDamage();
        }
    }
}
```

---

## ✅ Bước 8: Test Flow

### 8.1. Test Từng Phần
1. **Test Video Trigger:**
   - Đánh 6 zombie → Video phát → Dialogue Anti
   
2. **Test Boss Spawn:**
   - Sau dialogue → Boss spawn → Thanh máu fill
   
3. **Test Attack Phase:**
   - Boss tung meteor 15 giây
   - Vùng đỏ cảnh báo trước
   - Player né được
   
4. **Test Vulnerable Phase:**
   - Boss dừng lại
   - Player đánh → Mất 1/3 HP
   - Boss quay lại attack
   
5. **Test Victory:**
   - Boss chết → Video victory → Credits

---

## 🎨 Tùy Chỉnh

### Đổi Skill Boss
Thay vì meteor, thêm skill khác trong `BossAntiT1.CastRandomSkill()`:
- Laser beam
- Shockwave
- Lightning strike

### Đổi Số Đoạn Máu Boss
```csharp
Max Health Segments: 5 (thay vì 3)
→ Player phải đánh 5 lần
```

### Đổi Thời Gian Attack Phase
```csharp
Attack Phase Duration: 20 (thay vì 15 giây)
→ Boss tung skill lâu hơn
```

---

## 🐛 Troubleshooting

### ❌ Video không phát
- Kiểm tra VideoPlayer → Play On Awake = OFF
- Kiểm tra VideoClip đã assign
- Kiểm tra RenderTexture đã gán vào Raw Image

### ❌ Boss không spawn
- Kiểm tra StarCollectionSystem.GetStarCount() hoạt động
- Kiểm tra BossSpawner đã gán prefab

### ❌ Player không damage được boss
- Kiểm tra boss đang ở Vulnerable Phase
- Kiểm tra isInvulnerable = false

### ❌ Meteor không gây damage
- Kiểm tra Layer của player = "Player"
- Kiểm tra Meteor.OnCollisionEnter hoạt động

---

## 📊 Kết Quả Cuối Cùng

Một boss fight hoàn chỉnh với:
- ✅ Cinematic video triggers
- ✅ Dramatic dialogue với typing effect
- ✅ 3-phase boss fight với mechanics phức tạp
- ✅ AOE skills với warning zones
- ✅ Victory sequence đầy đủ
- ✅ Credits cuối game

Chúc bạn thành công! 🎉
