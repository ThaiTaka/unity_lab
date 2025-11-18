# 🎬 HƯỚNG DẪN SETUP VIDEO VICTORY

> **Quan trọng:** Video PHẢI hiển thị trên **Screen Space - Overlay Canvas** để luôn xuất hiện trước mắt người chơi!

---

## 📋 CÁC BƯỚC SETUP

### Bước 1: Tạo Victory Canvas
```
1. Hierarchy → Right-click → UI → Canvas
2. Rename: "VictoryCanvas"
3. Inspector:
   ✅ Render Mode: Screen Space - Overlay
   ✅ Sort Order: 9999 (hiển thị trên tất cả UI khác)
   ✅ Pixel Perfect: Checked
```

### Bước 2: Thêm Canvas Group
```
1. Select VictoryCanvas
2. Inspector → Add Component → Canvas Group
3. Settings:
   - Alpha: 0 (để fade in/out)
   - Interactable: Checked
   - Block Raycasts: Checked
```

### Bước 3: Tạo Video Display (RawImage)
```
1. Right-click VictoryCanvas → UI → Raw Image
2. Rename: "VideoDisplay"
3. Rect Transform:
   ✅ Anchor Presets: Stretch/Stretch (full screen)
   ✅ Left: 0, Right: 0, Top: 0, Bottom: 0
   - Width: auto (full screen)
   - Height: auto (full screen)
4. Raw Image:
   - Color: White (255, 255, 255, 255)
```

### Bước 4: Thêm Video Player
```
1. Select VideoDisplay GameObject
2. Inspector → Add Component → Video Player
3. Settings:
   ✅ Source: Video Clip (hoặc URL)
   ✅ Video Clip: Kéo video file vào đây
   ✅ Play On Awake: ❌ UNCHECKED (rất quan trọng!)
   ✅ Loop: ❌ UNCHECKED
   ✅ Render Mode: Render Texture
   ✅ Target Texture: Để trống (script sẽ tạo tự động)
   - Audio Output Mode: Direct
```

### Bước 5: Ẩn Canvas Ban Đầu
```
Select VictoryCanvas → Inspector:
- Canvas Group → Alpha: 0
- GameObject Active: ✅ CHECKED (để script truy cập được)
```

### Bước 6: Setup VictoryManager
```
1. Hierarchy → Create Empty
2. Rename: "VictoryManager"
3. Add Component → VictoryManager (script)
4. Inspector:
   
   Victory Video:
   - Victory Video Player: Kéo VideoDisplay (có Video Player component) vào
   - Video Display: Kéo VideoDisplay (RawImage) vào
   - Video Canvas Group: Kéo VictoryCanvas (Canvas Group) vào
   - Video Canvas: Kéo VictoryCanvas (Canvas) vào ⬅️ QUAN TRỌNG!
   
   Settings:
   - Menu Scene Name: "Menu"
```

---

## 🎯 KIỂM TRA

### ✅ Checklist Setup Đúng:
- [ ] VictoryCanvas có Render Mode = Screen Space - Overlay
- [ ] VictoryCanvas có Sort Order = 9999
- [ ] VictoryCanvas có Canvas Group component
- [ ] VideoDisplay là RawImage full screen (stretch/stretch)
- [ ] Video Player có Play On Awake = ❌ OFF
- [ ] Video Player có Render Mode = Render Texture
- [ ] VictoryManager có tất cả references được gắn đầy đủ
- [ ] Canvas Group Alpha = 0 ban đầu

### 🧪 Test:
1. Play game
2. Giết 6 zombie
3. Video PHẢI xuất hiện full screen trước mắt
4. Sau video → hiện "Thanks For Playing"
5. Sau đó → credits scroll
6. Bấm ESC → về menu

---

## 🐛 NẾU VIDEO VẪN ẨN:

### Kiểm tra Console logs:
```
✅ Video Canvas set to Screen Space Overlay with sort order 9999
✅ Created RenderTexture for video: 1920x1080
🎉 VICTORY! Bắt đầu sequence...
📺 Video Canvas active: True
📺 Video Canvas position: ...
▶️ Victory video đang phát!
```

### Nếu không có logs trên:
1. Check VictoryManager references trong Inspector
2. Check StarCollectionSystem có gọi `TriggerVictory()` không
3. Check video file có trong project không

### Nếu có logs nhưng không thấy video:
1. Canvas phải là Screen Space Overlay
2. Canvas Sort Order phải cao (9999)
3. Video file phải hợp lệ (MP4, WebM)
4. RawImage phải full screen

---

## 🎉 HOÀN TẤT!

Video giờ sẽ:
- ✅ Hiển thị full screen trước mặt người chơi
- ✅ Overlay lên tất cả UI khác (sort order 9999)
- ✅ Không tự phát khi vào game
- ✅ Chỉ phát khi giết đủ 6 zombie
- ✅ Pause game khi phát video
- ✅ Unlock cursor để xem thoải mái
