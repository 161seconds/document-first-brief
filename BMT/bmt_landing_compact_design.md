# Thiết kế DB rút gọn cho BMT Decor

## Kết luận

Mô hình tách Dự án và Tin tức, đồng thời giữ lịch sử slug, gồm **7 bảng**:

1. `pages` (nội dung các trang) — cây key-value đệ quy và JSON nội dung trong **một bảng**
2. `projects` (Dự án)
3. `news` (Tin tức)
4. `jobs` (Vị trí tuyển dụng)
5. `content_slugs` (lịch sử đường dẫn Dự án và Tin tức)
6. `site_settings` (cấu hình chung của website)
7. `form_submissions` (thông tin khách gửi qua biểu mẫu)

Không có user, role, session, audit log, revision, page builder, media library hay bảng section.

## Trang và layout (bố cục) cố định

`pages` chứa một cây node được seed sẵn cho mỗi `page_code`: node gốc là trang, con là section/group/item. `parent_id` tạo quan hệ đệ quy, `node_key` là key ổn định do FE định nghĩa, còn `value jsonb` giữ text và ảnh của node. Toàn bộ nội dung page nằm trong **một bảng `pages`**; `site_settings` vẫn dành cho Header/Partners/Footer dùng chung. Route, component, thứ tự section và số slot nội dung cố định nằm trong frontend/seed manifest.

Whitelist cho `value` được đối chiếu với các field thực sự hiện trong form admin của `BMT_FE`. Những thuộc tính nằm trong dữ liệu mock nhưng bị khóa, ẩn hoặc không có ô sửa chỉ ở FE/manifest: ảnh trang trí, alt, nhãn nhóm, số thứ tự, CTA và placeholder cố định. `id`, slug, trạng thái xử lý form và timestamp là metadata vận hành do hệ thống ghi, không phải nội dung admin biên tập.

Ví dụ cây của trang Giới thiệu:

```text
about (page, value={})
├─ hero (section, value={eyebrow, heading, description, desktopImage})
├─ journey (section, value={title})
│  ├─ item_01 (item, value={year, title, description})
│  ├─ ...
│  └─ item_06 (item, value={year, title, description})
├─ visionMission (section, value={visionHeading, visionDescription, missionHeading, missionDescription})
├─ coreValues (section, value={title})
│  ├─ item_01 (item, value={title, description, image})
│  ├─ ...
│  └─ item_05 (item, value={title, description, image})
├─ capabilities (section, value={title})
│  ├─ item_01 (item, value={title, mobileTitle?, description})
│  ├─ ...
│  └─ item_04 (item, value={title, mobileTitle?, description})
└─ contactForm (section, value=ContactFormConfig)
```

Trong cây trên: `hero` (mở đầu), `journey` (hành trình), `visionMission` (tầm nhìn và sứ mệnh), `coreValues` (giá trị cốt lõi), `capabilities` (năng lực), `contactForm` (biểu mẫu liên hệ). Tên key trong DB giữ nguyên để khớp mã FE.

Các key `item_01` là slot cố định; năm hoặc tiêu đề thay đổi không làm thay key. Trang `/about` hiện hiển thị 6 mốc Hành trình, 5 Giá trị cốt lõi và 4 Năng lực; seed/manifest tương ứng dùng `item_01`–`item_06`, `item_01`–`item_05` và `item_01`–`item_04`. FE quyết định thứ tự render từ manifest, không từ `sort_order` do admin nhập. Những section chỉ có ít field có thể lưu ngay trong `value`; nhóm lặp dùng node con để sửa từng item mà không ghi lại toàn bộ trang. Không đặt cùng một field ở cả node cha và node con.

Admin chỉ được thay chữ và ảnh trong những khung đã có. Admin không được:

- Tạo/xóa trang hoặc node của cây trang.
- Đổi route.
- Thêm/xóa/đổi thứ tự section hoặc slot item cố định.
- Đổi component hoặc loại section.
- Nhập CTA URL, menu URL hoặc đường dẫn nội bộ, ngoại trừ chọn route cho các dịch vụ trong Footer.
- Nhập tỷ lệ, chiều rộng hoặc chiều cao ảnh.

Backend nên cung cấp endpoint theo section, ví dụ:

```text
PATCH /admin/pages/about/hero
PATCH /admin/pages/about/journey
PATCH /admin/pages/about/vision-mission
```

Mỗi endpoint xác định node bằng `page_code` và path key cố định, chỉ merge field được whitelist vào `value` rồi validate bằng schema tương ứng. Backend không nhận `id`, `parent_id`, `code`, `node_key`, `node_kind` hay đường dẫn tùy ý từ payload admin. Đọc trang dùng recursive CTE hoặc tải toàn bộ node theo root rồi dựng cây trong code; FE render theo manifest cố định.

API công khai của người xem chỉ đọc dữ liệu để render và gửi form liên hệ/báo giá/ứng tuyển như quy tắc cũ; không có endpoint công khai ghi nội dung. Mọi PATCH nội dung phải qua xác thực admin ở backend. Không thêm bảng user/role/session vào mô hình DB này.

DB cần `CHECK` để root có `parent_id IS NULL`, `code` khác NULL, `node_kind = 'page'`; node con có `parent_id` khác NULL và `code IS NULL`. Unique partial index trên `code` của root; unique `(parent_id, node_key)` cho node con. Code/seed kiểm tra không có cycle, node path hợp lệ và mỗi trang đủ node bắt buộc. Quyền ghi của admin chỉ cập nhật `value` và `updated_at` trên node được whitelist.

Đối chiếu source FE tại `BMT_FE` ngày 22/09/2026: `/about` hiển thị Hero, Hành trình, Tầm nhìn/Sứ mệnh/Giá trị cốt lõi, Năng lực, Đối tác và form liên hệ. Một section “Về chúng tôi” khác trong `AboutPage.tsx` có class `hidden`, nên không phải section hiển thị hoặc mục quản trị; không seed node riêng cho nó. Hero có ảnh desktop và mobile riêng, nhưng admin mock hiện chỉ cho sửa ảnh desktop. Đối tác vẫn là nội dung dùng chung trong `site_settings.partners`. Admin hiện dùng dữ liệu mock trong FE, chưa kết nối DB/API.

Ảnh của các mốc Hành trình, số thứ tự cùng ảnh thường/hover của các thẻ Năng lực là asset và manifest cố định của FE, không lưu trong DB. Ảnh Hero mobile cũng chưa có ô thay trong admin mock. Backend chỉ whitelist đúng các field mà UI được phép sửa; vị trí slot do seed/manifest kiểm soát. `AdminCrudProvider` hiện chỉ lưu bản chỉnh sửa trong React state và trang công khai vẫn đọc hằng số từ source, nên cần nối cả admin PATCH lẫn public GET với bảng `pages` mới có hiệu lực thực tế.

Registry admin mock hiện cho phép `facebookUrl`, `tiktokUrl`, `instagramUrl`, `linkedinUrl` trong Footer, trái quy tắc cũ là social target cố định ở FE. Thiết kế DB không mở quyền này: khi nối API thật, backend phải loại các field đó khỏi payload cập nhật `site_settings.footer`.

## Ảnh

Frontend quy định sẵn kích thước khung, aspect ratio và `object-fit`. DB không lưu ratio/width/height.

Admin gửi file ảnh qua form. Backend upload file lên Cloudinary, nhận URL từ Cloudinary rồi lưu URL đó vào DB. API GET trả lại URL ảnh cho FE. Admin không có ô nhập URL thủ công.

Ví dụ giá trị ảnh trong JSON: `"https://res.cloudinary.com/.../image.webp"`.

Mỗi field ảnh chỉ lưu một chuỗi URL; không lưu object ảnh hoặc văn bản thay thế.

Nội dung và target của mọi nút bấm được fix trong FE. Admin không sửa CTA label, submit label, nút quay lại hoặc nút tiếp tục.

## Dự án và tin tức

Admin được thêm/sửa/xóa Dự án, Tin tức và Vị trí tuyển dụng. Dự án/Tin tức không có field slug hoặc href trong form.

`projects` và `news` là hai bảng độc lập:

- Cả hai có `id`, `created_at`, `updated_at`; các cột nội dung mang tên theo đúng vai trò của từng form.
- `projects` có `card_title`, `card_category`, `card_image_url` cho thẻ ở Danh sách Dự án; `is_featured` là checkbox “Tiêu biểu” (`highlight`), tối đa 8 Dự án mỗi danh mục. `detail_content jsonb` giữ toàn bộ nội dung trang chi tiết dưới các nhóm `overview`, `survey`, `solution`, `renders`, `process`, `comparisons`, `contactForm`. Nội dung các thẻ Dự án nổi bật trên Trang chủ và trang dịch vụ vẫn là slot cố định riêng trong `pages`.
- `news` có `excerpt` (Mô tả ngắn), `image_url` ánh xạ ô `desktopImage` (Ảnh bài viết), `content.body` (Nội dung rich text), `is_featured` ánh xạ checkbox `featured` (Tin nổi bật) và `highlight_home` ánh xạ checkbox `highlightHome` (Trang chủ). Form tạo/sửa bài gồm `title`, `excerpt`, `desktopImage`, `body`; hai checkbox nằm tại bảng danh sách admin.
- `projects.detail_content` và `news.content` được backend validate bằng schema riêng. Endpoint `/admin/projects` và `/admin/news` ghi vào đúng bảng tương ứng.

Form thẻ Dự án và form Chi tiết Dự án có ô `title`/`category` riêng. `projects.card_title`/`projects.card_category` phục vụ thẻ danh sách; `projects.detail_content.overview.title`/`category` phục vụ trang chi tiết. Dữ liệu FE hiện có giá trị khác nhau giữa hai form, nên không dùng chung hai cặp field này. Hai form đọc/ghi **cùng `projects.id`**; không tạo bảng hoặc row chi tiết khác. Khi mới tạo thẻ mà chưa nhập chi tiết, `detail_content = {}`. Mỗi ảnh/nhãn trong JSON giữ field trùng tên ô admin, ví dụ `survey1Image`, `render1Image`, `comparison1BeforeImage`. API trả các nhóm và field này nguyên tên để FE đọc trực tiếp, không chuyển thành mảng dựa trên vị trí. Source FE hiện dùng mock dạng mảng nên khi nối API cần đổi cách đọc dữ liệu; không thay đổi DB hoặc quyền chỉnh sửa của admin. Hợp đồng response nằm ở mục 4.1 của từ điển dữ liệu.

Form Vị trí tuyển dụng có hai ô rich text bắt buộc: `responsibilities` (Trách nhiệm) và `benefits` (Quyền lợi). Cả hai nằm trong một cột `jobs.content jsonb` nhưng là hai key độc lập, ví dụ `{"responsibilities":"<ul>...</ul>","benefits":"<ul>...</ul>"}`. API admin đọc/ghi riêng từng key; API công khai chuyển từng danh sách HTML thành `string[]` riêng để khớp hai thuộc tính của `CareerJob` mà FE đang dùng. Backend kiểm tra hai key đều có giá trị và không trộn nội dung của chúng.

Khi tạo record:

1. Backend slugify `projects.card_title` khi tạo Dự án hoặc `news.title` khi tạo Tin tức.
2. Kiểm tra `content_slugs.slug` trên toàn hệ thống.
3. Nếu slug thuộc record khác, tự thêm hậu tố `-2`, `-3`...
4. Ghi slug với `is_current = true` trong cùng transaction.

Khi đổi `projects.card_title` hoặc `news.title`:

1. Sinh slug mới.
2. Nếu slug mới khác slug hiện tại, đánh dấu slug hiện tại `is_current = false`.
3. Thêm/khôi phục slug mới thành `is_current = true`.
4. Không xóa slug cũ.

Khi truy cập slug cũ của record còn tồn tại, backend tìm record qua `project_id` hoặc `news_id` và trả HTTP 301 đến slug hiện tại. Khi admin xóa hẳn record, backend xóa các slug của record trong cùng transaction trước khi xóa record; URL cũ trả 404.

`content_slugs.slug` unique nên một slug không thể đồng thời thuộc Project và News. Mỗi row slug có đúng một trong hai FK `project_id` hoặc `news_id`; DB dùng `CHECK` để khóa quy tắc này. Backend dựa vào FK có giá trị để chọn route prefix.

## Tin nổi bật

Tin nổi bật được xác định duy nhất bằng:

```text
news.is_featured = true
```

Frontend query các bài từ `news` có `is_featured = true` và sắp xếp theo `created_at DESC`. Admin không quản lý trạng thái public, ngày public, thứ tự nổi bật, link hoặc bản sao title/image/href trong page JSON.

Trang chủ query riêng các bài có `news.highlight_home = true`; không dùng chung cờ `is_featured` vì admin có hai công tắc độc lập.

Admin FE giới hạn tối đa 5 Tin nổi bật và 4 tin trên Trang chủ; backend cần kiểm tra cùng giới hạn khi cập nhật cờ. `slug`, `href`, `order` không phải ô nhập nội dung. Source FE còn khai báo `imageAlt`, nhưng DB không lưu alt theo quy tắc ảnh đã chốt. `mobileImage` hiện có trong dữ liệu render nhưng admin không có ô sửa riêng, nên DB chỉ lưu URL ảnh từ `desktopImage`.

## Site settings (cấu hình chung của website)

`site_settings` chỉ có ba field nội dung JSON:

- `header` (đầu trang): URL logo đầu trang. Danh mục đầu trang không lưu DB, hoàn toàn cố định trong FE.
- `partners` (đối tác): `title` (tiêu đề) và `partner1LogoImage` đến `partner6LogoImage` (URL của sáu logo cố định). FE phân biệt từng ảnh bằng tên field; không lưu mảng ảnh hay link.
- `footer` (chân trang): `footerLogo` (logo), `socialWidgetImage` (ảnh fanpage), thông tin liên hệ/địa chỉ và `service1Label`/`service1PageName` đến `service4Label`/`service4PageName` cho bốn ô dịch vụ cố định.

`footer.service1PageName` đến `footer.service4PageName` lưu tên trang admin chọn (ví dụ `Trang chủ`), không lưu href. Backend chỉ nhận tên trang trong danh sách cố định; FE tra tên → route khi render (`Trang chủ` → `/` trong source hiện tại). FE mock hiện gửi URL trong `service1Href`…`service4Href`, nên bước tích hợp API cần đổi payload và danh sách chọn; trang chi tiết Dự án có tên biến đổi và neo `#...` không được nhận trong bốn field tên trang. Header navigation, social targets, CTA targets, page routes và Google Maps target vẫn do FE cố định.

## Phân chia trách nhiệm

DB bảo đảm:

- Cây `pages` có một root cho mỗi `page_code` và key con không trùng trong cùng parent; các ràng buộc root/child và kiểu JSON.
- Slug duy nhất toàn hệ thống.
- Tối đa một slug hiện tại cho mỗi Project/News bằng hai partial unique index trên `project_id` và `news_id`; quy trình tạo record bảo đảm mỗi record có một slug hiện tại.
- Mỗi slug thuộc đúng một Project hoặc News bằng FK và `CHECK`.
- Kiểu dữ liệu, trạng thái và các index danh sách.
- Biểu mẫu dùng chung chỉ lưu tên khách hàng, số điện thoại và trạng thái `pending`/`done`; không có email, loại biểu mẫu, nguồn gửi, payload riêng hoặc FK đến bảng khác.

Code bảo đảm:

- Admin không tạo/xóa/reparent/rekey node của `pages`, không đổi layout hoặc slot cố định; kiểm tra cycle và node path theo manifest.
- Chỉ cập nhật `pages.value` theo schema của page/node; dựng nội dung page từ cây khi đọc.
- Slug tự sinh và lịch sử redirect.
- URL ảnh chỉ được backend ghi từ kết quả upload Cloudinary, không lấy từ text admin nhập.
- JSON đúng contract của từng page.
- Không có field href trong payload admin; `site_settings.footer.service1PageName` đến `service4PageName` chỉ nhận tên trang hợp lệ.
- Không có field ratio/width/height trong payload admin.
- Tin nổi bật lấy từ `news.is_featured`.
