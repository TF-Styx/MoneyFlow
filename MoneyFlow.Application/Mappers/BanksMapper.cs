using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    internal static class BanksMapper
    {
        public static (BankDTO BankDTO, string Message) ToDTO(this BankDomain bank)
        {
            if (bank == null) { return (null, "Банк не найден!!"); }

            return BankDTO.Create(bank.IdBank, bank.BankName);
        }

        public static List<BankDTO> ToListDTO(this IEnumerable<BankDomain> banks)
        {
            var list = new List<BankDTO>();

            foreach (var item in banks)
            {
                list.Add(item.ToDTO().BankDTO);
            }

            return list;
        }

        public static (BankDomain BankDomain, string Message) ToDomain(this BankDTO bank)
        {
            if (bank == null) { return (null, "Банк не найден!!"); }

            return BankDomain.Create(bank.IdBank, bank.BankName);
        }

        public static List<BankDomain> ToListDomain(this IEnumerable<BankDTO> banks)
        {
            var list = new List<BankDomain>();

            foreach (var item in banks)
            {
                list.Add(item.ToDomain().BankDomain);
            }

            return list;
        }
    }
}
