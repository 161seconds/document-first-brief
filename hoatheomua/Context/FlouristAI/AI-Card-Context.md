# AI Card Context - HTM_Flourist_AI

> Document First Project: hoa-theo-mua-ai-customize
> Date: 2026-08-27
> Last Updated: 2026-08-27

---

## MỤC LỤC

1. [Tổng quan](#1-tổng-quan)
2. [Luồng nghiệp vụ](#2-luồng-nghiệp-vụ)
3. [Database Schema](#3-database-schema)
4. [Quy tắc nghiệp vụ](#4-quy-tắc-nghiệp-vụ)
5. [Quota Rules](#5-quota-rules)
6. [Pricing Rules](#6-pricing-rules)
7. [API Endpoints](#7-api-endpoints)

---

## 1. TỔNG QUAN

### Mô tả
Hệ thống thiệp AI cho phép khách hàng tạo thiệp cá nhân hóa bằng AI trong quá trình Checkout.

### Luồng chính
```
┌─────────────────────────────────────────────────────────────────────────────┐
│                         CARD FLOW                                          │
├─────────────────────────────────────────────────────────────────────────────┤
│  Checkout (từ Flower Flow)                                                 │
│      │                                                                      │
│      ▼                                                                      │
│  [STORY-035] "Tạo thiệp AI" / "Tạo lại" (trong Checkout)                 │
│      │                                                                      │
│      ▼                                                                      │
│  Thiệp AI + client_histories                                               │
│      │                                                                      │
│      ▼                                                                      │
│  Xác nhận thiệp → Gắn vào Checkout                                        │
│      │                                                                      │
│      ▼                                                                      │
│  Order với thiệp đã chọn                                                   │
└─────────────────────────────────────────────────────────────────────────────┘
```

### Stories liên quan
| Story | Tên | Sprint | Priority | Status |
|-------|-----|--------|----------|--------|
| **STORY-035** | Tạo thiệp thiết kế | 1 | Must | Draft |
| **STORY-036** | Tạo lại thiệp từ lịch sử | 1 | Must | Draft |

---

## 2. LUỒNG NGHIỆP VỤ

### STORY-035: Tạo thiệp thiết kế

#### Metadata
- **Story**: Là một khách hàng đã đăng nhập, tôi muốn tạo thiệp mới tại Checkout, để có một mẫu thiệp cá nhân hóa phù hợp với đơn hàng của mình.
- **Context**: Khách hàng có thể tạo thiệp mới tại Checkout.
- **Sprint**: 1
- **Priority**: Must

#### Điều kiện

**Preconditions:**
- Khách hàng đã đăng nhập
- Checkout phải tồn tại, thuộc khách hàng hiện tại và chưa hoàn tất
- Có ít nhất 01 Template và 01 Size đang ở trạng thái khả dụng từ Core DB

**Trigger:**
Khách hàng chọn "Tạo thiệp" tại Checkout.

#### Input Data - Thông tin bắt buộc
| Trường | Giới hạn |
|--------|----------|
| Người gửi (sender_name) | tối đa 20 từ |
| Người nhận (receiver_name) | tối đa 20 từ |
| Lời chúc (message_content) | tối đa 100 từ |
| Template (card_template_id) | bắt buộc - chọn từ card_templates |
| Size (size_key) | bắt buộc - chọn từ Config group="card_size" |
| Hình thức (form_type) | Calligraphy hoặc Gõ máy |

#### Ảnh đính kèm
- Không bắt buộc
- Tối đa 01 ảnh
- Định dạng: PNG, JPG
- Dung lượng tối đa: 10 MB

#### Thông tin bổ sung cho AI Module
- **card_size**: Thông tin size được chọn từ Config (name, width, height, base_price, max_words)
- **card_config**: Thông tin calligraphy config từ Config (min_words, max_words, extra_price)
- **flower_info**: Thông tin bó hoa mà thiệp dựa vào:
  - Nếu dựa vào bó hoa bình thường: image_url của product
  - Nếu dựa vào bó hoa AI: image_url của generated_flowers

### STORY-036: Tạo lại thiệp từ lịch sử

#### Metadata
- **Story**: Là một khách hàng đã đăng nhập, tôi muốn tạo lại từ một thiệp đã có, để có một mẫu thiệp cá nhân hóa phù hợp với nhu cầu của mình.
- **Context**: Khách hàng có thể chọn "Tạo lại" tại màn Chi tiết thiệp đã tạo.
- **Sprint**: 1
- **Priority**: Must

#### Đặc điểm
- Sử dụng nguyên dữ liệu đầu vào của lần generate hiện tại
- Không mở form chỉnh sửa
- Kết quả mới không tự động gắn vào Checkout

---

## 3. DATABASE SCHEMA

### 3.1 Enums

```sql
Enum form_type {
  go_may       -- Gõ máy
  calligraphy  -- Calligraphy
}

Enum generation_type {
  flower
  card
  post
}

Enum system_prompt_types {
  flower
  card
  post
}
```

### 3.2 Tables

#### Config (Bảng có sẵn - sử dụng cho card configs)

**Quy tắc sử dụng field:**
- `is_public`: true = đang hoạt động (người dùng có thể chọn), false = không hoạt động
- `is_deleted`: true = đã xóa mềm, false = chưa xóa

```sql
Table Config {
  id uuid [pk]
  key varchar                    -- Key định danh: "size_A", "size_B", "calligraphy_words"
  value jsonb                   -- JSON chứa cấu hình chi tiết
  is_public boolean              -- Dùng để xác định config có đang hoạt động để người dùng chọn
  group varchar                  -- "card_size" hoặc "card_config"
  kind varchar                   -- "Setting" cho card configs
  user_id guid
  created_at timestamp
  updated_at timestamp
  is_deleted boolean            -- Dùng cho xóa mềm
}
```

**Group values:**
- `card_size` - cấu hình size thiệp
- `card_config` - cấu hình calligraphy words

**Kind values:**
- `Setting` - cho card configs

#### system_prompts

Prompt hệ thống cho từng loại generation.

```sql
Table system_prompts {
  id uuid [pk]
  type system_prompt_types      -- flower, card, post
  content text
}
```

#### card_templates

Template dùng để tạo card.

```sql
Table card_templates {
  id uuid [pk]
  name varchar
  description text
  character_limit int
  image_url varchar
  metadata json                 -- Thông tin template cho AI
  created_at timestamp
  updated_at timestamp
  is_deleted boolean
}
```

#### generated_cards

Card được tạo ra từ AI.

```sql
Table generated_cards {
  id uuid [pk]
  content text                  -- Nội dung thiệp
  image_url varchar             -- URL ảnh thiệp
  user_id uuid
  created_at timestamp

  -- Thông tin thiệp:
  form_type form_type           -- "go_may" hoặc "calligraphy"
  size_key varchar              -- Key của size: "size_A", "size_B"
  sender_name varchar           -- Người gửi
  receiver_name varchar         -- Người nhận
  message_content text          -- Lời chúc
  attached_image_url varchar     -- Ảnh đính kèm (nullable)

  -- Snapshot Size tại thời điểm tạo:
  size_name varchar              -- "A", "B", "C"
  size_width decimal             -- 10 (cm)
  size_height decimal            -- 15 (cm)
  size_base_price decimal       -- Giá base tại thời điểm đó: 100000
  size_max_words int            -- Giới hạn từ của size đó: 100

  -- Snapshot Word count tại thời điểm tạo:
  word_count int               -- Số từ thực tế: 60
  word_config_snapshot json     -- JSON lưu rule áp dụng: { "min_words": 36, "max_words": 70, "extra_price": 39000 }

  -- Trạng thái:
  is_confirmed boolean         -- Đã xác nhận gắn vào Checkout chưa (default: false)

  -- Giá đã tính toán:
  base_price decimal            -- = size_base_price
  extra_price decimal           -- Phụ phí Calligraphy = word_config_snapshot.extra_price
  total_price decimal           -- = base_price + extra_price
}
```

#### client_histories

Lịch sử thao tác của người dùng (dùng chung cho flower và card).

```sql
Table client_histories {
  id uuid [pk]
  user_id uuid                  -- Ai tạo history
  input text                    -- JSON string của request gốc
  type generation_type          -- "flower", "card", "post"
  system_prompt_id uuid
  metadata json                 -- Chứa thông tin phụ thuộc vào loại
  created_at timestamp

  -- base_id: polymorphic reference - xác định nguồn gốc
  --   - Nếu type = "card" và base là combo bình thường: reference đến product.id
  --   - Nếu type = "card" và base là bó hoa AI: reference đến generated_flowers.id
  --   - Nếu type = "flower": reference đến product.id (combo)
  base_id uuid

  -- output_id: polymorphic reference đến kết quả AI
  --   - Nếu type = "card": reference đến generated_cards.id
  --   - Nếu type = "flower": reference đến generated_flowers.id
  output_id uuid

  -- Refs:
  system_prompt_id -> system_prompts.id
}
```

### 3.3 Mối quan hệ giữa client_histories và Card

#### Trường hợp 1: Thiệp AI cho bó hoa bình thường
```
base_id = product.id (combo không có biến thể HOẶC biến thể là con của biến thể cha)
output_id = generated_cards.id
metadata = { ...card_template_info... }
```
**Ý nghĩa:** Thiệp AI được tạo ra cho bó hoa bình thường, dựa vào card_template mà người dùng chọn.

#### Trường hợp 2: Thiệp AI cho bó hoa AI
```
base_id = generated_flowers.id
output_id = generated_cards.id
metadata = { ...card_template_info... }
```
**Ý nghĩa:** Thiệp AI được tạo cho bó hoa AI, dựa vào card_template mà người dùng chọn.

### 3.4 Cấu trúc JSON

#### Size Config
```json
{
  "name": "A",
  "width": 10,
  "height": 15,
  "base_price": 100000,
  "max_words": 100
}
```

#### Calligraphy Words Config
```json
[
  { "min_words": 0, "max_words": 35, "extra_price": 0 },
  { "min_words": 36, "max_words": 70, "extra_price": 39000 },
  { "min_words": 71, "max_words": 100, "extra_price": 69000 }
]
```

#### Card Template Metadata
```json
{
  "template_id": "uuid",
  "template_name": "Tên template",
  "template_image_url": "url"
}
```

---

## 4. QUY TẮC NGHIỆP VỤ

### 4.1 Logic xác định base_id cho thiệp

```csharp
public Guid DetermineBaseId(Guid? flowerRequestId, Guid? generatedFlowerId, Product product)
{
    // Nếu có generatedFlowerId (bó hoa AI) -> base_id = generatedFlowerId
    if (generatedFlowerId.HasValue)
        return generatedFlowerId.Value;

    // Nếu không có generatedFlowerId (bó hoa bình thường)
    // -> base_id = product.id (combo không có biến thể HOẶC biến thể là con của biến thể cha)
    return product.Id;
}
```

### 4.2 Quy tắc đếm từ

- Một từ là một chuỗi ký tự liên tục được phân tách bởi khoảng trắng, tab hoặc ký tự xuống dòng
- Khoảng trắng ở đầu và cuối nội dung không được tính
- Nhiều khoảng trắng, tab hoặc ký tự xuống dòng liên tiếp được tính là một dấu phân tách
- Dấu câu đi liền với một từ không được tính thành từ riêng
- Nội dung chỉ gồm khoảng trắng được tính là 0 từ

### 4.3 Render nội dung thiệp

| Hình thức | Render Người gửi, Người nhận, Lời chúc | Ghi chú |
|-----------|-----------------------------------------|---------|
| Gõ máy | Hiển thị nguyên văn | AI giữ nguyên nội dung |
| Calligraphy | Không hiển thị trên ảnh | Lưu trong history, chỉ render khi in |

### 4.4 Ảnh đính kèm

- AI giữ nguyên nội dung gốc của ảnh
- Không cắt mất chủ thể, xoay, đổi màu, thêm/xóa hoặc làm biến dạng
- Được phép scale đồng dạng hoặc thêm khoảng đệm để phù hợp Template
- Ảnh kết quả lưu dưới định dạng PNG, không gắn logo

---

## 5. QUOTA RULES

### Giới hạn
| Loại | Giới hạn | Đơn vị |
|------|-----------|---------|
| Tạo thiệp | 10 lượt | /ngày/khách hàng |
| Tạo thiệp theo mẫu hoa | 3 lượt | /mẫu hoa nguồn |

### Quy trình Quota

1. **Check quota** → TRƯỚC khi gọi AI
2. **Trừ quota** → KHI CÓ KẾT QUẢ THÀNH CÔNG
3. **Hoàn quota** → KHI AI THẤT BẠI (sau retry)

### Retry Logic

- **2 lần retry** với interval **2 giây**
- Nếu vẫn thất bại sau retry → **Hoàn quota**
- Mỗi job chỉ hoàn quota tối đa **1 lần**

---

## 6. PRICING RULES

### 6.1 Lấy cấu hình từ bảng Config

```csharp
// Lấy cấu hình size
var sizeConfig = await _dbContext.Config
    .Where(c => c.Group == "card_size" && c.Key == $"size_{size}" && c.Kind == "Setting" && !c.IsDeleted && c.IsPublic)
    .FirstOrDefaultAsync();
// sizeConfig.Value: { "name": "A", "width": 10, "height": 15, "base_price": 100000, "max_words": 100 }

// Lấy cấu hình calligraphy words
var wordsConfig = await _dbContext.Config
    .Where(c => c.Group == "card_config" && c.Key == "calligraphy_words" && c.Kind == "Setting" && !c.IsDeleted && c.IsPublic)
    .FirstOrDefaultAsync();
// wordsConfig.Value: [ { "min_words": 0, "max_words": 35, "extra_price": 0 }, ... ]
```

### 6.2 Tính giá

```csharp
public async Task<decimal> CalculateExtraPriceAsync(string formType, int wordCount)
{
    if (formType != "calligraphy")
        return 0;

    var wordsConfig = await GetCalligraphyWordsConfigAsync();
    var rules = JsonSerializer.Deserialize<List<WordRule>>(wordsConfig.Value);

    var rule = rules.FirstOrDefault(r => wordCount >= r.MinWords && wordCount <= r.MaxWords);
    return rule?.ExtraPrice ?? 0;
}

// Total Price = base_price + extra_price
```

### 6.3 Ví dụ kết quả

**Input:**
- size: "A"
- form_type: "calligraphy"
- message_content: "Chúc bạn sinh nhật vui vẻ, hạnh phúc và thành công trong cuộc sống"

**Xử lý:**
1. Size config (Group="card_size", Key="size_A"): `{ "name": "A", "width": 10, "height": 15, "base_price": 100000, "max_words": 100 }`
2. Word count: 12
3. Word rule: `{ "min_words": 0, "max_words": 35, "extra_price": 0 }`

**Generated Card:**
```json
{
  "size_key": "size_A",
  "size_name": "A",
  "size_width": 10,
  "size_height": 15,
  "size_base_price": 100000,
  "size_max_words": 100,
  "word_count": 12,
  "word_config_snapshot": { "min_words": 0, "max_words": 35, "extra_price": 0 },
  "base_price": 100000,
  "extra_price": 0,
  "total_price": 100000
}
```

---

## 7. API ENDPOINTS

### TDD-035: Tạo thiệp mới
- **Method**: POST
- **Route**: `/api/ai-cards`

### TDD-036: Tạo lại thiệp từ History
- **Method**: POST
- **Route**: `/api/ai-cards/{id}/regenerate`

### Card Config CRUD (Sử dụng bảng Config có sẵn)
| TDD | Chức năng | Method | Route | Quyền |
|-----|-----------|--------|-------|--------|
| TDD-055 | Tạo card config | POST | `/api/v1/configs` | Admin |
| TDD-054 | Lấy danh sách | GET | `/api/v1/configs/content` | Admin, Staff |
| TDD-062 | Cập nhật card config | PUT | `/api/v1/configs/{id}` | Admin |
| TDD-056 | Xóa card config | DELETE | `/api/v1/configs/{id}` | Admin |

---

## Ghi chú quan trọng

- **`source_flower_id` đã được bỏ khỏi `generated_cards`** - mối quan hệ được xác định qua `client_histories`
- **thiệp không gắn logo** - ảnh kết quả được lưu dưới định dạng PNG mà không có logo
- **Snapshot giá** - tất cả thông tin ảnh hưởng đến giá được lưu tại thời điểm tạo thiệp
- **AI Module tách riêng** - các hàm xử lý logic gọi đến AI Module để thực hiện việc gọi AI
- **Input cho Card**: sender_name, receiver_name, message_content, form_type, size_key, card_template_id, attached_image_url, card_size (từ Config), card_config (từ Config), flower_info (image_url của product hoặc generated_flowers) |

---

## LIÊN KẾT DOCUMENT FIRST

### Stories (Draft)
- **STORY-035**: Tạo thiệp thiết kế
- **STORY-036**: Tạo lại thiệp từ lịch sử

### System Tests (Approved)
- ST-035-01-01: Tạo thiệp thành công
- ST-036-01-01: Tạo lại thiệp thành công từ History
- ST-036-05-01: Chặn Tạo lại khi hết quota

---

## Ghi chú quan trọng

- **`source_flower_id` đã được bỏ khỏi `generated_cards`** - mối quan hệ được xác định qua `client_histories`
- **thiệp không gắn logo** - ảnh kết quả được lưu dưới định dạng PNG mà không có logo
- **Snapshot giá** - tất cả thông tin ảnh hưởng đến giá được lưu tại thời điểm tạo thiệp
- **AI Module tách riêng** - các hàm xử lý logic gọi đến AI Module để thực hiện việc gọi AI
