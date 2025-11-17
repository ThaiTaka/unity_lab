# 🏃 HƯỚNG DẪN SETUP SPRINT (CHẠY NHANH)

## 🎯 CHỨC NĂNG
- Giữ **Shift** → Chạy nhanh hơn
- Thả **Shift** → Về tốc độ bình thường

---

## ⚡ SETUP TRONG UNITY - 2 BƯỚC

### BƯỚC 1: Setup Input Action (Thêm Shift binding)

1. **Tìm Input Actions file**:
   - Project → Assets → Tìm file **Input Actions** (thường là `PlayerInputActions.asset` hoặc tương tự)
   - Hoặc trong Hierarchy → Player → Inspector → Player Input component → Actions

2. **Mở Input Actions Editor**:
   - Double click vào file Input Actions
   - Hoặc Player → Inspector → Player Input → **Edit Asset**

3. **Thêm Sprint Action**:
   ```
   Actions
   ├─ Movement (đã có)
   ├─ Look (đã có)
   ├─ Jump (đã có)
   └─ Sprint (← THÊM MỚI)
   ```

4. **Tạo Sprint Action**:
   - Click vào **Action Maps** (vd: "Player")
   - Click nút **+** để thêm Action
   - Rename thành: **"Sprint"**
   - Action Type: **Button**
   
5. **Add Binding cho Sprint**:
   - Chọn "Sprint" action
   - Click dấu **+** → **Add Binding**
   - Click vào **<No Binding>**
   - Nhấn phím **Left Shift** trên bàn phím
   - Sẽ hiển thị: **Left Shift [Keyboard]**

6. **Save**:
   - Ctrl+S hoặc **Save Asset**

---

### BƯỚC 2: Kết nối Event trong Player

1. **Chọn Player object** trong Hierarchy

2. **Inspector → Player Input component**:
   - Tìm mục **Events**
   - Mở rộng **Events** (nếu đang thu gọn)

3. **Thêm Sprint Event**:
   - Scroll xuống, tìm **Sprint** (hoặc events list)
   - Click **+** để thêm listener
   - Kéo **Player object** vào ô trống
   - Dropdown: Chọn **PlayerController → OnSprintInput**

---

### BƯỚC 3: Setup Speed trong Inspector

1. **Chọn Player** trong Hierarchy

2. **Inspector → PlayerController script**:
   ```
   Movement
   ├─ Move Speed: 5 (tốc độ đi bình thường)
   └─ Sprint Speed: 8 (tốc độ chạy nhanh) ← SET GIÁ TRỊ NÀY
   ```

**Gợi ý giá trị:**
- Move Speed: `5` (bình thường)
- Sprint Speed: `8-10` (nhanh hơn 1.5-2 lần)

---

## ✅ TEST

1. **Play game**
2. **Di chuyển bình thường** (WASD)
3. **Giữ Shift** → Nhân vật chạy nhanh hơn 🏃
4. **Thả Shift** → Về tốc độ bình thường 🚶
5. **Mở Console** → Sẽ thấy log:
   - "🏃 Sprint ON - Speed: 8"
   - "🚶 Sprint OFF - Speed: 5"

---

## 🎮 NẾU DÙNG NEW INPUT SYSTEM

### Nếu file Input Actions không tìm thấy:

1. **Tạo mới**:
   - Project → Right Click → Create → **Input Actions**
   - Rename: "PlayerInputActions"

2. **Thiết lập Actions**:
   ```
   Action Maps: Player
   ├─ Movement (Vector2, WASD)
   ├─ Look (Vector2, Mouse Delta)
   ├─ Jump (Button, Space)
   └─ Sprint (Button, Left Shift) ← THÊM
   ```

3. **Generate C# Class**:
   - Chọn file Input Actions
   - Inspector → **Generate C# Class** ✓
   - Click **Apply**

---

## 🎨 TÙY CHỈNH

### Đổi tốc độ sprint:
```
PlayerController → Sprint Speed: 10 (nhanh hơn)
PlayerController → Sprint Speed: 7 (chậm hơn)
```

### Đổi phím sprint:
Trong Input Actions → Sprint → Binding:
- **Right Shift**: Dùng Shift phải
- **Ctrl**: Dùng Control
- **Alt**: Dùng Alt

### Thêm stamina (thể lực):
Bạn có thể mở rộng sau:
```csharp
// Trong PlayerController
public float stamina = 100f;
public float staminaDrainRate = 10f;

void Update() {
    if (isSprinting && stamina > 0) {
        stamina -= staminaDrainRate * Time.deltaTime;
    } else {
        stamina += staminaDrainRate * 0.5f * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0, 100);
    }
}
```

---

## 🐛 TROUBLESHOOTING

### ❌ Nhấn Shift không chạy nhanh:
**Fix:**
1. Check Console có log "Sprint ON" không?
   - **CÓ**: Sprint Speed chưa set → Set Sprint Speed = 8
   - **KHÔNG**: Event chưa kết nối → Làm lại Bước 2

### ❌ Không tìm thấy Sprint trong Events:
**Fix:**
1. Input Actions chưa có Sprint action → Làm lại Bước 1
2. Save Input Actions và recompile Unity

### ❌ Sprint Speed = 0:
**Fix:**
- Inspector → PlayerController → Sprint Speed: Set = 8 (hoặc giá trị khác)

---

## 📊 GIÁ TRỊ KHUYẾN NGHỊ

| Tốc độ | Move Speed | Sprint Speed | Tỷ lệ |
|--------|-----------|--------------|-------|
| Chậm   | 3         | 5            | 1.67x |
| Bình thường | 5  | 8            | 1.6x  |
| Nhanh  | 7         | 12           | 1.7x  |
| Rất nhanh | 10    | 18           | 1.8x  |

---

## ✅ CHECKLIST

- [ ] Code PlayerController đã update
- [ ] Input Actions có Sprint action
- [ ] Sprint binding = Left Shift
- [ ] Player Input Events có Sprint → OnSprintInput
- [ ] Sprint Speed được set trong Inspector (vd: 8)
- [ ] Test: Giữ Shift → Chạy nhanh
- [ ] Console có log "Sprint ON/OFF"

---

**🎉 XONG! Giờ bạn có thể sprint như game AAA!** 🏃💨

**💡 TIP:** Giá trị Sprint Speed = Move Speed × 1.5 đến 2 là hợp lý nhất!
