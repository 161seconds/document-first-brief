# UT-056-03: Xóa Config đang được card snapshot tham chiếu

## Thông tin tài liệu

- **Mã tài liệu**: UT-056-03
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: CARD CONFIG
- **Loại**: Branch
- **Unit under test**: Service/handler của TDD-056.
- **Kịch bản**: Xóa Config đang được card snapshot tham chiếu.
- **Expected output**: Trả kết quả đúng Business Rule; kiểm tra response, dependency calls và state dữ liệu sau xử lý.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-056
- **Section**: Kịch bản UT-056-03

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-056
- **Section**: Business Rules / API examples

