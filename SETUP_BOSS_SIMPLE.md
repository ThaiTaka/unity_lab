# 🎮 HƯỚNG DẪN SETUP BOSS ANTI T1 - SIÊU ĐƠN GIẢN

> **Mục tiêu:** Tạo boss fight với Anti T1 khi đạt 6 sao
> 
> **Thời gian:** ~30 phút
> 
> **Độ khó:** ⭐⭐⭐ (Trung bình)

---

## 📖 MỤC LỤC NHANH

1. [Setup Intro Dialogue (Khi vào game)](#1-setup-intro-dialogue)
2. [Setup Video Khi Đủ 6 Sao](#2-setup-video-khi-đủ-6-sao)
3. [Setup Boss Anti T1](#3-setup-boss-anti-t1)
4. [Setup Meteor & Skill](#4-setup-meteor--skill)
5. [Setup Victory (Thắng boss)](#5-setup-victory)
6. [Test & Debug](#6-test--debug)

---

## 🎬 FLOW GAME ĐẦY ĐỦ

```
Vào Game
  ↓
Dialogue Intro (4 dòng)
  - Feaker: "WTF, Đây là ở đâu ?"
  - Anti Fan: "Hỏi làm cái *** gì ?"
  - Anti Fan: "Mày chỉ cần biết m thắng 6 đội hạng 2 kia"
  - Anti Fan: "Thì mày được về nhà, haha see yaaaaa!"
  ↓
Player có thể di chuyển
  ↓
Đánh zombie → Thu thập 6 sao
  ↓
Video #1 phát
  ↓
Dialogue Anti:
  - "Tất cả chỉ là quảng bá thôi..."
  - "Tê liệt thì mãi là TÊ... LIỆT!!!" (chữ to đỏ)
  ↓
Boss spawn → Thanh máu fill
  ↓
Boss tung skill 15 giây (meteor)
  ↓
Boss dừng lại → Player đánh
  ↓
Lặp lại 3 lần
  ↓
Boss chết → Video #2
  ↓
"Thanks For Playing" → Credits
```

---

## 1. SETUP INTRO DIALOGUE

### Bước 1: Tạo Canvas
```
1. Hierarchy → Right-click → UI → Canvas
2. Đổi tên: "IntroCanvas"
```

### Bước 2: Tạo Black Screen
```
1. Right-click IntroCanvas → UI → Image
2. Đổi tên: "BlackScreen"
3. Inspector:
   - Anchor: Stretch All (giữ Alt+Shift, click góc dưới phải)
   - Color: Black (0, 0, 0, 255)
```

### Bước 3: Tạo Dialogue Panel
```
1. Right-click IntroCanvas → UI → Panel
2. Đổi tên: "DialoguePanel"
3. Inspector:
   - Anchor: Bottom (1 nửa dưới)
   - Width: 1400, Height: 200
   - Pos Y: 150
   - Color: Black (0, 0, 0, 200) - semi-transparent
```

### Bước 4: Tạo Dialogue Text
```
1. Right-click DialoguePanel → UI → Text - TextMeshPro
2. Đổi tên: "DialogueText"
3. Inspector:
   - Width: 1300, Height: 150
   - Font Size: 30
   - Color: White
   - Alignment: Left + Middle
```

### Bước 5: Gắn Script
```
1. Create Empty GameObject: "GameIntroManager"
2. Add Component → GameIntroDialogue
3. Kéo vào:
   - Dialogue Panel → DialoguePanel
   - Dialogue Text → DialogueText
   - Black Screen → BlackScreen
4. Settings:
   - Typing Speed: 0.05
   - Delay Between Lines: 1.5
```

**✅ XONG PHẦN 1!** Test: Play game → Sẽ thấy 4 dòng dialogue → Player được phép di chuyển

---

## 2. SETUP VIDEO KHI ĐỦ 6 SAO

### Bước 1: Import Video
```
1. Copy file video .mp4 vào Assets/Videos/
2. Đổi tên: "video_intro.mp4"
```

### Bước 2: Tạo VideoPlayer GameObject
```
1. Hierarchy → Create Empty
2. Đổi tên: "VideoPlayer_Intro"
3. Add Component → Video Player
4. Inspector:
   - Source: Video Clip
   - Video Clip: Kéo video_intro.mp4 vào
   - Play On Awake: ❌ UNCHECKED
   - Loop: ❌ UNCHECKED
   - Render Mode: Render Texture
   - Target Texture: (Tạo mới - xem bước 3)
```

### Bước 3: Tạo RenderTexture
```
1. Assets → Right-click → Create → Render Texture
2. Đổi tên: "VideoRenderTexture"
3. Kéo vào VideoPlayer → Target Texture
```

### Bước 4: Tạo Video Canvas
```
1. Hierarchy → UI → Canvas
2. Đổi tên: "VideoCanvas"
3. Inspector:
   - Sort Order: 100 (hiển thị trên cùng)
   - Add Component: Canvas Group
```

### Bước 5: Tạo Video Display
```
1. Right-click VideoCanvas → UI → Raw Image
2. Đổi tên: "VideoDisplay"
3. Inspector:
   - Anchor: Stretch All
   - Texture: Kéo VideoRenderTexture vào
   - Color: White
```

### Bước 6: Tạo Anti Dialogue Canvas
```
1. Hierarchy → UI → Canvas
2. Đổi tên: "AntiDialogueCanvas"
3. Tạo:
   - BlackScreen (Image đen, stretch all)
   - DialogueText (TextMeshPro, font 30)
4. Ẩn canvas này ban đầu (Active: OFF)
```

### Bước 7: Gắn VideoTriggerManager
```
1. Create Empty: "VideoTriggerManager"
2. Add Component → VideoTriggerManager
3. Kéo vào:
   - Video Player: VideoPlayer_Intro
   - Video Display: VideoDisplay
   - Video Canvas Group: VideoCanvas
   - Anti Dialogue Canvas: AntiDialogueCanvas
   - Anti Dialogue Text: (Text trong AntiDialogueCanvas)
   - Black Screen: (BlackScreen trong AntiDialogueCanvas)
```

**✅ XONG PHẦN 2!** Test: Đánh 6 zombie → Video phát → Dialogue Anti

---

## 3. SETUP BOSS ANTI T1

### Bước 1: Tạo Boss Model
```
1. Hierarchy → 3D Object → Cube
2. Đổi tên: "BossAntiT1"
3. Scale: (5, 5, 5) - To gấp 2-3 lần zombie thường
4. Material: Màu đỏ hoặc đen
5. Tag: "Enemy"
```

### Bước 2: Add Boss Components
```
1. Add Component → Rigidbody
   - Mass: 100
   - Freeze Rotation: X, Y, Z = ✅
2. Add Component → Capsule Collider
   - Radius: 2.5
   - Height: 5
3. Add Component → Audio Source
4. Add Component → BossAntiT1
```

### Bước 3: Tạo UI Trên Đầu Boss
```
1. Right-click BossAntiT1 → UI → Canvas
2. Đổi tên: "BossCanvas"
3. Inspector:
   - Render Mode: World Space
   - Width: 300, Height: 100
   - Scale: 0.01, 0.01, 0.01
   - Position: Y = 6 (trên đầu boss)

4. Tạo con:
   a) BossNameText (TextMeshPro):
      - Text: "Anti T1"
      - Font Size: 40
      - Color: Red
      - Alignment: Center + Top
      
   b) HealthBar (Slider):
      - Pos Y: -20
      - Width: 250, Height: 20
      - Min: 0, Max: 1, Value: 0
      - Fill Color: Red
      - Interactable: ❌ OFF
```

### Bước 4: Gắn References Vào Script
```
Select BossAntiT1 → Inspector → BossAntiT1 Script:

Boss Stats:
- Max Health Segments: 3

UI:
- Health Bar: Kéo HealthBar Slider vào
- Boss Name Text: Kéo BossNameText vào
- Boss Canvas: Kéo BossCanvas vào

Phase Settings:
- Attack Phase Duration: 15
- Vulnerable Phase Wait Time: 3
- Health Bar Fill Duration: 3
- Skill Cast Interval: 2
```

### Bước 5: Tạo Prefab
```
1. Kéo BossAntiT1 từ Hierarchy vào Assets/Prefabs/
2. Xóa BossAntiT1 khỏi Hierarchy
```

### Bước 6: Tạo Boss Spawner
```
1. Hierarchy → Create Empty
2. Đổi tên: "BossSpawner"
3. Position: Giữa map (nơi boss sẽ xuất hiện)
4. Add Component → BossAntiT1Spawner
5. Inspector:
   - Boss Anti T1 Prefab: Kéo prefab boss vào
   - Spawn Point: Kéo BossSpawner (chính nó) vào
```

**✅ XONG PHẦN 3!** Boss sẽ spawn khi video kết thúc

---

## 4. SETUP METEOR & SKILL

### Bước 1: Tạo Warning Zone (Vùng Đỏ Cảnh Báo)
```
1. 3D Object → Cylinder
2. Đổi tên: "WarningZone"
3. Scale: (6, 0.1, 6) - Mỏng ngang
4. Rotation: (90, 0, 0) - Nằm sát đất

5. Material:
   - Create → Material → "WarningMaterial"
   - Shader: Legacy Shaders → Transparent → Diffuse
   - Color: Red (255, 0, 0, 128)

6. Add Component → WarningZone

7. Kéo vào Assets/Prefabs/ → Tạo prefab
8. Xóa khỏi scene
```

### Bước 2: Tạo Meteor
```
1. 3D Object → Sphere
2. Đổi tên: "Meteor"
3. Scale: (2, 2, 2)

4. Material:
   - Màu cam/đỏ sáng (255, 100, 0)
   - Emission: ✅ ON, Color: Orange

5. Add Component → Rigidbody
   - Use Gravity: ❌ OFF
   - Mass: 10

6. Add Component → Sphere Collider

7. Add Component → Meteor
   - Fall Speed: 20
   - Damage: 20
   - Explosion Radius: 3

8. Kéo vào Assets/Prefabs/ → Tạo prefab
9. Xóa khỏi scene
```

### Bước 3: Gán Prefabs Vào Boss
```
1. Assets/Prefabs/ → Double-click BossAntiT1 prefab
2. Inspector → BossAntiT1 Script:
   - Meteor Prefab: Kéo Meteor prefab vào
   - Warning Zone Prefab: Kéo WarningZone prefab vào
3. File → Save (Ctrl+S)
```

**✅ XONG PHẦN 4!** Boss sẽ tung meteor có cảnh báo

---

## 5. SETUP VICTORY

### Bước 1: Tạo Victory Video
```
1. Import video thắng: "video_victory.mp4"
2. Tạo VideoPlayer_Victory (giống bước 2)
3. Tạo RenderTexture mới: "VictoryRenderTexture"
```

### Bước 2: Tạo Victory Canvas
```
1. UI → Canvas → "VictoryCanvas"
2. Tạo con:
   - VideoDisplay (Raw Image) - Hiển thị video
   - Add Canvas Group
3. Active: ❌ OFF (ẩn ban đầu)
```

### Bước 3: Tạo Credits Canvas
```
1. UI → Canvas → "CreditsCanvas"
2. Tạo con:
   - Background (Image - Black)
   - CreditsText (TextMeshPro):
     - Font Size: 24
     - Alignment: Center + Top
     - Width: 1200, Height: 2000
     - Overflow: Overflow
3. Active: ❌ OFF
```

### Bước 4: Tạo Thank You Canvas
```
1. UI → Canvas → "ThankYouCanvas"
2. Tạo con:
   - Background (Image - Black)
   - ThankYouText (TextMeshPro):
     - Text: "THANKS FOR PLAYING"
     - Font Size: 60
     - Alignment: Center + Middle
     - Color: White
3. Active: ❌ OFF
```

### Bước 5: Gắn VictoryManager
```
1. Create Empty: "VictoryManager"
2. Add Component → VictoryManager
3. Kéo vào:
   - Victory Video Player: VideoPlayer_Victory
   - Video Display: (Raw Image trong VictoryCanvas)
   - Video Canvas Group: VictoryCanvas
   - Credits Canvas: CreditsCanvas
   - Credits Text: CreditsText
   - Thank You Canvas: ThankYouCanvas
   - Thank You Text: ThankYouText
4. Settings:
   - Menu Scene Name: "Menu"
   - Credits Scroll Speed: 50
```

**✅ XONG PHẦN 5!** Boss chết → Video → Credits

---

## 6. TEST & DEBUG

### Checklist Cuối Cùng

**✅ Intro Dialogue:**
- [ ] 4 dòng dialogue hiện ra
- [ ] Player không di chuyển được khi dialogue
- [ ] Player di chuyển được sau dialogue

**✅ Video Trigger:**
- [ ] Đánh 6 zombie → Video phát
- [ ] Dialogue Anti hiện sau video
- [ ] Boss spawn sau dialogue

**✅ Boss Fight:**
- [ ] Boss có thanh máu trên đầu
- [ ] Boss tung meteor có vùng đỏ cảnh báo
- [ ] Meteor gây damage cho player
- [ ] Boss dừng lại sau 15 giây
- [ ] Player đánh boss → Mất 1/3 HP
- [ ] Lặp 3 lần

**✅ Victory:**
- [ ] Boss chết → Video victory phát
- [ ] "Thanks For Playing" hiện
- [ ] Credits scroll
- [ ] ESC → Về menu

---

## 🐛 LỖI THƯỜNG GẶP

### ❌ Video không phát
**Nguyên nhân:** VideoPlayer → Play On Awake = ON  
**Sửa:** Tắt Play On Awake

### ❌ Boss không spawn
**Nguyên nhân:** Chưa đủ 6 sao  
**Sửa:** Kiểm tra StarCollectionSystem

### ❌ Player không damage boss
**Nguyên nhân:** Boss đang bất tử (Attack Phase)  
**Sửa:** Đợi boss dừng lại (Vulnerable Phase)

### ❌ Meteor không rơi
**Nguyên nhân:** Prefab chưa gán vào boss  
**Sửa:** Double-click boss prefab → Gán Meteor Prefab

---

## 🎉 HOÀN THÀNH!

Bây giờ bạn đã có:
- ✅ Intro dialogue khi vào game
- ✅ Video trigger khi đủ 6 sao
- ✅ Boss fight với 3 phases
- ✅ Meteor skill với warning
- ✅ Victory video + Credits

**Thời gian setup:** ~30 phút  
**Số file cần import:** 2 video, 1 typing sound, 1 roar sound

Chúc bạn may mắn! 🎮🔥
