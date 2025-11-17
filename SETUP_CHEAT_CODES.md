# 🎮 HƯỚNG DẪN SETUP HỆ THỐNG CHEAT CODES

> **Mục tiêu:** Tạo hệ thống cheat code bí mật cho developer
> 
> **Thời gian:** ~10 phút
> 
> **Độ khó:** ⭐ (Dễ)

---

## 📖 CÁCH HOẠT ĐỘNG

```
Đang chơi game
  ↓
Bấm ENTER
  ↓
Khung nhập cheat xuất hiện (góc trái dưới)
  ↓
Gõ mã cheat:
  - +cheath → Bất tử (God Mode)
  - +cheatf → Luôn no (Infinite Hunger)
  - +cheatd → Đánh 1 phát zombie chết (One Hit Kill)
  ↓
Bấm ENTER lần nữa
  ↓
Cheat kích hoạt! (Bấm lại để TẮT)
  ↓
Góc phải trên màn hình hiện status: 🛡️ God Mode, etc.
```

**LƯU Ý:** Boss Anti T1 KHÔNG chết 1 hit (vẫn cần đánh 3 lần)

---

## 1. SETUP UI CHEAT PANEL

### Bước 1: Tạo Canvas Cheat
```
1. Hierarchy → Right-click → UI → Canvas
2. Đổi tên: "CheatCanvas"
3. Inspector:
   - Render Mode: Screen Space - Overlay
   - Sort Order: 999 (hiển thị trên cùng)
```

### Bước 2: Tạo Cheat Panel
```
1. Right-click CheatCanvas → UI → Panel
2. Đổi tên: "CheatPanel"
3. Inspector:
   - Anchor: Bottom-Left (góc trái dưới)
   - Width: 500
   - Height: 100
   - Pos X: 250 (cách mép trái 0)
   - Pos Y: 50 (cách mép dưới 0)
   - Color: Black (0, 0, 0, 200) - semi-transparent
```

### Bước 3: Tạo Input Field
```
1. Right-click CheatPanel → UI → Input Field - TextMeshPro
2. Đổi tên: "CheatInputField"
3. Inspector:
   - Width: 450
   - Height: 50
   - Placeholder Text: "Enter cheat code... (+cheath, +cheatf, +cheatd)"
   - Font Size: 18
   - Text Color: White
   - Background Color: Dark Gray (50, 50, 50, 255)
```

### Bước 4: Tạo Feedback Text
```
1. Right-click CheatPanel → UI → Text - TextMeshPro
2. Đổi tên: "FeedbackText"
3. Inspector:
   - Pos Y: 60 (trên input field)
   - Width: 450
   - Height: 30
   - Font Size: 16
   - Color: Yellow
   - Alignment: Left + Middle
   - Text: "" (để trống)
```

### Bước 5: Ẩn Panel Ban Đầu
```
Select CheatPanel → Inspector:
- Active: ❌ UNCHECKED (ẩn lúc bắt đầu game)
```

---

## 2. SETUP CHEAT MANAGER

### Bước 1: Tạo GameObject Manager
```
1. Hierarchy → Create Empty
2. Đổi tên: "CheatCodeManager"
3. Add Component → CheatCodeManager (script)
```

### Bước 2: Gắn References
```
Select CheatCodeManager → Inspector:

UI References:
- Cheat Panel: Kéo CheatPanel vào
- Cheat Input Field: Kéo CheatInputField vào
- Feedback Text: Kéo FeedbackText vào

Player References:
- Player Needs: Kéo Player → PlayerNeeds component vào
- Player Controller: Kéo Player → PlayerController component vào

Settings:
- Feedback Display Time: 2 (giây hiển thị thông báo)
```

---

## 3. TEST CHEAT CODES

### Kiểm Tra Từng Cheat:

**✅ God Mode (+cheath):**
1. Play game
2. Bấm Enter → Gõ `+cheath` → Enter
3. Thấy "✅ GOD MODE: ON" góc trái dưới
4. Góc phải trên hiện: 🛡️ God Mode
5. Để zombie đánh → Máu KHÔNG giảm
6. Bấm Enter → `+cheath` → Enter lần nữa → Tắt cheat

**✅ Infinite Hunger (+cheatf):**
1. Bấm Enter → `+cheatf` → Enter
2. Thấy "✅ INFINITE HUNGER: ON"
3. Góc phải trên hiện: 🍖 Infinite Hunger
4. Thanh đói LUÔN đầy
5. Gõ lại `+cheatf` → Tắt cheat

**✅ One Hit Kill (+cheatd):**
1. Bấm Enter → `+cheatd` → Enter
2. Thấy "✅ ONE HIT KILL: ON"
3. Góc phải trên hiện: ⚔️ One Hit Kill
4. Đánh zombie 1 phát → CHẾT NGAY
5. Đánh Boss Anti T1 → VẪN PHẢI 3 LẦN (không áp dụng cho boss)

**❌ Mã Sai:**
1. Bấm Enter → Gõ `abc123` → Enter
2. Thấy "❌ Invalid Code! Try: +cheath, +cheatf, +cheatd"

---

## 4. CÁCH SỬ DỤNG KHI CHƠI

### Bật Cheat:
```
1. Đang chơi game → Bấm ENTER
2. Khung nhập code xuất hiện
3. Gõ mã: +cheath hoặc +cheatf hoặc +cheatd
4. Bấm ENTER lần nữa
5. Cheat kích hoạt!
```

### Tắt Cheat:
```
1. Bấm ENTER
2. Gõ ĐÚNG MÃ ĐÃ BẬT (ví dụ: +cheath)
3. Bấm ENTER
4. Cheat tắt!
```

### Hủy Nhập Cheat:
```
Bấm ESC → Khung nhập đóng lại, không kích hoạt gì
```

---

## 5. THÔNG TIN CHEAT CODES

| Mã Cheat | Tên | Chức Năng | Áp Dụng Cho Boss? |
|----------|-----|-----------|-------------------|
| `+cheath` | God Mode | Máu luôn đầy, không chết | ✅ Có |
| `+cheatf` | Infinite Hunger | Độ đói luôn đầy | ✅ Có |
| `+cheatd` | One Hit Kill | Zombie chết 1 phát | ❌ KHÔNG (Boss vẫn phải 3 lần) |

---

## 6. CHI TIẾT KỸ THUẬT

### Cách Hoạt Động:

**God Mode:**
- Mỗi frame kiểm tra máu của player
- Nếu máu < max → Tự động hồi lên max
- Code: `playerNeeds.health = playerNeeds.maxHealth;`

**Infinite Hunger:**
- Mỗi frame kiểm tra độ đói
- Nếu đói < max → Tự động hồi lên max
- Code: `playerNeeds.hunger = playerNeeds.maxHunger;`

**One Hit Kill:**
- Khi zombie bị đánh (NPC.TakePhysicDamage)
- Kiểm tra: `CheatCodeManager.IsOneHitKillActive()`
- Nếu true VÀ KHÔNG phải boss → `health = 0;`
- Code: `if (cheatManager.IsOneHitKillActive() && !isBoss)`

---

## 🐛 LỖI THƯỜNG GẶP

### ❌ Bấm Enter không có gì xảy ra
**Nguyên nhân:** CheatPanel chưa gắn vào script  
**Sửa:** Kiểm tra CheatCodeManager → Cheat Panel có gắn chưa

### ❌ Gõ cheat nhưng không kích hoạt
**Nguyên nhân:** 
1. Player Needs chưa gắn vào script
2. Gõ sai mã (phải có dấu +)

**Sửa:** 
1. Gắn PlayerNeeds vào CheatCodeManager
2. Gõ đúng: `+cheath` (không phải `cheath`)

### ❌ Boss chết 1 hit
**Nguyên nhân:** Logic kiểm tra boss sai  
**Sửa:** Đảm bảo Boss Anti T1 có component `BossAntiT1` script

### ❌ Cheat không tắt được
**Nguyên nhân:** Bấm mã khác, không phải mã đã bật  
**Sửa:** 
- Nếu đã bật `+cheath` → Phải gõ lại `+cheath` để tắt
- Mỗi mã là toggle (bật/tắt)

---

## 🎉 HOÀN THÀNH!

Bây giờ bạn đã có:
- ✅ Hệ thống cheat code bí mật
- ✅ 3 mã cheat: God Mode, Infinite Hunger, One Hit Kill
- ✅ UI góc trái dưới để nhập mã
- ✅ Status hiển thị góc phải trên
- ✅ Boss vẫn khó đánh (không áp dụng one hit kill)

**Người chơi bình thường:** Không biết → Chơi game như bình thường  
**Developer/Tester:** Biết mã → Bật cheat để test nhanh

**Mã Cheat:**
- `+cheath` → Bất tử
- `+cheatf` → Luôn no
- `+cheatd` → Đánh zombie 1 phát chết

Chúc bạn test game vui vẻ! 🎮🔥
