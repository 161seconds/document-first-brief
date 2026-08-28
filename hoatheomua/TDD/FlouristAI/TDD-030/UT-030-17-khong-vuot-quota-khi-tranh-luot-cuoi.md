# UT-030-17: Không vượt quota khi tranh lượt cuối

## Thông tin tài liệu

- **Mã tài liệu**: UT-030-17
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: AI FLOWER
- **Loại**: Boundary
- **Unit under test**: Service/handler của TDD-030.
- **Kịch bản**: Không vượt quota khi tranh lượt cuối.
- **Expected output**: Trả kết quả đúng Business Rule; kiểm tra response, dependency calls và state dữ liệu sau xử lý.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-030
- **Section**: Kịch bản UT-030-17

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Business Rules / API examples

