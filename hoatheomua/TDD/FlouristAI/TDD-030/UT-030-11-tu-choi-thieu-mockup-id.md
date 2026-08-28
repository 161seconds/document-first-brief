# UT-030-11: Từ chối thiếu mockup_id

## Thông tin tài liệu

- **Mã tài liệu**: UT-030-11
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: AI FLOWER
- **Loại**: Validation / Authorization
- **Unit under test**: Service/handler của TDD-030.
- **Kịch bản**: Từ chối thiếu mockup_id.
- **Expected output**: Trả đúng HTTP/messageCode theo TDD; không gọi dependency không cần thiết và không thay đổi dữ liệu.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-030
- **Section**: Kịch bản UT-030-11

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Business Rules / API examples

