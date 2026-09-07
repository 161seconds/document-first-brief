# HTM_Marketing_AI — Phân hệ Marketing & Truyền thông AI (`hoa-theo-mua-ai-marketing`)

> **Mã dự án (Key)**: `hoa-theo-mua-ai-marketing`  
> **Tên dự án**: `HTM_Marketing_AI`  
> **Phương pháp tiếp cận**: Document-First System Specification  

---

## 📖 Tổng quan Phân hệ

**HTM_Marketing_AI** là phân hệ trí tuệ nhân tạo phục vụ hoạt động truyền thông và tiếp thị nội dung của thương hiệu Hoa Theo Mùa:
- **Tự động tạo nội dung bài viết tiếp thị (AI Copywriting)**: Sinh bài viết giới thiệu sản phẩm hoa theo mùa, sự kiện đặc biệt, ngày lễ (Valentine, 8/3, Ngày của Mẹ, Ngày Nhà giáo...), các câu chuyện ý nghĩa về từng loài hoa.
- **Tự động viết lại nội dung theo từng nền tảng**: Tối ưu hóa văn phong, cấu trúc và đề xuất hashtag phù hợp cho Facebook, Instagram, Zalo OA.
- **Tự động sinh ảnh đa tỷ lệ từ ảnh core**: Mở rộng/cắt ảnh bằng AI theo các tỷ lệ chuẩn (1:1, 4:5, 9:16, 16:9, 2:1) cho từng nền tảng mạng xã hội, đảm bảo bố cục và safe zone của sản phẩm/logo.
- **Quản lý Thư viện Hình ảnh & Content đã lưu**: Xem danh sách, chi tiết, cập nhật thông tin và xóa an toàn các nội dung và hình ảnh đã lưu.
- **Quản lý & Tìm kiếm System Prompts**: Tìm kiếm, lọc, xem danh sách, xem chi tiết, chỉnh sửa quản lý phiên bản và xóa bỏ các Prompt không còn sử dụng.
- **Lên lịch & Tự động đăng đa kênh**: Thiết lập, cập nhật và xóa lịch đăng bài tự động lên các nền tảng mạng xã hội (Facebook, Instagram, Zalo OA...), quản lý token, xử lý rate-limit và trùng lịch.
- **Thu thập & Quản lý Báo cáo đa nền tảng**: Tự động lấy dữ liệu báo cáo định kỳ theo ngày/tuần/tháng theo múi giờ `Asia/Ho_Chi_Minh`, xem chi tiết và xuất báo cáo dưới định dạng tệp XLSX.

---

## 📂 Cấu trúc Thư mục

```text
hoa-theo-mua-ai-marketing/
├── README.md                                      # Tài liệu tổng quan phân hệ
├── BusinessRules/                                 # Quy tắc nghiệp vụ (Giới hạn ký tự, bản quyền ảnh, kiểm duyệt nội dung)
├── ConfirmedDoc/                                  # Hợp đồng API & tài liệu đã chốt
├── Context/                                       # Ngữ cảnh kiến trúc & Sơ đồ CSDL
│   └── Marketing_AI_Context.md                    # Tài liệu kiến trúc dữ liệu và bảng generated_posts / post_histories
├── UserStory/                                     # Đặc tả yêu cầu người dùng (User Stories)
│   ├── 02-ScheduleAndAutoPublishPosts.md          # STORY-002: Lên lịch và tự động đăng bài
│   ├── 03-AutoGenerateMultiRatioImagesFromCore.md # STORY-003: Tự động sinh ảnh đa tỷ lệ từ ảnh core
│   ├── 04-ViewSystemPromptsListAndDetail.md       # STORY-004: Xem danh sách và chi tiết System Prompt
│   ├── 05-UpdateSystemPrompt.md                   # STORY-005: Sửa System Prompt & Quản lý phiên bản
│   ├── 06-DeleteSystemPrompt.md                   # STORY-006: Xóa System Prompt
│   ├── 12-SearchSystemPrompts.md                  # STORY-012: Tìm kiếm System Prompt
│   ├── 13-FilterSystemPrompts.md                  # STORY-013: Lọc System Prompt
│   ├── 14-ViewAutoPublishSchedules.md             # STORY-014: Xem lịch đăng bài tự động
│   ├── 15-DeleteAutoPublishSchedule.md            # STORY-015: Xóa lịch đăng bài tự động
│   ├── 16-UpdateAutoPublishSchedule.md            # STORY-016: Sửa lịch đăng bài tự động
│   ├── 17-AutoGenerateContentAndHashtags.md       # STORY-017: Tự động viết content và hashtag
│   ├── 18-ViewSavedContentListAndDetail.md        # STORY-018: Xem content đã lưu lại
│   ├── 19-DeleteSavedContent.md                   # STORY-019: Xóa content đã lưu lại
│   ├── 20-UpdateSavedContent.md                   # STORY-020: Sửa content đã lưu lại
│   ├── 21-ViewSavedImagesListAndDetail.md         # STORY-021: Xem ảnh đã lưu lại
│   ├── 22-DeleteSavedImage.md                     # STORY-022: Xóa ảnh đã lưu lại
│   ├── 23-UpdateSavedImageInfo.md                 # STORY-023: Sửa ảnh đã lưu lại
│   ├── 24-RewriteContentByPlatform.md             # STORY-024: Tự động viết lại nội dung theo từng nền tảng
│   └── 25-CollectAndManagePlatformReports.md      # STORY-025: Thu thập và quản lý báo cáo từ các nền tảng
├── TDD/                                           # Thiết kế kỹ thuật chi tiết (Technical Design Documents)
└── UnitTest/                                      # Kịch bản kiểm thử đơn vị
```

---

## 📑 Danh mục User Stories & Kỹ thuật

### 1. Danh mục User Stories
| Mã Story | Tên tài liệu | Priority | Sprint | Mô tả tóm tắt |
| :--- | :--- | :---: | :---: | :--- |
| **STORY-002** | [`02-ScheduleAndAutoPublishPosts.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/02-ScheduleAndAutoPublishPosts.md) | **Must** | S1 | Chọn content, ảnh, chọn nền tảng và thiết lập thời gian để hệ thống tự động đăng bài đa kênh. |
| **STORY-003** | [`03-AutoGenerateMultiRatioImagesFromCore.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/03-AutoGenerateMultiRatioImagesFromCore.md) | **Must** | S1 | Tải lên ảnh core, AI tự động sinh các phiên bản ảnh theo tỷ lệ (1:1, 4:5, 9:16, 16:9, 2:1) cho từng mạng xã hội. |
| **STORY-004** | [`04-ViewSystemPromptsListAndDetail.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/04-ViewSystemPromptsListAndDetail.md) | **Must** | S1 | Xem danh sách và nội dung chi tiết của từng System Prompt dùng để định hướng giọng văn cho AI. |
| **STORY-005** | [`05-UpdateSystemPrompt.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/05-UpdateSystemPrompt.md) | **Must** | S1 | Chỉnh sửa nội dung System Prompt (1 - 20.000 ký tự), tự động tạo phiên bản mới và lưu vết lịch sử. |
| **STORY-006** | [`06-DeleteSystemPrompt.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/06-DeleteSystemPrompt.md) | **Won't** | S1 | Xóa bỏ các System Prompt không còn nhu cầu sử dụng kèm popup cảnh báo xác nhận. |
| **STORY-012** | [`12-SearchSystemPrompts.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/12-SearchSystemPrompts.md) | **Won't** | S1 | Tìm kiếm nhanh System Prompt theo tên hoặc mã định danh bằng từ khóa không phân biệt hoa thường. |
| **STORY-013** | [`13-FilterSystemPrompts.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/13-FilterSystemPrompts.md) | **Won't** | S1 | Lọc danh sách System Prompt theo trạng thái kích hoạt (Hoạt động / Tạm dừng). |
| **STORY-014** | [`14-ViewAutoPublishSchedules.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/14-ViewAutoPublishSchedules.md) | **Must** | S1 | Xem danh sách và chi tiết các lịch đăng bài tự động, lọc theo trạng thái và nền tảng. |
| **STORY-015** | [`15-DeleteAutoPublishSchedule.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/15-DeleteAutoPublishSchedule.md) | **Must** | S1 | Xóa lịch đăng bài ở trạng thái cho phép ("Đã lên lịch", "Đã hủy"), hủy tiến trình Cron Job tương ứng. |
| **STORY-016** | [`16-UpdateAutoPublishSchedule.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/16-UpdateAutoPublishSchedule.md) | **Must** | S1 | Chỉnh sửa lịch đăng bài tự động (content, hình ảnh, nền tảng, thời gian đăng) và cập nhật lại Job. |
| **STORY-017** | [`17-AutoGenerateContentAndHashtags.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/17-AutoGenerateContentAndHashtags.md) | **Must** | S1 | Sử dụng AI tự động viết bài viết và đề xuất hashtag theo chủ đề, mục tiêu, đối tượng và giọng văn. |
| **STORY-018** | [`18-ViewSavedContentListAndDetail.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/18-ViewSavedContentListAndDetail.md) | **Must** | S2 | Xem danh sách, tìm kiếm, lọc và xem chi tiết các content và hashtag đã lưu ở chế độ chỉ đọc. |
| **STORY-019** | [`19-DeleteSavedContent.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/19-DeleteSavedContent.md) | **Must** | S2 | Xóa an toàn content đã lưu không bị tham chiếu bởi lịch đăng bài hoặc bài đăng nào. |
| **STORY-020** | [`20-UpdateSavedContent.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/20-UpdateSavedContent.md) | **Must** | S2 | Chỉnh sửa nội dung văn bản (1–10.000 ký tự) và danh sách hashtag (1–30 hashtag) của content đã lưu. |
| **STORY-021** | [`21-ViewSavedImagesListAndDetail.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/21-ViewSavedImagesListAndDetail.md) | **Must** | S1 | Xem danh sách lưới, tìm kiếm theo tên/ID/tag, lọc và xem chi tiết ảnh thu phóng trong thư viện ảnh. |
| **STORY-022** | [`22-DeleteSavedImage.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/22-DeleteSavedImage.md) | **Must** | S3 | Xóa ảnh đã lưu và tự động xóa liên đới (Cascade Delete) các biến thể con theo tỷ lệ nếu không bị sử dụng. |
| **STORY-023** | [`23-UpdateSavedImageInfo.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/23-UpdateSavedImageInfo.md) | **Must** | S1 | Cập nhật thông tin quản lý của ảnh (tên ảnh, mô tả, thẻ tags) mà không can thiệp tệp ảnh gốc. |
| **STORY-024** | [`24-RewriteContentByPlatform.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/24-RewriteContentByPlatform.md) | **Must** | S1 | Dùng AI viết lại nội dung gốc và đề xuất hashtag riêng biệt cho từng nền tảng (Facebook, Instagram, Zalo OA). |
| **STORY-025** | [`25-CollectAndManagePlatformReports.md`](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/25-CollectAndManagePlatformReports.md) | **Must** | S1 | Thu thập báo cáo định kỳ (ngày, tuần, tháng múi giờ Asia/Ho_Chi_Minh), xem chi tiết và tải tệp XLSX. |

### 2. Ngữ cảnh & Kiến trúc
- **[Marketing_AI_Context.md](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/Context/Marketing_AI_Context.md)**: Ngữ cảnh kiến trúc phân hệ Marketing AI, đặc tả bảng CSDL `generated_posts`, `post_histories` và liên kết với `client_histories`.
