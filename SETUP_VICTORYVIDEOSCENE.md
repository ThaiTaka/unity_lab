# 🎬 HƯỚNG DẪN SETUP VICTORYVIDEOSCENE

## 📋 MỤC ĐÍCH
Scene này chỉ để phát video chiến thắng sau khi giết 6 zombie.
Sau khi video kết thúc → Tự động chuyển sang BossIntroScene.

---

## 🔧 CÁCH TẠO (NẾU CHƯA CÓ)

### BƯỚC 1: Tạo Scene Mới
```
1. File → New Scene
2. Scene Template: Basic (Built-in)
3. File → Save As: "VictoryVideoScene"
4. Lưu vào folder: Assets/Survival 3D/Scenes/ (hoặc Assets/)
```

---

### BƯỚC 2: Setup UI Canvas

```
1. Hierarchy → Right-click → UI → Canvas
   - Rename: "VideoCanvas"
   
2. Inspector → Canvas component:
   - Render Mode: Screen Space - Overlay
   - Pixel Perfect: ✅ (checked)
   
3. Add CanvasScaler:
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080
   - Match: 0.5 (width/height)
```

---

### BƯỚC 3: Tạo Background (Optional)

```
Hierarchy → Right-click VideoCanvas → UI → Image
- Rename: "Background"
- Color: Black (R:0, G:0, B:0, A:255)
- Anchor: Stretch cả 4 góc
- Left/Right/Top/Bottom: 0
```

---

### BƯỚC 4: Tạo Video Display

```
Hierarchy → Right-click VideoCanvas → UI → Raw Image
- Rename: "VideoDisplay"

Inspector → RawImage:
- Anchor: Stretch cả 4 góc
- Left: 0, Right: 0, Top: 0, Bottom: 0
- Color: White (để video hiển thị đúng màu)
```

---

### BƯỚC 5: Tạo Video Player

```
Hierarchy → Create Empty
- Rename: "VideoManager"
- Add Component → Video Player

Inspector → Video Player:
- Source: Video Clip
- Video Clip: [Kéo video clip vào đây]
- Play On Awake: ✅ (checked)
- Loop: ❌ (unchecked) - Vì chỉ phát 1 lần
- Render Mode: Render Texture (code sẽ tự tạo)
- Audio Output Mode: Direct hoặc Audio Source
```

---

### BƯỚC 6: Gắn Script VictoryVideoSceneManager

```
1. Select GameObject "VideoManager"
2. Inspector → Add Component → VictoryVideoSceneManager

3. Gắn references:

[Video Setup]
- Video Player: Kéo VideoPlayer component vào
- Video Display: Kéo RawImage "VideoDisplay" vào

[Scene Transition]
- Next Scene Name: "BossIntroScene"
- Delay After Video: 1

[Skip Settings]
- Allow Skip: true
- Skip Key: Space
```

---

## 🎥 CHUẨN BỊ VIDEO CLIP

### Option 1: Nếu bạn có video file
```
1. Import video vào Unity:
   - Kéo file .mp4 vào Assets/Videos/
   
2. Select video file trong Project
   Inspector → Import Settings:
   - Transcode: ✅
   - Codec: H.264
   - Apply
   
3. Kéo video vào Video Player → Video Clip
```

### Option 2: Nếu chưa có video
```
Tạm thời có thể:
- Để trống Video Clip
- Hoặc dùng video placeholder bất kỳ
- Scene vẫn hoạt động, chỉ không hiển thị gì

Sau này khi có video → Import và gắn vào
```

---

## 📐 HIERARCHY CUỐI CÙNG

```
VictoryVideoScene
├── VideoCanvas
│   ├── Background (Image - Black)
│   └── VideoDisplay (RawImage)
└── VideoManager
    ├── Video Player (component)
    └── VictoryVideoSceneManager (script)
```

---

## ✅ KIỂM TRA

### Test 1: Chạy Scene Trực Tiếp
```
1. Double-click VictoryVideoScene để mở
2. Nhấn Play
3. Video phải tự động phát
4. Bấm Space → Video stop
5. Sau 1 giây → Chuyển BossIntroScene
```

### Test 2: Check Console
```
Khi scene load:
✅ "🎬 Victory Video Scene started!"
✅ "🎬 Starting video playback..."

Khi video kết thúc:
✅ "✅ Victory video finished!"
✅ "🔄 Transitioning to BossIntroScene in 1s..."

Khi bấm Space:
✅ "⏩ Video skipped!"
✅ "🔄 Transitioning to BossIntroScene in 1s..."
```

### Test 3: Từ Game Scene
```
1. Chạy Game scene
2. Giết 6 zombie
3. Console: "🔄 Transitioning to VictoryVideoScene via loading 1 screen"
4. Loading 1 scene xuất hiện
5. Loading bar 100%
6. VictoryVideoScene load
7. Video tự động phát
8. Video kết thúc → BossIntroScene
```

---

## ⚠️ LƯU Ý

### 1. Video Clip có thể để trống
Nếu chưa có video, scene vẫn hoạt động:
- VideoPlayer sẽ báo warning
- Nhưng sau `delayAfterVideo` giây vẫn chuyển scene bình thường

### 2. Next Scene Name phải chính xác
```
- Phải là: "BossIntroScene"
- Không phải: "Boss Intro Scene" (có space)
- Phải khớp với tên trong Build Settings
```

### 3. Skip Key tùy chỉnh
```
Có thể đổi sang phím khác:
- KeyCode.Escape
- KeyCode.Return (Enter)
- KeyCode.Mouse0 (Click chuột)
```

### 4. Video không hiển thị?
```
Nguyên nhân:
- RawImage không được gắn vào Video Display
- Canvas không phải Screen Space Overlay
- VideoPlayer không có Video Clip

Fix:
- Check tất cả references trong Inspector
- Đảm bảo Canvas sort order = cao (999)
```

---

## 🎉 HOÀN TẤT!

Sau khi setup xong:
✅ VictoryVideoScene tự động phát video
✅ Có thể skip bằng Space
✅ Tự động chuyển sang BossIntroScene
✅ Flow hoàn chỉnh: Game → Loading1 → Video → BossIntro

---

## 🔄 FLOW TỔNG THỂ

```
Game Scene (giết 6 zombie)
    ↓
Loading 1 Scene (loading bar)
    ↓
VictoryVideoScene (video phát) ← BẠN ĐANG Ở ĐÂY
    ↓
BossIntroScene (cutscene dialogue)
    ↓
BossArena (đánh boss)
```
