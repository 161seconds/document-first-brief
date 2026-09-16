# Database Diagram

Database schema definition written in [DBML](https://dbml.dbdiagram.io/docs/).

```dbml
//////////////////////////////////////////////////////////////
// RACEHORSE CROSS-BORDER TRANSPORT SYSTEM
// PHYSICAL ERD - AUDITED CONSOLIDATED V1
//
// Basis:
// - Original SWP.drawio.xml logical/conceptual ERD
// - Confirmed relationship corrections from the design discussion
//
// Important conservative choices kept from the source model:
// - Vehicle is still linked directly to TransportPlan.
// - Location keeps both transport_plan_id and route_id.
// - Compliance requirements are dossier-level; per-horse legal scope
//   has not yet been added because it was not present in the source ERD.
// - Reviewer/requester/submission actor FKs are not added where the
//   source ERD did not define those relationships.
//////////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////
// ENUMS - CORE
//////////////////////////////////////////////////////////////

Enum account_status {
  ACTIVE
  INACTIVE
  SUSPENDED
}

Enum account_type {
  CUSTOMER
  EMPLOYEE
}

Enum employee_role {
  LOGISTICS_MANAGER 
  TRANSPORT_SPECIALIST
  FLEET_ROUTE_COORDINATOR
  DRIVER_ESCORT
}

Enum horse_sex {
  MALE
  FEMALE
  GELDING
}

Enum booking_status {
  PENDING
  UNDER_REVIEW
  APPROVED
  REJECTED
  CANCELLED
}

Enum transport_status {
  CREATED
  PLANNING
  READY
  IN_TRANSIT
  COMPLETED
  CANCELLED
}


//////////////////////////////////////////////////////////////
// ENUMS - TRANSPORT PLANNING
//////////////////////////////////////////////////////////////

Enum transport_plan_status {
  DRAFT
  PENDING_APPROVAL
  APPROVED
  IN_PROGRESS
  COMPLETED
  CANCELLED
}

Enum route_plan_status {
  DRAFT
  CONFIRMED
  ACTIVE
  COMPLETED
  CANCELLED
}

Enum transport_mode {
  ROAD
  SEA
  AIR
}

Enum route_type {
  PRIMARY
  BACKUP
}

Enum location_type {
  ORIGIN
  DESTINATION
  TRANSIT
  BORDER_GATE
  AIRPORT
  SEAPORT
  STABLE
  REST_STOP
  OTHER
}

Enum vehicle_status {
  AVAILABLE
  ASSIGNED
  IN_USE
  MAINTENANCE
  INACTIVE
}

Enum provider_status {
  ACTIVE
  INACTIVE
  SUSPENDED
}


//////////////////////////////////////////////////////////////
// ENUMS - EXECUTION
//////////////////////////////////////////////////////////////

Enum handover_type {
  PICKUP
  TRANSFER
  BORDER_HANDOVER
  CARRIER_HANDOVER
  DELIVERY
  OTHER
}

Enum handover_status {
  PLANNED
  IN_PROGRESS
  COMPLETED
  CANCELLED
}

Enum horse_health_log_type {
  PRE_TRIP_CHECK
  ROUTINE_CHECK
  BORDER_CHECK
  REST_STOP_CHECK
  INCIDENT_FOLLOWUP
  POST_TRIP_CHECK
  OTHER
}

Enum horse_health_status {
  NORMAL
  OBSERVATION_REQUIRED
  AT_RISK
  CRITICAL
}

Enum hydration_status {
  NORMAL
  MILD_DEHYDRATION
  MODERATE_DEHYDRATION
  SEVERE_DEHYDRATION
}


//////////////////////////////////////////////////////////////
// ENUMS - INCIDENT / EMERGENCY
//////////////////////////////////////////////////////////////

Enum incident_category {
  VEHICLE
  HORSE_HEALTH
  MIXED
  GENERAL
  OTHER
}

Enum incident_severity {
  LOW
  MEDIUM
  HIGH
  CRITICAL
}

Enum incident_status {
  REPORTED
  UNDER_INVESTIGATION
  ACTION_REQUIRED
  RESOLVED
  CLOSED
  CANCELLED
}

Enum vehicle_incident_type {
  BREAKDOWN
  ACCIDENT
  TIRE_DAMAGE
  ENGINE_FAILURE
  TEMPERATURE_CONTROL_FAILURE
  EQUIPMENT_FAILURE
  OTHER
}

Enum horse_incident_type {
  INJURY
  DEHYDRATION
  RESPIRATORY_ISSUE
  FEVER
  STRESS
  COLIC
  FATIGUE
  OTHER
}

Enum emergency_cost_category {
  VETERINARY
  MEDICATION
  VEHICLE_REPAIR
  TOWING
  REPLACEMENT_VEHICLE
  EMERGENCY_STABLE
  ROUTE_CHANGE
  ACCOMMODATION
  OTHER
}

Enum emergency_cost_status {
  PENDING_APPROVAL
  APPROVED
  REJECTED
  CANCELLED
}

Enum approval_decision {
  APPROVED
  REJECTED
}


//////////////////////////////////////////////////////////////
// ENUMS - FINANCE
//////////////////////////////////////////////////////////////

Enum invoice_status {
  DRAFT
  ISSUED
  PARTIALLY_PAID
  PAID
  OVERDUE
  CANCELLED
}

Enum invoice_item_type {
  TRANSPORT_FEE
  HORSE_SERVICE_FEE
  HANDLING_FEE
  COMPLIANCE_FEE
  EMERGENCY_COST
  OTHER
}

Enum payment_method {
  CASH
  BANK_TRANSFER
  CREDIT_CARD
  OTHER
}

Enum receipt_status {
  PENDING
  CONFIRMED
  FAILED
  REFUNDED
}

Enum financial_report_status {
  DRAFT
  FINALIZED
}


//////////////////////////////////////////////////////////////
// ENUMS - CLAIM
//////////////////////////////////////////////////////////////

Enum claim_type {
  HORSE_INJURY
  HORSE_HEALTH
  LOSS
  DAMAGE
  DELAY
  DOCUMENT_ISSUE
  SERVICE_ISSUE
  OTHER
}

Enum claim_status {
  SUBMITTED
  UNDER_REVIEW
  APPROVED
  PARTIALLY_APPROVED
  REJECTED
  RESOLVED
  CANCELLED
}

Enum claim_resolution_type {
  COMPENSATION
  PARTIAL_COMPENSATION
  REFUND
  SERVICE_RECOVERY
  NO_COMPENSATION
  OTHER
}


//////////////////////////////////////////////////////////////
// ENUMS - COMPLIANCE
//////////////////////////////////////////////////////////////

Enum compliance_requirement_status {
  ACTIVE
  INACTIVE
}

Enum dossier_template_status {
  DRAFT
  ACTIVE
  INACTIVE
}

Enum compliance_dossier_status {
  DRAFT
  IN_PREPARATION
  READY_FOR_SUBMISSION
  SUBMITTED
  UNDER_REVIEW
  APPROVED
  REJECTED
  CLOSED
}

Enum dossier_requirement_status {
  PENDING
  REQUESTED
  PROVIDED
  UNDER_REVIEW
  SATISFIED
  REJECTED
  WAIVED
}

Enum compliance_document_status {
  DRAFT
  PROVIDED
  UNDER_REVIEW
  APPROVED
  REJECTED
  EXPIRED
}

Enum document_review_result {
  APPROVED
  REJECTED
  REVISION_REQUIRED
}

Enum document_request_type {
  MISSING_DOCUMENT
  REVISION
  REUPLOAD
  ADDITIONAL_INFORMATION
}

Enum document_request_status {
  OPEN
  FULFILLED
  CANCELLED
}

Enum authority_submission_status {
  DRAFT
  SUBMITTED
  UNDER_REVIEW
  APPROVED
  REJECTED
  RETURNED
}


//////////////////////////////////////////////////////////////
// ENUMS - NOTIFICATION
//////////////////////////////////////////////////////////////

Enum notification_type {
  BOOKING
  TRANSPORT
  ROUTE
  HANDOVER
  HORSE_HEALTH
  INCIDENT
  EMERGENCY_COST
  PAYMENT
  CLAIM
  COMPLIANCE
  DOCUMENT
  SYSTEM
}

Enum notification_priority {
  LOW
  NORMAL
  HIGH
  URGENT
}

Enum notification_recipient_status {
  PENDING
  DELIVERED
  READ
  FAILED
}


//////////////////////////////////////////////////////////////
// FLOW 1 - ACCOUNT / BOOKING / TRANSPORT CORE
//////////////////////////////////////////////////////////////

Table Account {
  account_id bigint [pk, increment]

  email varchar(255) [not null, unique]
  password_hash varchar(255) [not null]

  account_type account_type [not null]
  status account_status [not null, default: 'ACTIVE']

  last_login_at timestamp

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    status
    account_type
  }
}


Table Customer {
  customer_id bigint [pk, increment]

  account_id bigint [not null, unique]

  full_name varchar(150) [not null]
  phone varchar(30)
  date_of_birth date

  address varchar(500)
  country varchar(100)

  identity_number varchar(100)

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    phone
  }
}


Table Employee {
  employee_id bigint [pk, increment]

  account_id bigint [not null, unique]

  employee_code varchar(50) [not null, unique]
  full_name varchar(150) [not null]
  phone varchar(30)

  role employee_role [not null]

  is_active boolean [not null, default: true]

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    role
    is_active
  }
}


Table Horse {
  horse_id bigint [pk, increment]

  customer_id bigint [not null]

  horse_name varchar(150) [not null]

  passport_number varchar(100) [unique]
  microchip_number varchar(100) [unique]

  breed varchar(100)
  sex horse_sex

  date_of_birth date
  color varchar(100)

  country_of_origin varchar(100)

  identification_notes text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    customer_id
    horse_name
  }
}


Table Booking {
  booking_id bigint [pk, increment]

  booking_code varchar(50) [not null, unique]

  customer_id bigint [not null]

  assigned_employee_id bigint

  status booking_status [not null, default: 'PENDING']

  origin_address varchar(500) [not null]
  origin_country varchar(100) [not null]

  destination_address varchar(500) [not null]
  destination_country varchar(100) [not null]

  requested_departure_date date [not null]
  requested_arrival_date date

  special_requirements text
  customer_note text

  submitted_at timestamp
  approved_at timestamp
  rejected_at timestamp
  cancelled_at timestamp

  rejection_reason text
  cancellation_reason text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    customer_id
    assigned_employee_id
    status
    requested_departure_date
  }
}


Table BookingHorse {
  booking_id bigint [not null]
  horse_id bigint [not null]

  special_handling_notes text

  created_at timestamp [not null]

  indexes {
    (booking_id, horse_id) [pk]
    horse_id
  }
}


Table BookingStatusHistory {
  booking_status_history_id bigint [pk, increment]

  booking_id bigint [not null]

  previous_status booking_status
  new_status booking_status [not null]

  changed_by_account_id bigint [not null]

  reason text

  changed_at timestamp [not null]

  indexes {
    booking_id
    changed_by_account_id
    changed_at
  }
}


Table Transport {
  transport_id bigint [pk, increment]

  transport_code varchar(50) [not null, unique]

  booking_id bigint [not null, unique]

  status transport_status [not null, default: 'CREATED']

  actual_departure_at timestamp
  actual_arrival_at timestamp

  completed_at timestamp
  cancelled_at timestamp

  cancellation_reason text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    status
  }
}


//////////////////////////////////////////////////////////////
// FLOW 2 - TRANSPORT / ROUTE PLANNING
//////////////////////////////////////////////////////////////

Table TransportPlan {
  transport_plan_id bigint [pk, increment]

  transport_id bigint [not null]

  plan_code varchar(50) [not null, unique]

  status transport_plan_status [not null, default: 'DRAFT']

  planned_start_at timestamp
  planned_end_at timestamp

  planning_note text

  estimated_duration_minutes int
  estimated_distance_km decimal(10,2)

  approved_by_employee_id bigint
  approved_at timestamp

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    transport_id
    status
    approved_by_employee_id
  }
}


Table RoutePlan {
  route_plan_id bigint [pk, increment]

  transport_plan_id bigint [not null, unique]

  route_plan_code varchar(50) [not null, unique]

  status route_plan_status [not null, default: 'DRAFT']

  total_distance_km decimal(10,2)
  estimated_duration_minutes int

  route_note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    status
  }
}


Table Route {
  route_id bigint [pk, increment]

  route_plan_id bigint [not null]

  route_code varchar(50) [not null]

  route_type route_type [not null, default: 'PRIMARY']

  transport_mode transport_mode [not null]

  sequence_no int [not null]

  route_name varchar(200)

  estimated_distance_km decimal(10,2)
  estimated_duration_minutes int

  planned_departure_at timestamp
  planned_arrival_at timestamp

  route_note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    route_plan_id
    route_type
    transport_mode

    (route_plan_id, route_code) [unique]
    (route_plan_id, sequence_no) [unique]
  }
}


Table Location {
  location_id bigint [pk, increment]

  transport_plan_id bigint [not null]
  route_id bigint [not null]

  location_name varchar(200) [not null]

  location_type location_type [not null]

  sequence_no int [not null]

  address varchar(500)

  city varchar(100)
  state_province varchar(100)
  country varchar(100) [not null]

  latitude decimal(10,7)
  longitude decimal(10,7)

  planned_arrival_at timestamp
  planned_departure_at timestamp

  actual_arrival_at timestamp
  actual_departure_at timestamp

  contact_name varchar(150)
  contact_phone varchar(30)

  location_note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    transport_plan_id
    route_id
    location_type

    (route_id, sequence_no) [unique]
  }
}


Table Vehicle {
  vehicle_id bigint [pk, increment]

  transport_plan_id bigint

  vehicle_code varchar(50) [not null, unique]

  license_plate varchar(50) [unique]

  vehicle_type varchar(100)

  manufacturer varchar(100)
  model varchar(100)

  capacity_horses int

  status vehicle_status [not null, default: 'AVAILABLE']

  registration_number varchar(100)
  registration_expiry_date date

  insurance_number varchar(100)
  insurance_expiry_date date

  special_equipment text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    transport_plan_id
    status
  }
}


Table TransportProvider {
  transport_provider_id bigint [pk, increment]

  provider_code varchar(50) [not null, unique]

  provider_name varchar(200) [not null]

  status provider_status [not null, default: 'ACTIVE']

  contact_name varchar(150)
  phone varchar(30)
  email varchar(255)

  address varchar(500)
  country varchar(100)

  tax_code varchar(100)

  supported_modes varchar(100)

  note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    provider_name
    status
  }
}


Table TransportPlanProvider {
  transport_plan_id bigint [not null]
  transport_provider_id bigint [not null]

  service_type varchar(100)

  assigned_at timestamp [not null]

  note text

  indexes {
    (transport_plan_id, transport_provider_id) [pk]
    transport_provider_id
  }
}


Table EmployeeTransportPlan {
  employee_id bigint [not null]
  transport_plan_id bigint [not null]

  responsibility varchar(150)

  assigned_at timestamp [not null]

  is_primary boolean [not null, default: false]

  note text

  indexes {
    (employee_id, transport_plan_id) [pk]
    transport_plan_id
  }
}


//////////////////////////////////////////////////////////////
// FLOW 3 - EXECUTION / HANDOVER / HORSE HEALTH
//////////////////////////////////////////////////////////////

Table HandoverRecord {
  handover_record_id bigint [pk, increment]

  transport_id bigint [not null]

  handover_code varchar(50) [not null, unique]

  sequence_no int [not null]

  handover_type handover_type [not null]
  status handover_status [not null, default: 'PLANNED']

  planned_handover_at timestamp
  actual_handover_at timestamp

  from_party_name varchar(200)
  from_contact_name varchar(150)
  from_contact_phone varchar(30)

  to_party_name varchar(200)
  to_contact_name varchar(150)
  to_contact_phone varchar(30)

  location_name varchar(200)
  address varchar(500)
  country varchar(100)

  horse_condition_note text

  document_handover_note text
  equipment_handover_note text
  discrepancy_note text

  sender_signature_url varchar(500)
  receiver_signature_url varchar(500)

  completed_at timestamp
  cancelled_at timestamp
  cancellation_reason text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    transport_id
    status
    actual_handover_at

    (transport_id, sequence_no) [unique]
  }
}


Table HorseHealthLog {
  horse_health_log_id bigint [pk, increment]

  transport_id bigint [not null]
  horse_id bigint [not null]
  employee_id bigint [not null]

  log_type horse_health_log_type [not null]

  health_status horse_health_status [not null]

  checked_at timestamp [not null]

  temperature_celsius decimal(4,1)
  heart_rate_bpm int
  respiratory_rate_bpm int

  hydration_status hydration_status

  behavior_note text
  symptom_note text
  injury_note text
  feeding_note text
  hydration_note text

  treatment_provided text
  medication_given text

  veterinarian_required boolean [not null, default: false]
  veterinarian_note text

  location_description varchar(500)

  photo_url varchar(500)

  general_note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    transport_id
    horse_id
    employee_id
    health_status
    checked_at

    (transport_id, horse_id)
  }
}


//////////////////////////////////////////////////////////////
// FLOW 4 - INCIDENT / EMERGENCY
//////////////////////////////////////////////////////////////

Table IncidentReport {
  incident_report_id bigint [pk, increment]

  incident_code varchar(50) [not null, unique]

  employee_id bigint [not null]
  location_id bigint [not null]

  category incident_category [not null]
  severity incident_severity [not null]

  status incident_status [not null, default: 'REPORTED']

  title varchar(200) [not null]

  description text [not null]

  occurred_at timestamp [not null]
  reported_at timestamp [not null]

  immediate_action text

  resolution_summary text

  resolved_at timestamp
  closed_at timestamp

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    employee_id
    location_id
    category
    severity
    status
    occurred_at
  }
}


Table VehicleIncidentDetail {
  vehicle_incident_detail_id bigint [pk, increment]

  incident_report_id bigint [not null, unique]

  vehicle_id bigint [not null]

  incident_type vehicle_incident_type [not null]

  vehicle_condition text

  damage_description text

  vehicle_operable boolean [not null, default: true]

  repair_required boolean [not null, default: false]
  towing_required boolean [not null, default: false]
  replacement_vehicle_required boolean [not null, default: false]

  estimated_repair_cost decimal(18,2)

  repair_note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    vehicle_id
    incident_type
  }
}


Table HorseHealthIncidentDetail {
  horse_health_incident_detail_id bigint [pk, increment]

  incident_report_id bigint [not null]

  horse_id bigint [not null]

  incident_type horse_incident_type [not null]

  condition_description text [not null]

  symptoms text

  body_temperature_celsius decimal(4,1)

  heart_rate_bpm int
  respiratory_rate_bpm int

  first_aid_provided text

  veterinarian_required boolean [not null, default: false]

  veterinarian_contacted_at timestamp

  veterinarian_instruction text

  medication_given text

  horse_transportable boolean

  outcome_note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    incident_report_id
    horse_id
    incident_type

    (incident_report_id, horse_id)
  }
}


Table EmergencyCostRequest {
  emergency_cost_request_id bigint [pk, increment]

  incident_report_id bigint [not null]

  request_code varchar(50) [not null, unique]

  category emergency_cost_category [not null]

  description text [not null]

  requested_amount decimal(18,2) [not null]

  currency varchar(3) [not null]

  status emergency_cost_status [not null, default: 'PENDING_APPROVAL']

  justification text [not null]

  requested_at timestamp [not null]

  approved_amount decimal(18,2)

  cancelled_at timestamp
  cancellation_reason text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    incident_report_id
    category
    status
  }
}


Table CostApprovalDecision {
  cost_approval_decision_id bigint [pk, increment]

  emergency_cost_request_id bigint [not null, unique]

  employee_id bigint [not null]

  decision approval_decision [not null]

  approved_amount decimal(18,2)

  decision_reason text

  decided_at timestamp [not null]

  created_at timestamp [not null]

  indexes {
    employee_id
    decision
    decided_at
  }
}


//////////////////////////////////////////////////////////////
// FLOW 5 - FINANCE / BILLING / PAYMENT
//////////////////////////////////////////////////////////////

Table Invoice {
  invoice_id bigint [pk, increment]

  invoice_code varchar(50) [not null, unique]

  booking_id bigint [not null]
  employee_id bigint [not null]

  status invoice_status [not null, default: 'DRAFT']

  currency varchar(3) [not null]

  subtotal decimal(18,2) [not null, default: 0]
  discount_amount decimal(18,2) [not null, default: 0]
  tax_amount decimal(18,2) [not null, default: 0]
  total_amount decimal(18,2) [not null, default: 0]

  issued_at timestamp
  due_date date
  paid_at timestamp

  cancelled_at timestamp
  cancellation_reason text

  billing_note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    booking_id
    employee_id
    status
    due_date
  }
}


Table InvoiceItem {
  invoice_item_id bigint [pk, increment]

  invoice_id bigint [not null]

  horse_id bigint

  emergency_cost_request_id bigint [unique]

  item_type invoice_item_type [not null]

  description varchar(500) [not null]

  quantity decimal(10,2) [not null, default: 1]

  unit_price decimal(18,2) [not null]

  line_amount decimal(18,2) [not null]

  note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    invoice_id
    horse_id
    item_type
  }
}


Table Receipt {
  receipt_id bigint [pk, increment]

  receipt_code varchar(50) [not null, unique]

  invoice_id bigint [not null]

  amount decimal(18,2) [not null]

  currency varchar(3) [not null]

  payment_method payment_method [not null]

  status receipt_status [not null, default: 'PENDING']

  transaction_reference varchar(150)

  paid_at timestamp
  confirmed_at timestamp

  note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    invoice_id
    status
    transaction_reference
    paid_at
  }
}


Table InvoiceStatusHistory {
  invoice_status_history_id bigint [pk, increment]

  invoice_id bigint [not null]

  previous_status invoice_status
  new_status invoice_status [not null]

  reason text

  changed_at timestamp [not null]

  created_at timestamp [not null]

  indexes {
    invoice_id
    new_status
    changed_at
  }
}


Table FinancialReport {
  financial_report_id bigint [pk, increment]

  report_code varchar(50) [not null, unique]

  transport_id bigint [not null, unique]

  status financial_report_status [not null, default: 'DRAFT']

  currency varchar(3) [not null]

  total_invoiced_amount decimal(18,2) [not null, default: 0]
  total_paid_amount decimal(18,2) [not null, default: 0]
  total_emergency_cost decimal(18,2) [not null, default: 0]
  outstanding_amount decimal(18,2) [not null, default: 0]

  summary_note text

  generated_at timestamp
  finalized_at timestamp

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    status
  }
}


//////////////////////////////////////////////////////////////
// FLOW 6 - CLAIM / CLAIM RESOLUTION
//////////////////////////////////////////////////////////////

Table Claim {
  claim_id bigint [pk, increment]

  claim_code varchar(50) [not null, unique]

  customer_id bigint [not null]

  handover_record_id bigint [not null]

  claim_type claim_type [not null]

  status claim_status [not null, default: 'SUBMITTED']

  title varchar(200) [not null]

  description text [not null]

  requested_compensation_amount decimal(18,2)

  currency varchar(3)

  evidence_note text
  customer_note text

  submitted_at timestamp [not null]

  reviewed_at timestamp
  resolved_at timestamp
  cancelled_at timestamp

  cancellation_reason text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    customer_id
    handover_record_id
    claim_type
    status
    submitted_at
  }
}


Table ClaimResolution {
  claim_resolution_id bigint [pk, increment]

  claim_id bigint [not null, unique]

  resolution_type claim_resolution_type [not null]

  resolution_summary text [not null]

  approved_compensation_amount decimal(18,2)

  currency varchar(3)

  corrective_action text

  internal_note text

  resolved_at timestamp [not null]

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    resolution_type
    resolved_at
  }
}


//////////////////////////////////////////////////////////////
// FLOW 7 - COMPLIANCE / LEGAL DOSSIER
//////////////////////////////////////////////////////////////

Table ComplianceRequirement {
  compliance_requirement_id bigint [pk, increment]

  requirement_code varchar(50) [not null, unique]

  requirement_name varchar(200) [not null]

  description text

  issuing_authority varchar(200)

  validity_required boolean [not null, default: false]

  status compliance_requirement_status [not null, default: 'ACTIVE']

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    requirement_name
    status
  }
}


Table DossierTemplate {
  dossier_template_id bigint [pk, increment]

  template_code varchar(50) [not null, unique]

  template_name varchar(200) [not null]

  description text

  version_no int [not null, default: 1]

  status dossier_template_status [not null, default: 'DRAFT']

  effective_from date
  effective_to date

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    template_name
    status
  }
}


Table DossierTemplateItem {
  dossier_template_item_id bigint [pk, increment]

  dossier_template_id bigint [not null]

  compliance_requirement_id bigint [not null]

  sequence_no int [not null]

  is_mandatory boolean [not null, default: true]

  instruction text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    dossier_template_id
    compliance_requirement_id

    (dossier_template_id, compliance_requirement_id) [unique]
    (dossier_template_id, sequence_no) [unique]
  }
}


Table ComplianceDossier {
  compliance_dossier_id bigint [pk, increment]

  dossier_code varchar(50) [not null, unique]

  transport_id bigint [not null, unique]

  status compliance_dossier_status [not null, default: 'DRAFT']

  opened_at timestamp [not null]

  ready_for_submission_at timestamp
  approved_at timestamp
  rejected_at timestamp
  closed_at timestamp

  rejection_reason text

  note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    status
  }
}


Table DossierRequirement {
  dossier_requirement_id bigint [pk, increment]

  compliance_dossier_id bigint [not null]

  compliance_requirement_id bigint [not null]

  status dossier_requirement_status [not null, default: 'PENDING']

  is_mandatory boolean [not null, default: true]

  due_date date

  instruction text

  satisfied_at timestamp

  waived_reason text

  note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    compliance_dossier_id
    compliance_requirement_id
    status

    (compliance_dossier_id, compliance_requirement_id) [unique]
  }
}


Table ComplianceDocument {
  compliance_document_id bigint [pk, increment]

  dossier_requirement_id bigint [not null, unique]

  document_code varchar(50) [not null, unique]

  document_name varchar(200) [not null]

  status compliance_document_status [not null, default: 'DRAFT']

  document_number varchar(150)

  issuing_authority varchar(200)

  issued_date date
  expiry_date date

  note text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    document_number
    status
    expiry_date
  }
}


Table DocumentVersion {
  document_version_id bigint [pk, increment]

  compliance_document_id bigint [not null]

  version_no int [not null]

  file_name varchar(255) [not null]

  file_url varchar(1000) [not null]

  mime_type varchar(100)

  file_size_bytes bigint

  upload_note text

  uploaded_at timestamp [not null]

  created_at timestamp [not null]

  indexes {
    compliance_document_id

    (compliance_document_id, version_no) [unique]
  }
}


Table DocumentReview {
  document_review_id bigint [pk, increment]

  document_version_id bigint [not null]

  result document_review_result [not null]

  review_comment text

  reviewed_at timestamp [not null]

  created_at timestamp [not null]

  indexes {
    document_version_id
    result
    reviewed_at
  }
}


Table DocumentRequest {
  document_request_id bigint [pk, increment]

  dossier_requirement_id bigint [not null]

  request_code varchar(50) [not null, unique]

  request_type document_request_type [not null]

  status document_request_status [not null, default: 'OPEN']

  message text [not null]

  requested_at timestamp [not null]

  due_date date

  fulfilled_at timestamp

  cancelled_at timestamp
  cancellation_reason text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    dossier_requirement_id
    request_type
    status
    requested_at
  }
}


Table AuthoritySubmission {
  authority_submission_id bigint [pk, increment]

  compliance_dossier_id bigint [not null]

  submission_no int [not null]

  authority_name varchar(200) [not null]

  status authority_submission_status [not null, default: 'DRAFT']

  submission_reference varchar(150)

  submission_method varchar(100)

  submitted_at timestamp

  response_received_at timestamp

  decision_note text

  rejection_reason text

  created_at timestamp [not null]
  updated_at timestamp [not null]

  indexes {
    compliance_dossier_id
    status
    submission_reference

    (compliance_dossier_id, submission_no) [unique]
  }
}


//////////////////////////////////////////////////////////////
// FLOW 8 - NOTIFICATION
//////////////////////////////////////////////////////////////

Table Notification {
  notification_id bigint [pk, increment]

  transport_id bigint [not null]

  notification_type notification_type [not null]

  priority notification_priority [not null, default: 'NORMAL']

  title varchar(200) [not null]

  message text [not null]

  reference_type varchar(100)
  reference_id bigint

  action_url varchar(1000)

  created_at timestamp [not null]

  expires_at timestamp

  indexes {
    transport_id
    notification_type
    priority
    created_at

    (reference_type, reference_id)
  }
}


Table AccountNotification {
  account_id bigint [not null]

  notification_id bigint [not null]

  status notification_recipient_status [not null, default: 'PENDING']

  delivered_at timestamp
  read_at timestamp

  created_at timestamp [not null]

  indexes {
    (account_id, notification_id) [pk]

    notification_id
    status
    read_at
  }
}


//////////////////////////////////////////////////////////////
// RELATIONSHIPS - CORE
//////////////////////////////////////////////////////////////

Ref: Customer.account_id - Account.account_id
Ref: Employee.account_id - Account.account_id

Ref: Horse.customer_id > Customer.customer_id

Ref: Booking.customer_id > Customer.customer_id
Ref: Booking.assigned_employee_id >? Employee.employee_id

Ref: BookingHorse.booking_id > Booking.booking_id
Ref: BookingHorse.horse_id > Horse.horse_id

Ref: BookingStatusHistory.booking_id > Booking.booking_id
Ref: BookingStatusHistory.changed_by_account_id > Account.account_id

Ref: Transport.booking_id - Booking.booking_id


//////////////////////////////////////////////////////////////
// RELATIONSHIPS - TRANSPORT PLANNING
//////////////////////////////////////////////////////////////

Ref: TransportPlan.transport_id > Transport.transport_id
Ref: TransportPlan.approved_by_employee_id >? Employee.employee_id

Ref: RoutePlan.transport_plan_id - TransportPlan.transport_plan_id

Ref: Route.route_plan_id > RoutePlan.route_plan_id

Ref: Location.transport_plan_id > TransportPlan.transport_plan_id
Ref: Location.route_id > Route.route_id

Ref: Vehicle.transport_plan_id >? TransportPlan.transport_plan_id

Ref: TransportPlanProvider.transport_plan_id > TransportPlan.transport_plan_id
Ref: TransportPlanProvider.transport_provider_id > TransportProvider.transport_provider_id

Ref: EmployeeTransportPlan.employee_id > Employee.employee_id
Ref: EmployeeTransportPlan.transport_plan_id > TransportPlan.transport_plan_id


//////////////////////////////////////////////////////////////
// RELATIONSHIPS - EXECUTION
//////////////////////////////////////////////////////////////

Ref: HandoverRecord.transport_id > Transport.transport_id

Ref: HorseHealthLog.transport_id > Transport.transport_id
Ref: HorseHealthLog.horse_id > Horse.horse_id
Ref: HorseHealthLog.employee_id > Employee.employee_id


//////////////////////////////////////////////////////////////
// RELATIONSHIPS - INCIDENT / EMERGENCY
//////////////////////////////////////////////////////////////

Ref: IncidentReport.employee_id > Employee.employee_id
Ref: IncidentReport.location_id > Location.location_id

Ref: VehicleIncidentDetail.incident_report_id - IncidentReport.incident_report_id
Ref: VehicleIncidentDetail.vehicle_id > Vehicle.vehicle_id

Ref: HorseHealthIncidentDetail.incident_report_id > IncidentReport.incident_report_id
Ref: HorseHealthIncidentDetail.horse_id > Horse.horse_id

Ref: EmergencyCostRequest.incident_report_id > IncidentReport.incident_report_id

Ref: CostApprovalDecision.emergency_cost_request_id - EmergencyCostRequest.emergency_cost_request_id
Ref: CostApprovalDecision.employee_id > Employee.employee_id


//////////////////////////////////////////////////////////////
// RELATIONSHIPS - FINANCE
//////////////////////////////////////////////////////////////

Ref: Invoice.booking_id > Booking.booking_id
Ref: Invoice.employee_id > Employee.employee_id

Ref: InvoiceItem.invoice_id > Invoice.invoice_id
Ref: InvoiceItem.horse_id >? Horse.horse_id

// Business rule confirmed:
// EmergencyCostRequest 1 -> 1 InvoiceItem once billed.
// Physical lifecycle allows 0..1 before billing.
Ref: InvoiceItem.emergency_cost_request_id - EmergencyCostRequest.emergency_cost_request_id

Ref: Receipt.invoice_id > Invoice.invoice_id

Ref: InvoiceStatusHistory.invoice_id > Invoice.invoice_id

Ref: FinancialReport.transport_id - Transport.transport_id


//////////////////////////////////////////////////////////////
// RELATIONSHIPS - CLAIM
//////////////////////////////////////////////////////////////

Ref: Claim.customer_id > Customer.customer_id
Ref: Claim.handover_record_id > HandoverRecord.handover_record_id

Ref: ClaimResolution.claim_id - Claim.claim_id


//////////////////////////////////////////////////////////////
// RELATIONSHIPS - COMPLIANCE
//////////////////////////////////////////////////////////////

Ref: DossierTemplateItem.compliance_requirement_id > ComplianceRequirement.compliance_requirement_id
Ref: DossierTemplateItem.dossier_template_id > DossierTemplate.dossier_template_id

Ref: ComplianceDossier.transport_id - Transport.transport_id

Ref: DossierRequirement.compliance_dossier_id > ComplianceDossier.compliance_dossier_id
Ref: DossierRequirement.compliance_requirement_id > ComplianceRequirement.compliance_requirement_id

Ref: DocumentRequest.dossier_requirement_id > DossierRequirement.dossier_requirement_id

Ref: ComplianceDocument.dossier_requirement_id - DossierRequirement.dossier_requirement_id

Ref: DocumentVersion.compliance_document_id > ComplianceDocument.compliance_document_id

Ref: DocumentReview.document_version_id > DocumentVersion.document_version_id

Ref: AuthoritySubmission.compliance_dossier_id > ComplianceDossier.compliance_dossier_id


//////////////////////////////////////////////////////////////
// RELATIONSHIPS - NOTIFICATION
//////////////////////////////////////////////////////////////

Ref: Notification.transport_id > Transport.transport_id

Ref: AccountNotification.account_id > Account.account_id
Ref: AccountNotification.notification_id > Notification.notification_id


//////////////////////////////////////////////////////////////
// AUDIT NOTES / OPEN DESIGN RISKS
//
// 1) Vehicle is currently owned/assigned directly by TransportPlan.
//    If vehicles are reusable across trips, refactor to:
//    Vehicle N-N TransportPlan through TransportPlanVehicle.
//
// 2) Location stores both transport_plan_id and route_id.
//    Since Route -> RoutePlan -> TransportPlan already exists,
//    these two FKs can become inconsistent unless backend validates them.
//
// 3) Compliance requirements are currently dossier-level only.
//    If some permits/certificates are per horse, extend
//    DossierRequirement with horse scope or a mapping table.
//
// 4) Source ERD does not record actors for DocumentReview,
//    DocumentRequest, AuthoritySubmission, ClaimResolution,
//    InvoiceStatusHistory. Add Employee/Account FKs if auditability
//    requires knowing exactly who performed each action.
//
// 5) Account -> Customer and Account -> Employee are individually 1:0..1,
//    but the database cannot enforce "exactly one profile type"
//    using these FKs alone. Enforce account_type/profile consistency
//    in application logic or DB constraints/triggers.
//
// 6) Notification.reference_type + reference_id is polymorphic.
//    Referential integrity for that pair must be validated by the app.
//
// 7) Invoice/FinancialReport monetary summaries should be calculated
//    from source rows by backend/service logic, not trusted from clients.
//////////////////////////////////////////////////////////////
```
