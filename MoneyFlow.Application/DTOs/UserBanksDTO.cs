using MoneyFlow.Domain.DomainModels;
using System.Collections.ObjectModel;

namespace MoneyFlow.Application.DTOs
{
    public class UserBanksDTO
    {
        private UserBanksDTO(int idUser, IEnumerable<BankDomain> banks)
        {
            IdUser = idUser;

            foreach (var item in banks)
            {
                Banks.Add(item);
            }
        }

        public int IdUser { get; set; }
        public ObservableCollection<BankDomain> Banks { get; set; } = [];

        public static (UserBanksDTO UserBanksDTO, string Message) Create(int idUser, List<BankDomain> banks)
        {
            var message = string.Empty;
            var userBanks = new UserBanksDTO(idUser, banks);

            return (userBanks, message);
        }
    }
}
