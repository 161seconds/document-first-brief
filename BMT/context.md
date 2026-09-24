# BMT Decor backend context

This is a working summary for agents. Read the linked source section when implementing a feature; this file does not replace the full contracts.

## Source of truth

- [`docs/database.md`](database.md): current PostgreSQL schema **v2**, 10 tables, PascalCase names. Use this for current tables, columns, types, and constraints.
- [`docs/bmt_landing_data_dictionary.md`](bmt_landing_data_dictionary.md): field meaning, page JSON paths, admin inputs, and FE response mapping. Some examples still use older snake_case names and `CardCategory`; map their meaning to the v2 schema.
- [`docs/bmt_landing_compact_design.md`](bmt_landing_compact_design.md): design rationale and fixed frontend behavior. Its 7-table model predates v2; do not use its table count or claims of no admin users as the current schema.
- [`docs/Ham_uoc_tinh_chi_phi_Bao_gia.md`](Ham_uoc_tinh_chi_phi_Bao_gia.md): quotation formula and the 16 initial unit-price ranges. Current persistence and frontend/backend split are specified in `database.md`.

If sources disagree, use the current schema for database structure, the data dictionary for detailed field behavior, and the latest explicit user decision. Call out unresolved differences rather than silently combining versions. Check implementation code before describing behavior as already implemented.

## Current data model

`Pages`, `ProjectCategories`, `Projects`, `News`, `Jobs`, `ContentSlugs`, `SiteSettings`, `FormSubmissions`, `PriceRanges`, and `AdminUsers`. Every table has `IsDeleted` (default `false`), required `CreatedAt`, and nullable `UpdatedAt` until first update, matching `BaseEntity` and `IAuditableEntity`. Public reads must exclude soft-deleted records. `PriceRange` is currently the only entity class present; schema documentation is ahead of implementation.

### Pages and shared content

`Pages` is a seeded tree: one root per `PageCode`, with fixed section/group/item descendants identified by `ParentId` and `NodeKey`. Frontend/seed owns routes, layout, node paths, slot counts, and render order. Admin may update whitelisted fields in `Value jsonb` only; no creating, deleting, moving, or rekeying page nodes. Validate by page code and node path. A page JSON example in the dictionary is a FE view model assembled from nodes, not one stored row. `SiteSettings` stores shared Header, Partners, and Footer content. Header navigation, social destinations, and CTA actions remain fixed in FE. Footer service links store an allowlisted page **name** in `service1PageName`–`service4PageName`; FE maps names to routes.

Admin uploads image files; backend stores returned Cloudinary URLs. Do not store image dimensions, aspect ratios, alt text, or manually entered navigation URLs in editable content. Consult data dictionary sections 3.3–3.12 for exact page fields and slots.

### Projects and news

`Projects.CategoryId` references seeded `ProjectCategories` (v2 replacement for older `CardCategory`). Card data (`CardTitle`, category, `CardImageUrl`) and `DetailContent.overview` title/category have different purposes. The list and detail admin forms edit the **same** project row; a new card may have `DetailContent = {}`. Preserve named JSON fields such as `survey1Image` and `comparison1BeforeImage`; do not infer positional arrays.

`Projects.IsFeatured` is the `projects/list` “Tiêu biểu” checkbox, limited to 8 active projects per category. It does **not** fill the fixed featured-project slots in Home or service page content.

`News.IsFeatured` selects up to 5 featured items for the News page. `News.HighlightHome` independently selects up to 4 items for Home; Home orders them by `CreatedAt` descending. `News.Content` contains required rich-text `body`; `Excerpt` and `ImageUrl` are optional. Enforce selection limits in backend transactions, including project category changes.

`ContentSlugs` holds globally unique backend-generated slugs from `Projects.CardTitle` or `News.Title`. Each slug belongs to exactly one project or news item; at most one current slug exists per record. Create/change title and slug in one transaction, preserve old slugs for 301 redirects while the record is active, and exclude soft-deleted records from public resolution. See database constraints and dictionary section 6.

### Jobs, forms, and admin

`Jobs.Content` requires separate string keys `responsibilities` and `benefits`; the public API maps each HTML list separately for FE. `FormSubmissions` stores only `CustomerName`, `Phone`, and `Status` (`pending`/`done`), including quotation contact submissions. `AdminUsers` stores normalized email, password hash, and active status. Admin write endpoints require real authentication; FE demo credentials/session are not the backend contract.

## Quotation flow

Seed `PriceRanges` with all 16 building-type/service-type pairs from the quotation formula document. Admin edits only `UnitPriceMin` and `UnitPriceMax` for existing pairs. Enforce positive minimum and maximum at least minimum. Public FE sends building type, area, optional budget, and service type to `POST /api/v1/quotation/estimate`; BE reads the matching range and calculates the estimate, rounded to 1,000 VND. Budget classifies below/within/above the range without changing it. Estimator inputs/results are not persisted. The contact step submits only name and phone to `FormSubmissions`. The formula document allows null budget while current FE requires one; update FE validation during integration.

## Implementation checkpoint

These documents define the intended contract, not proof that endpoints or migrations exist. Before coding, inspect the relevant entity, mapping, service, controller, and FE integration point. Do not add fields, routes, or permissions merely because an older mock or design note contains them.
