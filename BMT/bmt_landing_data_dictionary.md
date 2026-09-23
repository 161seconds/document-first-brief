# BMT Decor — Data dictionary (từ điển dữ liệu) chính thức

## 1. Quy tắc bất biến

1. Page, route, section, thứ tự section và khung ảnh được fix cứng trong FE.
2. Admin chỉ sửa text, rich text và thay ảnh trong khung có sẵn.
3. Admin không được tạo/xóa page, section, group hoặc slot item cố định; không được đổi key, parent hoặc thứ tự render.
4. Admin không được nhập slug, URL điều hướng, route, tỷ lệ ảnh, width hoặc height. Ngoại lệ duy nhất: bốn dịch vụ trong Footer được chọn **tên trang** từ danh sách trang nội bộ cố định; DB không lưu URL điều hướng.
5. Admin không được sửa nội dung hoặc target của nút bấm; toàn bộ button label/action do FE cố định.
6. Dự án, Tin tức và Vị trí tuyển dụng là ba loại duy nhất được thêm/sửa/xóa record.
7. Slug Dự án/Tin tức do backend tự sinh từ title và unique toàn hệ thống.
8. Khi record còn tồn tại, slug cũ không bị xóa và truy cập slug cũ redirect 301 đến slug mới nhất. Xóa hẳn record sẽ xóa các slug của record.
9. Tin nổi bật trang Tin tức lấy từ `news.is_featured`; tin trên Trang chủ lấy từ `news.highlight_home`. Không lưu link/bản sao bài viết trong page JSON.
10. Admin gửi file ảnh; backend upload lên Cloudinary rồi chỉ lưu URL trả về vào DB. Admin không gõ URL ảnh hoặc nhập văn bản thay thế ảnh.
11. Nội dung của tất cả page được lưu trong **một bảng `pages`** theo cây key-value đệ quy (`parent_id`, `node_key`) kết hợp `value jsonb`. Các bảng còn lại giữ nguyên vai trò cũ.

## 2. Kiểu JSON dùng chung

Ví dụ JSON cho `ContactFormConfig` (cấu hình biểu mẫu liên hệ):

```json
{
  "title": "...",
  "subtitle": "...",
  "successMessage": "..."
}
```

Trường tùy chọn (có thể bỏ khỏi JSON): `subtitle`. Riêng form Báo giá có thêm `requiredMessage`, vì admin có ô sửa thông báo này.

Placeholder tên/số điện thoại và nhãn ô nhập thuộc cấu hình cố định của FE; các form thông thường chỉ lưu ba trường nội dung ở ví dụ trên. Form Báo giá lưu thêm `requiredMessage` do admin có ô sửa riêng. Chữ trên nút gửi vẫn cố định theo quy tắc cũ. FE hiện đặt tên prop hiển thị dòng phụ là `description`; khi nối API, mapper đọc `subtitle` từ DB và truyền vào prop đó, không cần đổi tên cột hoặc key DB.

Mỗi field ảnh trong JSON là một chuỗi URL Cloudinary, không có object ảnh hoặc field `alt`. Không lưu `width`, `height`, `ratio` hoặc `aspectRatio`; tỷ lệ/size/object-fit nằm trong component FE. URL do backend nhận từ Cloudinary sau khi upload, không phải chuỗi admin tự nhập.

---

# 3. Bảng `pages` (nội dung các trang) — cây key-value đệ quy + JSON

## 3.1 Cột

| Cột | Kiểu | Null | Ai được sửa | Mô tả |
|---|---|---:|---|---|
| `id` | `uuid` | Không | Không | PK của node; backend/seed cấp. |
| `parent_id` | `uuid` | Có | Không | FK tự tham chiếu `pages.id`; chỉ root có `NULL`. |
| `code` | `page_code` | Có | Không | Chỉ root có code, unique theo root; FE dùng để chọn route/layout. |
| `node_key` | `varchar(100)` | Không | Không | Key ổn định trong parent, do manifest FE/seed định nghĩa. |
| `node_kind` | `varchar(20)` | Không | Không | `page`, `section`, `group`, `item`; không phải loại component do admin chọn. |
| `value` | `jsonb` | Không | Admin qua endpoint whitelist | Object nội dung text/ảnh của node; container thuần dùng `{}`. |
| `updated_at` | `timestamptz` | Không | Hệ thống | Thời điểm update gần nhất. |

Không có cột `route`, `slug`, `href`, `template_key`, `ratio`, `width`, `height`, `sort_order` hoặc `is_published`. Nhãn nội bộ của trang lấy từ manifest admin cố định theo `page_code`, không cần `title` trong DB. `node_key` không phải slug hay URL.

### 3.1.1 Hiểu đơn giản về một row trong `pages`

Có thể hình dung mỗi row là một **hộp nội dung đã có vị trí cố định trên trang**:

- Row gốc `page` (trang) cho biết toàn bộ cây thuộc trang nào, ví dụ `about` (Giới thiệu). Row này thường không chứa nội dung nên `value = {}`.
- Row `section` (khu vực nội dung) là một phần lớn của trang, ví dụ `hero` (mở đầu) hoặc `journey` (Hành trình).
- Row `group` (nhóm) chỉ dùng khi section cần chia thêm một tầng nhóm.
- Row `item` (mục) là một ô lặp cố định bên trong section/group, ví dụ mốc Hành trình thứ nhất.

`parent_id` nối hộp con với hộp cha. Vì cùng một bảng vừa chứa cha vừa chứa con nên đây là quan hệ đệ quy. Ví dụ:

```text
about (Trang Giới thiệu)
├─ hero (Mở đầu)
├─ journey (Hành trình)
│  ├─ item_01 (mốc Hành trình 1)
│  ├─ item_02 (mốc Hành trình 2)
│  └─ ...
└─ visionMission (Tầm nhìn và Sứ mệnh)
```

Đường dẫn nhận diện một node được tạo từ các `node_key` trên cây. Ví dụ `about → journey → item_01`. Backend và FE dùng đường dẫn key này để biết dữ liệu thuộc chỗ nào; không dựa vào row đứng thứ mấy trong kết quả SQL.

`value` là phần nội dung admin được sửa trong đúng hộp đó. Từng giá trị bên trong vẫn có tên field rõ ràng, ví dụ `year`, `title`, `description`, `desktopImage`. FE không suy đoán field đầu tiên là tiêu đề hay ảnh thứ hai là ảnh nào.

### 3.1.2 Dữ liệu mẫu thực tế của Trang Giới thiệu

Bảng dưới đây minh họa sáu row. UUID chỉ là dữ liệu mẫu để nhìn rõ quan hệ cha–con:

| `id` | `parent_id` | `code` | `node_key` | `node_kind` | `value` |
|---|---|---|---|---|---|
| `10000000-0000-4000-8000-000000000001` | `NULL` | `about` | `about` | `page` | `{}` |
| `10000000-0000-4000-8000-000000000002` | `10000000-0000-4000-8000-000000000001` | `NULL` | `hero` | `section` | JSON Hero ở dưới |
| `10000000-0000-4000-8000-000000000003` | `10000000-0000-4000-8000-000000000001` | `NULL` | `journey` | `section` | `{"title":"Hành trình của BMT Decor"}` |
| `10000000-0000-4000-8000-000000000004` | `10000000-0000-4000-8000-000000000003` | `NULL` | `item_01` | `item` | JSON mốc 2011 ở dưới |
| `10000000-0000-4000-8000-000000000005` | `10000000-0000-4000-8000-000000000003` | `NULL` | `item_02` | `item` | JSON mốc 2014 ở dưới |
| `10000000-0000-4000-8000-000000000006` | `10000000-0000-4000-8000-000000000001` | `NULL` | `visionMission` | `section` | JSON Tầm nhìn/Sứ mệnh ở dưới |

Giá trị `value` của node `hero` (Mở đầu):

```json
{
  "eyebrow": "Về chúng tôi",
  "heading": "Kiến tạo giá trị từ mỗi không gian",
  "description": "BMT Decor là đơn vị thiết kế kiến trúc, thiết kế nội thất, thi công xây dựng và cải tạo trọn gói với hơn 15 năm kinh nghiệm.",
  "desktopImage": "https://res.cloudinary.com/bmt/image/upload/about/hero-interior.webp"
}
```

Giá trị `value` của `journey → item_01` (mốc Hành trình 1):

```json
{
  "year": "2011",
  "title": "Thành lập công ty",
  "description": "Chính thức hoạt động trong lĩnh vực thiết kế kiến trúc, thiết kế nội thất và thi công công trình."
}
```

Giá trị `value` của `journey → item_02` (mốc Hành trình 2):

```json
{
  "year": "2014",
  "title": "Mở rộng hoạt động",
  "description": "Triển khai dịch vụ thiết kế thi công trọn gói cho nhà ở và công trình thương mại."
}
```

Hai mốc trên không lưu `image`: giao diện admin chỉ cho sửa năm, tiêu đề và mô tả; hình mốc Hành trình là asset tĩnh của FE.

Giá trị `value` của node `visionMission` (Tầm nhìn và Sứ mệnh):

```json
{
  "visionHeading": "Tầm nhìn",
  "visionDescription": "Trở thành đơn vị thiết kế và thi công được khách hàng tin tưởng lựa chọn nhờ năng lực chuyên môn, quy trình chuyên nghiệp và chất lượng công trình.",
  "missionHeading": "Sứ mệnh",
  "missionDescription": "Mang đến những giải pháp thiết kế và thi công trọn gói chuyên nghiệp, hài hòa về thẩm mỹ và bền vững về chất lượng."
}
```

Node `visionMission` lưu chung bốn field vì admin sửa chúng trong cùng một cụm giao diện. Không tách thêm row nếu không có danh sách lặp hoặc nhu cầu cập nhật độc lập.

### 3.1.3 Backend lấy và ghép dữ liệu cho FE như thế nào

Khi FE yêu cầu Trang Giới thiệu, backend thực hiện theo thứ tự:

1. Tìm row gốc có `code = 'about'`.
2. Lấy toàn bộ node con thuộc root đó bằng `parent_id`.
3. Nhận diện từng phần bằng đường dẫn `node_key`, ví dụ `about.hero` hoặc `about.journey.item_01`.
4. Kiểm tra `value` đúng schema của đường dẫn đó.
5. Dựng object trả cho FE. Các slot lặp chỉ được sắp theo manifest cố định sau khi đã nhận diện bằng key.

Ví dụ response rút gọn:

```json
{
  "hero": {
    "eyebrow": "Về chúng tôi",
    "heading": "Kiến tạo giá trị từ mỗi không gian",
    "description": "BMT Decor là đơn vị thiết kế kiến trúc, thiết kế nội thất, thi công xây dựng và cải tạo trọn gói với hơn 15 năm kinh nghiệm.",
    "desktopImage": "https://res.cloudinary.com/bmt/image/upload/about/hero-interior.webp"
  },
  "journey": {
    "title": "Hành trình của BMT Decor",
    "items": {
      "item_01": {
        "year": "2011",
        "title": "Thành lập công ty",
        "description": "Chính thức hoạt động trong lĩnh vực thiết kế kiến trúc, thiết kế nội thất và thi công công trình."
      },
      "item_02": {
        "year": "2014",
        "title": "Mở rộng hoạt động",
        "description": "Triển khai dịch vụ thiết kế thi công trọn gói cho nhà ở và công trình thương mại."
      }
    }
  },
  "visionMission": {
    "visionHeading": "Tầm nhìn",
    "visionDescription": "Trở thành đơn vị thiết kế và thi công được khách hàng tin tưởng lựa chọn nhờ năng lực chuyên môn, quy trình chuyên nghiệp và chất lượng công trình.",
    "missionHeading": "Sứ mệnh",
    "missionDescription": "Mang đến những giải pháp thiết kế và thi công trọn gói chuyên nghiệp, hài hòa về thẩm mỹ và bền vững về chất lượng."
  }
}
```

Trong response này, `items` là object có key `item_01`, `item_02` để FE nhận diện đúng slot. Nếu component cần một array để dùng `.map()`, FE hoặc mapper có thể chuyển object thành array theo thứ tự manifest; nội dung vẫn được nhận diện bằng key trước, không dựa vào thứ tự row trả về từ DB.

### 3.1.4 Ví dụ khi admin cập nhật nội dung

Khi admin đổi tiêu đề Hero, backend chỉ cập nhật `value` của node `about → hero`:

```json
{
  "heading": "Thiết kế không gian sống bền vững"
}
```

Backend merge field này vào JSON hiện tại. `eyebrow`, `description`, `desktopImage` vẫn giữ nguyên. Backend không cho request đổi `parent_id`, `node_key`, `node_kind`, thêm section mới hoặc đưa field lạ vào `value`.

### 3.1.5 Quy tắc lưu bắt buộc

- Mỗi page có đúng một root seed: `parent_id = NULL`, `code` có giá trị, `node_kind = 'page'`, `node_key = code`, `value = {}`. Các node con có `parent_id`, `code = NULL`.
- Root → section → group/item có thể lồng nhiều cấp. Section chỉ có ít trường giữ chúng trong `value`; danh sách lặp được tách thành các slot `item_01`, `item_02`… dưới section/group. Một field chỉ được lưu ở một node.
- Key, cây, số slot và thứ tự render được seed theo manifest của FE. Admin chỉ đổi `value` trên node được phép; backend không cho thêm/xóa/chuyển node hoặc đổi `node_key`/`node_kind`.
- `value` chỉ là JSON object đúng schema của `(page_code, node path)`. Field ảnh là chuỗi URL; field dạng số/chuỗi khác giữ kiểu tương ứng. Không chứa thông tin layout, URL điều hướng hay label/action của button.
- Các mẫu JSON `HomeContent`, `AboutContent`… bên dưới là **view model mà component FE hiện tại cần sau bước mapper**, không phải một JSON blob được lưu trong một row. Dữ liệu gốc được nhận diện theo đường dẫn `node_key`; mapper chỉ chuyển các slot đã nhận diện thành array khi component hiện tại cần `.map()`. `"..."`, `0` và URL rút gọn là giá trị minh họa, không phải nội dung seed.

Với `home.featuredProjects.groups[].items[]`, cây đi qua `featuredProjects → group_01 → item_01`; `group_01.value = {}` vì key/nhãn nhóm do FE cố định, còn `item_01.value` chỉ chứa chữ và ảnh admin sửa. `home.hero[]` dùng `hero → item_01`. Các danh sách khác (`journey.items`, `coreValues.items`, `capabilities.items`, `serviceList`, `process.items`, `faq.items`; `partners` là ngoại lệ dùng chung) áp dụng cùng quy tắc slot; `partners` vẫn lưu tại `site_settings.partners`, không nhân bản vào từng page. Nếu một nhóm cần lồng sâu hơn, dev thêm cấp `group` trong manifest/seed.

PostgreSQL cần ràng buộc nền tảng:

```sql
ALTER TABLE pages ADD CONSTRAINT ck_pages_root_child CHECK (
  (parent_id IS NULL AND code IS NOT NULL AND node_kind = 'page' AND node_key = code::text)
  OR
  (parent_id IS NOT NULL AND code IS NULL AND node_kind IN ('section','group','item'))
);
ALTER TABLE pages ADD CONSTRAINT ck_pages_value_object CHECK (jsonb_typeof(value) = 'object');
CREATE UNIQUE INDEX uq_pages_root_code ON pages(code) WHERE parent_id IS NULL;
CREATE UNIQUE INDEX uq_pages_child_key ON pages(parent_id, node_key) WHERE parent_id IS NOT NULL;
CREATE INDEX ix_pages_parent ON pages(parent_id);
```

FK tự tham chiếu và `CHECK` không tự chặn cycle; seed và service phải kiểm tra cây không có cycle, mọi node thuộc một root hợp lệ và mọi path nằm trong manifest. Nên giới hạn quyền DB của tài khoản chạy admin API để không thể `INSERT/DELETE` hoặc sửa các cột cấu trúc qua API; migration/seed dùng vai trò riêng. Mỗi lần PATCH chỉ cập nhật `value`, `updated_at` của node đích sau khi validate schema node.

Đối chiếu source FE `BMT_FE` ngày 22/09/2026: các section hiển thị là Hero, Journey, Vision/Mission/Core values, Capabilities, Partners và Contact form. Section “Về chúng tôi” thứ hai trong `AboutPage.tsx` có class `hidden`, không có resource trong admin, nên không tạo node `introduction`. Hero có ảnh desktop/mobile; admin mock chỉ có ô đổi ảnh desktop. Dữ liệu admin là mock trong FE, chưa kết nối DB/API.

## 3.2 Các `page_code` (mã trang) cố định

| `page_code` | Tên tiếng Việt |
|---|---|
| `home` | Trang chủ |
| `about` | Giới thiệu |
| `services` | Tổng quan dịch vụ |
| `service_turnkey` | Xây dựng trọn gói |
| `service_architecture_interior` | Thiết kế kiến trúc và nội thất |
| `service_construction` | Thi công xây dựng |
| `service_renovation` | Cải tạo và sửa chữa |
| `projects` | Dự án |
| `news` | Tin tức |
| `recruitment` | Tuyển dụng |
| `quotation` | Báo giá |
| `contact` | Liên hệ |
| `capability_profile` | Hồ sơ năng lực |

Route tương ứng là constant/router config của FE, không nằm trong DB.

## 3.3 Contract dựng từ cây `home` (Trang chủ)

Ví dụ JSON cho `HomeContent` (nội dung Trang chủ):

```json
{
  "hero": [
    {
      "title": "...",
      "description": "...",
      "desktopImage": "https://res.cloudinary.com/.../image.webp"
    }
  ],
  "featuredProjects": {
    "title": "...",
    "description": "...",
    "groups": [
      {
        "items": [
          {
            "title": "...",
            "area": "...",
            "styleText": "...",
            "year": 0,
            "image": "https://res.cloudinary.com/.../image.webp"
          }
        ]
      }
    ]
  },
  "featuredServices": {
    "title": "...",
    "description": "...",
    "items": [
      {
        "title": "...",
        "description": "...",
        "desktopImage": "https://res.cloudinary.com/.../image.webp",
        "mobileImage": "https://res.cloudinary.com/.../image.webp"
      }
    ]
  },
  "statistics": [
    {
      "value": 0,
      "label": "...",
      "suffix": "..."
    }
  ],
  "whyBmt": {
    "titleDesktop": "...",
    "titleMobile": "...",
    "descriptionDesktop": "...",
    "descriptionMobile": "...",
    "items": [
      {
        "title": "...",
        "description": "...",
        "defaultImage": "https://res.cloudinary.com/.../image.webp",
        "mobileImage": "https://res.cloudinary.com/.../image.webp"
      }
    ]
  },
  "profileSection": {
    "title": "...",
    "subtitle": "...",
    "description": "...",
    "oneBookImage": "https://res.cloudinary.com/.../image.webp",
    "threeBooksImage": "https://res.cloudinary.com/.../image.webp"
  },
  "contactForm": {
    "title": "...",
    "subtitle": "...",
    "successMessage": "..."
  }
}
```

Trường tùy chọn (có thể bỏ khỏi JSON): `featuredProjects.description`, `featuredProjects.groups[].items[].area`, `featuredProjects.groups[].items[].styleText`, `featuredProjects.groups[].items[].year`, `featuredProjects.groups[].items[].image`, `featuredServices.items[].mobileImage`, `statistics[].suffix`, `whyBmt.titleMobile`, `whyBmt.descriptionMobile`, `whyBmt.items[].mobileImage`, `profileSection.oneBookImage`, `profileSection.threeBooksImage`.

Lưu ý:

- Nội dung và target của CTA Hero, Profile và News đều do FE cố định; JSON không lưu button label/action.
- Trang chủ không có resource sửa tiêu đề Tin tức. Danh sách tin Trang chủ query `news WHERE highlight_home = true ORDER BY created_at DESC`; trang Tin tức dùng `is_featured`. Không chép bài viết vào page JSON.
- Nhóm và thứ tự 8 slot dự án mỗi nhóm là manifest FE. Admin sửa trực tiếp `title`, `area`, `styleText`, `year`, `image` của từng slot; không có ô chọn `projectId`, `key` hoặc `label` nhóm.

## 3.4 Contract dựng từ cây `about` (Giới thiệu)

Ví dụ JSON cho `AboutContent` (nội dung trang Giới thiệu):

```json
{
  "hero": {
    "eyebrow": "...",
    "heading": "...",
    "description": "...",
    "desktopImage": "https://res.cloudinary.com/.../image.webp"
  },
  "journey": {
    "title": "...",
    "items": [
      {
        "year": "...",
        "title": "...",
        "description": "..."
      }
    ]
  },
  "coreValues": {
    "title": "...",
    "items": [
      {
        "title": "...",
        "description": "...",
        "image": "https://res.cloudinary.com/.../image.webp"
      }
    ]
  },
  "visionMission": {
    "visionHeading": "...",
    "visionDescription": "...",
    "missionHeading": "...",
    "missionDescription": "..."
  },
  "capabilities": {
    "title": "...",
    "items": [
      {
        "title": "...",
        "mobileTitle": "...",
        "description": "..."
      }
    ]
  },
  "contactForm": {
    "title": "...",
    "subtitle": "...",
    "successMessage": "..."
  }
}
```

Trường tùy chọn (có thể bỏ khỏi JSON): `capabilities.items[].mobileTitle`.

Layout đã đối chiếu: Hero → Journey → Vision/Mission/Core values → Capabilities → Partners dùng chung → Contact form → Footer dùng chung. Trang hiện có 6 slot `journey.item_01`–`item_06`, 5 slot `coreValues.item_01`–`item_05` và 4 slot `capabilities.item_01`–`item_04`. Những con số này nằm trong manifest/seed của FE, không phải trường cho admin cấu hình. Resource admin hiện có `about/hero`, `about/journey`, `about/vision-mission`, `about/core-values`, `about/capabilities`, `about/contact-form` và ba resource phụ cho tiêu đề section. Các collection của trang này mặc định là `fixed`; trường `order` bị lọc khỏi form chỉnh sửa. Không thêm node từ section đang ẩn trong FE.

Đối chiếu field admin với `pages.value`: `about/hero` sửa `eyebrow`, `heading`, `description`, `desktopImage`; `about/journey` sửa `year`, `title`, `description` của 6 slot; `about/vision-mission` sửa bốn trường chữ; `about/core-values` sửa `title`, `description`, `image` của 5 slot; `about/capabilities` sửa `title`, `mobileTitle`, `description` của 4 slot; `about/contact-form` sửa `title`, `subtitle`, `successMessage`; ba resource phụ sửa `title` của section tương ứng. FE mock hiện gọi field phụ này là `description` và đặt nhãn “Tiêu đề phụ”; mapper API đổi tên field khi đọc/ghi. Không lưu các field alt mà FE mock còn khai báo. Số `01`–`04`, ảnh thường và ảnh hover của Năng lực, ảnh Hành trình và ảnh Hero mobile là dữ liệu render cố định trong FE, không lưu trong `pages.value` và không có trong whitelist PATCH. Label, placeholder và chữ trên nút gửi của form cũng không cho sửa theo quy tắc cũ.

Admin hiện chỉ dùng `AdminCrudProvider` và `crud-mock.service.ts`, lưu thay đổi trong React state; trang `/about` đọc hằng số từ `features/about/data/about-content.ts` và chữ trực tiếp trong component. Do đó cập nhật trong admin chưa ghi DB hoặc thay đổi trang công khai. Việc kết nối admin PATCH và public GET với `pages` là bước triển khai cần làm để thiết kế này hoạt động.

## 3.5 Contract dựng từ cây `services` (Tổng quan dịch vụ)

Ví dụ JSON cho `ServicesOverviewContent` (nội dung trang Tổng quan dịch vụ):

```json
{
  "hero": {
    "eyebrow": "...",
    "title": "...",
    "subtitle": "...",
    "description": "...",
    "backgroundImage": "https://res.cloudinary.com/.../image.webp",
    "cards": [
      {
        "image": "https://res.cloudinary.com/.../image.webp"
      }
    ]
  },
  "serviceList": [
    {
      "tabLabel": "...",
      "title": "...",
      "tagline": "...",
      "description": "...",
      "image": "https://res.cloudinary.com/.../image.webp"
    }
  ],
  "process": {
    "title": "...",
    "description": "...",
    "items": [
      {
        "title": "...",
        "description": "...",
        "image": "https://res.cloudinary.com/.../image.webp",
        "imageOpen": "https://res.cloudinary.com/.../image.webp"
      }
    ]
  },
  "faq": {
    "title": "...",
    "description": "...",
    "photo": "https://res.cloudinary.com/.../image.webp",
    "items": [
      {
        "question": "...",
        "answer": "..."
      }
    ]
  },
  "contactForm": {
    "title": "...",
    "subtitle": "...",
    "successMessage": "..."
  }
}
```

Trường tùy chọn (có thể bỏ khỏi JSON): `serviceList[].tagline`, `serviceList[].description`, `process.items[].image`, `process.items[].imageOpen`, `faq.photo`.

Danh tính và đường dẫn của từng dịch vụ được ghép từ slot cố định trong manifest FE; JSON chỉ lưu chữ và ảnh hiển thị.

Các key và số slot trong `hero.cards`, `serviceList`, `process.items`, `faq.items` được seed cố định. Admin chỉ sửa trường nội dung/ảnh trong `value` của slot có sẵn.

## 3.6 Contract dựng từ cây của bốn trang dịch vụ (`service_turnkey` — Xây dựng trọn gói; `service_architecture_interior` — Thiết kế kiến trúc và nội thất; `service_construction` — Thi công xây dựng; `service_renovation` — Cải tạo và sửa chữa)

Ví dụ JSON cho `ServiceDetailContent` (nội dung chi tiết trang dịch vụ):

```json
{
  "hero": {
    "title": "...",
    "subtitle": "...",
    "images": {
      "desktopArtwork": "https://res.cloudinary.com/.../image.webp",
      "mobileArtwork": "https://res.cloudinary.com/.../image.webp",
      "sideDecoration": "https://res.cloudinary.com/.../image.webp"
    }
  },
  "featuredProjects": {
    "title": "...",
    "description": "...",
    "items": [
      {
        "title": "...",
        "tag": "...",
        "image": "https://res.cloudinary.com/.../image.webp"
      }
    ]
  },
  "solutions": {
    "title": "...",
    "description": "...",
    "items": [
      {
        "titlePrefix": "...",
        "titleCategory": "...",
        "tagline": "...",
        "description": "...",
        "checklistLabel": "...",
        "checklist": [
          "..."
        ],
        "image": "https://res.cloudinary.com/.../image.webp"
      }
    ]
  },
  "process": {
    "title": "...",
    "description": "...",
    "brandLogo": "https://res.cloudinary.com/.../image.webp",
    "items": [
      {
        "title": "...",
        "description": "...",
        "image": "https://res.cloudinary.com/.../image.webp"
      }
    ]
  },
  "contactForm": {
    "title": "...",
    "subtitle": "...",
    "successMessage": "..."
  }
}
```

Trường tùy chọn (có thể bỏ khỏi JSON): `featuredProjects.description`, `featuredProjects.items[].tag`, `solutions.description`, `solutions.items[].tagline`, `solutions.items[].description`, `process.description`, `process.brandLogo`, `process.items[].image`.

Tập key ảnh Hero được FE fix theo từng page:

| Page | Chỉ các image key admin sửa được |
|---|---|
| `service_turnkey` | `desktopArtwork`, `mobileArtwork`, `sideDecoration` |
| `service_architecture_interior` | `wireframeImage`, `leftImage`, `centerImage`, `rightImage`, `mobileArtwork` |
| `service_construction` | `wireframeImage`, `topImage`, `rightImage`, `bottomImage`, `leftImage`, `mobileBlueprint` |
| `service_renovation` | `wireframeImage`, `largeImage`, `topImage`, `bottomImage` |

Admin thay file ảnh trong đúng khung. Backend upload Cloudinary và thay URL tương ứng; admin không nhập URL và không cấu hình tỷ lệ.

Trang dịch vụ đã được xác định bằng `page_code` của node gốc; tên key ảnh và cấu trúc `solutions/process` là manifest cố định. Các `items` được dựng từ slot con có sẵn, không nhận thao tác thêm/xóa/đổi thứ tự từ admin. Ảnh trang trí `lineImage`, `backgroundImage`, `accentLine`, `lineLogo`, `introLogo`, `dotsImage` chỉ nằm trong FE. `brandLogo` của tiêu đề quy trình chỉ lưu ở trang Thiết kế và Cải tạo, nơi admin có ô thay ảnh.

## 3.7 Contract dựng từ cây `projects` (Dự án)

Ví dụ JSON cho `ProjectsPageContent` (nội dung trang Dự án):

```json
{
  "hero": {
    "title": "...",
    "description": "...",
    "desktopImage": "https://res.cloudinary.com/.../image.webp"
  },
  "listSection": {
    "title": "..."
  },
  "relatedSection": {
    "title": "...",
    "items": [
      {
        "title": "...",
        "image": "https://res.cloudinary.com/.../image.webp"
      }
    ]
  },
  "contactForm": {
    "title": "...",
    "subtitle": "...",
    "successMessage": "..."
  }
}
```

Project cards query từ `projects`. FE tự tạo link bằng slug hiện tại. `relatedSection.items` là các slot cố định của resource `projects/related`, dùng chung ở các trang chi tiết; admin chỉ sửa `title` và `image`, không sửa `href` hoặc thứ tự.

### Các resource hiển thị Dự án nhưng lưu ở `pages` (nội dung trang)

Tên resource có chữ `projects` vì vị trí của nó trên giao diện, **không có nghĩa là nó sửa một row trong bảng `projects`**. Bảng dưới chỉ mô tả các phần nội dung của trang/cụm giới thiệu Dự án mà admin biên tập. Chúng được dựng từ các node cố định trong bảng `pages`:

| Resource admin | Nội dung admin sửa | Nơi lưu | Phạm vi hiển thị |
|---|---|---|---|
| `projects/related` | `title`, `image` của các thẻ liên quan | `pages.projects.relatedSection.items[]` | Cụm Dự án liên quan dùng chung ở trang chi tiết |
| `projects/related-section-content` | `title` của cả cụm | `pages.projects.relatedSection.title` | Tiêu đề cụm Dự án liên quan |
| `projects/page-hero` | `title`, `description`, `desktopImage` | `pages.projects.hero` | Phần mở đầu trang Danh sách Dự án |
| `projects/list-section-content` | `title` | `pages.projects.listSection.title` | Tiêu đề danh sách trên trang Dự án |
| `projects/contact-form` | `title`, `description` (phụ đề), `successMessage` | `pages.projects.contactForm`; `description` → `subtitle` | Form của trang Danh sách Dự án, khác form riêng trong `projects.detail_content.contactForm` |
| `home/featured-projects/*` | `title`, `area`, `styleText`, `year`, `image` | Slot cố định trong `pages.home.featuredProjects` | Thẻ Dự án tiêu biểu ở Trang chủ |
| `home/projects-section-content` | `title`, `description` | `pages.home.featuredProjects` | Tiêu đề và mô tả của cả cụm ở Trang chủ |
| `services/*/featured-project` | `title`, `tag`, `image` | Slot cố định trong `pages.service_*.featuredProjects` | Thẻ Dự án tiêu biểu ở từng trang dịch vụ |
| `services/*/featured-project-intro` | `title`, `description` | `pages.service_*.featuredProjects` | Giới thiệu cụm Dự án ở từng trang dịch vụ |

Các thẻ giới thiệu tại `pages` là nội dung biên tập riêng của trang chứa chúng; sửa một row `projects` không tự đổi tiêu đề/ảnh đã lưu ở các slot này. `projects/related` không có ô chọn `project_id`; link và thứ tự slot do cấu trúc FE/manifest xác định theo quy tắc hiện tại. Bảng `projects` chỉ giữ thẻ danh sách và trang chi tiết của **từng Dự án**.

## 3.8 Contract dựng từ cây `news` (Tin tức)

Ví dụ JSON cho `NewsPageContent` (nội dung trang Tin tức):

```json
{
  "hero": {
    "eyebrow": "...",
    "title": "...",
    "description": "...",
    "desktopImage": "https://res.cloudinary.com/.../image.webp"
  },
  "featuredSection": {
    "title": "..."
  },
  "contactForm": {
    "title": "...",
    "subtitle": "...",
    "successMessage": "..."
  }
}
```

Không có ô admin sửa tiêu đề danh sách Tin tức; tiêu đề đó thuộc FE. Không có `href`, `articleIds` hoặc bản sao dữ liệu bài viết. Featured query từ `news` bằng `is_featured = true`.

## 3.9 Contract dựng từ cây `recruitment` (Tuyển dụng)

Ví dụ JSON cho `RecruitmentPageContent` (nội dung trang Tuyển dụng):

```json
{
  "hero": {
    "title": "...",
    "description": "...",
    "desktopImage": "https://res.cloudinary.com/.../image.webp"
  },
  "jobsSection": {
    "title": "..."
  },
  "contactForm": {
    "title": "...",
    "subtitle": "...",
    "successMessage": "..."
  }
}
```

Danh sách job query từ bảng `jobs`.

## 3.10 Contract dựng từ cây `quotation` (Báo giá)

Ví dụ JSON cho `QuotationContent` (nội dung trang Báo giá):

```json
{
  "hero": {
    "eyebrow": "...",
    "title": "...",
    "description": "...",
    "mobileImage": "https://res.cloudinary.com/.../image.webp",
    "mainPhoto": "https://res.cloudinary.com/.../image.webp"
  },
  "estimator": {
    "stepLabels": [
      "..."
    ],
    "buildingType": {
      "heading": "...",
      "instruction": "...",
      "options": ["..."]
    },
    "area": {
      "heading": "...",
      "instruction": "...",
      "placeholder": "...",
      "unit": "..."
    },
    "budget": {
      "heading": "...",
      "instruction": "...",
      "placeholder": "...",
      "unit": "..."
    },
    "service": {
      "heading": "...",
      "instruction": "...",
      "options": ["..."]
    },
    "resultIncludeLabel": "..."
  },
  "contactForm": {
    "title": "...",
    "subtitle": "...",
    "requiredMessage": "...",
    "successMessage": "..."
  }
}
```

Trường tùy chọn (có thể bỏ khỏi JSON): `hero.mobileImage`, `hero.mainPhoto`, `contactForm.subtitle`.

CTA, nút quay lại và nút tiếp tục có label/action cố định trong FE.

`estimator.buildingType.options` và `estimator.service.options` là danh sách chuỗi trong các slot cố định, đúng hai ô `buildingOptions` và `serviceOptions` của admin; không lưu object tùy chọn, metadata hoặc ảnh. `stepLabels` là danh sách chuỗi có ô sửa trong admin, nhưng số bước và vị trí từng bước vẫn do FE cố định. Phép tính báo giá phải dựa vào chỉ số slot cố định, không dùng nhãn hiển thị có thể sửa làm khóa bảng giá. Riêng form Báo giá cho sửa `requiredMessage`; các form còn lại không có field này.

## 3.11 Contract dựng từ cây `contact` (Liên hệ)

Ví dụ JSON cho `ContactContent` (nội dung trang Liên hệ):

```json
{
  "hero": {
    "title": "...",
    "description": "...",
    "photo": "https://res.cloudinary.com/.../image.webp"
  },
  "map": {
    "title": "..."
  },
  "form": {
    "title": "...",
    "subtitle": "...",
    "successMessage": "..."
  }
}
```

Google Maps target/embed config nằm trong FE hoặc environment config, không do admin nhập.

## 3.12 Contract dựng từ cây `capability_profile` (Hồ sơ năng lực)

Ví dụ JSON cho `CapabilityProfileContent` (nội dung trang Hồ sơ năng lực):

```json
{
  "hero": {
    "title": "...",
    "subtitle": "...",
    "description": "...",
    "heroImage": "https://res.cloudinary.com/.../image.webp",
    "decor08": "https://res.cloudinary.com/.../image.webp"
  },
  "document": {
    "heading": "..."
  },
  "contactForm": {
    "title": "...",
    "subtitle": "...",
    "successMessage": "..."
  }
}
```

Trường tùy chọn (có thể bỏ khỏi JSON): `hero.decor08`, `contactForm.subtitle`. `document.heading` nhận `documentHeading` từ admin. Các ảnh trang trí, đường kẻ, URL tài liệu và ảnh preview chưa có ô sửa nên không lưu trong DB.

---

# 4. Bảng `projects` (Dự án) và `news` (Tin tức)

Admin được thêm/sửa/xóa Dự án và Tin tức qua endpoint riêng, không nhập slug/href. Chi tiết Dự án dùng `projects.detail_content jsonb`; nội dung bài Tin tức dùng `news.content jsonb`.

## 4.1 Bảng `projects` (Dự án)

| Cột | Kiểu | Null | Mô tả |
|---|---|---:|---|
| `id` | `uuid` | Không | PK. |
| `card_title` | `varchar(300)` | Không | Ô `title` của form Danh sách Dự án; tên trên thẻ và nguồn để sinh slug. Ví dụ: `Nhà phố 2 tầng Quận 9`. |
| `card_category` | `varchar(100)` | Không | Ô `category` của form Danh sách Dự án; danh mục thẻ. Ví dụ: `Nhà ở`. |
| `card_image_url` | `varchar(1200)` | Có | Ô `thumbnail`; URL Cloudinary của ảnh thẻ. Chỉ lưu URL, không lưu alt hoặc kích thước. |
| `is_featured` | `boolean` | Không | Checkbox `highlight` (Tiêu biểu) trong bảng danh sách; tối đa 8 dự án mỗi `card_category`. |
| `detail_content` | `jsonb` | Không | Các ô của form Chi tiết Dự án, chia nhóm `overview`, `survey`, `solution`, `renders`, `process`, `comparisons`, `contactForm`; mỗi ảnh/nhãn là một field có tên trùng ô admin; mặc định `{}` nếu mới tạo thẻ. |
| `created_at` | `timestamptz` | Không | Hệ thống ghi. |
| `updated_at` | `timestamptz` | Không | Hệ thống ghi. |

Đây là **một row cho một Dự án**: form `projects/list` ghi bốn field `card_*`/`is_featured`, còn form `projects/details` của cùng Dự án cập nhật `detail_content`. Backend liên kết hai form bằng `projects.id`, không tạo row chi tiết thứ hai và không dựa vào tên/slug để ghép. Khi admin mới tạo thẻ, `detail_content = {}`; khi nhập trang chi tiết, backend merge và validate từng nhóm JSON. FE mock hiện giữ hai collection riêng, nên bước nối API phải dùng cùng `projects.id` cho cả hai form.

Tên thẻ mẫu là “Nhà phố 2 tầng Quận 9”, còn tiêu đề chi tiết là “Dự án nhà phố Quận 9”; danh mục thẻ là “Nhà ở”, còn danh mục chi tiết là “Nhà Phố”. Vì vậy `card_title`/`card_category` và `detail_content.overview.title`/`category` là **hai cặp field độc lập**. Slug tự sinh từ `card_title`. Backend kiểm tra tối đa 8 Dự án tiêu biểu mỗi `card_category` trong transaction, kể cả khi đổi danh mục. Cờ `is_featured` không tự thay thế các slot Dự án nổi bật cố định tại Trang chủ hoặc trang dịch vụ.

### `projects.detail_content` (nội dung chi tiết Dự án dưới dạng JSON)

Ví dụ **một row `projects`** dựa trên mock “Nhà phố 2 tầng Quận 9”. Chữ lấy từ mock và có rút gọn các đoạn mô tả dài; `id`, thời gian và URL Cloudinary chỉ minh họa cách lưu, không phải dữ liệu đã ghi vào DB:

```json
{
  "id": "4d12f2da-20a6-4ba1-91aa-74268a106a1f",
  "card_title": "Nhà phố 2 tầng Quận 9",
  "card_category": "Nhà ở",
  "card_image_url": "https://res.cloudinary.com/.../project-cover.png",
  "is_featured": false,
  "detail_content": {
    "overview": {
      "title": "Dự án nhà phố Quận 9",
      "projectName": "Nhà Phố 2 Tầng Hiện Đại",
      "category": "Nhà Phố",
      "location": "Quận 9, TP.HCM (cũ)",
      "client": "Anh Nhân",
      "area": "210m²",
      "scale": "2 tầng + tum (sân thượng)",
      "heroImage": "https://res.cloudinary.com/.../project-cover.png",
      "wordmarkImage": "https://res.cloudinary.com/.../moc-mien-wordmark.png",
      "description": "Mỗi ngôi nhà đều bắt đầu từ một mong muốn riêng. Khách hàng hướng đến không gian sống Wabi Sabi, gọn gàng và tối ưu công năng."
    },
    "survey": {
      "survey1Image": "https://res.cloudinary.com/.../survey-facade.png",
      "survey2Image": "https://res.cloudinary.com/.../survey-living-room.png",
      "survey3Image": "https://res.cloudinary.com/.../survey-stair.png",
      "surveyDescription": "Qua khảo sát hiện trạng, BMT Decor thấy cách bố trí nội thất chưa tối ưu và ánh sáng tự nhiên còn hạn chế."
    },
    "solution": {
      "drawingCaption": "Bản vẽ mặt bằng bố trí",
      "solutionDescription": "BMT Decor đề xuất phương án cải tạo với các đường bo cong nhẹ nhàng, cân đối khoảng đặc và rỗng.",
      "drawingImage": "https://res.cloudinary.com/.../facade-drawing.png"
    },
    "renders": {
      "render1Image": "https://res.cloudinary.com/.../render-facade.png",
      "render2Image": "https://res.cloudinary.com/.../render-living-wide.png",
      "render3Image": "https://res.cloudinary.com/.../render-tv-wall.png",
      "render4Image": "https://res.cloudinary.com/.../render-lounge-wide.png",
      "render5Image": "https://res.cloudinary.com/.../render-stair-wide.png",
      "render6Image": "https://res.cloudinary.com/.../render-kitchen.png",
      "galleryDescription": "BMT Decor lựa chọn phong cách Wabi Sabi, kết hợp gam kem ấm và ánh sáng dịu nhẹ."
    },
    "process": {
      "process1Label": "Khảo sát hiện trạng",
      "process1Image": "https://res.cloudinary.com/.../process-survey.png",
      "process2Label": "Lên phương án & thiết kế",
      "process2Image": "https://res.cloudinary.com/.../process-design.png",
      "process3Label": "Thi công và giám sát",
      "process3Image": "https://res.cloudinary.com/.../process-construction.png",
      "process4Label": "Bàn giao",
      "process4Image": "https://res.cloudinary.com/.../process-handover.png",
      "processDescription": "Đội ngũ triển khai từ khảo sát hiện trạng đến hoàn thiện từng hạng mục theo quy trình kiểm soát chất lượng."
    },
    "comparisons": {
      "comparison1BeforeLabel": "Hiện trạng",
      "comparison1BeforeImage": "https://res.cloudinary.com/.../before-facade-cropped-v2.png",
      "comparison1AfterLabel": "Hoàn thiện",
      "comparison1AfterImage": "https://res.cloudinary.com/.../after-facade.png",
      "comparison2BeforeLabel": "3D",
      "comparison2BeforeImage": "https://res.cloudinary.com/.../after-stair-render.png",
      "comparison2AfterLabel": "Thực tế",
      "comparison2AfterImage": "https://res.cloudinary.com/.../after-stair-built.png",
      "comparison3BeforeLabel": "Hiện trạng",
      "comparison3BeforeImage": "https://res.cloudinary.com/.../before-living.png",
      "comparison3AfterLabel": "Hoàn thiện",
      "comparison3AfterImage": "https://res.cloudinary.com/.../after-living.png"
    },
    "contactForm": {
      "ctaTitle": "BẠN YÊU THÍCH KHÔNG GIAN NÀY?",
      "ctaDescription": "Liên hệ BMT Decor để được tư vấn giải pháp thiết kế – thi công phù hợp.",
      "ctaSuccessMessage": "Cảm ơn bạn đã gửi thông tin. BMT Decor sẽ liên hệ với bạn trong thời gian sớm nhất."
    }
  },
  "created_at": "2026-08-08T00:00:00+07:00",
  "updated_at": "2026-08-08T00:00:00+07:00"
}
```

Ý nghĩa các nhóm trong `detail_content`:

| Nhóm JSON | Dữ liệu lưu | Ô tương ứng ở form Chi tiết Dự án |
|---|---|---|
| `overview` (tổng quan) | Tiêu đề, tên dự án, danh mục chi tiết, khu vực, chủ đầu tư, diện tích, quy mô, ảnh mở đầu, ảnh chữ và đoạn giới thiệu | Thông tin chung |
| `survey` (khảo sát) | `survey1Image`–`survey3Image` là 3 vị trí ảnh hiện trạng; `surveyDescription` là mô tả khảo sát | Khảo sát hiện trạng và lên phương án |
| `solution` (phương án) | Chú thích bản vẽ, mô tả giải pháp, URL ảnh bản vẽ | Phương án |
| `renders` (phối cảnh 3D) | `render1Image`–`render6Image` là 6 vị trí ảnh phối cảnh; `galleryDescription` là mô tả thư viện | Hình ảnh 3D |
| `process` (quá trình thi công) | `process1Label`/`process1Image` đến slot 4; `processDescription` là mô tả quy trình | Quá trình và năng lực thi công |
| `comparisons` (trước/sau) | `comparison1BeforeImage`/`BeforeLabel`/`AfterImage`/`AfterLabel` đến slot 3 | Thành quả bàn giao |
| `contactForm` (biểu mẫu liên hệ) | Tiêu đề, tiêu đề phụ, thông báo gửi thành công | Biểu mẫu liên hệ ở cuối trang chi tiết |

Mỗi ảnh và mỗi đoạn chữ có **đường dẫn field riêng** gồm tên nhóm và tên ô admin. Ví dụ `renders.render1Image`, `renders.render2Image` và `comparisons.comparison2BeforeImage` là ba field độc lập. Các số trong tên là một phần của **tên field**, không phải chỉ số mảng; `comparison2BeforeImage` đi cùng `comparison2BeforeLabel`, `comparison2AfterImage` và `comparison2AfterLabel` bằng tiền tố `comparison2`. Backend không đổi tên field hoặc dồn ảnh khi một ảnh còn trống. Field ảnh chưa có file giữ `null` trong bản nháp; API không trả một trang chi tiết chưa đủ dữ liệu mà FE cần để render. Field chữ tùy chọn có thể bỏ key; khi trả form admin, backend điền chuỗi rỗng. Mỗi field ảnh chỉ lưu URL Cloudinary hoặc `null`, không lưu object alt/kích thước.

### Quy tắc trả dữ liệu từ DB sang FE công khai

API tìm một Dự án bằng `content_slugs.slug` → `content_slugs.project_id` → `projects.id`. Nếu URL dùng slug cũ, backend chuyển sang slug hiện tại; `projects.id` là định danh ổn định nối form danh sách với form chi tiết. Response dùng `card` (thẻ danh sách) và `detail` (nội dung chi tiết) là **hai cụm có tên khác nhau**. `detail` giữ nguyên các nhóm và tên field trong `detail_content`; backend không đổi chúng thành mảng. Slug là metadata hệ thống, không phải nội dung admin nhập.

| Nguồn trong DB | Field trong response | FE dùng cho |
|---|---|---|
| `projects.id` và slug hiện hành trong `content_slugs` | `id`, `slug` | Xác định dự án; link `/projects/{slug}` |
| `card_title`, `card_category`, `card_image_url`, `is_featured` | `card.title`, `card.category`, `card.imageUrl`, `card.isFeatured` | Thẻ Danh sách Dự án; không lấy dữ liệu từ `detail.overview` |
| `detail_content.overview` | `detail.overview` với `title`, `projectName`, `category`, `location`, `client`, `area`, `scale`, `heroImage`, `wordmarkImage`, `description` | Khối tổng quan; mỗi ảnh/chữ được đọc theo tên field |
| `detail_content.survey` | `detail.survey` với `survey1Image`, `survey2Image`, `survey3Image`, `surveyDescription` | Khối khảo sát; từng ảnh có field riêng |
| `detail_content.solution` | `detail.solution` với `drawingImage`, `drawingCaption`, `solutionDescription` | Khối phương án/bản vẽ |
| `detail_content.renders` | `detail.renders` với `render1Image` đến `render6Image`, `galleryDescription` | Khối phối cảnh; từng ảnh có field riêng |
| `detail_content.process` | `detail.process` với `process1Label`, `process1Image` đến `process4Label`, `process4Image`, `processDescription` | Khối thi công; nhãn và ảnh cùng số tạo thành một cặp field |
| `detail_content.comparisons` | `detail.comparisons` với `comparison1BeforeImage`, `comparison1BeforeLabel`, `comparison1AfterImage`, `comparison1AfterLabel` đến cụm `comparison3*` | Khối trước/sau; tiền tố `comparison1`, `comparison2`, `comparison3` phân biệt ba cụm |
| `detail_content.contactForm` | `detail.contactForm` với `ctaTitle`, `ctaDescription`, `ctaSuccessMessage` | Form cuối trang chi tiết |

Ví dụ response rút gọn; các field còn lại trong mỗi nhóm được trả theo đúng tên ở row JSON phía trên:

```json
{
  "id": "4d12f2da-20a6-4ba1-91aa-74268a106a1f",
  "slug": "nha-pho-2-tang-quan-9",
  "card": {
    "title": "Nhà phố 2 tầng Quận 9",
    "category": "Nhà ở",
    "imageUrl": "https://res.cloudinary.com/.../project-cover.png",
    "isFeatured": false
  },
  "detail": {
    "overview": {
      "title": "Dự án nhà phố Quận 9",
      "heroImage": "https://res.cloudinary.com/.../project-cover.png"
    },
    "survey": {
      "survey1Image": "https://res.cloudinary.com/.../survey-facade.png",
      "survey2Image": "https://res.cloudinary.com/.../survey-living-room.png",
      "survey3Image": "https://res.cloudinary.com/.../survey-stair.png",
      "surveyDescription": "Qua khảo sát hiện trạng, BMT Decor thấy cách bố trí nội thất chưa tối ưu."
    },
    "solution": {
      "drawingImage": "https://res.cloudinary.com/.../facade-drawing.png",
      "drawingCaption": "Bản vẽ mặt bằng bố trí"
    },
    "renders": {
      "render1Image": "https://res.cloudinary.com/.../render-facade.png",
      "render2Image": "https://res.cloudinary.com/.../render-living-wide.png"
    },
    "process": {
      "process1Label": "Khảo sát hiện trạng",
      "process1Image": "https://res.cloudinary.com/.../process-survey.png"
    },
    "comparisons": {
      "comparison1BeforeLabel": "Hiện trạng",
      "comparison1BeforeImage": "https://res.cloudinary.com/.../before-facade-cropped-v2.png",
      "comparison1AfterLabel": "Hoàn thiện",
      "comparison1AfterImage": "https://res.cloudinary.com/.../after-facade.png"
    },
    "contactForm": {
      "ctaTitle": "BẠN YÊU THÍCH KHÔNG GIAN NÀY?"
    }
  }
}
```

FE đọc trực tiếp theo đường dẫn, ví dụ `project.detail.survey.survey2Image` hoặc `project.detail.comparisons.comparison1AfterImage`; không dùng `survey[1]` hay `comparisons[0].after`. JSON response chỉ chứa URL ảnh, không có alt/kích thước. Source FE hiện tại vẫn đọc mock `ProjectDetail` dạng mảng và trang danh sách còn gán ảnh theo chỉ số `projectPageImages`, nên **source này chưa thể dùng response trên trực tiếp**. Khi nối API sau này, FE cần đọc các field có tên như hợp đồng này; phần alt/kích thước render do FE hoặc xử lý ảnh cung cấp, không bổ sung vào DB. `ctaSubmitLabel`, `style`, `displayName`, `scope` không có ô admin hợp lệ để lưu theo quy tắc hiện tại; các phần trình bày liên quan do FE xử lý.

| Field FE cần đọc | Vai trò trong giao diện cố định |
|---|---|
| `detail.survey.survey1Image`, `survey2Image`, `survey3Image` | Ảnh khảo sát trái, giữa, phải |
| `detail.renders.render1Image` | Ảnh phối cảnh lớn mở đầu |
| `detail.renders.render2Image`, `render3Image` | Ảnh trên và dưới trong cụm cạnh ảnh lớn |
| `detail.renders.render4Image`, `render5Image`, `render6Image` | Ba ảnh ở cụm phối cảnh tiếp theo |
| `detail.process.process1Label` + `process1Image` đến cặp số 4 | Từng thẻ thi công; FE đọc nhãn và ảnh theo cùng tên tiền tố |
| `detail.comparisons.comparison1*` đến `comparison3*` | Từng hàng trước/sau; FE đọc bốn field `BeforeLabel`, `BeforeImage`, `AfterLabel`, `AfterImage` của cùng một tiền tố |

### Đối chiếu ô nhập của một bản ghi Dự án trong admin

Chỉ hai resource `projects/list` và `projects/details` dưới đây ghi vào bảng `projects`. Các resource khác có chữ “Dự án” trong tên nhưng sửa nội dung của `pages` đã được liệt kê tại mục 3.7.

| Resource admin | Nhóm trên form | Field admin được ghi | Nơi lưu |
|---|---|---|---|
| `projects/list` | Thẻ danh sách | `title`, `category`, `thumbnail` | `projects.card_title`, `projects.card_category`, `projects.card_image_url` |
| `projects/list` | Checkbox Tiêu biểu | `highlight` | `projects.is_featured` |
| `projects/details` | Thông tin chung | `title`, `projectName`, `category`, `location`, `client`, `area`, `scale`, `heroImage`, `wordmarkImage`, `description` | `projects.detail_content.overview` |
| `projects/details` | Khảo sát | `survey1Image`–`survey3Image`, `surveyDescription` | `projects.detail_content.survey` với cùng tên key |
| `projects/details` | Phương án | `drawingCaption`, `solutionDescription`, `drawingImage` | `projects.detail_content.solution` với cùng tên key |
| `projects/details` | Phối cảnh 3D | `render1Image`–`render6Image`, `galleryDescription` | `projects.detail_content.renders` với cùng tên key |
| `projects/details` | Quá trình thi công | `process1Label`–`process4Label`, `process1Image`–`process4Image`, `processDescription` | `projects.detail_content.process` với cùng tên key |
| `projects/details` | Trước/sau thi công | `comparison1BeforeImage`–`comparison3BeforeImage`, `comparison1BeforeLabel`–`comparison3BeforeLabel`, các field `AfterImage`/`AfterLabel` tương ứng | `projects.detail_content.comparisons` với cùng tên key |
| `projects/details` | Biểu mẫu liên hệ riêng | `ctaTitle`, `ctaDescription`, `ctaSuccessMessage` | `projects.detail_content.contactForm` với cùng tên key |

`slug`, `href`, `order`, `status`, `displayName`, `style`, `scope`, ảnh `alt` và `ctaSubmitLabel` có trong dữ liệu mock/FE nhưng không là ô admin được phép sửa theo quy tắc hiện tại; không đưa vào nội dung Dự án của DB. `id`, slug hệ thống và thời gian tạo/cập nhật là metadata vận hành riêng. Khi kết nối FE thật, FE phải đọc các field JSON theo tên; không chuyển chúng thành mảng theo thứ tự rồi mới nhận diện. Alt/kích thước ảnh nếu cần được xử lý lúc render, không thêm vào `detail_content`.

`ctaSubmitLabel`, badge trước/sau, slug và danh sách ID dự án liên quan không có ô sửa. Form admin ghi/đọc các key cùng tên vào nhóm JSON. Ô `overview.description` giữ nguyên chuỗi văn bản nhiều đoạn; khi tích hợp API, FE có thể tách đoạn tại bước render nếu cần. Các ảnh trong DB và response chỉ là URL; FE đọc URL bằng tên field tương ứng, còn alt và kích thước render do phần hiển thị xử lý.

## 4.2 Bảng `news` (Tin tức)

| Cột | Kiểu | Null | Mô tả |
|---|---|---:|---|
| `id` | `uuid` | Không | PK. |
| `title` | `varchar(300)` | Không | Tên hiển thị, dùng để tự sinh slug. |
| `excerpt` | `text` | Có | Ô `excerpt` (Mô tả ngắn) trong form tạo/sửa bài; không bắt buộc. |
| `image_url` | `varchar(1200)` | Có | Ô `desktopImage` (Ảnh bài viết) trong form admin; chỉ lưu một URL ảnh, không bắt buộc. |
| `content` | `jsonb` | Không | Object chỉ chứa `body` (Nội dung bài viết, rich text) theo `NewsContent`; `body` bắt buộc khi tạo/sửa bài. |
| `is_featured` | `boolean` | Không | Checkbox `featured` (Tin nổi bật) tại bảng danh sách; tối đa 5 bài. |
| `highlight_home` | `boolean` | Không | Checkbox `highlightHome` (Trang chủ) tại bảng danh sách; tối đa 4 bài. |
| `created_at` | `timestamptz` | Không | Hệ thống ghi. |
| `updated_at` | `timestamptz` | Không | Hệ thống ghi. |

### Đối chiếu ô nhập trang Danh sách Tin tức trong admin

| Vị trí admin | Field FE | Nơi lưu trong DB | Ghi chú |
|---|---|---|---|
| Form tạo/sửa bài | `title` (Tiêu đề) | `news.title` | Bắt buộc; backend dùng để sinh slug. |
| Form tạo/sửa bài | `excerpt` (Mô tả ngắn) | `news.excerpt` | Có thể để trống. |
| Form tạo/sửa bài | `desktopImage` (Ảnh bài viết) | `news.image_url` | Backend lưu URL Cloudinary sau upload. |
| Form tạo/sửa bài | `body` (Nội dung bài viết) | `news.content.body` | Rich text bắt buộc. |
| Bảng danh sách | `featured` (Tin nổi bật) | `news.is_featured` | Checkbox riêng, giới hạn 5 bài. |
| Bảng danh sách | `highlightHome` (Trang chủ) | `news.highlight_home` | Checkbox riêng, giới hạn 4 bài. |

`slug` chỉ hiển thị dạng không cho sửa; `href` bị lọc khỏi form; `order` là số thứ tự hiển thị do UI tính, không cho nhập. Source FE còn khai báo `imageAlt` (văn bản thay thế), nhưng theo yêu cầu đã chốt về ảnh, DB không lưu alt. `mobileImage` có trong dữ liệu hiển thị của FE nhưng không có ô sửa trong admin, nên không lưu URL ảnh mobile riêng; khi nối API cần để FE dùng `image_url` cho khung ảnh tương ứng hoặc dùng asset cố định của FE. Backend validate `news.content.body` là chuỗi rich text không rỗng.

Giới hạn 5 tin nổi bật và 4 tin Trang chủ hiện được kiểm tra ở UI; backend cũng phải kiểm tra trong transaction khi bật checkbox để hai yêu cầu đồng thời không vượt giới hạn. Các giới hạn này là quy tắc chọn nội dung, không phải field lưu thêm.

### `news.content` (nội dung bài Tin tức)

Ví dụ JSON cho `NewsContent` (nội dung bài Tin tức):

```json
{
  "body": "..."
}
```

Query tin nổi bật:

```sql
SELECT n.*
FROM news n
WHERE n.is_featured = true
ORDER BY n.created_at DESC;
```

---

# 5. Bảng `jobs` (Vị trí tuyển dụng)

Admin được thêm/sửa/xóa. Hiện job hiển thị trong trang Recruitment nên không cần slug.

| Cột | Kiểu | Null | Mô tả |
|---|---|---:|---|
| `id` | `uuid` | Không | PK. |
| `title` | `varchar(300)` | Không | Tên vị trí. |
| `department` | `varchar(150)` | Có | Phòng ban. |
| `location` | `varchar(255)` | Có | Địa điểm. |
| `schedule` | `varchar(255)` | Có | Lịch làm việc. |
| `compensation` | `varchar(255)` | Có | Thu nhập. |
| `summary` | `text` | Có | Mô tả ngắn. |
| `image_url` | `varchar(1200)` | Có | URL Cloudinary của ảnh. |
| `content` | `jsonb` | Không | Một object có **hai key bắt buộc**: `responsibilities` (Trách nhiệm) và `benefits` (Quyền lợi). Mỗi key chứa riêng chuỗi HTML từ ô rich text tương ứng trong admin. |
| `created_at` | `timestamptz` | Không | Hệ thống ghi. |
| `updated_at` | `timestamptz` | Không | Hệ thống ghi. |

Ví dụ giá trị của **một cột `jobs.content`** cho `JobContent` (nội dung Vị trí tuyển dụng):

```json
{
  "responsibilities": "<ul><li>Khảo sát hiện trạng công trình.</li><li>Triển khai bản vẽ kỹ thuật.</li></ul>",
  "benefits": "<ul><li>Được hướng dẫn chuyên môn.</li><li>Thưởng theo dự án.</li></ul>"
}
```

Admin có hai ô rich text độc lập: `responsibilities` (Trách nhiệm) ghi vào `content.responsibilities`; `benefits` (Quyền lợi) ghi vào `content.benefits`. API admin trả lại nguyên hai chuỗi để điền đúng hai ô khi sửa. Backend không nối chúng thành một đoạn văn và phải kiểm tra cả hai key là chuỗi rich text không rỗng.

Trang Tuyển dụng hiện dùng `CareerJob.responsibilities: string[]` và `CareerJob.benefits: string[]`, rồi render từng mảng ở hai phần khác nhau. Khi trả dữ liệu công khai, backend chuyển danh sách `<li>` của từng key thành mảng tương ứng. Ví dụ:

```json
{
  "responsibilities": ["Khảo sát hiện trạng công trình.", "Triển khai bản vẽ kỹ thuật."],
  "benefits": ["Được hướng dẫn chuyên môn.", "Thưởng theo dự án."]
}
```

Mapper phải xử lý HTML an toàn và giữ nguyên ranh giới giữa hai key; rich text không thể chuyển thành danh sách mục hợp lệ thì API báo lỗi khi lưu. Form vị trí tuyển dụng không có ô sửa yêu cầu tuyển dụng, hướng dẫn nộp, email liên hệ, thứ tự, trạng thái mở hoặc hạn tuyển.

Ràng buộc PostgreSQL cho hai key (ngoài kiểm tra nội dung rich text ở backend):

```sql
ALTER TABLE jobs ADD CONSTRAINT ck_jobs_content_parts CHECK (
  jsonb_typeof(content) = 'object'
  AND content ? 'responsibilities'
  AND content ? 'benefits'
  AND jsonb_typeof(content->'responsibilities') = 'string'
  AND jsonb_typeof(content->'benefits') = 'string'
);
```

---

# 6. Bảng `content_slugs` (lịch sử đường dẫn Dự án và Tin tức)

Đây là bảng hệ thống; admin không xem hoặc chỉnh trực tiếp.

| Cột | Kiểu | Null | Mô tả |
|---|---|---:|---|
| `id` | `uuid` | Không | PK. |
| `slug` | `varchar(255)` | Không | Unique toàn bộ Project + News. |
| `project_id` | `uuid` | Có | FK đến `projects.id`; có giá trị khi slug thuộc Dự án. |
| `news_id` | `uuid` | Có | FK đến `news.id`; có giá trị khi slug thuộc Tin tức. |
| `is_current` | `boolean` | Không | Slug hiện hành của record. |
| `created_at` | `timestamptz` | Không | Thời điểm slug được tạo. |

Mỗi row có đúng một trong hai FK. PostgreSQL cần `CHECK` và hai partial unique index:

```sql
ALTER TABLE content_slugs
ADD CONSTRAINT ck_content_slugs_one_owner
CHECK (num_nonnulls(project_id, news_id) = 1);

CREATE UNIQUE INDEX uq_content_slugs_current_project
ON content_slugs(project_id)
WHERE is_current = true AND project_id IS NOT NULL;

CREATE UNIQUE INDEX uq_content_slugs_current_news
ON content_slugs(news_id)
WHERE is_current = true AND news_id IS NOT NULL;
```

Các index bảo đảm mỗi record có **tối đa một** slug hiện hành. Backend tạo record và slug hiện hành trong cùng transaction để mỗi record có một slug ngay khi tạo.

## Thuật toán tạo slug (đường dẫn định danh)

```text
source_title = projects.card_title nếu là Dự án, news.title nếu là Tin tức
base = slugify(source_title)
candidate = base
counter = 2

while candidate đã thuộc Dự án hoặc Tin tức khác:
    candidate = base + "-" + counter
    counter++
```

Tất cả thao tác chạy trong transaction. `slug` có unique constraint; backend retry khi hai transaction cùng chọn một slug.

## Khi `projects.card_title` hoặc `news.title` thay đổi

- Nếu slug mới giống current slug: không làm gì.
- Nếu slug mới là slug cũ của chính record: chuyển slug đó thành current, current cũ thành historical.
- Nếu slug mới chưa tồn tại: insert row mới current, row current cũ thành historical.
- Nếu slug đã thuộc record khác: tự thêm hậu tố.

Slug cũ được giữ khi record còn tồn tại. Khi admin xóa hẳn Project/News, backend xóa các slug của record trong cùng transaction trước khi xóa record. URL cũ sau đó trả 404.

## Resolve và redirect (tìm nội dung và chuyển hướng)

```sql
SELECT * FROM content_slugs WHERE slug = $1;
```

- Không tìm thấy: 404.
- `is_current = true`: tải record từ `projects` hoặc `news` theo FK có giá trị.
- `is_current = false`: tìm slug hiện hành có cùng `project_id` hoặc `news_id` và trả 301.

Backend dựa vào FK có giá trị để ghép prefix cố định:

```text
project → /projects/{currentSlug}
news    → /news/{currentSlug}
```

Prefix không nằm trong DB và admin không can thiệp.

---

# 7. Bảng `site_settings` (cấu hình chung của website)

Chỉ có một record `id = 1` và đúng ba field nội dung JSON.

| Cột | Kiểu | Null | Mô tả |
|---|---|---:|---|
| `id` | `smallint` | Không | Luôn bằng 1. |
| `header` | `jsonb` | Không | URL logo đầu trang; không chứa navigation. |
| `partners` | `jsonb` | Không | Tiêu đề và sáu URL logo đối tác có key riêng; không link. |
| `footer` | `jsonb` | Không | Nội dung footer; mỗi dịch vụ cố định có key riêng và lưu tên trang đích, không lưu URL. |
| `updated_at` | `timestamptz` | Không | Hệ thống ghi. |

## `header` (đầu trang)

Ví dụ JSON cho `HeaderSettings` (cấu hình đầu trang):

```json
{
  "logo": "https://res.cloudinary.com/.../image.webp"
}
```

Admin chỉ thay ảnh logo. Cây danh mục đầu trang, label và target được fix cứng trong FE, không nằm trong DB.

## `partners` (đối tác)

Ví dụ JSON cho `PartnersSettings` (cấu hình Đối tác):

```json
{
  "title": "...",
  "partner1LogoImage": "https://res.cloudinary.com/.../partner-go.webp",
  "partner2LogoImage": "https://res.cloudinary.com/.../partner-lkc.webp",
  "partner3LogoImage": "https://res.cloudinary.com/.../partner-zena.webp",
  "partner4LogoImage": "https://res.cloudinary.com/.../partner-yumyum.webp",
  "partner5LogoImage": "https://res.cloudinary.com/.../partner-satra.webp",
  "partner6LogoImage": "https://res.cloudinary.com/.../partner-cafe.webp"
}
```

`title` (tiêu đề Đối tác) lấy từ ô `settings/partners-section-content`. `partner1LogoImage` đến `partner6LogoImage` (logo Đối tác 1 đến 6) lần lượt lấy từ sáu ô Logo cố định trong `settings/partners`. FE dùng **tên field** để gắn đúng logo với slot; nếu cần render thành danh sách, FE dựng danh sách từ các key này theo thứ tự cố định. Không lưu `items[]`, `name`, `order`, `alt`, `href`, URL website đối tác hoặc target điều hướng; tên và bố cục đối tác hiện nằm trong FE. Mỗi field ảnh chỉ là một URL.

## `footer` (chân trang)

Ví dụ JSON cho `FooterSettings` (cấu hình chân trang):

```json
{
  "footerLogo": "https://res.cloudinary.com/.../image.webp",
  "service1Label": "...",
  "service1PageName": "Dịch vụ - Xây dựng trọn gói",
  "service2Label": "...",
  "service2PageName": "Dịch vụ - Thiết kế kiến trúc & nội thất",
  "service3Label": "...",
  "service3PageName": "Dịch vụ - Thi công xây dựng",
  "service4Label": "...",
  "service4PageName": "Dịch vụ - Cải tạo & sửa chữa",
  "contactHeading": "...",
  "officeAddress": "...",
  "phone": "...",
  "email": "...",
  "branchesHeading": "...",
  "branch1Address": "...",
  "branch2Address": "...",
  "workshopAddress": "...",
  "socialWidgetImage": "https://res.cloudinary.com/.../image.webp"
}
```

`footerLogo` (logo chân trang) và `socialWidgetImage` (ảnh fanpage dưới biểu tượng mạng xã hội) là hai ảnh khác nhau, mỗi ảnh có key và URL riêng. `service1Label`/`service1PageName` đến `service4Label`/`service4PageName` là nội dung dịch vụ và **tên trang đích** của bốn ô cố định; FE đọc theo key, không theo vị trí mảng. `contactHeading` (tiêu đề Liên hệ), `officeAddress` (địa chỉ chính), `phone` (số hỗ trợ), `email` (email), `branchesHeading` (tiêu đề Chi nhánh và nhà xưởng), `branch1Address`/`branch2Address` (địa chỉ hai chi nhánh), `workshopAddress` (địa chỉ xưởng).

Trường tùy chọn (có thể bỏ khỏi JSON): `officeAddress`, `phone`, `email`, `branch1Address`, `branch2Address`, `workshopAddress`, `socialWidgetImage`.

`footer.service1PageName` đến `footer.service4PageName` chỉ lưu tên trang admin chọn, ví dụ `"Trang chủ"`; **không lưu** `/`, `/home` hay bất kỳ URL/slug nào. `serviceNLabel` là chữ hiển thị trong Footer, độc lập với `serviceNPageName` là trang sẽ mở khi nhấn. Danh sách tên trang và ánh xạ tên → route là cấu hình cố định trong FE; backend kiểm tra tên trang nằm trong danh sách hợp lệ. Khi trả dữ liệu, API giữ nguyên tên trang, FE tra route rồi tạo liên kết. Ví dụ theo source FE hiện tại: `"Trang chủ"` → `/`, `"Dịch vụ - Xây dựng trọn gói"` → `/services/turnkey`. Nếu admin chọn Trang chủ cho dịch vụ 1 thì DB lưu `"service1PageName": "Trang chủ"`.

Source FE hiện dùng các ô `service1Href`…`service4Href` và danh sách chọn có `value` là URL. Khi tích hợp API cần đổi cách gửi/đọc bốn ô này sang tên trang. Danh sách chọn chung hiện còn có trang chi tiết Dự án (tên có thể đổi) và neo `#...` (không phải trang); các lựa chọn đó không hợp lệ cho bốn field `PageName` cho tới khi có quy tắc định danh ổn định riêng. Không lưu nhãn trang tùy ý hoặc tự suy URL từ chuỗi do admin gõ.

Social links, label/target của header menu và những CTA khác tiếp tục fix trong FE, không nằm trong `footer`.

---

# 8. Bảng `form_submissions` (thông tin khách gửi qua biểu mẫu)

Tất cả biểu mẫu trên website ghi chung vào bảng này. Admin chỉ quản lý thông tin liên hệ của khách hàng và đánh dấu đã xử lý; DB không phân biệt biểu mẫu được gửi từ trang nào.

| Cột | Kiểu | Null | Mô tả |
|---|---|---:|---|
| `id` | `uuid` | Không | Khóa chính do hệ thống sinh. |
| `customer_name` | `varchar(200)` | Không | Tên khách hàng. |
| `phone` | `varchar(30)` | Không | Số điện thoại khách hàng. Giữ kiểu chuỗi để không mất số 0 đầu. |
| `status` | enum | Không | Trạng thái xử lý: `pending` (đang chờ) hoặc `done` (đã xử lý); mặc định `pending`. |
| `created_at` | `timestamptz` | Không | Thời điểm hệ thống tiếp nhận biểu mẫu. |

Ví dụ một bản ghi trả cho FE/admin:

```json
{
  "id": "01900000-0000-7000-8000-000000000001",
  "customerName": "Nguyễn Văn A",
  "phone": "0934888881",
  "status": "pending",
  "createdAt": "2026-09-23T09:00:00+07:00"
}
```

Không có `email`, `type` (loại biểu mẫu), `source_code` (nguồn gửi), `related_job_id` (khóa ngoại vị trí tuyển dụng), `payload` (dữ liệu riêng), nội dung báo giá, lời nhắn, CV, dự án tham chiếu hoặc FK đến bất kỳ bảng nào. Mọi trang gửi cùng một cấu trúc `customerName`, `phone`; admin chỉ chuyển `status` giữa `pending` và `done`.

Quy tắc ghi và quản lý:

```text
POST  /form-submissions             nhận customerName, phone; status luôn được tạo là pending
GET   /admin/form-submissions       trả danh sách biểu mẫu dùng chung
PATCH /admin/form-submissions/:id   chỉ nhận status = pending hoặc done
```

Client không được gửi `id`, `createdAt` hoặc tự đặt `status` khi tạo. Admin không sửa tên và số điện thoại mà khách đã gửi.

---

# 9. API và validation (kiểm tra dữ liệu) bắt buộc

## Page (trang nội dung)

Người xem chỉ gọi API đọc page và gửi các form đã định nghĩa; không có quyền ghi nội dung. Backend bắt buộc xác thực admin trước mọi PATCH. Cơ chế xác thực nằm ngoài 7 bảng nội dung này, như quy tắc cũ.

```text
GET   /admin/pages/:code                         đọc cây và trả contract đã dựng
PATCH /admin/pages/:code/:section                cập nhật value của section theo whitelist
PATCH /admin/pages/:code/:section/:slotKey       cập nhật value của slot cố định nếu section có item
POST  /admin/pages                             không tồn tại
DELETE /admin/pages/:code                      không tồn tại
POST/DELETE /admin/pages/:code/:section/...    không tồn tại
```

Path được resolve theo manifest và quan hệ `parent_id`; không dùng `slotKey` tùy ý để tạo node mới. Payload chỉ gồm field nội dung của `value`, không nhận các cột cấu trúc `id`, `parent_id`, `code`, `node_key`, `node_kind` hay danh sách item mới. Backend merge vào `value` hiện tại, validate theo `(page_code, node path)` và cập nhật `updated_at` của node và root trong cùng transaction. Khi đọc, backend nhận diện node bằng path key và giữ các slot dưới key `item_01`, `item_02`…; mapper có thể sắp chúng theo manifest để tạo array cho view model ở mục 3.3–3.12.

Payload page không được có các key điều hướng: `href`, `route`, `slug`; cũng không có `ratio`, `aspectRatio`, `width`, `height` hoặc `alt`. Giá trị `ImageUrl` chỉ do backend gán sau khi upload Cloudinary. `site_settings.footer` chỉ giữ `service1PageName` đến `service4PageName` (tên trang hợp lệ), không giữ href/route.

## Project/News (Dự án/Tin tức)

```text
POST/PATCH payload không nhận slug hoặc href.
POST /admin/projects ghi vào bảng projects.
POST /admin/news ghi vào bảng news.
PATCH chỉ sửa record trong bảng tương ứng; payload không nhận type.
Backend tự gọi slug service sau khi validate projects.card_title hoặc news.title.
Đổi projects.card_title/news.title thì tạo current slug mới và giữ slug cũ.
Form projects/list ghi card_title, card_category, card_image_url, is_featured.
Form projects/details ghi detail_content của cùng projects.id; không tạo row khác.
PATCH detail_content merge theo nhóm/slot cố định và validate schema JSON.
```

## Image (hình ảnh)

```text
1. Admin chọn file.
2. Backend kiểm tra MIME và dung lượng.
3. Backend upload file lên Cloudinary.
4. Cloudinary trả URL; backend lưu URL vào field ảnh trong DB.
5. API GET trả URL cho FE.
6. FE render URL vào khung có aspect ratio cố định.
```

## Featured news (Tin tức nổi bật)

```text
PATCH /admin/news/:id
{
  "isFeatured": true
}
```

Không có form chọn link hoặc nhập URL cho Tin nổi bật.
