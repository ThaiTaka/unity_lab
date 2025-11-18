# ✅ CHECKLIST SETUP NHANH - IN RA GIẤY

## 🎮 GAME SCENE
```
□ Xóa: VictoryCanvas, VictoryManager, VideoTriggerManager
□ StarCollectionSystem → Victory Video Scene Name: "VictoryVideoScene"
□ Save scene (Ctrl + S)
```

## 🎬 VICTORY VIDEO SCENE (Tạo mới)
```
□ New Scene → Save as "VictoryVideoScene"
□ Canvas → RawImage (full screen, stretch/stretch)
□ RawImage → Add Video Player
   - Video Clip: [Kéo file .mp4 vào]
   - Play On Awake: OFF
   - Render Mode: Render Texture
□ Create Empty → VictoryVideoSceneManager
   - Video Player: [Kéo vào]
   - Video Display: [Kéo vào]
   - Next Scene Name: "BossIntroScene"
□ Build Settings → Add Open Scenes
□ Save (Ctrl + S)
```

## 😈 BOSS INTRO SCENE (Tạo mới)
```
□ New Scene → Save as "BossIntroScene"
□ Canvas → Panel (bottom, height 220)
   - Black semi-transparent
□ Panel → SpeakerNameText (yellow, bold, top-left)
□ Panel → DialogueText (white, stretch)
□ Kéo Boss Model vào → UNCHECK Active
□ Create Empty → BossSpawnPoint (vị trí boss)
□ Create Empty → BossIntroSceneManager
   - Dialogue Panel: [Kéo]
   - Dialogue Text: [Kéo]
   - Speaker Name Text: [Kéo]
   - Boss Model: [Kéo]
   - Boss Spawn Point: [Kéo]
   - Boss Arena Scene Name: "Game"
   - Dialogue Lines: Size 4
     Line 0: "Các ngươi dám giết..." (3s, spawn OFF)
     Line 1: "Ta đã nuôi chúng..." (3s, spawn OFF)
     Line 2: "Các ngươi sẽ phải..." (3s, spawn OFF)
     Line 3: "CHUẨN BỊ CHIẾN ĐẤU!" (2s, spawn ON ✅)
□ Build Settings → Add Open Scenes
□ Save (Ctrl + S)
```

## 📦 BUILD SETTINGS
```
□ File → Build Settings
□ Kiểm tra có đủ scenes:
   - LoadingScreen ✅
   - VictoryVideoScene ✅
   - BossIntroScene ✅
```

## 🧪 TEST
```
□ Play Game Scene
□ Giết 6 zombie
□ Check flow: Game → Loading → Video → Intro → Boss
□ Check Console logs (không có error màu đỏ)
□ Video có thể skip bằng Space
□ Dialogue chạy đúng thứ tự
□ Boss spawn sau dialogue cuối
```

---

## 🐛 NẾU CÓ LỖI

| Lỗi | Kiểm tra |
|-----|----------|
| Scene không chuyển | Build Settings thiếu scene |
| Video không phát | Video Clip chưa gắn |
| Dialogue không hiện | Dialogue Lines Size = 0 |
| Boss không spawn | Spawn Boss After = OFF |
| Loading mãi | Scene name sai (viết hoa/thường) |

---

**Xem chi tiết:** `SETUP_FINAL_COMPLETE.md` 📖
