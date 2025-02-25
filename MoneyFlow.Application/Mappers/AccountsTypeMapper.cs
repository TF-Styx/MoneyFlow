using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    internal static class AccountsTypeMapper
    {
        public static (AccountTypeDTO AccountTypeDTO, string Message) ToDTO(this AccountTypeDomain accountType)
        {
            if (accountType == null) { return (null, "Тип счета не найден!!"); }

            return AccountTypeDTO.Create(accountType.IdAccountType, accountType.AccountTypeName);
        }

        public static List<AccountTypeDTO> ToListDTO(this IEnumerable<AccountTypeDomain> accountsType)
        {
            var list = new List<AccountTypeDTO>();

            foreach (var item in accountsType)
            {
                list.Add(item.ToDTO().AccountTypeDTO);
            }

            return list;
        }

        public static (AccountTypeDomain AccountTypeDomain, string Message) ToDomain(this AccountTypeDTO accountType)
        {
            if (accountType == null) { return (null, "Тип счета не найден!!"); }

            return AccountTypeDomain.Create(accountType.IdAccountType, accountType.AccountTypeName);
        }

        public static List<AccountTypeDomain> ToListDomain(this IEnumerable<AccountTypeDTO> accountsType)
        {
            var list = new List<AccountTypeDomain>();

            foreach (var item in accountsType)
            {
                list.Add(item.ToDomain().AccountTypeDomain);
            }

            return list;
        }
    }
}
