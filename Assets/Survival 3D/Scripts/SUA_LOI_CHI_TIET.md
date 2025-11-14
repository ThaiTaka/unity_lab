# 🔧 HƯỚNG DẪN SỬA LỖI CHI TIẾT - Unity 3D Survival Game

## ⚠️ QUAN TRỌNG: Làm theo thứ tự từng bước!

---

## BƯỚC 1: Cài Đặt TextMeshPro (TMPro) ⭐⭐⭐

### Cách 1: Qua Package Manager (Khuyên dùng)
1. Mở Unity Editor
2. Click menu: **Window → Package Manager**
3. Ở góc trên bên trái, chọn dropdown: **Packages: Unity Registry**
4. Tìm trong danh sách hoặc search: **TextMesh Pro**
5. Click vào package **TextMesh Pro**
6. Click nút **Install** ở góc dưới bên phải
7. Đợi Unity download và import (5-10 giây)
8. Một cửa sổ popup hiện ra **"TMP Importer"**, click **Import TMP Essentials**
9. Đợi import xong

### Cách 2: Qua Menu (Nếu cách 1 không được)
1. Mở Unity Editor  
2. Click menu: **Window → TextMeshPro → Import TMP Essential Resources**

---

## BƯỚC 2: Cài Đặt Input System ⭐⭐⭐

### Cài đặt:
1. Mở Unity Editor
2. Click menu: **Window → Package Manager**
3. Chọn dropdown: **Packages: Unity Registry**
4. Search hoặc tìm: **Input System**
5. Click vào package **Input System**
6. Click nút **Install**
7. Đợi download xong

### ⚠️ CỰC KỲ QUAN TRỌNG - Cấu hình Input System:
Sau khi cài xong, Unity sẽ hiện popup hỏi restart:

**OPTION 1: Nếu bạn chỉ dùng Input System mới:**
- Click **Yes** để restart ngay

**OPTION 2: Nếu bạn muốn dùng cả Old Input và New Input (Khuyên dùng):**
1. Click **No** (không restart ngay)
2. Vào menu: **Edit → Project Settings**
3. Click tab **Player** ở bên trái
4. Kéo xuống phần **Other Settings**
5. Tìm dòng **Active Input Handling**
6. Đổi từ "Input Manager (Old)" sang **"Both"**
7. Click **Apply**
8. Restart Unity Editor thủ công

---

## BƯỚC 3: Kiểm Tra Unity UI (UGUI)

Unity UI thường có sẵn, nhưng hãy kiểm tra:

1. Mở **Window → Package Manager**
2. Chọn dropdown: **Packages: In Project** (để xem packages đã cài)
3. Tìm xem có **"Unity UI"** hoặc **"UI"** không
4. Nếu KHÔNG có:
   - Chuyển sang **Packages: Unity Registry**
   - Tìm **"Unity UI"**
   - Click **Install**

---

## BƯỚC 4: Kiểm Tra Lỗi Đã Hết Chưa

1. Đợi Unity import xong tất cả (thanh progress bar ở góc dưới phải)
2. Nhìn vào **Console** (nếu không thấy, vào **Window → General → Console**)
3. Nếu vẫn còn lỗi màu đỏ, đọc tiếp bước 5

---

## BƯỚC 5: Xóa Library Folder (Nếu vẫn lỗi)

Đôi khi Unity cache bị lỗi, cần xóa và rebuild:

1. **ĐÓNG Unity Editor hoàn toàn**
2. Vào thư mục project: `E:\3D-Survival-Game-Unity\`
3. Tìm thư mục **Library** (folder màu xám/ẩn)
4. **XÓA** toàn bộ thư mục Library
5. Mở lại Unity project
6. Đợi Unity rebuild (3-5 phút, tùy máy)

---

## BƯỚC 6: Kiểm Tra Phiên Bản Unity

Các package này yêu cầu Unity phiên bản tối thiểu:
- **Unity 2019.4** trở lên (khuyên dùng **Unity 2020.3 LTS** hoặc **2021.3 LTS**)

Để kiểm tra phiên bản:
1. Mở Unity Hub
2. Xem phiên bản Unity bên cạnh tên project
3. Nếu quá cũ (< 2019.4), hãy update Unity

---

## 📋 CHECKLIST - Đánh dấu khi hoàn thành

- [ ] ✅ Đã cài TextMeshPro
- [ ] ✅ Đã Import TMP Essential Resources  
- [ ] ✅ Đã cài Input System
- [ ] ✅ Đã cấu hình Active Input Handling = "Both"
- [ ] ✅ Đã restart Unity Editor
- [ ] ✅ Đã kiểm tra Unity UI có trong project
- [ ] ✅ Console không còn lỗi đỏ
- [ ] ✅ (Nếu cần) Đã xóa Library và rebuild

---

## 🔍 TROUBLESHOOTING - Nếu vẫn lỗi

### Lỗi: "The type or namespace name 'TMPro' could not be found"
**Giải pháp:**
- Kiểm tra Package Manager → Packages: In Project → Có TextMesh Pro chưa?
- Nếu có rồi nhưng vẫn lỗi: Xóa Library folder và restart Unity
- Import lại TMP Essential Resources

### Lỗi: "The type or namespace name 'InputSystem' does not exist"  
**Giải pháp:**
- Kiểm tra Package Manager → Có Input System chưa?
- Kiểm tra Edit → Project Settings → Player → Active Input Handling = "Both"
- Phải restart Unity sau khi cài Input System
- Nếu vẫn lỗi: Xóa Library folder

### Lỗi: "The type or namespace name 'UI' does not exist in namespace 'UnityEngine'"
**Giải pháp:**
- Kiểm tra có Unity UI trong Package Manager không
- Thử cài lại: Remove rồi Install lại Unity UI
- Restart Unity Editor

### Lỗi: "Assembly reference missing"
**Giải pháp:**
1. Vào menu: **Assets → Reimport All** (cẩn thận, mất thời gian)
2. Hoặc xóa Library folder (nhanh hơn)

---

## 📦 Danh Sách Packages Cần Thiết

Sau khi cài đặt xong, trong Package Manager → In Project phải có:

1. ✅ **TextMesh Pro** (com.unity.textmeshpro) - version 3.0.x trở lên
2. ✅ **Input System** (com.unity.inputsystem) - version 1.4.x trở lên  
3. ✅ **Unity UI** (com.unity.ugui) - version 1.0.0

---

## 🎮 Sau Khi Sửa Xong

1. Nhấn **Ctrl + R** (hoặc **Cmd + R** trên Mac) để recompile
2. Kiểm tra Console sạch (không còn lỗi đỏ)
3. Thử chạy game: nhấn **Play** ▶️
4. Nếu chạy được → Hoàn thành! 🎉

---

## ⏱️ Ước Tính Thời Gian

- Cài packages: 2-5 phút
- Restart Unity: 30 giây - 1 phút  
- Rebuild Library (nếu cần): 3-10 phút tùy máy

**Tổng: Khoảng 10-15 phút**

---

## 📞 Nếu Vẫn Không Được

Cung cấp thông tin sau:
1. Phiên bản Unity đang dùng (ví dụ: 2021.3.15f1)
2. Screenshot Console với lỗi
3. Screenshot Package Manager → In Project
4. Đã làm theo hướng dẫn đến bước nào?

---

**Lưu ý cuối:** Các lỗi này là do thiếu packages, KHÔNG phải lỗi code. Chỉ cần cài đặt đúng packages là sẽ hết lỗi!
