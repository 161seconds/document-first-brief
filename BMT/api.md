# BMT Decor — API contract để nối toàn bộ FE

**Trạng thái:** tài liệu thiết kế, chưa phải API đã triển khai. Ngày 2026-09-24. Nguồn ưu tiên: [database.md](database.md) (schema v2, 10 bảng), [context.md](context.md), [bmt_landing_data_dictionary.md](bmt_landing_data_dictionary.md), [công thức báo giá](Ham_uoc_tinh_chi_phi_Bao_gia.md), `BMT_FE/features`, `BMT_FE/features/admin/lib/content-pages.ts`, `BMT_FE/docs/bmt_api_contract_v2.md` và `BMT_FE/docs/projects_api_contract_v2.md`. Hiện BE mới có entity `PriceRange`, chưa có controller nghiệp vụ. Các JSON dưới đây là **response/payload đề xuất** để BE và FE cùng triển khai.

## 1. Quy ước chung

| Mục | Contract |
|---|---|
| Base path | `/api/v1` cho mọi endpoint trong tài liệu. `/admin/...` và `/projects/...` là route FE. |
| JSON | `camelCase`; UUID là string; thời gian ISO 8601 UTC; tiền VND là số nguyên, không dùng float. DB dùng tên PascalCase. |
| Auth | Public GET, POST liên hệ và POST ước tính không cần phiên. Mọi `/api/v1/admin/*` cần phiên admin hợp lệ; BE kiểm tra quyền ở API. |
| Create/update | BE sinh `id`, `createdAt`, `updatedAt`, slug và trạng thái mặc định. Client không gửi `isDeleted`, audit fields hoặc `passwordHash`. PATCH chỉ sửa field được phép; field vắng mặt nghĩa là giữ nguyên. |
| Delete | Xóa mềm theo `IsDeleted`; GET public không trả row đã xóa. `204 No Content` khi xóa thành công. |
| List admin | `pageIndex=1&pageSize=20`; trả `value.items`, `pageIndex`, `pageSize`, `totalCount`, `hasNextPage`, `hasPreviousPage`. Bộ lọc cụ thể ở từng resource. |
| Ảnh | Admin upload file trước; các API nội dung nhận URL Cloudinary do upload trả về. Không lưu `blob:`, `data:`, alt, kích thước hay tỷ lệ ảnh trong DB. |
| Success | Đề xuất dùng `BaseResponse` hiện có của BE: `{ "isSuccess": true, "isFailed": false, "value": {...}, "error": null, "traceId": "...", "timestampUtc": "..." }`. `201` có `Location`; `204` không body. |
| Error | Dùng `ErrorResponse` hiện có: `{ "title": "...", "status": 400, "detail": "...", "messageCode": "VALIDATION_ERROR", "errors": {...}, "traceId": "...", "timestampUtc": "..." }`. `400` input sai, `401` chưa đăng nhập, `403` bị từ chối, `404` không thấy, `409` xung đột/giới hạn. |

`BMT_FE/docs/bmt_api_contract_v2.md` dùng response trực tiếp và `{items,total,page,pageSize}`; class `ApiResponse.cs` của BE lại dùng envelope `value` và `pageIndex/totalCount`. **Đây là lựa chọn contract cần đồng bộ trước khi viết client**; tài liệu này chọn envelope BE hiện có. Các ví dụ `value` bên dưới là phần nằm trong envelope, trừ `204` và `301`.

### FE lấy dữ liệu theo page

| FE route | API cần gọi | Dữ liệu ghép |
|---|---|---|
| `/` | `GET /api/v1/pages/home`, `/api/v1/site-settings`, `/api/v1/news?highlightHome=true` | Hero, dự án và dịch vụ nổi bật dạng slot trong `Pages`; tin Home từ `News`; header/partners/footer từ `SiteSettings`. |
| `/about` | `GET /api/v1/pages/about`, `/api/v1/site-settings` | Nội dung giới thiệu và partner/footer dùng chung. |
| `/services`, `/services/{turnkey,design,construction,renovation}` | `GET /api/v1/pages/{pageCode}`, `/api/v1/site-settings` | Mỗi trang dịch vụ có cây content riêng; project teaser trong `Pages`, không join `Projects`. |
| `/projects` | `GET /api/v1/pages/projects`, `/api/v1/project-categories`, `/api/v1/projects`, `/api/v1/site-settings` | Hero và related slot từ `Pages`; filter/card từ `ProjectCategories` + `Projects`. |
| `/projects/[slug]` | `GET /api/v1/projects/{slug}`, `GET /api/v1/pages/projects`, `/api/v1/site-settings` | Detail của row `Projects`, related/contact layout từ `Pages`. |
| `/news` | `GET /api/v1/pages/news`, `/api/v1/news`, `/api/v1/site-settings` | Nội dung trang từ `Pages`, featured/list từ `News`. |
| `/careers` | `GET /api/v1/pages/recruitment`, `/api/v1/jobs`, `/api/v1/site-settings` | Hero/section từ `Pages`, vị trí từ `Jobs`. |
| `/quotation` | `GET /api/v1/pages/quotation`, `POST /api/v1/quotation/estimate`, `/api/v1/site-settings` | Copy từ `Pages`; BE tra `PriceRanges` và tính kết quả, FE chỉ hiển thị. |
| `/contact`, `/capability-profile` | `GET /api/v1/pages/{contact,capability_profile}`, `/api/v1/site-settings` | Nội dung trang và phần chung. |
| Mọi contact form | `POST /api/v1/form-submissions` | Chỉ gửi tên và số điện thoại, kể cả form Báo giá. |
| `/admin/*` | `/api/v1/auth/*`, `/api/v1/admin/pages/*`, `/api/v1/admin/{resource}/*`, `/api/v1/admin/site-settings`, `/api/v1/admin/images` | Bỏ mock state/cookie hiện tại; dùng `id` của DB và API thật. |

`/admin/dashboard` có thể lấy danh sách/count `FormSubmissions` từ endpoint đã nêu; schema không có bảng thống kê dashboard riêng. Các thành phần chỉ render asset, menu, CTA, bản đồ hoặc hiệu ứng từ source FE không cần endpoint. Form liên hệ ở từng page dùng chung một POST nhưng lấy chữ hiển thị từ node `Pages` của page đó.

## 2. `Pages` — nội dung 13 trang và mọi slot cố định

**Bảng:** `Pages(Id, ParentId, Code, NodeKey, NodeKind, Value, IsDeleted, CreatedAt, UpdatedAt)`. `ParentId` tạo cây. `Code` chỉ ở root; `NodeKey` định danh ổn định trong parent; `NodeKind` là `page|section|group|item`; `Value` chỉ chứa nội dung được admin sửa. FE/seed sở hữu route, layout, số slot, thứ tự và CTA cố định. Admin **không** tạo/xóa/chuyển node hoặc sửa key/kind/code.

| Method/path | Dùng ở FE | Request | `value` trả về |
|---|---|---|---|
| `GET /api/v1/pages/{pageCode}` | Public page tương ứng | Path `pageCode` thuộc enum ở bảng dưới | `{pageCode, content}`; `content` là view model ghép từ các node. |
| `GET /api/v1/admin/pages/{pageCode}` | Admin mở editor trang | Path `pageCode` | `{pageCode, nodes:[{id,parentId,nodeKey,nodeKind,value,updatedAt,children}]}`; FE giữ `id` để PATCH. |
| `PATCH /api/v1/admin/pages/{pageCode}/nodes/{nodeId}` | Admin lưu một node/slot | `{ "value": { ... } }` | Node mới `{id,nodeKey,nodeKind,value,updatedAt}`. |
| `PATCH /api/v1/admin/pages/{pageCode}/nodes` | Admin lưu nhiều node/slot trong một lần | `{ "items": [{ "id": "...", "value": { ... } }] }` | `{items:[{id,nodeKey,nodeKind,value,updatedAt}]}`; cập nhật all-or-nothing. |

Ví dụ public:

```http
GET /api/v1/pages/about
```

```json
{"pageCode":"about","content":{"hero":{"eyebrow":"BMT Decor","heading":"Giới thiệu","description":"...","desktopImage":"https://res.cloudinary.com/.../hero.webp"},"journey":{"title":"Hành trình","items":[{"year":"2020","title":"Khởi đầu","description":"..."}]},"contactForm":{"title":"Liên hệ","subtitle":"...","successMessage":"Đã nhận thông tin"}}}
```

Ví dụ admin: FE lấy `id` từ node `about/hero`, upload ảnh nếu có, rồi gửi:

```http
PATCH /api/v1/admin/pages/about/nodes/4d12f2da-20a6-4ba1-91aa-74268a106a1f
Content-Type: application/json
```

```json
{"value":{"heading":"Giới thiệu BMT Decor"}}
```

BE merge field được gửi vào `Value`, validate theo **`(pageCode, đường dẫn node)`**, không thay cả cây hoặc xoá field không gửi. Field lạ, sửa node của page khác, ảnh không hợp lệ hoặc node không được whitelist → `400/404`. FE không gửi `parentId`, `code`, `nodeKey`, `nodeKind`, `children`, `order`.

#### Chi tiết API — Mở đầu Trang chủ (Home Hero)

Base path dùng trong tài liệu này là `/api/v1`. Public GET trả nội dung theo slot; admin lấy cây node để giữ `id` khi cập nhật. Các response thành công được bọc trong `BaseResponse`; JSON mẫu dưới đây cho thấy phần `value`.

**1. FE hiển thị Hero**

```http
GET /api/v1/pages/home
```

Response `200 OK` (`value`):

```json
{
  "pageCode": "home",
  "content": {
    "hero": [
      {
        "title": "BMT Decor - Đơn vị thiết kế và thi công trọn gói",
        "description": "Đồng hành cùng khách hàng từ tư vấn, thiết kế đến thi công hoàn thiện.",
        "desktopImage": "https://res.cloudinary.com/bmt/image/upload/home/hero-01.webp"
      },
      {
        "title": "Đáp ứng đa dạng nhu cầu xây dựng và cải tạo",
        "description": "Giải pháp phù hợp với từng loại hình công trình.",
        "desktopImage": "https://res.cloudinary.com/bmt/image/upload/home/hero-02.webp"
      }
    ]
  }
}
```

Mảng `hero` được dựng từ các node cố định `home → hero → item_01...`; giữ nguyên thứ tự slot đã seed. Mỗi phần tử chỉ có `title`, `description`, `desktopImage` trong dữ liệu BE.

**2. Admin tải nội dung để chỉnh sửa**

```http
GET /api/v1/admin/pages/home
```

Response `200 OK` (`value`, rút gọn còn hai slot Hero):

```json
{
  "pageCode": "home",
  "nodes": [
    {
      "id": "a1111111-1111-4111-8111-111111111111",
      "parentId": "a0000000-0000-4000-8000-000000000001",
      "nodeKey": "item_01",
      "nodeKind": "item",
      "value": {
        "title": "BMT Decor - Đơn vị thiết kế và thi công trọn gói",
        "description": "Đồng hành cùng khách hàng từ tư vấn, thiết kế đến thi công hoàn thiện.",
        "desktopImage": "https://res.cloudinary.com/bmt/image/upload/home/hero-01.webp"
      },
      "updatedAt": "2026-09-24T02:00:00Z",
      "children": []
    },
    {
      "id": "a2222222-2222-4222-8222-222222222222",
      "parentId": "a0000000-0000-4000-8000-000000000001",
      "nodeKey": "item_02",
      "nodeKind": "item",
      "value": {
        "title": "Đáp ứng đa dạng nhu cầu xây dựng và cải tạo",
        "description": "Giải pháp phù hợp với từng loại hình công trình.",
        "desktopImage": "https://res.cloudinary.com/bmt/image/upload/home/hero-02.webp"
      },
      "updatedAt": "2026-09-24T02:00:00Z",
      "children": []
    }
  ]
}
```

Response thực tế trả cả cây `home`; ví dụ lược các node ngoài Hero. Đây là API GET cần cho danh sách slide: FE tìm các node con `item_*` dưới `home/hero`, sắp theo key cố định (`item_01`, `item_02`, ...) và dùng `id` để lưu. Không cần GET riêng cho Hero. Khi chỉ sửa một slide, FE gọi PATCH theo `nodeId`; khi lưu nhiều slide cùng lúc, FE gọi PATCH danh sách bên dưới. Cả hai cách đều chỉ gửi các field đổi.

**3. Admin cập nhật một slide**

```http
PATCH /api/v1/admin/pages/home/nodes/a1111111-1111-4111-8111-111111111111
Content-Type: application/json
```

Request body (chỉ gửi field cần đổi):

```json
{
  "value": {
    "title": "Không gian sống được thiết kế cho bạn",
    "description": "Từ ý tưởng đến công trình hoàn thiện, BMT đồng hành trong từng bước.",
    "desktopImage": "https://res.cloudinary.com/bmt/image/upload/home/hero-new.webp"
  }
}
```

Response `200 OK` (`value`):

```json
{
  "id": "a1111111-1111-4111-8111-111111111111",
  "nodeKey": "item_01",
  "nodeKind": "item",
  "value": {
    "title": "Không gian sống được thiết kế cho bạn",
    "description": "Từ ý tưởng đến công trình hoàn thiện, BMT đồng hành trong từng bước.",
    "desktopImage": "https://res.cloudinary.com/bmt/image/upload/home/hero-new.webp"
  },
  "updatedAt": "2026-09-24T02:30:00Z"
}
```

Chỉ cho phép PATCH node con `item_*` của `home/hero`; `title` và `description` là chuỗi, `desktopImage` là URL ảnh đã upload. Nếu đổi ảnh, admin upload trước qua `POST /api/v1/admin/images` (`multipart/form-data`, field `file`), nhận `{ "url": "https://res.cloudinary.com/..." }` trong `value`, rồi gửi URL đó vào PATCH. FE tiếp tục quản lý `alt`, `href`/CTA, ảnh mobile, số lượng và thứ tự slide; không gửi các field này lên API.

**4. Admin cập nhật nhiều slide**

Dùng endpoint batch khi nút Lưu áp dụng cho nhiều slide đã chỉnh. Mỗi phần tử nhận `id` từ `GET /api/v1/admin/pages/home` và `value` chỉ chứa field cần đổi.

```http
PATCH /api/v1/admin/pages/home/nodes
Content-Type: application/json
```

```json
{
  "items": [
    {
      "id": "a1111111-1111-4111-8111-111111111111",
      "value": {
        "title": "Không gian sống được thiết kế cho bạn",
        "desktopImage": "https://res.cloudinary.com/bmt/image/upload/home/hero-new.webp"
      }
    },
    {
      "id": "a2222222-2222-4222-8222-222222222222",
      "value": {
        "description": "Giải pháp phù hợp với từng loại hình công trình."
      }
    }
  ]
}
```

Response `200 OK` (`value`):

```json
{
  "items": [
    {
      "id": "a1111111-1111-4111-8111-111111111111",
      "nodeKey": "item_01",
      "nodeKind": "item",
      "value": {
        "title": "Không gian sống được thiết kế cho bạn",
        "description": "Đồng hành cùng khách hàng từ tư vấn, thiết kế đến thi công hoàn thiện.",
        "desktopImage": "https://res.cloudinary.com/bmt/image/upload/home/hero-new.webp"
      },
      "updatedAt": "2026-09-24T02:30:00Z"
    },
    {
      "id": "a2222222-2222-4222-8222-222222222222",
      "nodeKey": "item_02",
      "nodeKind": "item",
      "value": {
        "title": "Đáp ứng đa dạng nhu cầu xây dựng và cải tạo",
        "description": "Giải pháp phù hợp với từng loại hình công trình.",
        "desktopImage": "https://res.cloudinary.com/bmt/image/upload/home/hero-02.webp"
      },
      "updatedAt": "2026-09-24T02:30:00Z"
    }
  ]
}
```

Batch chỉ chấp nhận node con `item_*` của `home/hero`; mỗi `value` được whitelist và merge vào node tương ứng. Nếu có phần tử trùng `id`, `id` không thuộc Hero, field không hợp lệ hoặc một giá trị sai định dạng, từ chối toàn bộ request, không cập nhật node nào (`400` cho payload/field sai, `404` cho node không tồn tại hoặc không thuộc Hero). BE cập nhật `updatedAt` của từng node thành công; danh sách slot và thứ tự vẫn do seed/FE quản lý.

### `pageCode`, UI và các nhóm field trong `content`

Các tên nhóm là **FE view model** ghép từ nhiều row `Pages`; không phải một row JSON duy nhất. Chi tiết mọi slot và field hợp lệ ở data dictionary mục 3.3–3.12.

| `pageCode` | Public route; admin area | `content` và ý nghĩa field chính |
|---|---|---|
| `home` | `/`; `/admin/home/*` | `hero[]` slide (`title`,`description`,`desktopImage`); `featuredProjects` có title/description và group 8 slot với `title`,`area`,`styleText`,`year`,`image`; `featuredServices.items[]` title/description/ảnh desktop, mobile; `statistics[]` value/label/suffix; `whyBmt` copy và ảnh; `profileSection` copy/ảnh sách; `contactForm` copy/thông báo. Tin Home từ `News`, không nằm trong JSON này. |
| `about` | `/about`; `/admin/about/*` | `hero`, `journey.items[]`, `coreValues.items[]`, `visionMission`, `capabilities.items[]`, `contactForm`. `year` là mốc hành trình; `mobileTitle` là chữ riêng mobile. |
| `services` | `/services`; `/admin/services/overview/*` | `hero` copy/ảnh/cards; `serviceList[]` label/title/tagline/description/image; `process.items[]` title/description/ảnh đóng-mở; `faq.items[]` question/answer; `contactForm`. Identity/route dịch vụ theo slot cố định FE. |
| `service_turnkey` | `/services/turnkey`; admin `xay-dung-tron-goi` | `hero` copy và các ảnh `desktopArtwork`,`mobileArtwork`,`sideDecoration`; `featuredProjects.items[]` teaser; `solutions.items[]` title/checklist/image; `process.items[]`; `contactForm`. |
| `service_architecture_interior` | `/services/design`; admin `thiet-ke-kien-truc-noi-that` | Cùng nhóm service detail; hero image keys `wireframeImage`,`leftImage`,`centerImage`,`rightImage`,`mobileArtwork`. |
| `service_construction` | `/services/construction`; admin `thi-cong-xay-dung` | Cùng nhóm service detail; hero image keys `wireframeImage`,`topImage`,`rightImage`,`bottomImage`,`leftImage`,`mobileBlueprint`. |
| `service_renovation` | `/services/renovation`; admin `cai-tao-sua-chua` | Cùng nhóm service detail; hero image keys `wireframeImage`,`largeImage`,`topImage`,`bottomImage`. |
| `projects` | `/projects`, `/projects/[slug]`; admin page content | `hero`, `listSection.title`, `relatedSection.title/items[]`, `contactForm`. Thẻ dự án thật từ `Projects`. |
| `news` | `/news`; admin page content | `hero`, `featuredSection.title`, `contactForm`. Bài viết từ `News`. |
| `recruitment` | `/careers`; admin page content | `hero`, `jobsSection.title`, `contactForm`. Job từ `Jobs`. |
| `quotation` | `/quotation`; `/admin/quotation/*` | `hero`; `estimator.stepLabels`, heading/instruction/options/placeholder/unit của building/area/budget/service, `resultIncludeLabel`; `contactForm` có `requiredMessage`. Giá thật từ `PriceRanges`, không lưu trong page. |
| `contact` | `/contact`; `/admin/contacts/*` | `hero.title/description/photo`, `map.title`, `form.title/subtitle/successMessage`. Google Maps URL nằm trong FE/config, không thuộc `Pages.Value`. |
| `capability_profile` | `/capability-profile`; `/admin/capability-profile/*` | `hero.title/subtitle/description/heroImage/decor08`, `document.heading`, `contactForm`. URL tài liệu/preview còn cố định ở FE. |

Các field như `title` là chữ hiển thị; `description`/`subtitle` là mô tả; `*Image`/`photo` là URL ảnh; `successMessage` là toast sau khi POST liên hệ thành công. `items[]` chỉ là **view model** từ slot seed cố định. Admin PATCH theo `nodeId`, không POST item mới. Các field `href`, `ctaHref`, `imageAlt`, `order`, `googleMapsUrl`, ảnh trang trí trong mock FE không mặc nhiên được lưu trong DB. Riêng `home.featuredProjects` và `service_*.featuredProjects` là nội dung biên tập trong `Pages`; không đồng bộ tự động với checkbox `Projects.IsFeatured`.

## 3. `ProjectCategories` — danh mục thẻ dự án

**Bảng:** `Id` UUID, `Name` tối đa 100 ký tự, audit/soft delete. Seed bốn danh mục hiện có trong FE; `Projects.CategoryId` tham chiếu `Id`.

| Method/path | UI | Request | `value` |
|---|---|---|---|
| `GET /api/v1/project-categories` | Tab/filter `/projects`, select admin | Không | `[{id,name}]` (chỉ active). |
| `GET /api/v1/admin/project-categories` | Admin danh mục | Không | `[{id,name,createdAt,updatedAt}]`. |
| `POST /api/v1/admin/project-categories` | Tạo danh mục nếu UI hỗ trợ | `{name}` | `201` `{id,name,createdAt,updatedAt}`. |
| `PATCH /api/v1/admin/project-categories/{id}` | Đổi tên | `{name}` | Object sau sửa. |
| `DELETE /api/v1/admin/project-categories/{id}` | Xóa | Không | `204`; `409` nếu còn project tham chiếu. |

`id` là giá trị FE gửi trong `categoryId`; `name` chỉ để hiển thị. `Name` unique; FE hiện có bốn tab/icon cố định, nên danh mục mới cần quy tắc UI trước khi bật thao tác tạo ở admin.

## 4. `Projects` — card và chi tiết cùng một row

**Bảng:** `Id`, `CardTitle`, `CategoryId`, `CardImageUrl`, `DetailContent jsonb`, `IsFeatured`, audit/soft delete. `CardTitle` sinh slug; `DetailContent.overview.title/category` là nhãn riêng trong trang detail. `IsFeatured` giới hạn 8 active project **mỗi category**, không điều khiển các teaser đã lưu trong `Pages`.

| Method/path | UI | Request | `value` |
|---|---|---|---|
| `GET /api/v1/projects?categoryId={uuid}` | `/projects`, filter client/server | `categoryId` tùy chọn | `[{id,slug,title,categoryId,categoryName,imageUrl,isFeatured}]`. |
| `GET /api/v1/projects/{slug}` | `/projects/[slug]` | Slug trong URL | Detail public; slug cũ → `301 Location: /projects/{currentSlug}` hoặc contract redirect thống nhất. |
| `GET /api/v1/admin/projects?pageIndex=1&pageSize=20&q=&categoryId=&isFeatured=` | Admin list | Query filter tùy chọn | Paginated project cards, cả bản nháp detail rỗng. |
| `GET /api/v1/admin/projects/{id}` | Admin card/detail editor | UUID | `{id,slug,card,detail,createdAt,updatedAt}`. |
| `POST /api/v1/admin/projects` | Tạo card | `{cardTitle,categoryId,cardImageUrl?}` | `201` project mới, `detail:{}`, slug BE sinh. |
| `PATCH /api/v1/admin/projects/{id}/card` | Form card | `{title?,categoryId?,imageUrl?}` | Project sau sửa. |
| `PATCH /api/v1/admin/projects/{id}/detail` | Form detail | Object gồm các nhóm cần sửa | Project sau sửa; merge field theo tên. |
| `PATCH /api/v1/admin/projects/{id}/featured` | Checkbox “Tiêu biểu” | `{isFeatured:boolean}` | Project sau sửa; `409` nếu vượt 8. |
| `DELETE /api/v1/admin/projects/{id}` | Admin xóa | Không | `204`; public và slug của row không còn resolve. |

Ví dụ admin GET (`value`):

```json
{"id":"4d12f2da-20a6-4ba1-91aa-74268a106a1f","slug":"nha-pho-quan-9","card":{"title":"Nhà phố Quận 9","categoryId":"11111111-1111-4111-8111-111111111111","categoryName":"Nhà ở","imageUrl":"https://res.cloudinary.com/.../card.webp","isFeatured":false},"detail":{"overview":{"title":"Dự án nhà phố","projectName":"Nhà Phố 2 Tầng","category":"Nhà Phố","location":"Quận 9","client":"Anh Nhân","area":"210m²","scale":"2 tầng","heroImage":"https://res.cloudinary.com/.../hero.webp","wordmarkImage":null,"description":"..."},"survey":{"survey1Image":null,"surveyDescription":"..."}},"createdAt":"2026-09-24T02:00:00Z","updatedAt":null}
```

| Field/nhóm | Ý nghĩa và FE ghép |
|---|---|
| `card.title`, `categoryId`, `categoryName`, `imageUrl`, `isFeatured` | Map từ `CardTitle`, FK, join `ProjectCategories.Name`, `CardImageUrl`, `IsFeatured`; render card/filter. FE hiển thị `categoryName`, gửi `categoryId`. |
| `slug` | Lookup từ `ContentSlugs` hiện hành; FE tạo link `/projects/{slug}`, không cho admin nhập. |
| `detail.overview` | Hero/tên/vị trí/chủ đầu tư/diện tích/quy mô/mô tả; `category` là nhãn detail, không phải FK. |
| `detail.survey` | `survey1Image`–`survey3Image`, `surveyDescription`: hiện trạng. |
| `detail.solution` | `drawingCaption`, `solutionDescription`, `drawingImage`: phương án. |
| `detail.renders` | `render1Image`–`render6Image`, `galleryDescription`: phối cảnh 3D. |
| `detail.process` | `process1Label/process1Image` đến 4, `processDescription`: quá trình thi công. |
| `detail.comparisons` | `comparison1BeforeLabel/Image` và `AfterLabel/Image` đến slot 3: slider trước/sau. |
| `detail.contactForm` | `ctaTitle`, `ctaDescription`, `ctaSuccessMessage`: form cuối detail. |

Tên field ảnh có số là key cố định, **không phải mảng**. `PATCH /detail` không được xóa nhóm/field không gửi. Ảnh chưa có có thể là `null` ở form admin. DB chưa có trạng thái publish/draft: **TBD** quy tắc ẩn detail rỗng khỏi public; không trả trang detail hỏng.

## 5. `News` — tin trang News và Home

**Bảng:** `Id`, `Title`, `ImageUrl?`, `Content {body}`, `IsFeatured`, `HighlightHome`, `Excerpt?`, audit/soft delete. `IsFeatured` tối đa 5 cho `/news`; `HighlightHome` tối đa 4 cho `/`, sắp xếp `CreatedAt DESC`. Hai cờ độc lập.

| Method/path | UI | Request | `value` |
|---|---|---|---|
| `GET /api/v1/news?isFeatured=true` | Featured `/news` | Filter tùy chọn | List `[{id,slug,title,excerpt,imageUrl,isFeatured,highlightHome,createdAt}]`. |
| `GET /api/v1/news?highlightHome=true` | Tin Trang chủ | Filter; BE sort mới trước | List tối đa 4. |
| `GET /api/v1/news/{slug}` | Bài chi tiết nếu FE bổ sung route | Slug | Bài gồm `content.body`; slug cũ 301. FE hiện dùng anchor `/news#...`, chưa có page bài riêng. |
| `GET /api/v1/admin/news?pageIndex=1&pageSize=20&q=&isFeatured=&highlightHome=` | Admin news list | Filter tùy chọn | Paginated item. |
| `GET /api/v1/admin/news/{id}` | Admin edit | UUID | Toàn bộ item + `content`. |
| `POST /api/v1/admin/news` | Admin create | `{title,imageUrl?,excerpt?,content:{body},isFeatured?,highlightHome?}` | `201` item + slug. |
| `PATCH /api/v1/admin/news/{id}` | Admin edit/checkbox | Các field trên cần sửa | Item sau sửa. |
| `DELETE /api/v1/admin/news/{id}` | Admin xóa | Không | `204`. |

`title` tối đa 300, là nguồn slug; `excerpt` mô tả ngắn; `imageUrl` ảnh card; `content.body` rich text bắt buộc; `isFeatured`/`highlightHome` là hai lựa chọn UI. FE hiện có `desktopImage`, `mobileImage`, `imageAlt`, `href` trong mock `NewsArticle`; DB chỉ lưu một `ImageUrl`. FE dùng ảnh đó cho các breakpoint, giữ alt/URL route theo logic FE. Không gửi `href`, ảnh mobile hay alt vào API.

Ví dụ request/response `value`:

```json
{"title":"Kinh nghiệm cải tạo nhà","imageUrl":"https://res.cloudinary.com/.../news.webp","excerpt":"Các bước chuẩn bị","content":{"body":"<p>Nội dung bài viết...</p>"},"isFeatured":true,"highlightHome":false}
```

```json
{"id":"22222222-2222-4222-8222-222222222222","slug":"kinh-nghiem-cai-tao-nha","title":"Kinh nghiệm cải tạo nhà","imageUrl":"https://res.cloudinary.com/.../news.webp","excerpt":"Các bước chuẩn bị","content":{"body":"<p>Nội dung bài viết...</p>"},"isFeatured":true,"highlightHome":false,"createdAt":"2026-09-24T02:00:00Z","updatedAt":null}
```

BE validate giới hạn featured trong transaction, kể cả request đồng thời. Quy tắc public cho bài draft/chưa hoàn thiện chưa có cột trạng thái: **TBD**.

## 6. `Jobs` — vị trí tuyển dụng

**Bảng:** `Id`, `Title`, `Department?`, `Location?`, `Schedule?`, `Compensation?`, `Summary?`, `ImageUrl?`, `Content`, audit/soft delete. `Content` có hai chuỗi HTML bắt buộc `responsibilities`, `benefits`.

| Method/path | UI | Request | `value` |
|---|---|---|---|
| `GET /api/v1/jobs` | `/careers` list + modal | Không | List public, mỗi item có `responsibilities:string[]`, `benefits:string[]`. |
| `GET /api/v1/jobs/{id}` | Chỉ nếu modal cần lazy fetch | UUID | Một job public; FE hiện có thể dùng list, endpoint này không bắt buộc. |
| `GET /api/v1/admin/jobs?pageIndex=1&pageSize=20` | Admin jobs list | Pagination | Paginated admin item. |
| `GET /api/v1/admin/jobs/{id}` | Admin edit | UUID | Item, `content` giữ nguyên hai HTML string. |
| `POST /api/v1/admin/jobs` | Admin create | DTO bên dưới | `201` item. |
| `PATCH /api/v1/admin/jobs/{id}` | Admin edit | Các field cần sửa | Item sau sửa. |
| `DELETE /api/v1/admin/jobs/{id}` | Admin xóa | Không | `204`. |

```json
{"title":"Kiến trúc sư","department":"Thiết kế","location":"TP.HCM","schedule":"Toàn thời gian","compensation":"Thỏa thuận","summary":"Triển khai hồ sơ","imageUrl":"https://res.cloudinary.com/.../job.webp","content":{"responsibilities":"<ul><li>Khảo sát công trình</li></ul>","benefits":"<ul><li>Đào tạo chuyên môn</li></ul>"}}
```

Admin response trả các field đó cùng `id,createdAt,updatedAt`. Public mapper trả `image` hoặc `imageUrl` theo DTO đã chốt, và **hai mảng riêng** để ghép `CareerJob.responsibilities`/`benefits` trong `BMT_FE/features/careers/data/jobs.ts`; không nối hai HTML list. `title` là tên vị trí; `department` phòng ban; `location` địa điểm; `schedule` hình thức; `compensation` thu nhập; `summary` mô tả ngắn; `imageUrl` ảnh. Không có slug/status/deadline trong DB; **TBD** quy tắc ẩn job không còn tuyển.

## 7. `ContentSlugs` — route dự án và tin

**Bảng:** `Id`, `Slug` unique toàn cục, `ProjectId?`, `NewsId?`, `IsCurrent`, audit/soft delete. Đúng **một** owner (`ProjectId` xor `NewsId`), tối đa một slug hiện hành mỗi owner. Không có CRUD slug cho FE/admin.

| Luồng | BE làm gì | FE làm gì |
|---|---|---|
| Tạo project/news | Sinh slug từ `CardTitle`/`Title` cùng transaction tạo row. | Dùng `slug` trong response để tạo href. |
| Đổi title | Sinh slug mới, `IsCurrent=true`, slug cũ false; retry khi trùng toàn cục. | Dùng slug mới sau PATCH. |
| GET slug hiện hành | Resolve owner active. | Render detail. |
| GET slug cũ | `301` tới slug hiện hành khi owner còn active. | Trình duyệt chuyển hướng. |
| Owner bị soft delete | Không resolve slug public. | Render 404. |

`GET /api/v1/projects/{slug}` và `GET /api/v1/news/{slug}` là hai entrypoint dùng bảng này. `ContentSlugs` không cần endpoint riêng.

## 8. `SiteSettings` — header, partners, footer dùng chung

**Bảng:** một row `Id=1`, `Header jsonb`, `Partners jsonb`, `Footer jsonb`, audit/soft delete. FE gọi public một lần ở layout rồi cấp dữ liệu cho Header, PartnerSection và Footer. Route menu, social link và CTA vẫn cố định trong FE.

| Method/path | UI | Request | `value` |
|---|---|---|---|
| `GET /api/v1/site-settings` | Layout mọi page | Không | `{header,partners,footer}`. |
| `GET /api/v1/admin/site-settings` | `/admin/settings/*` | Không | `{header,partners,footer,updatedAt}`. |
| `PATCH /api/v1/admin/site-settings/header` | Đổi logo đầu trang | `{logo}` | Section mới `{logo}`. |
| `PATCH /api/v1/admin/site-settings/partners` | Tiêu đề + 6 logo | `{title?,partner1LogoImage?,...,partner6LogoImage?}` | Section mới. |
| `PATCH /api/v1/admin/site-settings/footer` | Nội dung chân trang | Các field footer được phép | Section mới. |

```json
{"header":{"logo":"https://res.cloudinary.com/.../header.webp"},"partners":{"title":"Đối tác","partner1LogoImage":"https://res.cloudinary.com/.../p1.webp","partner2LogoImage":"https://res.cloudinary.com/.../p2.webp","partner3LogoImage":"https://res.cloudinary.com/.../p3.webp","partner4LogoImage":"https://res.cloudinary.com/.../p4.webp","partner5LogoImage":"https://res.cloudinary.com/.../p5.webp","partner6LogoImage":"https://res.cloudinary.com/.../p6.webp"},"footer":{"footerLogo":"https://res.cloudinary.com/.../footer.webp","service1Label":"Xây dựng trọn gói","service1PageName":"Dịch vụ - Xây dựng trọn gói","contactHeading":"Liên hệ","officeAddress":"...","phone":"0900000000","email":"hello@example.com","branchesHeading":"Chi nhánh","branch1Address":"...","branch2Address":"...","workshopAddress":"...","socialWidgetImage":"https://res.cloudinary.com/.../social.webp"}}
```

| Field | Ý nghĩa/FE mapping |
|---|---|
| `header.logo` | Ảnh logo đầu trang. Menu/route không nằm trong DB. |
| `partners.title`, `partner1LogoImage`–`partner6LogoImage` | Tiêu đề và sáu logo slot cố định. Không có array, URL đối tác hay thứ tự admin. |
| `footer.footerLogo`, `socialWidgetImage` | Hai ảnh riêng: logo chân trang và ảnh widget mạng xã hội. |
| `service1Label/service1PageName`–`service4Label/service4PageName` | Chữ hiển thị và **tên page** allowlisted. FE map page name sang route cố định; không gửi `serviceNHref` từ mock. |
| `contactHeading`, `officeAddress`, `phone`, `email` | Tiêu đề, địa chỉ, điện thoại và email ở footer. |
| `branchesHeading`, `branch1Address`, `branch2Address`, `workshopAddress` | Nhãn và địa chỉ chi nhánh/xưởng. |

BE validate `PageName` trong tập tên trang hợp lệ; FE select hiển thị tên và gửi chính tên. Không cho nhập URL/anchor/project detail tùy ý vào field này.

## 9. `FormSubmissions` — tất cả form liên hệ

**Bảng:** `Id`, `CustomerName`, `Phone`, `Status pending|done`, audit/soft delete. Không lưu page nguồn, email, lời nhắn, CV hoặc dữ liệu kết quả báo giá.

| Method/path | UI | Request | `value` |
|---|---|---|---|
| `POST /api/v1/form-submissions` | `ContactForm` dùng chung và `QuotationContactForm` | `{customerName,phone}` | `201` `{id,createdAt}`; `Status=pending` do BE đặt. |
| `GET /api/v1/admin/form-submissions?status=pending&pageIndex=1&pageSize=20` | `/admin/contact-submissions`, dashboard | Filter status + pagination | `items:[{id,customerName,phone,status,createdAt}]` + page metadata. |
| `PATCH /api/v1/admin/form-submissions/{id}/status` | Checkbox “Đã duyệt” | `{status:"done"}` hoặc `pending` | `{id,customerName,phone,status,createdAt,updatedAt}`. |
| `DELETE /api/v1/admin/form-submissions/{id}` | Nút Xóa | Không | `204` soft delete. |

```json
{"customerName":"Nguyễn Văn A","phone":"0901234567"}
```

```json
{"id":"33333333-3333-4333-8333-333333333333","customerName":"Nguyễn Văn A","phone":"0901234567","status":"pending","createdAt":"2026-09-24T02:00:00Z"}
```

FE map form field `name` → `customerName`; dashboard `reviewed=false` ↔ `pending`, `true` ↔ `done`; `submittedAt` ↔ `createdAt`. Chỉ toast/reset sau `201`, giữ input khi lỗi. Số điện thoại giữ dạng string để không mất số 0; validate nonempty, max 30 và định dạng phù hợp. Public endpoint cần rate limit. Riêng form Báo giá **không** gửi building/service/area/budget/estimate.

## 10. `PriceRanges` — 16 cặp giá cho `/quotation`

**Bảng:** `Id`, `BuildingType`, `ServiceType`, `UnitPriceMin`, `UnitPriceMax`, audit/soft delete. Seed đúng 4×4 cặp từ [công thức báo giá](Ham_uoc_tinh_chi_phi_Bao_gia.md). Admin chỉ sửa hai giá của cặp đã seed; không create/delete/đổi enum pair.

| Method/path | UI | Request | `value` |
|---|---|---|---|
| `POST /api/v1/quotation/estimate` | Bước 05 của `/quotation` | `{buildingType,areaM2,budget?,serviceType}` | `200` khoảng ước tính, đơn giá hiển thị và so sánh ngân sách; không lưu request/kết quả. |
| `GET /api/v1/admin/price-ranges` | Admin bảng giá cần bổ sung | Không | 16 item như trên. |
| `PATCH /api/v1/admin/price-ranges/{id}` | Sửa min/max | `{unitPriceMin,unitPriceMax}` | Item sau sửa. |

```json
{"id":"44444444-4444-4444-8444-444444444444","buildingType":"nha_o","serviceType":"thi_cong_xay_dung","unitPriceMin":3350000,"unitPriceMax":4300000,"updatedAt":null}
```

`buildingType`: `nha_o`, `van_phong`, `tham_my_vien_showroom`, `nha_hang_khach_san`. `serviceType`: `xay_dung_tron_goi`, `thiet_ke_kien_truc_noi_that`, `thi_cong_xay_dung`, `cai_tao_sua_chua`. FE map vị trí option cố định sang enum code; **không** lookup bằng label `Pages.quotation.estimator.*.options` vì admin có thể sửa chữ. `unitPriceMin/Max` là VND/m², min > 0, max ≥ min. FE hiện có bốn mức hardcode theo service và sẽ bỏ phép tính đó để dùng response BE. FE chưa có UI sửa giá; bổ sung nếu admin được quản lý bảng giá.

Request ước tính dùng enum code, không dùng nhãn có thể sửa trong `Pages`. `areaM2` là số trong khoảng 10–50.000. `budget` là số VND từ 0 đến 1.000 tỷ, có thể bỏ hoặc gửi `null` theo tài liệu công thức. BE tra đúng cặp active, tính `estimateMin/Max = areaM2 × unitPriceMin/Max` và `displayUnitPrice = (unitPriceMin + unitPriceMax) / 2`, làm tròn cả ba đến 1.000 VND. Ngân sách chỉ tạo `budgetComparison`, không đổi khoảng ước tính. Nếu thiếu cặp giá, trả lỗi thay vì dùng giá mặc định. Response nằm trong `value` của envelope chung.

```json
{"buildingType":"nha_o","areaM2":100,"budget":400000000,"serviceType":"thi_cong_xay_dung"}
```

```json
{"buildingType":"nha_o","serviceType":"thi_cong_xay_dung","areaM2":100,"unitPriceMin":3350000,"unitPriceMax":4300000,"estimateMin":335000000,"estimateMax":430000000,"displayUnitPrice":3825000,"budgetComparison":{"status":"phu_hop","difference":0,"ratio":0,"budgetPerM2":4000000}}
```

`budgetComparison` là `null` nếu không có ngân sách. Khi có ngân sách, `status` là `thieu|phu_hop|du`; `difference`, `ratio` (3 chữ số thập phân) và `budgetPerM2` (làm tròn đến đồng) theo công thức báo giá. FE định dạng tiền/chữ hiển thị; không gửi kết quả ước tính vào `FormSubmissions`.

## 11. `AdminUsers` — phiên quản trị

**Bảng:** `Id`, `Email` normalized/lowercase unique, `PasswordHash`, `IsActive`, audit/soft delete. Không trả hash, không public CRUD tài khoản từ FE hiện tại. `DisplayName` đang comment trong schema, nên không đặt nó là field bắt buộc trong response.

| Method/path | UI | Request | `value` |
|---|---|---|---|
| `POST /api/v1/auth/login` | `/admin/login` | `{email,password}` | `{id,email}` và protected session cookie **hoặc** token theo cơ chế auth chốt. |
| `GET /api/v1/auth/me` | Guard/layout admin | Phiên hiện tại | `{id,email}`; `401` nếu phiên mất hiệu lực. |
| `POST /api/v1/auth/logout` | Admin logout | Phiên hiện tại | `204`, hủy phiên/token. |

```json
{"email":"admin@example.com","password":"<password>"}
```

BE kiểm tra hash và `IsActive`, chặn user soft deleted. FE xóa `MOCK_ADMIN_ACCOUNT`, `ADMIN_MOCK_SESSION` và cookie giá trị cố định trong `features/admin/lib/auth-config.ts`, `features/admin/auth/actions.ts`, `proxy.ts`; bảo vệ route FE chỉ để UX, mọi API admin vẫn tự kiểm tra auth. Cơ chế cookie/token, refresh/expiry và CSRF khi dùng cookie: **TBD** trước implement; không tự đưa token vào localStorage. Không có API tạo/sửa admin user vì UI hiện không có.

## 12. Upload ảnh (hỗ trợ nhiều bảng, không có bảng riêng)

| Method/path | UI | Request | `value` |
|---|---|---|---|
| `POST /api/v1/admin/images` | Mọi `ImageField` admin | `multipart/form-data`, field `file` là ảnh | `201` hoặc `200` `{url:"https://res.cloudinary.com/..."}`. |

FE chọn file → preview tạm → upload → nhận `url` → PATCH/POST nội dung tương ứng. Upload thành công **chưa** tự lưu form. BE kiểm tra MIME thực, kích thước và nguồn URL cho field ảnh; không nhận `blob:` từ `URL.createObjectURL`. Field ảnh có thể thuộc `Pages.Value`, `Projects`, `News`, `Jobs` hoặc `SiteSettings`.

## 13. Validation, tình huống lỗi và điểm cần chốt

| Trường hợp | Kỳ vọng |
|---|---|
| Không có phiên gọi admin | `401`; user inactive/không được phép `403` (hoặc `401` ở login để tránh lộ tài khoản). |
| PATCH node không thuộc page, field khóa/field lạ | `400/404`, không thay dữ liệu. |
| Danh mục trùng/đang có project; slug trùng | `409`; slug generator retry trong transaction. |
| Bật project featured thứ 9 trong category; news featured thứ 6 hoặc home highlight thứ 5 | `409`; kiểm tra transaction để chống race. |
| Giá ≤ 0, max < min, enum pair bị sửa | `400`. |
| POST liên hệ sai tên/phone; spam | `400`; rate limit `429`. |
| GET owner/slug đã soft delete | `404`. Slug cũ của owner active `301`. |

**Điểm cần chốt trước khi code client:** (1) BE envelope ở mục 1 hay DTO trực tiếp của contract FE cũ; (2) auth cookie hay token và CSRF; (3) quy tắc public cho project/news/job chưa có trạng thái publish trong schema; (4) form ngân sách tùy chọn hay bắt buộc; (5) news hiện dùng anchor trong `/news`, chưa có page `/news/[slug]`; (6) bốn tab project cố định sẽ hiển thị category mới thế nào; (7) chính sách xóa/retention của lead. Không tự thêm cột cho các câu hỏi này.

### Kiểm thử tích hợp tối thiểu

1. GET/PATCH page node đúng page, field lạ bị từ chối; FE reload vẫn thấy nội dung mới.
2. Project card/detail cùng UUID; category ID đúng; slug cũ 301; giới hạn featured đúng khi chuyển category.
3. News hai cờ độc lập; Home và News hiện đúng danh sách/giới hạn.
4. Jobs hai rich text HTML riêng được map thành hai mảng riêng trên `/careers`.
5. Gửi form chung và form Báo giá từ browser A, thấy ở admin browser B; PATCH status, delete và phân trang đúng.
6. FE gửi building + service + area + budget tùy chọn; BE tính từ cặp giá hiện hành. Sửa giá admin rồi gọi lại phải nhận kết quả mới; không lưu request/kết quả ước tính.
7. Upload ảnh trả URL bền vững; reload public không còn `blob:`.
8. Không có phiên không gọi được bất kỳ `/api/v1/admin/*`; logout làm phiên hết hiệu lực.
