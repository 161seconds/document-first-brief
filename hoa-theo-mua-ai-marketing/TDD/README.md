# Hoa Theo Mua - AI Marketing: Technical Design Documents (TDD)

Tài liệu thiết kế kỹ thuật (Technical Design Documents - TDD) chuẩn Document-First cho phân hệ **AI Marketing** thuộc hệ thống Hoa Theo Mùa.

Tất cả tài liệu tuân thủ nghiêm ngặt tiêu chuẩn:
- **Cấu trúc**: Document Info, Context & Goals, Architecture, Sequence Diagram, Activity Diagram, Data Model & Data Dictionary, Internal API Contract, Quy ước Mã lỗi (Error Code), References.
- **Biểu đồ**: Vẽ bằng Mermaid (Sequence Diagram, Flowchart Activity, ER Diagram).
- **Format API**: Chuẩn hóa cấu trúc ApiResponse (`isSuccess`, `isFailed`, `value`, `error`, `traceId`, `timestampUtc`).

---

## Mục lục Danh mục TDD (17 Tài liệu)

| STT | Mã TDD | Tên tính năng | Story liên quan | Tác giả | Trạng thái |
| :---: | :--- | :--- | :--- | :---: | :---: |
| 1 | [TDD-002](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-002-ScheduleAndAutoPublishPosts.md) | Lên lịch và tự động đăng bài lên mạng xã hội | [STORY-002](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/02-ScheduleAndAutoPublishPosts.md) | Hồ Hoàng Nam | In Review |
| 2 | [TDD-003](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-003-ViewSystemPromptsListAndDetail.md) | Xem danh sách và chi tiết System Prompt | [STORY-004](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/04-ViewSystemPromptsListAndDetail.md) | Phùng Nguyễn Thiên Hào | In Review |
| 3 | [TDD-004](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-004-UpdateSystemPrompt.md) | Chỉnh sửa System Prompt | [STORY-005](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/05-UpdateSystemPrompt.md) | Phùng Nguyễn Thiên Hào | In Review |
| 4 | [TDD-014](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-014-ViewAutoPublishSchedules.md) | Xem danh sách và chi tiết lịch đăng bài tự động | [STORY-014](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/14-ViewAutoPublishSchedules.md) | Hồ Hoàng Nam | In Review |
| 5 | [TDD-015](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-015-DeleteAutoPublishSchedule.md) | Hủy và xóa lịch đăng bài tự động | [STORY-015](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/15-DeleteAutoPublishSchedule.md) | Hồ Hoàng Nam | In Review |
| 6 | [TDD-016](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-016-UpdateAutoPublishSchedule.md) | Chỉnh sửa lịch đăng bài tự động | [STORY-016](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/16-UpdateAutoPublishSchedule.md) | Hồ Hoàng Nam | In Review |
| 7 | [TDD-017](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-017-AutoGenerateContentAndHashtags.md) | Tự động viết content và hashtag bằng AI | [STORY-017](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/17-AutoGenerateContentAndHashtags.md) | Hồ Hoàng Nam | In Review |
| 8 | [TDD-018](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-018-ViewSavedContentListAndDetail.md) | Xem danh sách và chi tiết content đã lưu | [STORY-018](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/18-ViewSavedContentListAndDetail.md) | Hồ Hoàng Nam | In Review |
| 9 | [TDD-019](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-019-DeleteSavedContent.md) | Xóa content đã lưu | [STORY-019](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/19-DeleteSavedContent.md) | Hồ Hoàng Nam | In Review |
| 10 | [TDD-020](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-020-UpdateSavedContent.md) | Sửa content đã lưu | [STORY-020](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/20-UpdateSavedContent.md) | Hồ Hoàng Nam | In Review |
| 11 | [TDD-021](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-021-ViewSavedImagesListAndDetail.md) | Xem danh sách và chi tiết ảnh đã lưu | [STORY-021](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/21-ViewSavedImagesListAndDetail.md) | Hồ Hoàng Nam | In Review |
| 12 | [TDD-022](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-022-DeleteSavedImage.md) | Xóa ảnh đã lưu trong thư viện | [STORY-022](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/22-DeleteSavedImage.md) | Hồ Hoàng Nam | In Review |
| 13 | [TDD-023](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-023-UpdateSavedImageInfo.md) | Sửa thông tin ảnh đã lưu (Metadata) | [STORY-023](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/23-UpdateSavedImageInfo.md) | Hồ Hoàng Nam | In Review |
| 14 | [TDD-024](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-024-RewriteContentByPlatform.md) | Tự động viết lại nội dung theo từng nền tảng | [STORY-024](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/24-RewriteContentByPlatform.md) | Hồ Hoàng Nam | In Review |
| 15 | [TDD-025](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-025-CollectAndManagePlatformReports.md) | Thu thập và quản lý báo cáo từ các nền tảng | [STORY-025](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/25-CollectAndManagePlatformReports.md) | Hồ Hoàng Nam | In Review |
| 16 | [TDD-026](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-026-AutoGenerateMultiRatioImagesFromCore.md) | Tự động sinh ảnh đa tỷ lệ từ ảnh core bằng Vision AI | [STORY-003](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/03-AutoGenerateMultiRatioImagesFromCore.md) | Hồ Hoàng Nam | In Review |
| 17 | [TDD-027](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/TDD/TDD-027-DeleteSavedReport.md) | Xóa báo cáo đã lưu | [STORY-027](file:///d:/VNZ/document-first-brief/hoa-theo-mua-ai-marketing/UserStory/27-DeleteSavedReport.md) | Hồ Hoàng Nam | In Review |

---

## Quy ước Kỹ thuật chung

1. **Chuẩn mã hóa**:
   - Tất cả mã lỗi tuân thủ bảng quy ước chuẩn (ví dụ: `VALIDATION_ERROR`, `CONCURRENCY_CONFLICT`, `SCHEDULE_TIME_INVALID`, `AI_SERVICE_UNAVAILABLE`).
   - Thứ tự mã lỗi trong bảng Error Code được đánh số thứ tự `STT` (1, 2, 3...) rõ ràng.
2. **Mô hình kiến trúc**:
   - Clean Architecture (.NET 8): `WebApi` $\rightarrow$ `Application` $\rightarrow$ `Domain` $\rightarrow$ `Infrastructure` $\rightarrow$ `Repository`.
   - Cơ sở dữ liệu: PostgreSQL 16 kết hợp EF Core (Snake_case naming convention, audit columns).
   - Tác vụ nền: Hangfire với PostgreSQL storage, đảm bảo độ trễ xuất bản $\le 1$ phút.
   - Trí tuệ nhân tạo: Tích hợp mô hình OpenAI GPT-4o / Google Gemini Pro (Nội dung & Hashtag) và Outpainting Composition AI (Hình ảnh đa tỷ lệ).
