# 🎮 HƯỚNG DẪN SETUP HOÀN CHỈNH - SCENE FLOW MỚI

> **MỤC TIÊU:** Setup flow chuyên nghiệp: Game → Loading → Video → Boss Intro → Boss Fight
> 
> **THỜI GIAN:** ~20 phút
> 
> **ĐỘ KHÓ:** ⭐⭐ (Trung bình)

---

## 📊 FLOW CUỐI CÙNG

```
🎮 GAME SCENE
    ↓ Giết 6 zombie → ⭐ 6/6 stars
📦 LOADING SCREEN (đã có sẵn)
    ↓ Progress bar + tips
🎬 VICTORY VIDEO SCENE (tạo mới)
    ↓ Video chiến thắng (skip bằng Space)
😈 BOSS INTRO SCENE (tạo mới)
    ↓ Dialogue Anti T1 giận dữ
⚔️ BOSS ARENA SCENE (có thể dùng Game scene hoặc tạo mới)
    ↓ Đấu boss
```

---

## ✅ PHẦN 1: DỌN DẸP GAME SCENE

### Bước 1: Xóa UI không dùng trong Unity

Vào **Game Scene** (scene chính đang chơi):

```
Hierarchy → Tìm và XÓA các GameObject sau:

❌ VictoryCanvas (hoặc bất kỳ canvas nào có video)
❌ VideoTriggerManager
❌ VictoryManager
❌ VictoryPanel
❌ VideoPlayer_Victory (nếu có)
❌ CreditsCanvas (nếu có)
❌ ThankYouCanvas (nếu có)

✅ GIỮ LẠI:
- StarCollectionSystem ✅
- WaveManager ✅
- Player ✅
- Canvas (UI chính: HP, Hunger, Stars) ✅
```

### Bước 2: Kiểm tra StarCollectionSystem

```
Select: StarCollectionSystem GameObject
Inspector:

Scene Transition:
- Victory Video Scene Name: "VictoryVideoScene"
- Delay Before Transition: 2

✅ KHÔNG cần gắn gì khác!
```

### Bước 3: Save Scene

```
Ctrl + S hoặc File → Save
```

---

## ✅ PHẦN 2: TẠO VICTORY VIDEO SCENE

### Bước 1: Tạo Scene mới

```
File → New Scene → Basic (Built-in)
Ctrl + S → Save As: "VictoryVideoScene"
Lưu vào: Assets/Scenes/VictoryVideoScene.unity
```

### Bước 2: Xóa hết (giữ lại Main Camera)

```
Hierarchy → Xóa tất cả NGOẠI TRỪ Main Camera
Main Camera:
- Position: (0, 0, -10)
- Clear Flags: Solid Color
- Background: Black
```

### Bước 3: Tạo Canvas

```
Hierarchy → Right-click → UI → Canvas
Rename: "VideoCanvas"

Inspector:
- Render Mode: Screen Space - Overlay
- Pixel Perfect: Checked
```

### Bước 4: Tạo RawImage (hiển thị video)

```
Right-click VideoCanvas → UI → Raw Image
Rename: "VideoDisplay"

Rect Transform:
- Click Anchor Presets → Giữ Alt + Shift → Click góc dưới phải (Stretch/Stretch)
- Left: 0, Right: 0, Top: 0, Bottom: 0
  
Raw Image:
- Color: White (255, 255, 255, 255)
- Texture: Để trống
```

### Bước 5: Add Video Player vào VideoDisplay

```
Select: VideoDisplay
Inspector → Add Component → Video Player

Settings:
- Source: Video Clip
- Video Clip: ⬅️ KÉO VIDEO FILE (.mp4) VÀO ĐÂY
- Play On Awake: ❌ UNCHECKED
- Wait For First Frame: ✅ Checked
- Loop: ❌ UNCHECKED
- Playback Speed: 1
- Render Mode: Render Texture
- Target Texture: Để trống (script tạo tự động)
- Audio Output Mode: Direct
- Skip On Drop: ✅ Checked
```

### Bước 6: Tạo VictoryVideoSceneManager

```
Hierarchy → Create Empty
Rename: "VictoryVideoSceneManager"
Add Component → VictoryVideoSceneManager

Inspector:
Video Setup:
- Video Player: Kéo VideoDisplay (có Video Player component) vào
- Video Display: Kéo VideoDisplay (RawImage) vào

Scene Transition:
- Next Scene Name: "BossIntroScene"
- Delay After Video: 1

Skip Settings:
- Allow Skip: ✅ Checked
- Skip Key: Space
```

### Bước 7: Add vào Build Settings

```
File → Build Settings
Click "Add Open Scenes"
```

### Bước 8: Save Scene

```
Ctrl + S
```

---

## ✅ PHẦN 3: TẠO BOSS INTRO SCENE

### Bước 1: Tạo Scene mới

```
File → New Scene → Basic (Built-in)
Ctrl + S → Save As: "BossIntroScene"
Lưu vào: Assets/Scenes/BossIntroScene.unity
```

### Bước 2: Setup Camera

```
Main Camera:
- Position: (0, 5, -20)
- Rotation: (15, 0, 0)
- Clear Flags: Skybox hoặc Solid Color
- Background: Dark Gray hoặc Black
```

### Bước 3: Tạo Ground (sàn đấu)

```
Hierarchy → 3D Object → Plane
Rename: "Ground"
Position: (0, 0, 0)
Scale: (5, 1, 5)

⚠️ Tạo Material màu tối để trông nghiêm túc hơn
```

### Bước 4: Tạo Boss Model

```
Hierarchy → Kéo Boss Prefab vào scene
(Hoặc Create → 3D Object → Cube tạm nếu chưa có)

Rename: "BossModel"
Position: (0, 1, 0)
Scale: (3, 3, 3) - Boss to hơn player

⚠️ QUAN TRỌNG:
Inspector → UNCHECK "Active" (ẩn ban đầu)
```

### Bước 5: Tạo Boss Spawn Point

```
Hierarchy → Create Empty
Rename: "BossSpawnPoint"
Position: (0, 1, 0) - Vị trí boss sẽ xuất hiện
```

### Bước 6: Tạo Canvas Dialogue

```
Hierarchy → UI → Canvas
Rename: "DialogueCanvas"

Inspector:
- Render Mode: Screen Space - Overlay
- Sort Order: 10
```

### Bước 7: Tạo Dialogue Panel

```
Right-click DialogueCanvas → UI → Panel
Rename: "DialoguePanel"

Rect Transform:
- Anchor: Bottom (stretch horizontal)
- Height: 220
- Pos Y: 110 (cách mép dưới)

Image:
- Color: Black (0, 0, 0, 220) - semi-transparent
```

### Bước 8: Tạo Speaker Name Text

```
Right-click DialoguePanel → UI → Text - TextMeshPro
Rename: "SpeakerNameText"

Rect Transform:
- Anchor: Top-Left
- Pos X: 30, Pos Y: -20
- Width: 300, Height: 40

TextMeshPro:
- Text: "ANTI T1"
- Font Size: 32
- Color: Yellow (255, 255, 0)
- Font Style: Bold
- Alignment: Left + Middle
```

### Bước 9: Tạo Dialogue Text

```
Right-click DialoguePanel → UI → Text - TextMeshPro
Rename: "DialogueText"

Rect Transform:
- Anchor: Stretch/Stretch
- Left: 30, Right: 30, Top: 70, Bottom: 30

TextMeshPro:
- Text: "Dialogue will appear here..."
- Font Size: 26
- Color: White
- Alignment: Top-Left
- Wrapping: Enabled
- Auto Size: OFF
```

### Bước 10: Tạo Speaker Portrait (Optional)

```
Right-click DialoguePanel → UI → Image
Rename: "SpeakerPortrait"

Rect Transform:
- Anchor: Left-Center
- Pos X: 100, Pos Y: 0
- Width: 150, Height: 150

Image:
- Color: White
- Preserve Aspect: ✅ Checked

⚠️ Có thể bỏ qua nếu không có ảnh boss
```

### Bước 11: Tạo BossIntroSceneManager

```
Hierarchy → Create Empty
Rename: "BossIntroSceneManager"
Add Component → BossIntroSceneManager

Inspector:

Dialogue UI:
- Dialogue Panel: Kéo DialoguePanel vào
- Dialogue Text: Kéo DialogueText vào
- Speaker Name Text: Kéo SpeakerNameText vào
- Speaker Portrait: Kéo SpeakerPortrait vào (hoặc để None)

Boss:
- Boss Model: Kéo BossModel vào
- Boss Spawn Point: Kéo BossSpawnPoint vào
- Boss Roar Sound: Để None (thêm sau nếu có)

Camera:
- Camera Animator: Để None (thêm sau nếu muốn cinematic)

Scene Transition:
- Boss Arena Scene Name: "Game" (hoặc tên scene boss fight)
- Transition Delay: 2

Dialogue Content:
- Size: 4 (hoặc nhiều hơn)
```

### Bước 12: Setup Dialogue Lines

```
Trong BossIntroSceneManager → Dialogue Lines:

Element 0:
- Speaker Name: "ANTI T1"
- Text: "Các ngươi dám giết hết đàn zombie của ta?!"
- Portrait: None (hoặc kéo ảnh boss vào)
- Display Duration: 3
- Spawn Boss After: ❌ UNCHECKED

Element 1:
- Speaker Name: "ANTI T1"
- Text: "Ta đã nuôi chúng rất cực nhọc, giờ chúng đều chết hết!"
- Display Duration: 3
- Spawn Boss After: ❌ UNCHECKED

Element 2:
- Speaker Name: "ANTI T1"
- Text: "Các ngươi sẽ phải trả giá đắt cho hành động này!"
- Display Duration: 3
- Spawn Boss After: ❌ UNCHECKED

Element 3:
- Speaker Name: "ANTI T1"
- Text: "CHUẨN BỊ CHIẾN ĐẤU!!!"
- Display Duration: 2
- Spawn Boss After: ✅ CHECKED ⬅️ Boss xuất hiện sau dòng này
```

### Bước 13: Add vào Build Settings

```
File → Build Settings
Click "Add Open Scenes"
```

### Bước 14: Thêm Lighting (Optional)

```
Hierarchy → Light → Directional Light
Rotate để ánh sáng chiếu xuống boss

Hoặc:
Window → Rendering → Lighting
Environment → Skybox Material: Chọn skybox tối
```

### Bước 15: Save Scene

```
Ctrl + S
```

---

## ✅ PHẦN 4: KIỂM TRA BUILD SETTINGS

```
File → Build Settings

Scenes In Build (theo thứ tự):
[0] Menu
[1] LoadingScreen ✅
[2] GameIntroDialogue (hoặc Game)
[3] VictoryVideoScene ✅ MỚI
[4] BossIntroScene ✅ MỚI

⚠️ Nếu thiếu scene nào, mở scene đó và "Add Open Scenes"
```

---

## ✅ PHẦN 5: TEST TOÀN BỘ FLOW

### Test 1: Trong Unity Editor

```
1. Mở Game Scene
2. Play
3. Giết 6 zombie (hoặc dùng cheat nếu có)
4. ⭐ Đạt 6/6 stars
5. Chờ 2 giây → Chuyển LoadingScreen
6. Loading bar 0-100% → Chuyển VictoryVideoScene
7. Video phát full screen
8. (Có thể bấm Space để skip)
9. Video hết → Chuyển BossIntroScene
10. Dialogue xuất hiện từng dòng
11. Sau dialogue cuối → Boss spawn (active)
12. Chờ 2 giây → Chuyển Game scene (hoặc Boss Arena)
```

### Test 2: Check Console Logs

```
Console phải hiển thị:

🎉 ĐỦ 6 SAO! Chuẩn bị chuyển sang Victory Video Scene!
✅ Đã dừng spawn zombie!
⏳ Waiting 2s before transition...
🔄 Loading Victory Video Scene via LoadingScreen
🔄 Transitioning to VictoryVideoScene via LoadingScreen
📦 Loading scene from code: VictoryVideoScene
🎬 Victory Video Scene started!
⏳ Preparing video...
✅ Video prepared! Starting playback...
✅ Victory video finished!
🔄 Transitioning to BossIntroScene in 1s...
😈 Boss Intro Scene started!
💬 ANTI T1: Các ngươi dám giết hết đàn zombie của ta?!
...
👹 BOSS SPAWNING!
✅ Dialogue finished! Transitioning to Boss Arena...
```

### Nếu không thấy logs:
→ Có lỗi! Check Console có error màu đỏ không

---

## ⚠️ TROUBLESHOOTING

### ❌ Scene không chuyển sau 6 sao
**Kiểm tra:**
- StarCollectionSystem → Victory Video Scene Name = "VictoryVideoScene"
- Build Settings có VictoryVideoScene chưa
- Console có error không

### ❌ Video không phát (chỉ màn hình đen)
**Kiểm tra:**
- Video Player → Video Clip đã gắn file .mp4 chưa
- Video file có trong project không (Assets/Videos/)
- Console có warning "Can't find video" không

### ❌ Dialogue không hiện
**Kiểm tra:**
- BossIntroSceneManager → Dialogue Lines → Size > 0
- Dialogue Panel → Active ✅ Checked
- DialogueText, SpeakerNameText đã gắn vào script chưa

### ❌ Boss không spawn
**Kiểm tra:**
- BossModel có trong scene không
- BossModel đã gắn vào BossIntroSceneManager chưa
- Dialogue Line cuối cùng → Spawn Boss After ✅ Checked

### ❌ Loading mãi không xong
**Kiểm tra:**
- Build Settings → Scene name đúng chưa (viết hoa/thường)
- LoadingScreen → Target Scene Name trong Inspector
- Console có error "Scene not found" không

---

## 🎯 CHECKLIST CUỐI CÙNG

Trước khi test, đảm bảo:

### Game Scene:
- [ ] Đã xóa VictoryCanvas, VictoryManager, VideoTriggerManager
- [ ] StarCollectionSystem có Victory Video Scene Name = "VictoryVideoScene"
- [ ] WaveManager vẫn hoạt động bình thường

### LoadingScreen Scene:
- [ ] Có LoadingScreen GameObject với script
- [ ] UI: Loading Bar, Loading Text, Tip Text, Fade Image
- [ ] Script LoadingScreen.cs đã được update (có method LoadScene static)

### VictoryVideoScene:
- [ ] Có VideoCanvas → VideoDisplay (RawImage)
- [ ] VideoDisplay có Video Player component
- [ ] Video Player → Video Clip đã gắn file video
- [ ] VictoryVideoSceneManager có đầy đủ references
- [ ] Next Scene Name = "BossIntroScene"

### BossIntroScene:
- [ ] Có DialogueCanvas → DialoguePanel
- [ ] SpeakerNameText và DialogueText đã tạo
- [ ] BossModel có trong scene (ẩn ban đầu)
- [ ] BossSpawnPoint đánh dấu vị trí spawn
- [ ] BossIntroSceneManager có đầy đủ references
- [ ] Dialogue Lines array đã setup (ít nhất 3-4 dòng)
- [ ] Dòng cuối có Spawn Boss After ✅ Checked
- [ ] Boss Arena Scene Name = "Game" (hoặc scene boss fight)

### Build Settings:
- [ ] Tất cả scenes đã add: LoadingScreen, VictoryVideoScene, BossIntroScene
- [ ] Thứ tự đúng (không bắt buộc nhưng nên sắp xếp logic)

---

## 🎉 HOÀN TẤT!

Giờ game có flow CHUYÊN NGHIỆP:

```
🎮 Game (6 zombie)
   ↓
📦 Loading (progress bar)
   ↓
🎬 Video (victory)
   ↓
😈 Cutscene (Anti T1 giận)
   ↓
⚔️ Boss Fight!
```

**Chúc bạn thành công! 🔥**

---

## 📝 GHI CHÚ THÊM

### Nếu muốn video khác:
1. Thay video clip trong Video Player
2. Không cần sửa code gì

### Nếu muốn thêm dialogue:
1. BossIntroSceneManager → Dialogue Lines → Increase Size
2. Điền text mới
3. Adjust Display Duration

### Nếu muốn boss spawn khác:
1. Thay BossModel bằng prefab boss khác
2. Adjust BossSpawnPoint position
3. Done!

### Nếu muốn skip cutscene:
- Victory Video: Bấm **Space**
- Boss Intro: Bấm **Space** hoặc **Enter**

---

**Made with ❤️ by GitHub Copilot**
