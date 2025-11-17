# ⭐ HỆ THỐNG THU THẬP SAO - ĐƠN GIẢN

## 🎯 CHỨC NĂNG
- Diệt 1 zombie = +1 sao
- Hiển thị "⭐ X/6" trên UI
- **Đủ 6 sao = DỪNG SPAWN ZOMBIE**
- Sau đó bạn có thể thêm event khác (boss, level mới, etc.)

---

## ⚡ SETUP NHANH - 3 BƯỚC

### 1. Tạo Text
```
Canvas → Panel (góc phải) → Text "⭐ 0/6"
```

### 2. Tạo StarCollectionSystem
```
GameObject → Add Component: StarCollectionSystem
Assign: Star Count Text = Text vừa tạo
```

### 3. Play & Test
```
Diệt zombie → Text cập nhật → Đủ 6 → Dừng spawn
```

---

## 📖 HƯỚNG DẪN CHI TIẾT

**Đọc file này để setup:** [`SETUP_UNITY_DON_GIAN.md`](SETUP_UNITY_DON_GIAN.md)

Có ảnh, giải thích từng bước, fix lỗi, tùy chỉnh.

---

## 🎨 NÂNG CAO (Optional)

Nếu muốn UI đẹp hơn:
- Thêm 6 star icons riêng lẻ
- Thêm Victory Panel
- Thêm animation + âm thanh

**Xem:** [`SETUP_STAR_SYSTEM.md`](SETUP_STAR_SYSTEM.md)

---

## 🔥 THÊM EVENT SAU KHI ĐỦ 6 SAO

Mở file `StarCollectionSystem.cs`, tìm hàm `OnAllStarsCollected()`:

```csharp
private void OnAllStarsCollected()
{
    // Đã có: Dừng spawn zombie
    WaveManager.instance.StopAllWaves();
    
    // 🔥 THÊM CODE CỦA BẠN Ở ĐÂY:
    
    // Ví dụ: Spawn Boss
    BossManager.instance.SpawnBoss();
    
    // Hoặc: Load level mới
    // SceneManager.LoadScene("BossLevel");
    
    // Hoặc: Hiển thị UI
    // ShowVictoryScreen();
}
```

---

## ✅ ĐÃ PUSH LÊN GITHUB

Tất cả code và tài liệu đã có trên repo: **unity_lab**

---

## 🚀 BẮT ĐẦU NGAY

1. Đọc [`SETUP_UNITY_DON_GIAN.md`](SETUP_UNITY_DON_GIAN.md)
2. Follow 3 bước
3. Test trong game
4. Thêm event của bạn

**Chỉ mất 5 phút!** 🎮
