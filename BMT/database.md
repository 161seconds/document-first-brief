// BMT Decor landing page - compact PostgreSQL design v2
// 10 tables. Table, column and enum names use PascalCase.
// Page/layout/routes and the editable page-node tree are fixed by frontend code.
// All tables follow BaseEntity + IAuditableEntity: IsDeleted defaults to false,
// CreatedAt is required, and UpdatedAt is nullable until the first update.

Enum SubmissionStatus {
  pending
  done
}

Enum PageCode {
  home
  about
  services
  service_turnkey
  service_architecture_interior
  service_construction
  service_renovation
  projects
  news
  recruitment
  quotation
  contact
  capability_profile
}

Enum QuotationBuildingType {
  nha_o
  van_phong
  tham_my_vien_showroom
  nha_hang_khach_san
}

Enum QuotationServiceType {
  xay_dung_tron_goi
  thiet_ke_kien_truc_noi_that
  thi_cong_xay_dung
  cai_tao_sua_chua
}

// Pages (Nội dung các trang)
Table Pages {
  Id uuid [pk]
  ParentId uuid [ref: > Pages.Id,
    note: 'NULL only for a seeded page root; descendants form a fixed tree']
  Code PageCode [note: 'Only page roots have a code; unique among roots']
  NodeKey varchar(100) [not null,
    note: 'Stable FE-defined key such as about, hero, journey, or item_01; admin cannot edit']
  NodeKind varchar(20) [not null,
    note: 'page, section, group, or item; fixed by seed']
  Value jsonb [not null, default: `{}`,
    note: 'Editable content only. Containers use {}; leaf/group payload follows a fixed JSON schema']
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz

  indexes {
    ParentId
    (ParentId, NodeKey) [unique]
  }
}

// ProjectCategories (Danh mục dùng để nhóm thẻ Dự án)
Table ProjectCategories {
  Id uuid [pk]
  Name varchar(100) [not null, unique, note: 'Tên danh mục hiển thị']
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz
}

// Projects (Dự án)
Table Projects {
  Id uuid [pk]
  CardTitle varchar(300) [not null, note: 'projects/list.title; project card title and source for generated slug']
  CategoryId uuid [not null, ref: > ProjectCategories.Id, note: 'Danh mục của thẻ dự án']
  CardImageUrl varchar(1200) [note: 'projects/list.thumbnail; one Cloudinary URL for the card image']
  DetailContent jsonb [not null, default: `{}`, note: 'projects/details fields grouped as overview, survey, solution, renders, process, comparisons, contactForm; each image and label has an explicit named field such as survey1Image, render1Image, comparison1BeforeImage; API preserves field names without positional arrays; {} until details are entered']
  IsFeatured boolean [not null, default: false,
    note: 'projects/list.highlight; editable Tieu bieu checkbox; max 8 per CategoryId']
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz

  indexes {
    (CategoryId, IsFeatured)
  }
}

// News (Tin tức)
Table News {
  Id uuid [pk]
  Title varchar(300) [not null]
  ImageUrl varchar(1200) [note: 'Optional desktopImage in admin; one Cloudinary URL, no alt or separate mobile image']
  Content jsonb [not null, default: `{}`, note: 'body rich text from the news editor; required by API validation']
  IsFeatured boolean [not null, default: false, note: 'featured checkbox in news list; max 5 selected']
  HighlightHome boolean [not null, default: false, note: 'highlightHome checkbox in news list; max 4 selected']
  Excerpt text
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz

  indexes {
    (IsFeatured, CreatedAt)
    (HighlightHome, CreatedAt)
  }
}

// Jobs (Vị trí tuyển dụng)
Table Jobs {
  Id uuid [pk]
  Title varchar(300) [not null]
  Department varchar(150)
  Location varchar(255)
  Schedule varchar(255)
  Compensation varchar(255)
  Summary text
  ImageUrl varchar(1200) [note: 'Cloudinary URL returned and stored by backend after upload']
  Content jsonb [not null,
    note: 'Separate required JSON string keys: responsibilities (Trách nhiệm) and benefits (Quyền lợi); admin rich text HTML']
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz

  indexes {
    Department
  }
}

// ContentSlugs (Lịch sử đường dẫn Dự án và Tin tức)
Table ContentSlugs {
  Id uuid [pk]
  Slug varchar(255) [not null, unique,
    note: 'Backend-generated from Projects.CardTitle or News.Title; globally unique']
  ProjectId uuid [ref: > Projects.Id]
  NewsId uuid [ref: > News.Id]
  IsCurrent boolean [not null, default: true]
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz

  indexes {
    ProjectId
    NewsId
  }
}

// SiteSettings (Cấu hình chung của website)
Table SiteSettings {
  Id smallint [pk, default: 1,
    note: 'Single-row table; application enforces id = 1']
  Header jsonb [not null, default: `{}`,
    note: 'Editable header logo URL only; navigation is fixed in frontend']
  Partners jsonb [not null, default: `{}`,
    note: 'Shared title and partner1LogoImage..partner6LogoImage URL keys; no array or link']
  Footer jsonb [not null, default: `{}`,
    note: 'Footer content: footerLogo, socialWidgetImage and service1Label/service1PageName..service4Label/service4PageName; PageName stores page name, never URL']
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz
}

// FormSubmissions (Mọi form liên hệ, gồm trang Báo giá: chỉ tên và SĐT)
Table FormSubmissions {
  Id uuid [pk]
  CustomerName varchar(200) [not null,
    note: 'Tên khách hàng']
  Phone varchar(30) [not null]
  Status SubmissionStatus [not null, default: 'pending']
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz

  indexes {
    (Status, CreatedAt)
    Phone
  }
}

// PriceRanges (16 khoảng giá thị trường; admin sửa min/max qua FE)
Table PriceRanges {
  Id uuid [pk]
  BuildingType QuotationBuildingType [not null]
  ServiceType QuotationServiceType [not null]
  UnitPriceMin numeric(14,0) [not null, note: 'Đơn giá thấp nhất, VND/m²']
  UnitPriceMax numeric(14,0) [not null, note: 'Đơn giá cao nhất, VND/m²']
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz

  indexes {
    (BuildingType, ServiceType) [unique]
  }
}

// AdminUsers (Tài khoản được phép đăng nhập khu vực quản trị)
Table AdminUsers {
  Id uuid [pk]
  Email varchar(320) [not null, note: 'Email đăng nhập; BE chuẩn hóa về chữ thường']
  PasswordHash varchar(255) [not null, note: 'Hash mật khẩu; không lưu mật khẩu gốc']
  // DisplayName varchar(200) [not null]
  IsActive boolean [not null, default: true]
  IsDeleted boolean [not null, default: false]
  CreatedAt timestamptz [not null]
  UpdatedAt timestamptz
}

// PostgreSQL implementation requirements inherited from v1:
// 1. Add CHECK (num_nonnulls("ProjectId", "NewsId") = 1) to "ContentSlugs".
// 2. Add two partial unique indexes for at most one current slug per record:
//    UNIQUE ("ProjectId") WHERE "IsCurrent" = true AND "ProjectId" IS NOT NULL;
//    UNIQUE ("NewsId") WHERE "IsCurrent" = true AND "NewsId" IS NOT NULL.
//    The creation workflow must also create one current slug for every new record.
// 3. Slug creation/title-change runs in one transaction; retry on unique conflicts.
// 4. Old slugs remain while their record is active and return HTTP 301 to its current slug.
//    On soft delete of a record, exclude its slugs from public lookup.
//    On hard delete, delete that record's slugs in the same transaction before deleting the record.
// 5. Pages is a seeded adjacency tree: exactly one root per PageCode; roots have
//    ParentId NULL, Code set, NodeKind=page, NodeKey=Code; descendants have
//    ParentId set and Code NULL. Add CHECK for these root/child rules and
//    CREATE UNIQUE INDEX uq_pages_root_code ON "Pages"("Code") WHERE "ParentId" IS NULL.
//    Reject cycles and cross-page moves in application/seed workflow. Admin may
//    UPDATE Value only on whitelisted nodes; never INSERT/DELETE/reparent/rekey.
//    Sections and item slots are fixed; no admin-controlled sort_order.
// 6. Page routes, CTA targets, header navigation, social links and image ratios live in frontend code/config.
//    Footer service links are the only admin-selectable navigation targets.
//    Editable page JSON contains no locked/decorative image, fixed label or static group identity.
// 7. Admin submits image files; backend uploads to Cloudinary and stores the returned URL.
// 8. Editable JSON may store Cloudinary image URLs, but never image width, height or aspect ratio.
//    Footer service1PageName..service4PageName store allowlisted page names; FE maps each name to its fixed route.
// 9. Validate Pages.Value by (PageCode, node path), Projects.DetailContent,
//    and News.Content separately. Project slug comes from CardTitle.
//    Seed ProjectCategories with the four current FE category names;
//    Projects.CategoryId replaces the v1 CardCategory. Map existing category
//    strings to category IDs before making CategoryId NOT NULL.
//    DetailContent.overview.category is
//    an independent display label for the detail page.
// 10. Jobs.Content has two required string keys: responsibilities and benefits.
//     Add CHECK for JSON object, key presence with content ? key,
//     and jsonb_typeof(content->key) = 'string' for both keys.
//     The public API converts each HTML list to its own string[] for CareerJob.

// V2 quotation flow and integrity requirements:
// 11. Seed PriceRanges with the 16 pairs in Ham_uoc_tinh_chi_phi_Bao_gia.md.
//     Admin FE edits UnitPriceMin/UnitPriceMax for existing pairs only; no create/delete
//     or editing BuildingType/ServiceType. BE reads by stable enum codes, never
//     by editable labels in Pages.Value. Add CHECK for positive min, max >= min,
//     and update UpdatedAt on each price change.
// 12. Public FE sends building type, area, optional budget, and service type to BE.
//     BE reads the matching PriceRanges row and returns min/max = area_m2 *
//     UnitPriceMin/UnitPriceMax, rounded to 1000 VND. Budget only classifies
//     below/within/above the range. No estimator inputs/results are persisted.
// 13. The quotation contact form sends CustomerName and Phone only to
//     FormSubmissions, exactly like other contact forms. It has no PriceRanges
//     FK, estimate snapshot, or quotation-specific submission table.
// 14. Budget is optional per the formula document; update current FE validation
//     (which requires budget) when integrating the estimate API.
// 15. Admin authentication: add CREATE UNIQUE INDEX uq_admin_users_email
//     ON "AdminUsers" (lower("Email")); reject inactive users. BE verifies
//     PasswordHash with a password hashing algorithm and issues/validates a
//     protected session or token. Every admin write endpoint requires that auth.
//     The current FE demo account and constant mock-session cookie are not real
//     authentication and must be replaced when connecting the backend.
