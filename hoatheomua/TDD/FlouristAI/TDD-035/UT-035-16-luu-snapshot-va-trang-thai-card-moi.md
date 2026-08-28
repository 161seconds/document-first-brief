# UT-035-16: Lưu snapshot và trạng thái card mới

## Thông tin tài liệu

- **Mã tài liệu**: UT-035-16
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: AI CARD
- **Loại**: Branch
- **Unit under test**: Service/handler của TDD-035.
- **Kịch bản**: Lưu snapshot và trạng thái card mới.
- **Expected output**: Tạo đúng generated_card/client_history, nguồn/snapshot/quota đúng Business Rule và không có side effect ngoài phạm vi.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-035
- **Section**: Kịch bản UT-035-16

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-035
- **Section**: Business Rules / API examples

