# 🎬 HƯỚNG DẪN SETUP HỆ THỐNG SCENE CHUYỂN TIẾP MỚI

> **Ý tưởng mới:** Thay vì video hiện trong game, giờ làm theo flow chuyên nghiệp:
> 
> **Game → Loading → Victory Video → Boss Intro → Boss Arena**

---

## 📊 FLOW MỚI

```
┌─────────────────┐
│   GAME SCENE    │  (Giết 6 zombie, đạt 6 sao)
└────────┬────────┘
         │ ⭐ 6/6 stars collected!
         ↓
┌─────────────────┐
│ LOADING SCENE   │  (Progress bar, tips, fade in/out)
└────────┬────────┘
         │ Loading complete
         ↓
┌─────────────────┐
│ VICTORY VIDEO   │  (Video chiến thắng full screen)
│     SCENE       │  (Có thể skip bằng Space)
└────────┬────────┘
         │ Video ends
         ↓
┌─────────────────┐
│ BOSS INTRO      │  (Dialogue: Anti T1 giận dữ)
│     SCENE       │  (Boss xuất hiện, cutscene)
└────────┬────────┘
         │ Cutscene ends
         ↓
┌─────────────────┐
│ BOSS ARENA      │  (Đấu với Boss Anti T1)
│     SCENE       │  (Sân đấu riêng, epic fight)
└─────────────────┘
```

---

## 🎯 DANH SÁCH SCENES CẦN TẠO

### 1. **LoadingScene** - Màn hình loading
### 2. **VictoryVideoScene** - Phát video chiến thắng
### 3. **BossIntroScene** - Cutscene Anti T1 bực tức
### 4. **BossArenaScene** - Sân đấu boss (có thể dùng scene hiện tại hoặc tạo mới)

---

## 📝 BƯỚC 1: TẠO LOADING SCENE

### Tạo Scene mới:
```
File → New Scene → Basic (Built-in)
Save As: "LoadingScene"
Lưu vào: Assets/Scenes/LoadingScene
```

### Setup UI:

1. **Canvas (Screen Space Overlay)**
   ```
   Hierarchy → Right-click → UI → Canvas
   Rename: "LoadingCanvas"
   Canvas → Render Mode: Screen Space - Overlay
   ```

2. **Background (Full Screen Black)**
   ```
   Right-click LoadingCanvas → UI → Image
   Rename: "Background"
   Color: Black (0, 0, 0, 255)
   Rect Transform: Stretch/Stretch (full screen)
   ```

3. **Progress Bar**
   ```
   Right-click LoadingCanvas → UI → Slider
   Rename: "ProgressBar"
   Anchor: Bottom-Center
   Width: 600, Height: 30
   Pos Y: 150
   
   Settings:
   - Min Value: 0
   - Max Value: 1
   - Whole Numbers: OFF
   - Fill Rect: Kéo Fill Area → Fill vào
   - Fill Color: Yellow hoặc Green
   ```

4. **Loading Text**
   ```
   Right-click LoadingCanvas → UI → Text (TextMeshPro)
   Rename: "LoadingText"
   Anchor: Center
   Pos Y: 200
   Text: "Loading... 0%"
   Font Size: 36
   Color: White
   Alignment: Center
   ```

5. **Tip Text**
   ```
   Right-click LoadingCanvas → UI → Text (TextMeshPro)
   Rename: "TipText"
   Anchor: Bottom-Center
   Pos Y: 100
   Text: "Loading tips will appear here..."
   Font Size: 20
   Color: Gray
   Alignment: Center
   ```

6. **Fade Image (For fade in/out)**
   ```
   Right-click LoadingCanvas → UI → Image
   Rename: "FadeImage"
   Color: Black (0, 0, 0, 255)
   Rect Transform: Stretch/Stretch (full screen)
   
   ⚠️ Kéo lên đầu Hierarchy để render trước
   ```

7. **LoadingSceneManager**
   ```
   Hierarchy → Create Empty
   Rename: "LoadingSceneManager"
   Add Component → LoadingSceneManager (script)
   
   Inspector:
   - Progress Bar: Kéo ProgressBar vào
   - Loading Text: Kéo LoadingText vào
   - Tip Text: Kéo TipText vào
   - Fade Image: Kéo FadeImage vào
   - Minimum Load Time: 2
   - Fade Duration: 0.5
   ```

### ✅ Thêm vào Build Settings:
```
File → Build Settings
Add Open Scenes
```

---

## 📝 BƯỚC 2: TẠO VICTORY VIDEO SCENE

### Tạo Scene mới:
```
File → New Scene → Basic (Built-in)
Save As: "VictoryVideoScene"
```

### Setup:

1. **Canvas**
   ```
   Hierarchy → UI → Canvas
   Rename: "VictoryVideoCanvas"
   Render Mode: Screen Space - Overlay
   Sort Order: 0
   ```

2. **VideoDisplay (RawImage)**
   ```
   Right-click VictoryVideoCanvas → UI → Raw Image
   Rename: "VideoDisplay"
   Rect Transform: Stretch/Stretch (full screen)
   Color: White
   ```

3. **Video Player**
   ```
   Select VideoDisplay
   Add Component → Video Player
   
   Settings:
   - Source: Video Clip
   - Video Clip: [KÉO VIDEO FILE VÀO]
   - Play On Awake: ❌ OFF
   - Loop: ❌ OFF
   - Render Mode: Render Texture
   - Target Texture: Để trống
   ```

4. **VictoryVideoSceneManager**
   ```
   Hierarchy → Create Empty
   Rename: "VictoryVideoSceneManager"
   Add Component → VictoryVideoSceneManager (script)
   
   Inspector:
   - Video Player: Kéo VideoDisplay (có Video Player component) vào
   - Video Display: Kéo VideoDisplay (RawImage) vào
   - Next Scene Name: "BossIntroScene"
   - Delay After Video: 1
   - Allow Skip: ✅ Checked
   - Skip Key: Space
   ```

### ✅ Thêm vào Build Settings

---

## 📝 BƯỚC 3: TẠO BOSS INTRO SCENE

### Tạo Scene mới:
```
File → New Scene → Basic (Built-in)
Save As: "BossIntroScene"
```

### Setup:

1. **Canvas cho Dialogue**
   ```
   Hierarchy → UI → Canvas
   Rename: "DialogueCanvas"
   Render Mode: Screen Space - Overlay
   ```

2. **Dialogue Panel**
   ```
   Right-click DialogueCanvas → UI → Panel
   Rename: "DialoguePanel"
   Anchor: Bottom (stretch horizontally)
   Height: 200
   Color: Black (0, 0, 0, 200) - semi-transparent
   ```

3. **Speaker Name Text**
   ```
   Right-click DialoguePanel → UI → Text (TextMeshPro)
   Rename: "SpeakerNameText"
   Anchor: Top-Left
   Pos X: 20, Pos Y: -20
   Text: "Anti T1"
   Font Size: 28
   Color: Yellow
   Font Style: Bold
   ```

4. **Dialogue Text**
   ```
   Right-click DialoguePanel → UI → Text (TextMeshPro)
   Rename: "DialogueText"
   Anchor: Stretch (fill panel)
   Padding: Left 20, Right 20, Top 60, Bottom 20
   Text: "Dialogue will appear here..."
   Font Size: 22
   Color: White
   Alignment: Top-Left
   ```

5. **Speaker Portrait (Optional)**
   ```
   Right-click DialoguePanel → UI → Image
   Rename: "SpeakerPortrait"
   Anchor: Left-Center
   Width: 150, Height: 150
   Pos X: 100
   ```

6. **Boss Model (trong Scene)**
   ```
   Kéo Boss Prefab vào scene
   Rename: "BossModel"
   Position: Đặt ở vị trí muốn boss xuất hiện
   ⚠️ UNCHECK Active (ẩn ban đầu)
   ```

7. **Boss Spawn Point**
   ```
   Hierarchy → Create Empty
   Rename: "BossSpawnPoint"
   Position: Vị trí boss sẽ spawn
   ```

8. **Main Camera**
   ```
   Add Component → Animator (nếu muốn camera cinematic)
   Hoặc để cố định nhìn vào boss
   ```

9. **BossIntroSceneManager**
   ```
   Hierarchy → Create Empty
   Rename: "BossIntroSceneManager"
   Add Component → BossIntroSceneManager (script)
   
   Inspector:
   - Dialogue Panel: Kéo DialoguePanel vào
   - Dialogue Text: Kéo DialogueText vào
   - Speaker Name Text: Kéo SpeakerNameText vào
   - Speaker Portrait: Kéo SpeakerPortrait vào
   - Boss Model: Kéo BossModel vào
   - Boss Spawn Point: Kéo BossSpawnPoint vào
   - Boss Roar Sound: Kéo AudioSource có tiếng gầm
   - Camera Animator: Kéo Main Camera (Animator) vào
   - Boss Arena Scene Name: "BossArenaScene"
   - Transition Delay: 2
   ```

10. **Setup Dialogue Lines**
    ```
    In BossIntroSceneManager → Dialogue Lines:
    
    Size: 3
    
    Element 0:
    - Speaker Name: "Anti T1"
    - Text: "Các ngươi dám giết hết đàn zombie của ta?!"
    - Portrait: [Ảnh boss]
    - Display Duration: 3
    - Spawn Boss After: ❌
    
    Element 1:
    - Speaker Name: "Anti T1"
    - Text: "Ta sẽ cho các ngươi biết thế nào là sức mạnh thực sự!"
    - Display Duration: 3
    - Spawn Boss After: ❌
    
    Element 2:
    - Speaker Name: "Anti T1"
    - Text: "CHUẨN BỊ CHIẾN ĐẤU!!!"
    - Display Duration: 2
    - Spawn Boss After: ✅ Checked (Boss xuất hiện sau dòng này)
    ```

### ✅ Thêm vào Build Settings

---

## 📝 BƯỚC 4: BOSS ARENA SCENE

**Option 1:** Dùng scene hiện tại (Game scene)
**Option 2:** Tạo scene mới với arena riêng

### Nếu tạo mới:
```
File → New Scene
Save As: "BossArenaScene"

Setup:
- Terrain hoặc Arena
- Spawn point cho Player
- Boss spawn point
- BossAntiT1 GameObject với script BossAntiT1.cs
- Camera follow player
- UI (Health bar, etc.)
```

### ✅ Thêm vào Build Settings

---

## 📝 BƯỚC 5: CẬP NHẬT GAME SCENE

### Trong StarCollectionSystem:

```
Select StarCollectionSystem GameObject
Inspector:

Scene Transition:
- Victory Video Scene Name: "VictoryVideoScene"
- Delay Before Transition: 2
```

---

## 📝 BƯỚC 6: BUILD SETTINGS

```
File → Build Settings

Scenes In Build: (theo thứ tự)
[0] Menu
[1] GameIntroDialogue (hoặc Game scene chính)
[2] LoadingScene ⬅️ MỚI
[3] VictoryVideoScene ⬅️ MỚI
[4] BossIntroScene ⬅️ MỚI
[5] BossArenaScene ⬅️ MỚI (hoặc dùng scene hiện tại)
```

---

## 🎮 FLOW TEST

### 1. Play Game Scene
```
- Giết 6 zombie
- ⭐ 6/6 stars
- Console: "🎉 ĐỦ 6 SAO! Chuẩn bị chuyển sang Victory Video Scene!"
```

### 2. Loading Scene
```
- Progress bar 0% → 100%
- Loading tips hiển thị random
- Fade in/out smooth
- Auto chuyển sang Victory Video
```

### 3. Victory Video Scene
```
- Video phát full screen
- Có thể skip bằng Space
- Console: "🎬 Victory Video Scene started!"
- Video hết → Auto chuyển Boss Intro
```

### 4. Boss Intro Scene
```
- Dialogue xuất hiện từng dòng
- Boss spawn sau dialogue cuối
- Camera cinematic (nếu có)
- Console: "😈 Boss Intro Scene started!"
- Auto chuyển Boss Arena sau cutscene
```

### 5. Boss Arena Scene
```
- Player spawn
- Boss đã có sẵn
- Bắt đầu battle
```

---

## 🎯 LỢI ÍCH CỦA CÁCH LÀM MỚI

✅ **Chuyên nghiệp hơn** - Giống game AAA (God of War, Final Fantasy)
✅ **Tách biệt rõ ràng** - Video, cutscene, gameplay độc lập
✅ **Dễ maintain** - Sửa video/cutscene không ảnh hưởng game
✅ **Loading screen đẹp** - Progress bar, tips, fade effects
✅ **Có thể skip** - Player không bị force xem video/cutscene
✅ **Scalable** - Dễ thêm nhiều boss/cutscene sau này
✅ **Performance tốt** - Mỗi scene load riêng, không lag

---

## 🐛 TROUBLESHOOTING

### Scene không chuyển?
→ Check Build Settings, đảm bảo tất cả scenes đã được add

### Video không phát?
→ Check Video Player → Video Clip đã gắn chưa

### Dialogue không hiện?
→ Check BossIntroSceneManager → Dialogue Lines array đã setup chưa

### Loading mãi không xong?
→ Check Console có error không, hoặc scene name sai

---

## 🎉 HOÀN TẤT!

Giờ game có flow mượt mà và chuyên nghiệp như game thật! 🎮✨

**Flow cuối cùng:**
```
Game → Loading → Victory Video → Boss Intro → Boss Fight
```

Chúc bạn dev vui! 🔥
