# 🎬 SỬA LỖI VIDEO KHÔNG HIỆN - CHỈ NGHE TIẾNG

> **Vấn đề:** Giết 6 zombie, nghe tiếng video nhưng không thấy hình ảnh
> 
> **Nguyên nhân:** RawImage không được gắn đúng hoặc Canvas không active

---

## ✅ CÁC BƯỚC KIỂM TRA & SỬA

### Bước 1: Kiểm tra VictoryCanvas trong Hierarchy

```
Hierarchy → VictoryCanvas (phải TỒN TẠI)
├─ VideoDisplay (GameObject với RawImage component)
```

**Kiểm tra:**
1. Select VictoryCanvas
2. Inspector:
   - ✅ Canvas component → Render Mode: **Screen Space - Overlay**
   - ✅ Canvas component → Sort Order: **9999**
   - ✅ Canvas Group component có sẵn
   - ✅ GameObject ACTIVE (checkbox ở đầu Inspector)

### Bước 2: Kiểm tra VideoDisplay (RawImage)

```
Select: VictoryCanvas → VideoDisplay
```

**Inspector:**
1. ✅ GameObject ACTIVE (checkbox checked)
2. ✅ RawImage component có sẵn
3. ✅ Rect Transform:
   - Anchor Presets: **Stretch/Stretch** (full screen)
   - Left: 0, Right: 0, Top: 0, Bottom: 0
4. ✅ Raw Image:
   - Color: White (255, 255, 255, 255)
   - Texture: Để trống (script sẽ gán)

### Bước 3: Kiểm tra VideoPlayer Component

```
Select: VictoryCanvas → VideoDisplay
Hoặc: Hierarchy → VideoPlayer_Victory
```

**Inspector - Video Player Component:**
1. ✅ Source: **Video Clip**
2. ✅ Video Clip: **KÉO VIDEO FILE VÀO ĐÂY** ⬅️ QUAN TRỌNG!
3. ✅ Play On Awake: **❌ UNCHECKED**
4. ✅ Loop: **❌ UNCHECKED**
5. ✅ Render Mode: **Render Texture**
6. ✅ Target Texture: Để trống (script tạo tự động)
7. ✅ Audio Output Mode: Direct
8. ✅ Skip On Drop: Checked

### Bước 4: Gắn References vào VictoryManager

```
Hierarchy → VictoryManager (GameObject)
```

**Inspector - Victory Manager Script:**

**Victory Video:** (GẮN ĐẦY ĐỦ!)
- **Victory Video Player:** Kéo GameObject có Video Player component vào
- **Video Display:** Kéo VideoDisplay (RawImage) vào
- **Video Canvas Group:** Kéo VictoryCanvas (Canvas Group component) vào
- **Video Canvas:** Kéo VictoryCanvas (Canvas component) vào

**Credits:** (Nếu có)
- Credits Canvas: Kéo CreditsCanvas vào
- Credits Text: Kéo CreditsText vào
- Credits Scroll Speed: 50

**Thank You Screen:** (Nếu có)
- Thank You Canvas: Kéo ThankYouCanvas vào
- Thank You Text: Kéo ThankYouText vào

**Settings:**
- Menu Scene Name: "Menu"

---

## 🎯 CẤU TRÚC HIERARCHY ĐÚNG

```
VictoryCanvas (Canvas, Canvas Group)
├─ VideoDisplay (RawImage)
│  └─ Video Player (Component)
│
VictoryManager (Empty GameObject)
└─ Victory Manager (Script)
```

---

## 🧪 KIỂM TRA TRONG CONSOLE

Khi giết đủ 6 zombie, Console phải hiện:

```
✅ VictoryManager initialized - Video KHÔNG phát tự động
🎉 VICTORY! Bắt đầu sequence...
📺 Video Canvas active: True
📺 Video Canvas enabled: True
📺 Video Display active: True
📺 Video Display enabled: True
📺 Video Display texture: True
📺 Video Display size: (1920, 1080)
✅ Video prepared!
▶️ Victory video đang phát! isPlaying: True
```

**Nếu thấy FALSE ở bất kỳ dòng nào:**
→ Reference trong VictoryManager chưa gắn đúng!

---

## ❌ LỖI THƯỜNG GẶP

### 1. Video Clip không được gắn
**Triệu chứng:** Console báo "Video clip is null"
**Sửa:** Video Player → Video Clip → Kéo file video .mp4 vào

### 2. VideoDisplay không gắn vào VictoryManager
**Triệu chứng:** Console báo "VideoDisplay (RawImage) is NULL!"
**Sửa:** VictoryManager → Video Display → Kéo VideoDisplay (RawImage) vào

### 3. Canvas không phải Screen Space Overlay
**Triệu chứng:** Video hiện ở góc hoặc ngoài màn hình
**Sửa:** VictoryCanvas → Canvas → Render Mode: Screen Space - Overlay

### 4. RawImage không full screen
**Triệu chứng:** Video hiện nhỏ ở góc
**Sửa:** VideoDisplay → Rect Transform → Anchor: Stretch/Stretch, Left/Right/Top/Bottom = 0

### 5. Canvas Group Alpha = 1 từ đầu
**Triệu chứng:** Video hiện luôn khi vào game
**Sửa:** VictoryCanvas → Canvas Group → Alpha: 0

---

## 🎬 TẠO VIDEO DISPLAY MỚI (NẾU CHƯA CÓ)

Nếu chưa có VideoDisplay trong Hierarchy:

```
1. Right-click VictoryCanvas → UI → Raw Image
2. Rename: "VideoDisplay"
3. Rect Transform:
   - Anchor: Stretch/Stretch
   - Left: 0, Right: 0, Top: 0, Bottom: 0
4. Raw Image:
   - Color: White
5. Add Component → Video Player
6. Video Player:
   - Source: Video Clip
   - Video Clip: Kéo video file vào
   - Play On Awake: OFF
   - Render Mode: Render Texture
```

---

## ✅ CHECKLIST CUỐI CÙNG

Trước khi test, đảm bảo:

- [ ] VictoryCanvas có Canvas (Screen Space Overlay, Sort Order 9999)
- [ ] VictoryCanvas có Canvas Group (Alpha = 0)
- [ ] VideoDisplay có RawImage (full screen stretch)
- [ ] VideoDisplay có Video Player
- [ ] Video Player có Video Clip được gắn
- [ ] Video Player: Play On Awake = OFF
- [ ] Video Player: Render Mode = Render Texture
- [ ] VictoryManager có TẤT CẢ 4 references:
  - Victory Video Player ✅
  - Video Display ✅
  - Video Canvas Group ✅
  - Video Canvas ✅

---

## 🎉 SAU KHI SỬA

Video sẽ:
- ✅ Hiển thị FULL SCREEN che hết màn hình
- ✅ Có cả hình ảnh và âm thanh
- ✅ Canvas overlay trên tất cả UI khác
- ✅ Tự động tạo RenderTexture 1920x1080
- ✅ Debug logs chi tiết để track issues

**Cheat status giờ ở GIỮA PHÍA TRÊN:**
```
        🎮 CHEATS ACTIVE
        🛡️ God Mode
        🍖 Infinite Hunger
```
→ KHÔNG CHE thanh máu/đồ ăn nữa!
