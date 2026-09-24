# Rà soát BMT_FE để tích hợp BMT_BE

Ngày rà soát: 2026-09-23. Phạm vi: các luồng dữ liệu, quản trị, liên hệ, báo giá và dự án trong `BMT_FE`; đối chiếu `docs/context.md`, `docs/database.md` và contract đề xuất tại `BMT_FE/docs`. Đây là review source, chưa phải kết quả kiểm thử trình duyệt. Backend hiện chỉ có `PriceRange` trong entity và chưa có controller nghiệp vụ (`BMT_BE/BMT_BE.Repository/Entity`, `BMT_BE/BMT_BE.API`); các endpoint trong contract vẫn là thiết kế.

## Phát hiện theo mức ưu tiên

### P0 — Cookie quản trị có giá trị cố định, có thể tự tạo phiên

`BMT_FE/features/admin/lib/auth-config.ts:1-8` chứa email, mật khẩu demo và giá trị session cố định. `BMT_FE/proxy.ts:11-13` coi bất kỳ request nào có cookie bằng chuỗi đó là đã đăng nhập; `features/admin/auth/actions.ts:25-43` không gọi backend. Người dùng biết source FE có thể tự đặt cookie để vào `/admin/*`. Trước khi triển khai admin thật, thay bằng phiên do BE phát hành và xác thực tại mọi API admin; bỏ tài khoản và cookie mock. Không xem bảo vệ route FE là kiểm tra quyền của BE.

### P1 — Biểu mẫu báo giá báo thành công nhưng không lưu hoặc gửi liên hệ

`BMT_FE/features/quotation/components/QuotationContactForm.tsx:75-92` chỉ kiểm tra ô không rỗng, hiện toast thành công rồi reset form. Khách hàng tưởng đã gửi nhưng admin không nhận được. Kết nối `POST /api/form-submissions` với đúng `{customerName, phone}`; chỉ hiện thành công sau response 201, giữ dữ liệu và báo lỗi khi request thất bại. Luồng form chung hiện cũng chỉ ghi vào `localStorage` (`shared/components/ContactForm.tsx:81-105`, `shared/lib/contact-submissions.ts:38-54`), nên không thể đồng bộ giữa thiết bị. Dashboard đọc cùng storage của trình duyệt admin (`features/admin/dashboard/ContactSubmissionsPanel.tsx:14-30,200-209`). Chuyển cả hai form và dashboard sang cùng API `FormSubmissions`.

### P1 — Nội dung admin chỉ tồn tại trong bộ nhớ, trang public vẫn dùng dữ liệu tĩnh

`features/admin/components/editor/AdminCrudProvider.tsx:35-77` khởi tạo từ mock và giữ kết quả CRUD trong React state. `features/admin/services/home-content.service.ts:5-16` và `project-content.service.ts:8-27` chỉ trả bản sao mock sau delay. Tải lại trang mất sửa đổi; trang public như `features/projects/pages/ProjectsPage.tsx:20,158` vẫn đọc `projectCards` tĩnh. Kết nối đọc/ghi API theo từng resource và chuyển trang public sang DTO tương ứng. Giữ mapping node/slot cố định ở FE theo `docs/context.md`; không lưu cả layout vào BE.

### P1 — Ước tính giá chưa khớp mô hình 16 cặp `PriceRanges`

`features/quotation/data/quotation-estimator.ts:47-52` có bốn khoảng giá hardcode theo **gói dịch vụ**. `features/quotation/components/QuotationEstimator.tsx:94-104` không dùng `building` khi chọn đơn giá, nên bốn loại hình cho cùng một kết quả nếu diện tích/gói giống nhau. Contract hiện yêu cầu 16 cặp `(BuildingType, ServiceType)` từ BE (`docs/database.md:181-193,244-252`). FE cần đọc các cặp bằng enum code ổn định, chọn theo cả hai input và làm tròn min/max tới 1.000 VND. `QuotationEstimator.tsx:118-138` hiện bắt buộc ngân sách, trong khi tài liệu công thức cho phép để trống; chốt một quy tắc trước khi nối API.

### P1 — Chọn ảnh admin tạo URL `blob:` không thể lưu bền vững

`features/admin/components/ImageField.tsx:58-67` gọi `URL.createObjectURL(file)` rồi đưa URL này vào `onChange`. Blob URL chỉ dùng được trong phiên browser, không phải URL Cloudinary mà contract nội dung yêu cầu. Upload file qua `POST /api/admin/images` trước, dùng URL trả về cho form; giữ blob URL riêng cho preview trong lúc upload và xử lý lỗi upload.

### P2 — Route chi tiết dự án phụ thuộc tập slug tĩnh

`BMT_FE/app/projects/[slug]/page.tsx:12-22,40-45` lấy slug và detail từ dữ liệu tĩnh. Khi admin thêm hoặc đổi tên project, route public chưa phản ánh bản ghi mới và slug cũ không thể redirect theo `ContentSlugs`. Khi nối BE, resolve theo `/api/projects/{slug}`, xử lý 404 và 301 theo response; không dùng tập slug build time làm giới hạn nội dung động.

## Thứ tự tích hợp ngắn nhất

1. Hoàn thiện entity, migration và API cho auth, `FormSubmissions`, `Pages`, `Projects`, `PriceRanges` theo `docs/database.md` và `BMT_FE/docs/bmt_api_contract_v2.md` (đang là đề xuất).
2. Nối auth và form liên hệ trước để tránh phiên admin giả và lead bị mất; sau đó thay mock admin/public theo từng resource.
3. Nối upload ảnh trước khi lưu nội dung có ảnh; nối 16 giá trước khi công bố kết quả báo giá.

Kiểm tra tích hợp tối thiểu: đăng nhập sai/đúng và API admin không có phiên; gửi hai loại form từ browser A rồi thấy trên admin browser B; sửa nội dung và tải lại trang public; upload ảnh rồi mở lại; so giá của hai loại hình cùng diện tích/gói; mở slug mới/cũ của project.
