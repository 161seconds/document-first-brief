# UT-030-08: Không lưu dữ liệu khi AI thất bại sau retry

## Thông tin tài liệu

- **Mã tài liệu**: UT-030-08
- **Phiên bản**: v1.0
- **Cập nhật gần nhất**: 2026-08-28

## Đơn vị kiểm thử

- **Module**: AI FLOWER
- **Loại**: Error
- **Unit under test**: Service/handler của TDD-030.
- **Kịch bản**: Không lưu dữ liệu khi AI thất bại sau retry.
- **Expected output**: Trả lỗi theo TDD; transaction/state được rollback và quota không bị tiêu hao sai.

## Thiết lập và assertion

- Dùng mock phù hợp cho repository, AI/storage và current user.
- Kiểm tra response, số lần gọi dependency và state cuối cùng của dữ liệu liên quan.

## TEST_LINKS

**Link 1**
- **Loại**: Business Rule
- **Mã**: BR-030
- **Section**: Kịch bản UT-030-08

**Link 2**
- **Loại**: TDD
- **Mã**: TDD-030
- **Section**: Business Rules / API examples

