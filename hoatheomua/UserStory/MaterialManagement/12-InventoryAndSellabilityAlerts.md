# STORY-012: Cảnh báo vật liệu sắp hết và combo hết khả năng bán

## Metadata
- **Story**: Là một Quản trị viên, tôi muốn được cảnh báo các vật liệu sắp hết và các combo hoa sắp không bán được, để chủ động nhập hàng trước khi mất doanh thu.
- **Context**: Cơ chế tính khả năng bán chỉ có giá trị nếu quản trị viên biết được tình hình sớm. Đưa cảnh báo lên dashboard biến dữ liệu tồn kho từ thông tin bị động thành công cụ vận hành chủ động. Vật liệu được hiểu là các sản phẩm cấu thành nên bó hoa combo như (hoa hồng, hoa hướng dương,...) Combo được hiểu là một sản phẩm hoa lớn chưa nhiều vật liệu thành phần.
- **Sprint**: S4
- **Priority**: Could
- **Assignee**: BE: Hồ Hoàng Nam | FE: Hồ Hoàng Nam
- **Creator**: Hồ Hoàng Nam
- **Status**: Cần làm

## Conditions
- **Preconditions**: 
  - Vật liệu đã được cấu hình ngưỡng cảnh báo tồn thấp.
  - Quản trị viên đã đăng nhập và có quyền xem dashboard.
- **Trigger**: Quản trị viên truy cập trang dashboard của admin portal.

## Flow
### Hiển thị cảnh báo tồn kho
1. Hệ thống tính danh sách vật liệu có tồn khả dụng nhỏ hơn hoặc bằng ngưỡng cảnh báo.
2. Hệ thống tính danh sách combo hoa đang mở bán có số lượng có thể bán bằng 0.
3. Hệ thống hiển thị hai khối cảnh báo, sắp xếp theo mức độ nghiêm trọng giảm dần.
4. Mỗi dòng cảnh báo có liên kết nhanh tới màn hình nhập kho của vật liệu tương ứng.

### Alternative Flow
- **ALT-02**: Hệ thống hiển thị trạng thái tồn kho ổn định (nếu không có cảnh báo nào).

### Exception Flow
- **EXC-01**: Hệ thống hiển thị thông báo không tải được cảnh báo và giữ nguyên các khối khác của dashboard.

## Acceptance Criteria
- **AC-001**: 
  - **Given**: Vật liệu có ngưỡng cảnh báo 10 và tồn khả dụng 8.
  - **When**: Quản trị viên mở dashboard.
  - **Then**: Vật liệu xuất hiện trong khối cảnh báo tồn thấp kèm số tồn khả dụng hiện tại.
- **AC-002**: 
  - **Given**: Combo hoa đang mở bán có số lượng có thể bán bằng 0.
  - **When**: Quản trị viên mở dashboard.
  - **Then**: Combo hoa xuất hiện trong khối cảnh báo.

## Non-Functional
- Khối cảnh báo tải trong dưới 2 giây và không chặn hiển thị các khối khác của dashboard.

## Out of Scope
- Gửi cảnh báo qua email hoặc tin nhắn.
- Dự báo nhu cầu nhập hàng.