# 🔧 HƯỚNG DẪN KHẮC PHỤC LỖI

## 🎮 SCENE FLOW HOÀN CHỈNH

```
Menu Scene
  ↓
Intro Cutscene (GameIntroDialogue)
  ↓
Loading Scene (lần 1) → Load "Game"
  ↓
Game Scene (chơi game, giết zombie)
  ↓ (Giết 6 zombie)
Loading Scene (lần 2 - REUSE) → Load "VictoryVideoScene"
  ↓
VictoryVideoScene (xem video)
  ↓
BossIntroScene (cutscene boss)
  ↓
BossArena (đánh boss)
```

**📝 Lưu ý quan trọng:**
- Scene "Loading" được **REUSE 2 lần** trong game flow
- Lần 1: `targetSceneName = "Game"` (set trong Inspector)
- Lần 2: `nextSceneToLoad = "VictoryVideoScene"` (set từ code khi giết 6 zombie)

---

## ✅ ĐÃ SỬA: BossAntiT1.cs

### Vấn đề:
```
Boss đang tìm VictoryManager (đã xóa)
→ Lỗi: VictoryManager could not be found
```

### Đã sửa:
```csharp
// CŨ (LỖI):
VictoryManager victoryManager = FindObjectOfType<VictoryManager>();
if (victoryManager != null)
{
    victoryManager.TriggerVictory();
}

// MỚI (ĐÚNG):
LoadingScreen.LoadScene("VictoryVideoScene");
```

**Giờ khi boss chết → Load Victory Video Scene qua LoadingScreen** ✅

---

## ✅ KIỂM TRA: StarCollectionSystem

### Vấn đề báo cáo:
"Tôi lỡ xóa StarCollectionSystem"

### Kết quả kiểm tra:
```
✅ File StarCollectionSystem.cs VẪN CÒN!
   Đường dẫn: Assets/Survival 3D/Scripts/UI/StarCollectionSystem.cs
   
✅ Script hoạt động bình thường
✅ Không có lỗi compile
```

---

## 🔧 NẾU BẠN XÓA STARCOLLECTIONSYSTEM TRONG UNITY

### Triệu chứng:
- Game Scene không có GameObject "StarCollectionSystem"
- UI không hiển thị "Stars 0/6"
- Giết zombie không tăng sao

### Cách khắc phục:

#### Bước 1: Tạo lại GameObject

```
1. Mở Game Scene
2. Hierarchy → Right-click → Create Empty
3. Rename: "StarCollectionSystem"
```

#### Bước 2: Add Component

```
Select StarCollectionSystem GameObject
Inspector → Add Component → StarCollectionSystem (script)
```

#### Bước 3: Gắn References

```
Inspector → StarCollectionSystem component:

Star Settings:
- Max Stars: 6
- Current Stars: 0 (tự động)

UI References:
- Star Count Text: [Kéo Text hiển thị Stars 0/6 vào]
- Star Icon Container: None (optional)
- Star Icon Prefab: None (optional)
- Victory Panel: None (không dùng nữa)

Star Visual:
- Star Prefab: None (optional)
- Star Drop Height: 2

Animation Settings:
- Star Animation Duration: 0.5
- Scale Animation Curve: Default

Audio:
- Star Collect Sound: [Kéo audio clip vào]
- Victory Sound: [Kéo audio clip vào]

Scene Transition:
- Victory Video Scene Name: "VictoryVideoScene"
- Delay Before Transition: 2
```

**⚠️ QUAN TRỌNG:**
Khi giết đủ 6 zombie, StarCollectionSystem sẽ:
1. Gọi `LoadingScreen.LoadScene("VictoryVideoScene")`
2. Chuyển sang scene "Loading" (lần 2)
3. Scene "Loading" sẽ đọc `nextSceneToLoad = "VictoryVideoScene"`
4. Load VictoryVideoScene

#### Bước 4: Setup Loading Scene

```
1. Mở scene "Loading" trong Unity
2. Kiểm tra GameObject có component LoadingScreen.cs
3. Trong Inspector → LoadingScreen component:

Settings:
- Target Scene Name: "Game" ← MẶC ĐỊNH load Game
  (khi từ Intro → Loading → Game)
  
- Min Loading Time: 2.0
- Tip Change Interval: 3.0
```

**Giải thích:**
- `targetSceneName = "Game"` là giá trị mặc định (Intro → Game)
- Khi code gọi `LoadingScreen.LoadScene("VictoryVideoScene")`:
  - Scene Loading được load lại
  - `nextSceneToLoad` được set = "VictoryVideoScene"
  - Loading scene sẽ ưu tiên `nextSceneToLoad` thay vì `targetSceneName`

#### Bước 5: Gắn vào WaveManager

Select WaveManager GameObject
Inspector → Wave Manager:

Trong mỗi Wave:
- Zombie Prefab phải có script NPC.cs
- NPC.cs phải có Event "On Death"
  → Add StarCollectionSystem.AddStar(zombiePosition)
```

**Hoặc code trong NPC.cs:**
```csharp
void Die()
{
    // Thêm sao khi chết
    if (StarCollectionSystem.instance != null)
    {
        StarCollectionSystem.instance.AddStar(transform.position);
    }
    
    // ... rest of code
}
```

---

## 🧪 TEST SAU KHI SỬA

### Test 1: Boss chết
```
1. Vào Game Scene
2. Giết boss (hoặc test)
3. Boss chết → Console hiện:
   "💀 Boss Anti T1 đã chết!"
   "🎉 BOSS DEFEATED! Loading Victory Video..."
4. Chuyển sang LoadingScreen
5. Sau đó chuyển VictoryVideoScene
```

### Test 2: Star Collection
```
1. Vào Game Scene
2. Giết zombie
3. UI hiển thị Stars tăng: 1/6, 2/6, ...
4. Đạt 6/6 → Console hiện:
   "🎉 ĐỦ 6 SAO! Chuẩn bị chuyển..."
5. Chuyển sang LoadingScreen → VictoryVideoScene
```

---

## ✅ CHECKLIST CUỐI CÙNG

- [ ] BossAntiT1.cs không còn lỗi compile
- [ ] StarCollectionSystem GameObject có trong Game Scene
- [ ] StarCollectionSystem có component script
- [ ] UI Text "Stars 0/6" được gắn vào Star Count Text
- [ ] Victory Video Scene Name = "VictoryVideoScene"
- [ ] Giết zombie → Stars tăng
- [ ] Đạt 6 sao → Chuyển scene
- [ ] Boss chết → Chuyển Victory Video

---

## 🎉 HOÀN TẤT!

Tất cả lỗi đã được sửa! Game giờ sẽ:
- ✅ Boss chết → Load Victory Video
- ✅ 6 zombie chết → Load Victory Video
- ✅ Không còn lỗi VictoryManager
- ✅ StarCollectionSystem hoạt động bình thường
