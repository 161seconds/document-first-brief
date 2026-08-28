# Ma trận coverage Unit Test

## Phạm vi

Mỗi nhóm dưới đây bao phủ Business Rule và các nhánh API được mô tả trong TDD hiện tại: happy path, validation, authorization, boundary, lỗi dependency/persistence, retry và dữ liệu không được thay đổi khi thất bại. Các biến thể cùng chung một kỳ vọng có thể được thực hiện bằng parameterized unit test khi viết mã.

| TDD | UT | Phạm vi đã cover |
|---|---|---|
| TDD-030 | 18 docs | Đăng nhập/tài khoản inactive, Combo/Mockup/input, quota và cạnh tranh quota, prompt, retry 0/1/2 lần, logo, history, transaction và ngày mới |
| TDD-035 | 19 docs | Hai nguồn hoa, template/config, form/size/text validation, pricing Calligraphy boundary, quota, retry, snapshot, history, ownership và rollback |
| TDD-036 | 01-12 | Ownership/history nguồn, quota ngày/theo hoa, dùng lại snapshot, immutable source, retry, rollback và không tác động Checkout |
| TDD-050 | 01-10 | Soft-delete, active/inactive filter, sort, pagination mặc định/allowed/invalid, empty result, authorization |
| TDD-051 | 14 docs | Tên/mô tả/file validation boundary, upload lỗi, trạng thái mặc định, quyền và idempotency tuần tự/đồng thời |
| TDD-052 | 6 docs | Hai hướng toggle, not-found, soft-delete, quyền, unauthenticated và lỗi persistence |
| TDD-053 | 01-07 | Soft-delete, dữ liệu/ảnh không bị xóa vật lý, not-found, xóa lại, inactive, quyền và lỗi persistence |
| TDD-054 | 01-08 | Admin/Staff, Kind/IsDeleted/IsPublic, group/key filter, empty list, pagination và tổng số |
| TDD-055 | 9 docs | Size/Calligraphy/duplicate, JSON/key/group/kind validation, trạng thái mặc định và quyền |
| TDD-056 | 01-08 | Soft-delete/UpdatedAt, config có card snapshot tham chiếu, not-found/deleted, quyền và persistence error |
| TDD-062 | 10 docs | Update size/Calligraphy, JSON/not-found/deleted/private, quyền, field bất biến và persistence error |

## Nguyên tắc assertion chung

- Nhánh bị từ chối không được gọi AI/upload hay ghi dữ liệu không cần thiết.
- Nhánh lỗi persistence phải giữ nguyên dữ liệu đã lưu hoặc rollback toàn transaction.
- Các giới hạn quota phải tính theo khách hàng, theo ngày nghiệp vụ và không được vượt do request đồng thời.
- Soft-delete không được xóa vật lý record/ảnh; dữ liệu bị xóa không được dùng trong flow mới.

## Giới hạn của tài liệu hiện tại

Các TDD chưa quy định chi tiết schema/ràng buộc từng field của một số DTO, timezone reset quota, hoặc response cụ thể cho mọi lỗi nội bộ. Các UT liên quan đã kiểm tra invariant an toàn; khi chốt contract triển khai, cần thay mã HTTP/messageCode chính xác trong các case đó.
