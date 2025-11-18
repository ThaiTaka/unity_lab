# 🎮 HƯỚNG DẪN SETUP 2 LOADING SCENES

## 📋 SCENE FLOW MỚI

```
Menu Scene
  ↓
Intro Cutscene
  ↓
Loading Scene (riêng cho Intro→Game)
  ↓
Game Scene
  ↓ (Giết 6 zombie)
Loading1 Scene (riêng cho Game→Victory)
  ↓
VictoryVideoScene
  ↓
BossIntroScene
  ↓
BossArena
```

---

## ✅ ĐÃ TẠO

### 1. Loading1Screen.cs
- **Đường dẫn:** `Assets/Survival 3D/Scripts/UI/Loading1Screen.cs`
- **Mục đích:** Loading screen riêng cho Game → Victory Video
- **Có sẵn tips về boss và chiến thắng**

### 2. Đã cập nhật StarCollectionSystem.cs
- Giết 6 zombie → gọi `Loading1Screen.LoadScene("VictoryVideoScene")`

### 3. Đã cập nhật BossAntiT1.cs
- Boss chết → gọi `Loading1Screen.LoadScene("VictoryVideoScene")`

---

## 🔧 SETUP TRONG UNITY

### BƯỚC 1: Tạo Scene "Loading1"

```
1. File → New Scene
2. Tạo UI giống scene "Loading" cũ:
   - Canvas (Screen Space Overlay)
   - Background Image (đen hoặc màu tối)
   - Loading Bar (Slider)
   - Loading Text (TextMeshProUGUI)
   - Tip Text (TextMeshProUGUI)
   - CanvasGroup (cho fade)

3. Lưu scene: "Loading1"
```

### BƯỚC 2: Gắn Script Loading1Screen

```
1. Trong scene "Loading1"
2. Tạo GameObject empty: "Loading1Manager"
3. Add Component → Loading1Screen.cs
4. Gắn references:

   [UI References]
   - Loading Bar: Slider
   - Loading Text: Text "Loading..."
   - Tip Text: Text tips
   - Canvas Group: CanvasGroup của Canvas

   [Settings]
   - Target Scene Name: "VictoryVideoScene"
   - Min Loading Time: 2.0
   - Tip Change Interval: 3.0

   [Loading Tips] - Đã có sẵn trong code:
   ✓ "🎉 Bạn đã hoàn thành nhiệm vụ thu thập sao!"
   ✓ "👑 Chuẩn bị chiến đấu với Boss mạnh nhất!"
   ✓ "⚔️ Boss sẽ xuất hiện sau cutscene..."
   ✓ "💪 Hãy chuẩn bị vũ khí và vật phẩm tốt nhất!"
   ✓ "🔥 Trận chiến khó khăn sắp bắt đầu!"
```

### BƯỚC 3: Setup Scene "Loading" (cũ)

```
1. Mở scene "Loading"
2. Kiểm tra LoadingScreen.cs component:

   [Settings]
   - Target Scene Name: "Game" ← QUAN TRỌNG!
   
   [Loading Tips] - Giữ nguyên tips về gameplay
```

### BƯỚC 4: Build Settings

```
File → Build Settings → Add Open Scenes:

Thứ tự:
0. Menu
1. IntroCutscene (hoặc tên scene intro của bạn)
2. Loading          ← Loading cho Intro→Game
3. Game
4. Loading1         ← Loading cho Game→Victory
5. VictoryVideoScene
6. BossIntroScene
7. BossArena
```

---

## 🎯 KIỂM TRA

### Test 1: Intro → Game
```
1. Chạy từ Menu
2. Xem Intro Cutscene
3. Cutscene kết thúc → Chuyển "Loading" scene
4. Loading bar 100% → Chuyển "Game" scene
```

### Test 2: Game → Victory
```
1. Trong Game scene
2. Giết 6 zombie
3. Console hiện: "🔄 Loading Victory Video Scene via Loading1Screen"
4. Chuyển sang "Loading1" scene
5. Loading bar 100% → Chuyển "VictoryVideoScene"
```

### Test 3: Boss → Victory
```
1. Đánh boss đến chết
2. Console hiện: "🎉 BOSS DEFEATED! Loading Victory Video..."
3. Chuyển sang "Loading1" scene
4. Loading bar 100% → Chuyển "VictoryVideoScene"
```

---

## 📊 SO SÁNH 2 LOADING SCENES

| Tính năng | Loading | Loading1 |
|-----------|---------|----------|
| **Khi nào dùng** | Intro → Game | Game → Victory |
| **Script** | LoadingScreen.cs | Loading1Screen.cs |
| **Target Scene** | "Game" | "VictoryVideoScene" |
| **Tips** | Gameplay tips | Boss & Victory tips |
| **Gọi từ đâu** | Scene Intro tự động | Code (6 zombie/boss chết) |

---

## ⚠️ LƯU Ý

1. **Scene "Loading" phải có `targetSceneName = "Game"`**
   - Vì nó load từ Intro → Game

2. **Scene "Loading1" phải có `targetSceneName = "VictoryVideoScene"`**
   - Vì nó load từ Game → Victory Video

3. **Đảm bảo tên scene chính xác:**
   - "Loading" (không phải "LoadingScreen")
   - "Loading1" (không phải "Loading1Screen")

4. **Cả 2 scene đều phải có trong Build Settings**

---

## ✅ CHECKLIST HOÀN THÀNH

- [ ] Tạo scene "Loading1" trong Unity
- [ ] Tạo UI cho Loading1 (Canvas, Slider, Texts, CanvasGroup)
- [ ] Gắn Loading1Screen.cs vào GameObject
- [ ] Set Target Scene Name = "VictoryVideoScene"
- [ ] Gắn tất cả UI references
- [ ] Kiểm tra scene "Loading" có Target Scene Name = "Game"
- [ ] Add cả 2 loading scenes vào Build Settings
- [ ] Test flow: Intro → Loading → Game
- [ ] Test flow: Game (6 zombie) → Loading1 → Victory
- [ ] Test flow: Boss chết → Loading1 → Victory

---

## 🎉 HOÀN TẤT!

Giờ game có 2 loading screens độc lập:
- ✅ **Loading:** Chuyên dụng cho Intro → Game
- ✅ **Loading1:** Chuyên dụng cho Game → Victory Video
- ✅ Không bao giờ bị nhầm lẫn hay reuse sai!
