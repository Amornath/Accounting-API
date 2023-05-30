using AutoMapper;
using ScopoERP.Models;
using ScopoERP.ViewModels;

namespace ScopoERP.Helpers
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Item, ItemViewModel>().ReverseMap();
            CreateMap<ItemCategory, ItemCategoryViewModel>().ReverseMap();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<Commission, CommissionViewModel>().ReverseMap();
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<Unit, UnitViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        
            CreateMap<Requisition, RequisitionViewModel>().ReverseMap();
            CreateMap<RequisitionDetails, RequisitionDetailsViewModel>().ReverseMap();
            CreateMap<InventoryReceive, InventoryReceiveViewModel>().ReverseMap();
            CreateMap<InventoryReceiveDetails, InventoryReceiveDetailsViewModel>().ReverseMap();
            CreateMap<InventoryIssue, InventoryIssueViewModel>().ReverseMap();
            CreateMap<InventoryIssueDetails, InventoryIssueDetailsViewModel>().ReverseMap();

            CreateMap<Production, ProductionViewModel>().ReverseMap();

            CreateMap<SalesOrder, SalesOrderViewModel>().ReverseMap();
            CreateMap<SalesOrderDetails, SalesOrderDetailsViewModel>().ReverseMap();
            CreateMap<Invoice, InvoiceViewModel>().ReverseMap();
            CreateMap<InvoiceDetails, InvoiceDetailsViewModel>().ReverseMap();
            CreateMap<Designation, DesignationViewModel>().ReverseMap();
            CreateMap<AccountType, AccountTypeViewModel>().ReverseMap();
            CreateMap<ParentAccount, ParentAccountViewModel>().ReverseMap();
            CreateMap<SubsidiaryAccount, SubsidiaryAccountViewModel>().ReverseMap();
            CreateMap<CostCenter, CostCenterViewModel>().ReverseMap();
            CreateMap<FinancialYear, FinancialYearViewModel>().ReverseMap();
            CreateMap<AccountBalance, AccountBalanceViewModel>().ReverseMap();
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
            CreateMap<TransactionDetails, TransactionDetailsViewModel>().ReverseMap();
            CreateMap<Attendance, AttendanceViewModel>().ReverseMap();
            CreateMap<LeaveApplication, LeaveApplicationViewModel>().ReverseMap();
            CreateMap<EmployeeSalary,EmployeeSalaryViewModel>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();
            CreateMap<MonthlySalary, MonthlySalaryViewModel>().ReverseMap();
            CreateMap<Lc, LcViewModel>().ReverseMap();
            CreateMap<DeliveryChallan, DeliveryChallanViewModel>().ReverseMap();
            CreateMap<DeliveryChallanDetails, DeliveryChallanDetailsViewModel>().ReverseMap();
            CreateMap<Company, CompanyViewModel>().ReverseMap();
            CreateMap<Division, DivisionViewModel>().ReverseMap();
            CreateMap<PurchaseOrder, PurchaseOrderViewModel>().ReverseMap();
            CreateMap<PurchaseOrderDetails, PurchaseOrderDetailsViewModel>().ReverseMap();
            CreateMap<Store, StoreViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<PurchaseReturn, PurchaseReturnViewModel>().ReverseMap();
            CreateMap<PurchaseReturnDetails, PurchaseReturnDetailsViewModel>().ReverseMap();
            CreateMap<SalesReturn, SalesReturnViewModel>().ReverseMap();
            CreateMap<SalesReturnDetails, SalesReturnDetailsViewModel>().ReverseMap();
            CreateMap<StoreTransfer, StoreTransferViewModel>().ReverseMap();
            CreateMap<StoreTransferDetails, StoreTransferDetailsViewModel>().ReverseMap();
            CreateMap<Bank, BankViewModel>().ReverseMap();
            CreateMap<ProductionUnit, ProductionUnitViewModel>().ReverseMap();
            CreateMap<Holiday, HolidayViewModel>().ReverseMap();
            CreateMap<WorkingShift, WorkingShiftViewModel>().ReverseMap();
            CreateMap<BackToBackLC, BackToBackLCViewModel>().ReverseMap();
            CreateMap<VoucherType, VoucherTypeViewModel>().ReverseMap();
            CreateMap<ItemCriteria, ItemCriteriaViewModel>().ReverseMap();
            CreateMap<LabTest, LabTestViewModel>().ReverseMap();
            CreateMap<LabTestDetails, LabTestDetailsViewModel>().ReverseMap();
            CreateMap<Formulation, FormulationViewModel>().ReverseMap();
            CreateMap<FormulationDetails, FormulationDetailsViewModel>().ReverseMap();
            CreateMap<ProductionPlan, ProductionPlanViewModel>().ReverseMap();
            CreateMap<Billing, BillingViewModel>().ReverseMap();
            CreateMap<BillingDetails, BillingDetailsViewModel>().ReverseMap();
            CreateMap<DiscountType, DiscountTypeViewModel>().ReverseMap();
            CreateMap<InvoiceDiscount, InvoiceDiscountViewModel>().ReverseMap();         
            CreateMap<Promotion, PromotionViewModel>().ReverseMap();
            CreateMap<SalaryIncrement, SalaryIncrementViewModel>().ReverseMap();         
            CreateMap<SalaryType, SalaryTypeViewModel>().ReverseMap();
            CreateMap<GeoGraph, GeoGraphViewModel>().ReverseMap();
            CreateMap<CompanyModule, CompanyModuleViewModel>().ReverseMap();
        }
    }
}
