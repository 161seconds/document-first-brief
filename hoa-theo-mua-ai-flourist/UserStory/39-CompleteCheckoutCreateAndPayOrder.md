# STORY-039: Hoàn tất Checkout, tạo và thanh toán Order (Complete Checkout, Create and Pay Order)

## Metadata
- **Story**: Là một khách hàng đã hoàn thành thông tin Checkout, tôi muốn tạo Order và thanh toán, để cửa hàng tiếp nhận và xử lý đơn hoa của tôi.
- **Context**: Order chỉ được tạo khi khách hàng bấm “Hoàn tất” và backend xác nhận dữ liệu, giá và tồn kho hợp lệ.
  - Order mới được tạo ở trạng thái PENDING và chuyển khách hàng tới màn Chi tiết đơn hàng có nút “Thanh toán ngay”.
  - Order tiếp tục PENDING khi khách hàng chưa bấm thanh toán hoặc thanh toán thất bại.
  - Khách hàng được thử thanh toán lại không giới hạn trong vòng 24 giờ kể từ lúc Order được tạo.
  - Sau 24 giờ chưa thanh toán thành công, hệ thống tự động chuyển Order sang CANCELLED và giải phóng tồn kho đã giữ.
- **Sprint**: S1
- **Priority**: Must
- **Assignee**: FE: Hoàng Thị Khánh Linh
- **Creator**: Hoàng Thị Khánh Linh
- **Author**: Hoàng Thị Khánh Linh
- **Reviewer**: Nguyễn Đức Bình
- **Approver**: Chưa chỉ định
- **Owner**: Hoàng Thị Khánh Linh
- **Status**: Cần làm
- **Version**: v0.1 (Nháp - Cập nhật 19/08/2026)

## Conditions
- **Preconditions**:
  - Khách hàng đã đăng nhập.
  - Checkout hợp lệ, có đúng 01 mẫu hoa và địa chỉ giao hàng hợp lệ.
  - Core của Combo còn hàng.
  - Nếu có thiệp, thiệp thuộc khách hàng, đã được khách hàng Xác nhận và là thiệp đang chọn của Checkout.
- **Trigger**: Khách hàng chọn “Hoàn tất”.

## Flow
### Main Flow
#### MF-01 – Tạo Order
1. Backend kiểm tra lại ownership, dữ liệu Checkout, mẫu hoa, giá và tồn kho.
2. Nếu Core hết hàng thì dừng flow.
3. Nếu Core còn nhưng một hoặc nhiều thành phần Support hết, hệ thống vẫn cho phép tạo Order, hiển thị thông báo cho khách hàng và ghi nhận các thành phần cần thay thế để cửa hàng chủ động liên hệ.
4. Hệ thống tính phí giao hàng và tổng bill cuối cùng.
5. Hệ thống tạo Order ở trạng thái PENDING.
6. Nếu Checkout có thiệp đã được khách hàng Xác nhận và đang chọn, hệ thống tạo liên kết Thiệp–Order và tính giá thiệp đó vào bill.
7. Nếu sử dụng Custom AI, hệ thống đánh dấu kết quả mẫu hoa đã được dùng để tạo Order.
8. Hệ thống chuyển tới Chi tiết đơn hàng và hiển thị “Thanh toán ngay”.

#### MF-02 – Thanh toán
9. Khách hàng chọn “Thanh toán ngay” khi Order còn PENDING và chưa quá 24 giờ.
10. Hệ thống khởi tạo giao dịch thanh toán.
11. Nếu thanh toán thành công, hệ thống cập nhật Order từ PENDING sang PAID.
12. Nếu thanh toán thất bại, Order vẫn PENDING và khách hàng có thể thử lại.
13. Mỗi lần thanh toán phải gắn với đúng Order và có mã giao dịch riêng.

#### MF-03 – Tự động hủy
14. Hệ thống xác định Order đã ở trạng thái PENDING đủ 24 giờ kể từ thời điểm tạo.
15. Nếu chưa có thanh toán thành công, hệ thống chuyển Order sang CANCELLED.
16. Hệ thống giải phóng toàn bộ tồn kho Combo đã giữ cho Order.
17. Order vẫn được lưu trong lịch sử đơn hàng với trạng thái “Đã hủy”.
18. Mẫu hoa Custom AI vẫn được giữ trong History và tiếp tục liên kết với Order đã hủy.
19. Order CANCELLED không được tiếp tục thanh toán.

### Alternative Flow
- **ALT-01 — Đặt hàng không kèm thiệp**:
  1. Khách hàng hoàn tất Checkout mà không chọn hoặc tạo thiệp.
  2. Hệ thống kiểm tra các thông tin bắt buộc còn lại của Checkout.
  3. Nếu Checkout hợp lệ và thành phần Core còn đủ hàng, hệ thống vẫn tạo Order.
  4. Order không được liên kết với thiệp; bill không hiển thị dòng giá thiệp; tổng tiền của Order không bao gồm giá thiệp.
  5. Khách hàng tiếp tục thực hiện thanh toán theo phương thức đã chọn.
- **ALT-02 — Thành phần Support không còn đủ hàng**:
  1. Khách hàng ấn chọn “Hoàn tất”.
  2. Hệ thống kiểm tra lại tồn kho: Core còn đủ hàng, một hoặc nhiều thành phần Support không còn đủ hàng.
  3. Hệ thống vẫn cho phép tạo Order và ghi nhận các thành phần Support cần được thay thế bằng thành phần tương đương.
  4. Việc xác định thành phần Support thay thế được thực hiện sau khi Staff liên hệ và thống nhất với khách hàng, không phải tại thời điểm tạo Order.
  5. Hệ thống hiển thị thông báo cho khách hàng và cửa hàng chủ động liên hệ để thống nhất thành phần thay thế trước khi thực hiện đơn.
  6. Bill và Order vẫn được tạo theo thông tin, giá và phí đã xác định tại thời điểm hoàn tất Checkout. Việc Support hết hàng không tự động làm thay đổi thành phần Core của Combo.
- **ALT-03 — Thanh toán lại Order đang PENDING**:
  1. Một Order đã được tạo nhưng giao dịch thanh toán trước đó không thành công.
  2. Order đang ở trạng thái PENDING và chưa vượt quá thời hạn thanh toán 24 giờ.
  3. Khách hàng chọn “Thanh toán lại”.
  4. Backend kiểm tra: Order thuộc khách hàng hiện tại, Order vẫn ở trạng thái PENDING, Order chưa vượt quá thời hạn thanh toán 24 giờ.
  5. Hệ thống khởi tạo một lần thanh toán mới cho cùng Order (không tạo Order mới).
  6. Khách hàng được phép thử thanh toán lại không giới hạn số lần trong thời hạn 24 giờ; mỗi lần thất bại không kéo dài thời hạn 24 giờ ban đầu.
  7. Nếu giao dịch thành công, hệ thống cập nhật Order sang PAID.
- **ALT-04 — Chốt bill tại thời điểm hoàn tất Checkout**:
  1. Khách hàng cung cấp đầy đủ thông tin Checkout và ấn chọn “Hoàn tất”.
  2. Hệ thống kiểm tra lại dữ liệu tính bill: Giá Combo, Giá thiệp (nếu có), Phí giao hàng theo địa chỉ, Các khoản giảm giá (nếu có).
  3. Hệ thống tính tổng tiền lần cuối tại thời điểm hoàn tất Checkout, tạo Order và lưu snapshot dữ liệu dùng để tính bill.
  4. Bill được gắn cố định với Order; tổng tiền khách hàng phải thanh toán là tổng tiền đã chốt trong bill. Mọi biến động chi phí sau đó không làm thay đổi bill đã chốt.

### Exception Flow
- **EXC-01 — Thành phần Core hết hàng tại thời điểm hoàn tất Checkout**: Khách hàng ấn chọn “Hoàn tất”. Hệ thống kiểm tra tồn kho Core ngay trước khi tạo Order và phát hiện Core không đủ số lượng cần thiết. Hệ thống không tạo Order, không tạo bill cuối, không tạo liên kết thiệp-Order, giữ lại thông tin Checkout nếu còn hợp lệ và hiển thị thông báo: “Combo hiện không còn đủ thành phần chính để đặt hàng. Vui lòng chọn sản phẩm khác.”
- **EXC-02 — Tạo Order thất bại**: Hệ thống gặp lỗi trong quá trình tạo Order. Toàn bộ thao tác tạo Order phải được hoàn tác (rollback); không để lại Order thiếu dữ liệu, không tạo bill/liên kết thiệp dở dang. Giữ lại Checkout và thông báo: “Không thể tạo đơn hàng. Vui lòng thử lại.” Đảm bảo không tạo trùng Order khi khách hàng thử lại.
- **EXC-03 — Thanh toán thất bại**: Cổng thanh toán trả về kết quả thất bại hoặc không xác nhận thành công. Hệ thống giữ Order ở trạng thái PENDING, ghi nhận log phục vụ đối soát, không thay đổi thời hạn 24 giờ và hiển thị: “Thanh toán chưa thành công. Bạn có thể thử lại trong thời hạn thanh toán của đơn hàng.”
- **EXC-04 — Thanh toán Order đã hết hạn hoặc bị hủy**: Khách hàng gửi yêu cầu thanh toán cho Order đã CANCELLED hoặc quá hạn 24 giờ. Backend từ chối khởi tạo giao dịch mới, không cập nhật sang đã thanh toán, chuyển Order sang CANCELLED nếu chưa cập nhật và hiển thị: “Đơn hàng đã hết thời hạn thanh toán và không thể tiếp tục thanh toán.”
- **EXC-05 — Cửa hàng không thể đáp ứng Order sau khi đặt**: Cửa hàng phát hiện không thể chuẩn bị/giao đơn. Hệ thống không tự động chuyển Order sang REFUNDED. Staff liên hệ khách hàng thống nhất phương án: nếu chưa thanh toán -> Admin chuyển CANCELLED; nếu đã thanh toán -> Admin hoàn tiền và chỉ chuyển sang REFUNDED sau khi hoàn tiền thành công; giải phóng tồn kho đã giữ và lưu log người thực hiện/lý do.
- **EXC-06 — Khách hàng không có quyền thao tác với Order**: Backend phát hiện Order không thuộc khách hàng hiện tại. Backend từ chối request, không tạo giao dịch thanh toán, không đổi trạng thái Order, không trả thông tin nhạy cảm và hiển thị thông báo chung: “Bạn không có quyền thực hiện thao tác này.”

## Acceptance Criteria
### AC-001: Chưa hoàn tất chưa có Order
- **Given**: Khách hàng đang ở Checkout.
- **When**: Khách hàng chưa bấm “Hoàn tất”.
- **Then**: Không tồn tại Order/PENDING.

### AC-002: Tạo Order PENDING
- **Given**: Checkout và tồn kho hợp lệ.
- **When**: Khách hàng bấm “Hoàn tất”.
- **Then**: Hệ thống tạo đúng 01 Order PENDING.
- **And**: Hiển thị Chi tiết đơn hàng và nút “Thanh toán ngay”.

### AC-003: Thiệp liên kết Order
- **Given**: Checkout có thiệp B đã được khách hàng Xác nhận và đang chọn.
- **When**: Order được tạo.
- **Then**: Tạo record liên kết Thiệp B–Order.
- **And**: Chỉ thiệp B được tính vào bill.

### AC-004: Chưa thanh toán
- **Given**: Order vừa tạo.
- **When**: Khách hàng chưa chọn “Thanh toán ngay”.
- **Then**: Order vẫn giữ trạng thái PENDING.

### AC-005: Thanh toán thất bại
- **Given**: Order còn PENDING trong thời hạn 24 giờ.
- **When**: Giao dịch thanh toán thất bại.
- **Then**: Order vẫn PENDING.
- **And**: Khách hàng được thử lại.

### AC-006: Thử lại không giới hạn
- **Given**: Order còn PENDING và chưa quá 24 giờ.
- **When**: Khách hàng thử thanh toán lại.
- **Then**: Hệ thống không giới hạn số lần thử.
- **And**: Không kéo dài thời hạn PENDING 24 giờ ban đầu.

### AC-007: Hủy sau 24 giờ
- **Given**: Order PENDING chưa thanh toán thành công.
- **When**: Đủ 24 giờ kể từ lúc tạo.
- **Then**: Hệ thống chuyển Order sang CANCELLED.
- **And**: Không cho thanh toán tiếp.

### AC-008: Core hết hàng
- **Given**: Core hết hàng ở lần kiểm tra cuối.
- **When**: Khách hàng hoàn tất Checkout.
- **Then**: Hệ thống không tạo Order.

### AC-009: Support hết hàng
- **Given**: Core còn hàng và Support hết hàng.
- **When**: Khách hàng hoàn tất Checkout.
- **Then**: Hệ thống vẫn tạo Order.
- **And**: Thông báo Staff sẽ liên hệ khách hàng để thống nhất thành phần Support thay thế.

### AC-010: Xử lý khi cửa hàng không thể đáp ứng Order
- **Given**: Order đã được tạo nhưng cửa hàng phát hiện không thể chuẩn bị hoặc giao đơn hàng theo thông tin đã xác nhận.
- **When**: Staff Hoa Theo Mùa xử lý Order.
- **Then**: Staff phải liên hệ trực tiếp với khách hàng để thông báo và thống nhất phương án xử lý.
- **And**: Hệ thống không được tự động chuyển Order sang REFUNDED.
- **And**: Nếu Order chưa thanh toán và hai bên thống nhất hủy, Admin chuyển Order sang CANCELLED.
- **And**: Nếu Order đã thanh toán và hai bên thống nhất hủy, Admin thực hiện hoàn tiền.
- **And**: Order chỉ được chuyển sang REFUNDED sau khi việc hoàn tiền được xác nhận thành công.
- **And**: Hệ thống phải giải phóng tồn kho của Combo đang được giữ cho Order nếu chưa được giải phóng trước đó.
- **And**: Hệ thống phải lưu người thực hiện, thời điểm và lý do hủy hoặc hoàn tiền.
- **And**: Order vẫn được lưu trong lịch sử đơn hàng với trạng thái tương ứng.

### AC-011: Thanh toán thành công
- **Given**: Order đang PENDING và còn trong thời hạn thanh toán.
- **When**: Giao dịch được xác nhận thành công.
- **Then**: Hệ thống cập nhật Order từ PENDING sang PAID.

## References
- **Rules**:
  - [BR-087](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/f0773ccf-da70-4dd1-8328-bc7d68d35824)
  - [BR-088](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/3b534fb0-c991-4ff7-89ad-eb4bd02921a8)
  - [BR-089](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/35efbcf7-42f4-4d17-b1e3-a419221bb001)
  - [BR-090](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/f3d3d0c2-21ae-42b0-864c-18027a6ebf6e)
  - [BR-091](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/7bc0baa5-01b5-4fe1-98c6-6b525be8e406)
  - [BR-092](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/92b589a3-f87b-4d15-a4e5-165a2be241a8)
  - [BR-093](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/31338ae7-cd10-434c-a2f6-f671ea33a5e6)
  - [BR-094](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/807a720b-e138-442e-b91b-0f701766fd32)
- **Dependencies**:
  - [STORY-038](https://document-first.vnzdna.com/projects/117393d8-1afc-4c89-baa9-9aeea430cdcd/documents/5cc87b78-e6bf-4996-8d67-787e0bc34bb4) / [38-InitializeCheckoutFromFlowerDesign.md](file:///d:/VNZ/document-first-brief/hoatheomua/UserStory/FlouristAI/38-InitializeCheckoutFromFlowerDesign.md)

## Non-Functional
- Tạo Order và các liên kết liên quan phải nhất quán; không để Order hoặc link bị tạo trùng do double click/retry.
- Backend kiểm tra trạng thái Order trước mọi lần thanh toán.
- Job tự hủy sau 24 giờ phải đảm bảo idempotent.
- Không hiển thị lỗi cổng thanh toán hoặc stack trace nội bộ cho người dùng.
- API tạo Order mục tiêu p95 ≤ 3 giây, không tính thời gian hệ thống bên thứ ba.
- Event và giao dịch thanh toán phải truy vết được theo Order.

## Out of Scope
- Admin xử lý fulfillment.
- Chi tiết tích hợp payment provider.
- Chính sách refund tự động.
- Quản trị tồn kho.
