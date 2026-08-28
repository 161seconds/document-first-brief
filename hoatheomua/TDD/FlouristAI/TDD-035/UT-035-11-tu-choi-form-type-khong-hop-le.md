# UT-035-11: Từ chối form_type không hợp lệ

## Thông tin tài liệu

- **Mã tài liệu**: UT-035-11
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: AI CARD
- **Loại**: Validation / Authorization
- **Unit under test**: Service/handler của TDD-035.
- **Kịch bản**: Từ chối form_type không hợp lệ.
- **Expected output**: Trả đúng HTTP/messageCode theo TDD; không gọi dependency không cần thiết và không thay đổi dữ liệu.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-035
- **Section**: Kịch bản UT-035-11

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: Business Rules / API examples

