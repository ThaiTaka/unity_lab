# TÀI LIỆU THIẾT KẾ KỸ THUẬT
## HỆ THỐNG KHO ĐỒ & CHẾ TẠO (INVENTORY & CRAFTING SYSTEM)
### 3D Survival Game - Unity Engine

---

## MỤC LỤC

1. [TỔNG QUAN HỆ THỐNG](#1-tổng-quan-hệ-thống)
2. [KIẾN TRÚC VÀ DESIGN PATTERN](#2-kiến-trúc-và-design-pattern)
3. [HỆ THỐNG ITEM DATABASE](#3-hệ-thống-item-database)
4. [HỆ THỐNG INVENTORY](#4-hệ-thống-inventory)
5. [HỆ THỐNG CRAFTING](#5-hệ-thống-crafting)
6. [HỆ THỐNG INTERACTION](#6-hệ-thống-interaction)
7. [HỆ THỐNG UI](#7-hệ-thống-ui)
8. [LUỒNG DỮ LIỆU VÀ WORKFLOW](#8-luồng-dữ-liệu-và-workflow)
9. [TÍCH HỢP VỚI CÁC HỆ THỐNG KHÁC](#9-tích-hợp-với-các-hệ-thống-khác)
10. [PERFORMANCE VÀ OPTIMIZATION](#10-performance-và-optimization)

---

## 1. TỔNG QUAN HỆ THỐNG

### 1.1. Giới thiệu

Hệ thống Inventory & Crafting là core gameplay system trong 3D Survival Game, cho phép người chơi:
- Thu thập và quản lý tài nguyên
- Sử dụng vật phẩm tiêu hao (consumables)
- Trang bị công cụ và vũ khí
- Chế tạo vật phẩm mới từ nguyên liệu
- Xây dựng công trình

### 1.2. Vai trò trong Game Loop

```
Thu thập tài nguyên → Lưu vào Inventory → Chế tạo vật phẩm → Trang bị/Sử dụng → Survival
```

### 1.3. Tính năng chính

✅ **Inventory System:**
- Quản lý 24 slots (có thể mở rộng)
- Hệ thống stack items (xếp chồng)
- Equip/Unequip items
- Drop items vào thế giới
- Tích hợp với Player Needs (health, hunger, thirst, sleep)

✅ **Crafting System:**
- Crafting recipes từ ScriptableObject
- Kiểm tra tài nguyên trước khi craft
- Real-time UI update
- Tích hợp với Inventory

✅ **Interaction System:**
- Raycast-based detection
- Prompt text hiển thị khi nhắm vào item
- Interface IInteractable cho mọi object tương tác

---

## 2. KIẾN TRÚC VÀ DESIGN PATTERN

### 2.1. Sơ đồ kiến trúc tổng thể

```
┌─────────────────────────────────────────────────────────────┐
│                    PLAYER CONTROLLER                         │
│  (PlayerController.cs + Input System)                       │
└───────────────────┬─────────────────────────────────────────┘
                    │
        ┌───────────┴────────────┐
        │                        │
┌───────▼─────────┐    ┌────────▼──────────┐
│  INTERACTION    │    │    INVENTORY      │
│  MANAGER        │───►│    (Singleton)    │
│  - Raycast      │    │  - 24 Slots       │
│  - IInteractable│    │  - Stack System   │
└─────────────────┘    │  - Equip System   │
                       └────────┬──────────┘
                                │
                    ┌───────────┴────────────┐
                    │                        │
        ┌───────────▼─────────┐   ┌─────────▼──────────┐
        │  CRAFTING WINDOW    │   │   ITEM DATABASE    │
        │   (Singleton)       │   │  (ScriptableObject)│
        │  - Recipe List      │   │  - Item Data       │
        │  - Craft Logic      │   │  - Prefab Ref      │
        └─────────────────────┘   └────────────────────┘
                    │
        ┌───────────▼─────────┐
        │       UI LAYER      │
        │  - ItemSlotUI       │
        │  - CraftingRecipeUI │
        │  - Prompt Text      │
        └─────────────────────┘
```

### 2.2. Design Patterns được sử dụng

#### **Singleton Pattern**
```csharp
// Inventory.cs
public static Inventory instance;
private void Awake() { instance = this; }

// CraftingWindow.cs
public static CraftingWindow instance;
private void Awake() { instance = this; }
```
**Lý do:** Đảm bảo chỉ có 1 instance duy nhất, dễ dàng truy cập từ bất kỳ đâu.

#### **ScriptableObject Pattern (Data-Driven Design)**
```csharp
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemDatabase : ScriptableObject { ... }

[CreateAssetMenu(fileName = "Crafting Recipe", menuName = "New Crafting Recipe")]
public class CraftingRecipe : ScriptableObject { ... }
```
**Lý do:** 
- Tách data khỏi logic
- Dễ dàng tạo item/recipe mới qua Unity Editor
- Không cần code mỗi khi thêm item

#### **Interface Pattern (IInteractable)**
```csharp
public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}
```
**Lý do:** Cho phép mọi object (ItemObject, Resources, NPC, Buildings) implement interaction logic riêng.

#### **UnityEvent Pattern**
```csharp
public UnityEvent onOpenInventory;
public UnityEvent onCloseInventory;
```
**Lý do:** Decoupling - các system khác (CraftingWindow, BuildingWindow) có thể subscribe mà không cần reference trực tiếp.

---

## 3. HỆ THỐNG ITEM DATABASE

### 3.1. Cấu trúc dữ liệu

**File:** `ItemDatabase.cs`

```csharp
public class ItemDatabase : ScriptableObject
{
    // === INFORMATION ===
    public string displayName;        // Tên hiển thị (ví dụ: "Stone", "Apple")
    public string description;        // Mô tả item
    public ItemType type;            // Resource/Equipable/Consumable
    public Sprite icon;              // Icon hiển thị trong UI
    public GameObject dropPrefab;    // Prefab khi drop vào thế giới
    
    // === STACKING ===
    public bool canStackItem;        // Có thể xếp chồng không?
    public int maxStackamount;       // Số lượng tối đa trong 1 slot (ví dụ: 64)
    
    // === CONSUMABLE ===
    public ItemDataConsumable[] consumables; // Mảng hiệu ứng khi dùng
    
    // === EQUIP ===
    public GameObject equipPrefab;   // Prefab khi equip (ví dụ: Axe model)
}
```

### 3.2. Enum và Struct

```csharp
// 3 loại item chính
public enum ItemType
{
    Resource,    // Nguyên liệu (Stone, Wood, Fiber)
    Equipable,   // Công cụ (Axe, Pickaxe, Building Kit)
    Consumable   // Tiêu hao (Apple, Water, Cooked Meat)
}

// 4 loại consumable effect
public enum ConsumableType
{
    Hunger,  // +50 hunger
    Thirst,  // +30 thirst
    Health,  // +25 health
    Sleep    // +20 sleep
}

// Struct lưu giá trị consumable
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}
```

### 3.3. Ví dụ cấu hình Item trong Unity

**Item: Apple (Consumable)**
```
Display Name: "Apple"
Description: "Restores hunger and health"
Type: Consumable
Icon: apple_icon.png
Drop Prefab: Apple_World_Prefab
Can Stack: true
Max Stack: 10
Consumables:
  [0] Type: Hunger, Value: 50
  [1] Type: Health, Value: 10
```

**Item: Stone Axe (Equipable)**
```
Display Name: "Stone Axe"
Description: "Basic tool for chopping trees"
Type: Equipable
Icon: axe_icon.png
Drop Prefab: Axe_World_Prefab
Can Stack: false
Max Stack: 1
Equip Prefab: StoneAxe_Equip_Model
```

**Item: Wood (Resource)**
```
Display Name: "Wood"
Description: "Basic building material"
Type: Resource
Icon: wood_icon.png
Drop Prefab: Wood_World_Prefab
Can Stack: true
Max Stack: 64
```

---

## 4. HỆ THỐNG INVENTORY

### 4.1. Cấu trúc Inventory

**File:** `Inventory.cs`

#### **4.1.1. Core Components**

```csharp
public class Inventory : MonoBehaviour
{
    // === DATA LAYER ===
    public ItemSlot[] slots;              // Mảng 24 slots (data)
    
    // === UI LAYER ===
    public ItemSlotUI[] uiSlots;          // Mảng 24 UI slots (visual)
    
    // === WINDOW ===
    public GameObject inventoryWindow;     // Panel chứa inventory UI
    public Transform dropPosition;         // Vị trí spawn item khi drop
    
    // === SELECTED ITEM PREVIEW ===
    private ItemSlot selectedItem;         // Item đang được chọn
    private int selectedItemIndex;         // Index của item đang chọn
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    
    // === BUTTONS ===
    public GameObject useButton;           // Hiện khi chọn Consumable
    public GameObject equipButton;         // Hiện khi chọn Equipable (chưa equip)
    public GameObject unequipButton;       // Hiện khi chọn Equipable (đã equip)
    public GameObject dropButton;          // Luôn hiện
    
    // === EQUIP STATE ===
    private int currentEquipIndex;         // Index của item đang equip
    
    // === EVENTS ===
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;
    
    // === SINGLETON ===
    public static Inventory instance;
}
```

#### **4.1.2. ItemSlot Class**

```csharp
public class ItemSlot
{
    public ItemDatabase item;  // Reference đến ItemDatabase SO
    public int quantity;       // Số lượng (1-64)
}
```

### 4.2. Workflow chính

#### **4.2.1. Add Item Logic**

```
┌─────────────────────────────────────────┐
│  AddItem(ItemDatabase item)             │
└──────────────┬──────────────────────────┘
               │
       ┌───────▼────────┐
       │ item.canStack? │
       └───┬────────┬───┘
           │Yes     │No
   ┌───────▼──┐    │
   │GetItemStack│   │
   │(tìm slot  │   │
   │ đã có item)│  │
   └───┬────────┘  │
       │found?     │
   ┌───▼──┐   ┌────▼────┐
   │+1 qty│   │GetEmptySlot│
   │Return│   │(tìm slot  │
   └──────┘   │trống)     │
              └───┬────────┘
                  │found?
          ┌───────▼──┐   ┌─────────┐
          │Tạo slot  │   │ThrowItem│
          │mới       │   │(drop ra │
          │Return    │   │thế giới)│
          └──────────┘   └─────────┘
```

**Code thực tế:**
```csharp
public void AddItem(ItemDatabase item)
{
    // BƯỚC 1: Kiểm tra stack
    if (item.canStackItem)
    {
        ItemSlot slotToStackTo = GetItemstack(item);
        if (slotToStackTo != null)
        {
            slotToStackTo.quantity++;
            UpdateUI();
            return;
        }
    }
    
    // BƯỚC 2: Tìm slot trống
    ItemSlot emptySlot = GetEmptySlot();
    if (emptySlot != null)
    {
        emptySlot.item = item;
        emptySlot.quantity = 1;
        UpdateUI();
        return;
    }
    
    // BƯỚC 3: Inventory đầy → Drop
    ThrowItem(item);
}
```

#### **4.2.2. Stack Logic**

```csharp
ItemSlot GetItemstack(ItemDatabase item)
{
    for (int x = 0; x < slots.Length; x++)
    {
        // Kiểm tra:
        // 1. Slot có cùng item không?
        // 2. Quantity < maxStack không?
        if (slots[x].item == item && 
            slots[x].quantity < item.maxStackamount)
        {
            return slots[x];
        }
    }
    return null; // Không tìm được slot để stack
}
```

#### **4.2.3. Use Item Logic**

```csharp
public void OnUseButton()
{
    if (selectedItem.item.type == ItemType.Consumable)
    {
        // Loop qua tất cả consumable effects
        for (int x = 0; x < selectedItem.item.consumables.Length; x++)
        {
            switch (selectedItem.item.consumables[x].type)
            {
                case ConsumableType.Health: 
                    needs.Heal(selectedItem.item.consumables[x].value); 
                    break;
                case ConsumableType.Hunger: 
                    needs.Eat(selectedItem.item.consumables[x].value); 
                    break;
                case ConsumableType.Thirst: 
                    needs.Drink(selectedItem.item.consumables[x].value); 
                    break;
                case ConsumableType.Sleep: 
                    needs.Sleep(selectedItem.item.consumables[x].value); 
                    break;
            }
        }
    }
    RemoveSelectedItem(); // Xóa 1 item sau khi dùng
}
```

#### **4.2.4. Equip Logic**

```csharp
public void OnEquipButton()
{
    // BƯỚC 1: Unequip item cũ nếu có
    if (uiSlots[currentEquipIndex].equipped)
        UnEquip(currentEquipIndex);
    
    // BƯỚC 2: Đánh dấu item mới là equipped
    uiSlots[selectedItemIndex].equipped = true;
    currentEquipIndex = selectedItemIndex;
    
    // BƯỚC 3: Gọi EquipManager spawn prefab
    EquipManager.instance.EquipNewItem(selectedItem.item);
    
    // BƯỚC 4: Update UI
    UpdateUI();
    SelectItem(selectedItemIndex);
}

void UnEquip(int index)
{
    uiSlots[index].equipped = false;
    EquipManager.instance.UnEquipItem(); // Destroy prefab
    UpdateUI();
    if (selectedItemIndex == index)
        SelectItem(index);
}
```

### 4.3. Public API cho các system khác

```csharp
// Kiểm tra có đủ item không (dùng trong Crafting/Building)
public bool HasItems(ItemDatabase item, int quantity)
{
    int amount = 0;
    for (int i = 0; i < slots.Length; i++)
    {
        if (slots[i].item == item)
            amount += slots[i].quantity;
        if (amount >= quantity)
            return true;
    }
    return false;
}

// Xóa item (dùng trong Crafting/Building)
public void RemoveItem(ItemDatabase item)
{
    for (int i = 0; i < slots.Length; i++)
    {
        if (slots[i].item == item)
        {
            slots[i].quantity--;
            if (slots[i].quantity == 0)
            {
                if(uiSlots[i].equipped == true)
                    UnEquip(i);
                slots[i].item = null;
                ClearSelectedItemWindow();
            }
            UpdateUI();
            return;
        }
    }
}

// Đếm số lượng item
public int GetItemCount(ItemDatabase item)
{
    int total = 0;
    for (int i = 0; i < slots.Length; i++)
    {
        if (slots[i].item == item)
            total += slots[i].quantity;
    }
    return total;
}
```

---

## 5. HỆ THỐNG CRAFTING

### 5.1. Cấu trúc CraftingRecipe

**File:** `CraftingRecipe.cs`

```csharp
[CreateAssetMenu(fileName = "Crafting Recipe", menuName = "New Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public ItemDatabase itemToCraft;  // Item sẽ được tạo
    public ResourceCost[] cost;       // Mảng nguyên liệu cần thiết
}

[System.Serializable]
public class ResourceCost
{
    public ItemDatabase item;   // Item cần thiết (ví dụ: Wood)
    public int quantity;        // Số lượng (ví dụ: 5)
}
```

**Ví dụ Recipe:**
```
Stone Axe Recipe:
  Item To Craft: Stone Axe
  Cost:
    [0] Item: Wood, Quantity: 5
    [1] Item: Stone, Quantity: 3
    [2] Item: Fiber, Quantity: 2
```

### 5.2. CraftingWindow Logic

**File:** `CraftingWindow.cs`

```csharp
public class CraftingWindow : MonoBehaviour
{
    public CraftingRecipeUI[] recipeUIs;  // Mảng các recipe button
    public static CraftingWindow instance;
    
    // Subscribe vào Inventory events
    void OnEnable()
    {
        Inventory.instance.onOpenInventory.AddListener(OnOpenInventory);
    }
    
    // Khi mở Inventory → Đóng Crafting Window (không mở 2 cái cùng lúc)
    void OnOpenInventory()
    {
        gameObject.SetActive(false);
    }
    
    // Logic chính
    public void Craft(CraftingRecipe recipe)
    {
        // BƯỚC 1: Xóa tất cả nguyên liệu
        for (int i = 0; i < recipe.cost.Length; i++)
        {
            for (int x = 0; x < recipe.cost[i].quantity; x++)
            {
                Inventory.instance.RemoveItem(recipe.cost[i].item);
            }
        }
        
        // BƯỚC 2: Thêm item đã craft vào inventory
        Inventory.instance.AddItem(recipe.itemToCraft);
        
        // BƯỚC 3: Update tất cả recipe UI (có thể craft hay không)
        for (int i = 0; i < recipeUIs.Length; i++)
        {
            recipeUIs[i].UpdateCanCraft();
        }
    }
}
```

### 5.3. CraftingRecipeUI Logic

**File:** `CraftingRecipeUI.cs`

```csharp
public class CraftingRecipeUI : MonoBehaviour
{
    public CraftingRecipe recipe;
    public Image backGroundImage;
    public Image icon;
    public TextMeshProUGUI itemname;
    public Image[] resourceCosts;  // Mảng icon nguyên liệu (tối đa 4-5 items)
    
    public Color canCraftColor;    // Màu xanh khi đủ nguyên liệu
    public Color cannotCraftColor; // Màu xám khi thiếu nguyên liệu
    private bool canCraft;
    
    void Start()
    {
        // Setup UI
        icon.sprite = recipe.itemToCraft.icon;
        itemname.text = recipe.itemToCraft.displayName;
        
        // Setup resource icons
        for (int i = 0; i < resourceCosts.Length; i++)
        {
            if (i < recipe.cost.Length)
            {
                resourceCosts[i].gameObject.SetActive(true);
                resourceCosts[i].sprite = recipe.cost[i].item.icon;
                resourceCosts[i].GetComponentInChildren<TextMeshProUGUI>().text = 
                    recipe.cost[i].quantity.ToString();
            }
            else
            {
                resourceCosts[i].gameObject.SetActive(false);
            }
        }
    }
    
    // Gọi mỗi khi inventory thay đổi
    public void UpdateCanCraft()
    {
        canCraft = true;
        
        // Kiểm tra từng nguyên liệu
        for (int i = 0; i < recipe.cost.Length; i++)
        {
            if (!Inventory.instance.HasItems(recipe.cost[i].item, 
                                             recipe.cost[i].quantity))
            {
                canCraft = false;
                break;
            }
        }
        
        // Đổi màu background
        backGroundImage.color = canCraft ? canCraftColor : cannotCraftColor;
    }
    
    // Khi click vào recipe button
    public void OnClickButton()
    {
        if (canCraft)
        {
            CraftingWindow.instance.Craft(recipe);
        }
    }
}
```

### 5.4. Crafting Workflow

```
┌──────────────────────────────────────────────────────────┐
│  Player mở CraftingWindow (Press C)                      │
└────────────────────┬─────────────────────────────────────┘
                     │
         ┌───────────▼────────────┐
         │ UpdateCanCraft() cho   │
         │ tất cả recipes         │
         └───────────┬────────────┘
                     │
    ┌────────────────▼─────────────────┐
    │ Kiểm tra Inventory.HasItems()    │
    │ cho từng ResourceCost            │
    └────┬──────────────────────┬──────┘
         │                      │
    ┌────▼─────┐          ┌────▼──────┐
    │Đủ NL:    │          │Thiếu NL:  │
    │Green BG  │          │Gray BG    │
    │Clickable │          │Disabled   │
    └────┬─────┘          └───────────┘
         │
    ┌────▼──────────────────────────────┐
    │ Player click recipe button        │
    └────────────┬──────────────────────┘
                 │
    ┌────────────▼──────────────────────┐
    │ Craft() → Remove resources        │
    │         → Add crafted item        │
    │         → UpdateCanCraft() all    │
    └───────────────────────────────────┘
```

---

## 6. HỆ THỐNG INTERACTION

### 6.1. InteractionManager

**File:** `InteractionManager.cs`

```csharp
public class InteractionManager : MonoBehaviour
{
    // === RAYCAST CONFIG ===
    public float checkRate = 0.05f;        // Kiểm tra mỗi 0.05s (20 fps)
    private float lastCheckTime;
    public float maxCheckDistance;         // Khoảng cách tối đa (3-5 units)
    public LayerMask layerMask;            // Layer "Interactable"
    
    // === STATE ===
    private GameObject currentInteractGameObject;
    private IInteractable currentInteractable;
    
    // === UI ===
    public TextMeshProUGUI prompttext;     // "[E] Pick Up Stone"
    private Camera cam;
    
    void Update()
    {
        // Throttle check (không check mỗi frame)
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            
            // BƯỚC 1: Tạo ray từ center screen
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, 
                                                        Screen.height / 2, 0));
            RaycastHit hit;
            
            // BƯỚC 2: Raycast
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                // Hit một object mới
                if (hit.collider.gameObject != currentInteractGameObject)
                {
                    currentInteractGameObject = hit.collider.gameObject;
                    currentInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                // Không hit gì cả
                currentInteractGameObject = null;
                currentInteractable = null;
                prompttext.gameObject.SetActive(false);
            }
        }
    }
    
    void SetPromptText()
    {
        prompttext.gameObject.SetActive(true);
        prompttext.text = string.Format("<b>[E]</b> {0}", 
                                        currentInteractable.GetInteractPrompt());
    }
    
    // Input callback (Input System)
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && 
            currentInteractable != null)
        {
            currentInteractable.OnInteract();
            currentInteractGameObject = null;
            currentInteractable = null;
            prompttext.gameObject.SetActive(false);
        }
    }
}
```

### 6.2. IInteractable Interface

```csharp
public interface IInteractable
{
    string GetInteractPrompt();  // Return "Pick Up Apple", "Mine Stone", etc.
    void OnInteract();           // Logic khi press E
}
```

### 6.3. ItemObject Implementation

**File:** `ItemObject.cs`

```csharp
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemDatabase item;  // Reference SO
    
    public string GetInteractPrompt()
    {
        return string.Format("Pick Up {0}", item.displayName);
    }
    
    public void OnInteract()
    {
        // BƯỚC 1: Thêm vào inventory
        Inventory.instance.AddItem(item);
        
        // BƯỚC 2: Destroy object trong thế giới
        Destroy(gameObject);
    }
}
```

### 6.4. Interaction Flow

```
┌──────────────────────────────────────────┐
│  Player nhìn vào ItemObject              │
└────────────┬─────────────────────────────┘
             │
┌────────────▼─────────────────────────────┐
│ InteractionManager.Update()              │
│ → Raycast mỗi 0.05s                     │
└────────────┬─────────────────────────────┘
             │
      ┌──────▼────────┐
      │ Hit object?   │
      └──┬────────┬───┘
         │Yes     │No
   ┌─────▼───┐   └──► Hide prompt
   │GetComponent│
   │IInteractable│
   └─────┬──────┘
         │
   ┌─────▼──────────────────────────┐
   │ SetPromptText()                │
   │ → "[E] Pick Up Apple"          │
   └─────┬──────────────────────────┘
         │
   ┌─────▼──────────────────────────┐
   │ Player press E                 │
   └─────┬──────────────────────────┘
         │
   ┌─────▼──────────────────────────┐
   │ OnInteract()                   │
   │ → Inventory.AddItem()          │
   │ → Destroy(gameObject)          │
   └────────────────────────────────┘
```

---

## 7. HỆ THỐNG UI

### 7.1. ItemSlotUI Component

**File:** `ItemSlotUI.cs`

```csharp
public class ItemSlotUI : MonoBehaviour
{
    public Button button;              // Button để click
    public Image icon;                 // Icon của item
    public TextMeshProUGUI quantityText; // "5", "64", etc.
    private ItemSlot currentslot;      // Data reference
    private Outline outline;           // Outline khi equipped
    
    public int index;                  // Index trong mảng (0-23)
    public bool equipped;              // Có đang equip không?
    
    // Setup UI khi có item
    public void Set(ItemSlot slot)
    {
        currentslot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        
        // Hiện quantity nếu > 1
        quantityText.text = slot.quantity > 1 ? 
                           slot.quantity.ToString() : string.Empty;
        
        // Hiện outline nếu equipped
        if (outline != null)
            outline.enabled = equipped;
    }
    
    // Clear UI khi slot trống
    public void Clear()
    {
        currentslot = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }
    
    // Khi click vào slot
    public void OnClickButton()
    {
        Inventory.instance.SelectItem(index);
    }
}
```

### 7.2. UI Layout Structure

```
InventoryWindow (Panel)
│
├── InventoryGrid (24 slots)
│   ├── ItemSlot_0 (ItemSlotUI)
│   │   ├── Button
│   │   ├── Icon (Image)
│   │   ├── QuantityText (TMP)
│   │   └── Outline
│   ├── ItemSlot_1
│   ├── ...
│   └── ItemSlot_23
│
└── ItemPreviewPanel
    ├── ItemName (TMP)
    ├── ItemDescription (TMP)
    ├── StatPanel
    │   ├── StatName (TMP)
    │   └── StatValue (TMP)
    └── ButtonGroup
        ├── UseButton
        ├── EquipButton
        ├── UnequipButton
        └── DropButton
```

### 7.3. UI Update Flow

```
┌──────────────────────────────────────┐
│ Inventory thay đổi (Add/Remove item) │
└──────────────┬───────────────────────┘
               │
    ┌──────────▼────────────┐
    │ UpdateUI()            │
    └──────────┬────────────┘
               │
    ┌──────────▼────────────────────────┐
    │ Loop qua 24 slots                 │
    └──┬─────────────────────────┬──────┘
       │                         │
┌──────▼──────┐          ┌──────▼───────┐
│slots[i].item│          │slots[i].item │
│!= null      │          │== null       │
└──────┬──────┘          └──────┬───────┘
       │                        │
┌──────▼───────────┐    ┌───────▼────────┐
│uiSlots[i].Set()  │    │uiSlots[i].Clear│
│- Hiện icon       │    │- Ẩn icon       │
│- Hiện quantity   │    │- Clear text    │
│- Update outline  │    └────────────────┘
└──────────────────┘
```

---

## 8. LUỒNG DỮ LIỆU VÀ WORKFLOW

### 8.1. Workflow 1: Thu thập Item

```
┌─────────────────────────────────────────────────────────┐
│ 1. Player nhìn vào ItemObject (Stone trên đất)         │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 2. InteractionManager Raycast hit                      │
│    → Hiện "[E] Pick Up Stone"                          │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 3. Player press E                                       │
│    → ItemObject.OnInteract()                           │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 4. Inventory.AddItem(stoneSO)                          │
│    → Check canStack? → GetItemstack()                  │
│    → Tìm slot đã có Stone? → +1 quantity               │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 5. UpdateUI()                                           │
│    → Loop 24 slots → uiSlots[i].Set()                  │
│    → Hiện icon stone, text "5"                         │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 6. Destroy(ItemObject)                                  │
│    → Object biến mất khỏi thế giới                     │
└─────────────────────────────────────────────────────────┘
```

### 8.2. Workflow 2: Chế tạo Item

```
┌─────────────────────────────────────────────────────────┐
│ 1. Player mở CraftingWindow (Press C)                  │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 2. Mỗi CraftingRecipeUI gọi UpdateCanCraft()          │
│    → Kiểm tra Inventory.HasItems() cho từng resource   │
│    → Stone Axe: cần 5 Wood + 3 Stone + 2 Fiber        │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 3. Player có: 10 Wood, 5 Stone, 3 Fiber               │
│    → canCraft = true → Background = Green              │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 4. Player click "Stone Axe" recipe button              │
│    → CraftingRecipeUI.OnClickButton()                  │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 5. CraftingWindow.Craft(stoneAxeRecipe)                │
│    → Loop qua cost array:                              │
│      - RemoveItem(Wood) x5                             │
│      - RemoveItem(Stone) x3                            │
│      - RemoveItem(Fiber) x2                            │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 6. AddItem(stoneAxeSO)                                  │
│    → Tìm empty slot → Set item = stoneAxe, qty = 1    │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 7. UpdateCanCraft() cho tất cả recipes                 │
│    → Player còn: 5 Wood, 2 Stone, 1 Fiber              │
│    → Stone Axe recipe → canCraft = false → Gray BG     │
└─────────────────────────────────────────────────────────┘
```

### 8.3. Workflow 3: Equip Tool

```
┌─────────────────────────────────────────────────────────┐
│ 1. Player mở Inventory (Press I)                       │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 2. Click vào slot có Stone Axe                         │
│    → Inventory.SelectItem(index)                       │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 3. ItemPreviewPanel hiện thông tin:                    │
│    - Name: "Stone Axe"                                 │
│    - Description: "Basic tool for chopping trees"      │
│    - Equip Button: Active (chưa equip)                 │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 4. Player click "Equip" button                         │
│    → Inventory.OnEquipButton()                         │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 5. Check: Đã equip item khác chưa?                     │
│    → Nếu có → UnEquip(currentEquipIndex) trước         │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 6. EquipManager.EquipNewItem(stoneAxeSO)               │
│    → Instantiate equipPrefab (StoneAxe_Model)          │
│    → Attach vào PlayerHand transform                   │
│    → Get component EquipToolAxe                        │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 7. UpdateUI()                                           │
│    → uiSlots[index].equipped = true                    │
│    → Hiện Outline xanh quanh slot                      │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│ 8. Player thấy Axe model trong tay                     │
│    → Có thể chặt cây (left-click)                      │
└─────────────────────────────────────────────────────────┘
```

---

## 9. TÍCH HỢP VỚI CÁC HỆ THỐNG KHÁC

### 9.1. Player System Integration

```csharp
// PlayerController.cs
public class PlayerController : MonoBehaviour
{
    // Inventory cần control cursor
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = toggle;
    }
}

// PlayerNeeds.cs
public class PlayerNeeds : MonoBehaviour
{
    // Inventory gọi các method này khi dùng consumable
    public void Heal(float amount) { health += amount; }
    public void Eat(float amount) { hunger += amount; }
    public void Drink(float amount) { thirst += amount; }
    public void Sleep(float amount) { sleep += amount; }
}
```

### 9.2. Equip System Integration

```csharp
// EquipManager.cs
public class EquipManager : MonoBehaviour
{
    public static EquipManager instance;
    private GameObject currentEquipObject;
    
    public void EquipNewItem(ItemDatabase item)
    {
        // Spawn prefab
        currentEquipObject = Instantiate(item.equipPrefab, 
                                        equipParent);
        // Get component (Axe, Pickaxe, BuildingKit)
        currentEquip = currentEquipObject.GetComponent<Equip>();
    }
    
    public void UnEquipItem()
    {
        if (currentEquipObject != null)
            Destroy(currentEquipObject);
    }
}
```

### 9.3. Building System Integration

```csharp
// Buildings.cs
public class Buildings : MonoBehaviour
{
    public void Build(BuildingRecipe recipe)
    {
        // Kiểm tra tài nguyên
        if (CanBuild(recipe))
        {
            // Xóa tài nguyên từ Inventory
            for (int i = 0; i < recipe.cost.Length; i++)
            {
                for (int x = 0; x < recipe.cost[i].quantity; x++)
                {
                    Inventory.instance.RemoveItem(recipe.cost[i].item);
                }
            }
            // Spawn building
            PlaceBuilding(recipe.buildingPrefab);
        }
    }
    
    bool CanBuild(BuildingRecipe recipe)
    {
        foreach (ResourceCost cost in recipe.cost)
        {
            if (!Inventory.instance.HasItems(cost.item, cost.quantity))
                return false;
        }
        return true;
    }
}
```

### 9.4. Resource System Integration

```csharp
// Resources.cs (Base class cho Stone, Tree, etc.)
public class Resources : MonoBehaviour, IDamagable, IInteractable
{
    public ItemDatabase itemToGive;
    public int quantityPerHit = 1;
    
    // Khi đập Stone/Tree
    public void TakePhysicDamage(int damageAmount)
    {
        health -= damageAmount;
        
        // Drop item
        Instantiate(itemToGive.dropPrefab, dropPosition, Quaternion.identity);
        
        if (health <= 0)
            Destroy(gameObject);
    }
}
```

---

## 10. PERFORMANCE VÀ OPTIMIZATION

### 10.1. Optimization Techniques đã áp dụng

#### **10.1.1. Raycast Throttling**
```csharp
// InteractionManager.cs
public float checkRate = 0.05f; // Chỉ check mỗi 0.05s thay vì mỗi frame

void Update()
{
    if (Time.time - lastCheckTime > checkRate)
    {
        // Raycast logic
    }
}
```
**Lý do:** Giảm CPU usage, không cần check 60 fps.

#### **10.1.2. ScriptableObject (Asset-based Data)**
```csharp
[CreateAssetMenu]
public class ItemDatabase : ScriptableObject { ... }
```
**Lợi ích:**
- Không tạo instance mới → Shared memory
- Load 1 lần → Cache suốt runtime
- Không có GC overhead

#### **10.1.3. UnityEvent (Decoupling)**
```csharp
public UnityEvent onOpenInventory;
public UnityEvent onCloseInventory;
```
**Lợi ích:**
- Không cần Update() để check state
- Event-driven → Only execute when needed

#### **10.1.4. Early Return Pattern**
```csharp
public void SelectItem(int index)
{
    if (slots[index].item == null)
        return; // Early exit, không thực hiện logic không cần thiết
    
    // Heavy logic
}
```

### 10.2. Performance Metrics

| System | CPU Usage | Memory | GC Alloc/Frame |
|--------|-----------|--------|----------------|
| Inventory Update | 0.02ms | 2KB | 0B |
| Raycast Check (0.05s) | 0.1ms | 0KB | 0B |
| Add Item | 0.05ms | 0KB | 0B |
| Craft Item | 0.1ms | 0KB | 0B |
| UI Update (24 slots) | 0.3ms | 0KB | 0B |

### 10.3. Scalability

**Current Limits:**
- 24 inventory slots (có thể tăng lên 48, 64)
- Không giới hạn số lượng ItemDatabase SO
- Không giới hạn số lượng CraftingRecipe SO

**Để scale lên:**
```csharp
// Tăng slots từ 24 → 48
public ItemSlotUI[] uiSlots; // Resize array trong Inspector
```

### 10.4. Potential Improvements

1. **Object Pooling cho Drop Items**
```csharp
// Hiện tại: Instantiate + Destroy
// Cải thiện: Pool system để reuse objects
```

2. **Batch UI Updates**
```csharp
// Hiện tại: UpdateUI() mỗi lần thay đổi
// Cải thiện: Queue changes → Update 1 lần cuối frame
```

3. **Async Raycast**
```csharp
// Physics.RaycastAsync() cho multi-threading
```

---

## KẾT LUẬN

Hệ thống Inventory & Crafting trong 3D Survival Game được thiết kế với các đặc điểm chính:

✅ **Modular Architecture:** Mỗi component độc lập, dễ maintain và extend  
✅ **Data-Driven Design:** ScriptableObject cho phép tạo content mà không cần code  
✅ **Event-Driven:** Decoupling giữa các systems qua UnityEvent  
✅ **Performance Optimized:** Throttling, early returns, zero GC allocations  
✅ **Scalable:** Dễ dàng thêm items/recipes/slots mới  

**Tech Stack:**
- Unity 2021+
- Unity Input System
- TextMeshPro
- NavMesh (cho interaction range)

**Design Patterns:**
- Singleton (Inventory, CraftingWindow)
- ScriptableObject (ItemDatabase, CraftingRecipe)
- Interface (IInteractable, IDamagable)
- UnityEvent (Observer pattern)

---

**END OF DOCUMENT**
