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
            CreateMap<AccountType, AccountTypeViewModel>().ReverseMap();
            CreateMap<ParentAccount, ParentAccountViewModel>().ReverseMap();
            CreateMap<SubsidiaryAccount, SubsidiaryAccountViewModel>().ReverseMap();
            CreateMap<CostCenter, CostCenterViewModel>().ReverseMap();
            CreateMap<FinancialYear, FinancialYearViewModel>().ReverseMap();
            CreateMap<AccountBalance, AccountBalanceViewModel>().ReverseMap();
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
            CreateMap<TransactionDetails, TransactionDetailsViewModel>().ReverseMap();
            CreateMap<VoucherType, VoucherTypeViewModel>().ReverseMap();
        }
    }
}
