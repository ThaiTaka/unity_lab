# 🎮 HƯỚNG DẪN ĐIỀU KHIỂN GAME - 3D SURVIVAL GAME

## 🎯 TẤT CẢ CÁC PHÍM ĐIỀU KHIỂN

---

## 🚶 DI CHUYỂN CƠ BẢN

| Phím | Chức Năng | Mô Tả |
|------|-----------|-------|
| **W** | Di chuyển tiến | Đi về phía trước |
| **A** | Di chuyển trái | Đi sang trái |
| **S** | Di chuyển lùi | Đi về phía sau |
| **D** | Di chuyển phải | Đi sang phải |
| **Space** | Nhảy | Nhảy lên (chỉ khi đứng trên mặt đất) |
| **Mouse Move** | Quay camera | Di chuyển chuột để nhìn xung quanh |

---

## ⚔️ CHIẾN ĐẤU & CÔNG CỤ

| Phím | Chức Năng | Mô Tả |
|------|-----------|-------|
| **Left Click** | Tấn công chính | Attack với tool/weapon đang cầm |
| **Right Click** | Tấn công phụ | Alternative attack (chức năng đặc biệt) |

**Ví dụ:**
- **Axe (Rìu)**: Left Click = Chặt cây
- **Pickaxe (Cuốc)**: Left Click = Đào đá/khoáng sản
- **Weapon**: Left Click = Đánh thường, Right Click = Đánh mạnh

---

## 🎒 INVENTORY (TÚI ĐỒ)

| Phím | Chức Năng | Mô Tả |
|------|-----------|-------|
| **I** hoặc **Tab** | Mở/Đóng Inventory | Toggle túi đồ |

**Khi Inventory mở:**
- ✅ Con trỏ chuột xuất hiện
- ✅ Camera không quay được
- ✅ Click vào items để xem chi tiết
- ✅ Buttons:
  - **Use** - Dùng item (đồ ăn, thuốc)
  - **Equip** - Trang bị (tools, weapons)
  - **Unequip** - Gỡ trang bị
  - **Drop** - Vứt item xuống đất

**Khi Inventory đóng:**
- ✅ Con trỏ chuột ẩn (locked)
- ✅ Camera quay tự do
- ✅ Có thể di chuyển và chiến đấu

---

## 🔨 TƯƠNG TÁC

| Phím | Chức Năng | Mô Tả |
|------|-----------|-------|
| **E** | Tương tác | Nhặt items, mở cửa, sử dụng objects |

**Hiển thị:**
- Khi nhìn vào object có thể tương tác
- Text hiện: **[E] (Tên object)**
- Ví dụ: "[E] Pick up Stone", "[E] Open Chest"

---

## 🏗️ XÂY DỰNG (BUILDING)

| Phím | Chức Năng | Mô Tả |
|------|-----------|-------|
| **Left Click** | Đặt building | Xây building tại vị trí preview |
| **R** | Rotate building | Xoay building preview (có thể) |

**Cách xây dựng:**
1. Equip Building Kit từ Inventory
2. Chọn building recipe (từ menu)
3. Preview xuất hiện:
   - **Xanh lá** = Đặt được
   - **Đỏ** = Không đặt được (va chạm)
4. Left Click để xây

---

## 📜 CRAFTING (CHẾ TẠO)

**Mở Crafting Menu:**
- Tương tác với **Crafting Table** (bàn chế tạo)
- Hoặc mở từ menu (tùy setup)

**Khi menu mở:**
- Xem danh sách recipes
- Check materials cần thiết
- Click để chế tạo

---

## 📊 PLAYER NEEDS (NHU CẦU)

Các thanh trạng thái (hiển thị trên UI):

| Biểu Tượng | Chỉ Số | Mô Tả |
|------------|---------|-------|
| ❤️ | **Health** | Máu - Giảm khi bị tấn công |
| 🍖 | **Hunger** | Đói - Giảm theo thời gian |
| 💧 | **Thirst** | Khát - Giảm theo thời gian |
| 😴 | **Sleep** | Ngủ - Giảm theo thời gian |

**Cách hồi:**
- Ăn đồ ăn → Tăng Hunger
- Uống nước → Tăng Thirst
- Ngủ (giường) → Tăng Sleep
- Dùng thuốc → Tăng Health

---

## 🎯 CÁC PHÍM TẮT (HOTKEYS)

| Phím | Chức Năng | Ghi Chú |
|------|-----------|---------|
| **1-9** | Quick slot | Chọn nhanh items (nếu có) |
| **Esc** | Menu/Pause | Mở menu game |

---

## 🖱️ CAMERA & MOUSE

### **Camera Settings:**
- **Look Sensitivity**: Độ nhạy chuột (có thể chỉnh trong Inspector)
- **Min/Max X Look**: Giới hạn nhìn lên/xuống

### **Cursor States:**
- **Locked** (Ẩn): Khi đang chơi, di chuyển
- **Unlocked** (Hiện): Khi mở Inventory, menu

---

## 📋 CONTROLS SUMMARY (TÓM TẮT)

### **Luôn Dùng:**
```
W/A/S/D     - Di chuyển
Mouse       - Nhìn xung quanh
Space       - Nhảy
E           - Tương tác
I/Tab       - Inventory
Left Click  - Tấn công/Hành động
```

### **Khi Cầm Tool:**
```
Left Click  - Dùng tool
Right Click - Chức năng phụ
```

### **Trong Inventory:**
```
Mouse       - Chọn items
Left Click  - Click buttons (Use/Equip/Drop)
I/Tab       - Đóng
```

---

## 🎮 INPUT SYSTEM (UNITY NEW INPUT SYSTEM)

Game sử dụng **Unity New Input System**, các phím có thể thay đổi trong:

### **Cách xem/thay đổi phím:**
```
1. Assets → Input Actions (file .inputactions)
2. Click đúp để mở
3. Xem tất cả bindings
4. Có thể rebind phím
```

### **Các Input Actions có trong game:**

#### **Player Actions:**
- `Move` → WASD / Left Stick
- `Look` → Mouse Delta / Right Stick
- `Jump` → Space
- `Interact` → E
- `Attack` → Left Mouse Button
- `AltAttack` → Right Mouse Button
- `Inventory` → I hoặc Tab

---

## 🔧 SETTINGS (CÀI ĐẶT)

### **Có thể chỉnh trong Inspector:**

**PlayerController:**
- `moveSpeed` - Tốc độ di chuyển
- `jumpForce` - Lực nhảy
- `lookSensitivity` - Độ nhạy chuột
- `minXLook` / `maxXLook` - Giới hạn góc nhìn

**Camera:**
- `Field of View` - Góc nhìn camera (60-90)
- `Clipping Planes` - Khoảng cách render

---

## 💡 TIPS & TRICKS

### **1. Di Chuyển Hiệu Quả:**
- Giữ W + Mouse để chạy và nhìn
- Space để nhảy qua chướng ngại vật
- Shift (nếu có) để chạy nhanh

### **2. Chiến Đấu:**
- Nhảy + Attack = Aerial attack
- Backpedal (S) khi bị tấn công
- Switch tools nhanh bằng hotkeys

### **3. Inventory Management:**
- Drop items không cần thiết
- Stack items giống nhau
- Equip tools trước khi dùng

### **4. Tương Tác:**
- Nhìn thẳng vào object để thấy [E] prompt
- Distance giới hạn: Check `maxCheckDistance` trong InteractionManager

---

## 🎯 GAME MECHANICS

### **Ground Detection:**
- Game check 4 rays từ player xuống đất
- Chỉ nhảy được khi `isGrounded() = true`
- Ground layer: Phải set trong Inspector

### **Interaction System:**
- Raycast từ center screen
- Check distance: `maxCheckDistance`
- Layer mask: Chỉ tương tác với layers được chọn

### **Equipment System:**
- Equip từ Inventory
- Mỗi lần chỉ equip 1 item
- Unequip trước khi equip item mới

---

## 📊 UI ELEMENTS

### **Hiển thị trên màn hình:**

1. **Health Bar** ❤️ (góc trái trên)
2. **Hunger Bar** 🍖
3. **Thirst Bar** 💧
4. **Sleep Bar** 😴
5. **Interaction Prompt** (giữa màn hình dưới)
   - "[E] Pick up Stone"
   - "[E] Open Chest"
6. **Crosshair** (center screen)
7. **Inventory UI** (khi nhấn I/Tab)

---

## 🆘 TROUBLESHOOTING

### **Không di chuyển được:**
- Check Inventory có đang mở không (đóng bằng I/Tab)
- Check Player có Rigidbody component
- Check Input System có active

### **Không tương tác được:**
- Nhìn thẳng vào object
- Check khoảng cách (phải gần)
- Check layer của object

### **Camera không quay:**
- Check `canLook` có = true không
- Check Inventory có đang mở (cursor unlocked)
- Check mouse sensitivity > 0

### **Không nhảy được:**
- Check có đứng trên đất không
- Check `groundLayerMask` có đúng layer
- Check `jumpForce` > 0

---

## 📝 NOTES CHO DEVELOPERS

### **Thêm phím mới:**
```csharp
// Trong PlayerController hoặc script khác
public void OnNewActionInput(InputAction.CallbackContext context)
{
    if (context.phase == InputActionPhase.Started)
    {
        // Code xử lý
    }
}
```

### **Thay đổi phím:**
1. Mở Input Actions asset
2. Tìm action
3. Đổi binding
4. Save

### **Disable controls:**
```csharp
// Disable movement
currentMovementInput = Vector2.zero;

// Disable camera
canLook = false;

// Unlock cursor
Cursor.lockState = CursorLockMode.None;
```

---

## ✅ CHECKLIST CONTROLS

**Player có thể:**
- [x] Di chuyển W/A/S/D
- [x] Nhảy Space
- [x] Nhìn xung quanh Mouse
- [x] Tấn công Left Click
- [x] Tương tác E
- [x] Mở Inventory I/Tab
- [x] Equip items
- [x] Use items
- [x] Drop items
- [x] Build structures

---

## 🎮 DEFAULT CONTROLS (MẶC ĐỊNH)

```
MOVEMENT:
  W - Forward
  A - Left
  S - Backward
  D - Right
  Space - Jump
  
CAMERA:
  Mouse - Look around
  
ACTIONS:
  E - Interact
  Left Click - Attack/Use
  Right Click - Alt Attack
  I/Tab - Inventory
  
INVENTORY:
  Mouse - Select
  Click - Use/Equip/Drop
  I/Tab - Close
```

---

## 🎯 QUICK REFERENCE

**Mới chơi? Nhớ 5 phím này:**
1. **WASD** - Di chuyển
2. **Mouse** - Nhìn
3. **E** - Nhặt đồ
4. **I** - Mở túi
5. **Left Click** - Đánh/Dùng tool

---

**🎉 Chúc bạn chơi game vui vẻ! 🎉**

*Game sử dụng Unity New Input System - Có thể customize tất cả phím!*
