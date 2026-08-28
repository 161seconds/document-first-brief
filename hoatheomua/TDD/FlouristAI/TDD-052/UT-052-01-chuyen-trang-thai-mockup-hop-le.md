# UT-052-01: Chuyển trạng thái Mockup hợp lệ

## Thông tin tài liệu

- **Mã tài liệu**: UT-052-01
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: MOCKUP
- **Loại**: Happy
- **Unit under test**: Service/handler của TDD-052.
- **Kịch bản**: Chuyển trạng thái Mockup hợp lệ.
- **Expected output**: Trả kết quả đúng Business Rule; kiểm tra response, dependency calls và state dữ liệu sau xử lý.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-052
- **Section**: Kịch bản UT-052-01

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-052
- **Section**: Business Rules / API examples

