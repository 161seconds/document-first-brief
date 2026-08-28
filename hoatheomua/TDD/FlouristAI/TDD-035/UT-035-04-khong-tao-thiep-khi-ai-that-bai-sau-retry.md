# UT-035-04: Không tạo thiệp khi AI thất bại sau retry

## Thông tin tài liệu

- **Mã tài liệu**: UT-035-04
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: AI CARD
- **Loại**: Error
- **Unit under test**: Service/handler của TDD-035.
- **Kịch bản**: Không tạo thiệp khi AI thất bại sau retry.
- **Expected output**: Trả lỗi theo TDD; transaction/state được rollback và quota không bị tiêu hao sai.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-035
- **Section**: Kịch bản UT-035-04

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: Business Rules / API examples

