using MoneyFlow.Application.DTOs.BaseDTOs;
using System.Collections.ObjectModel;

namespace MoneyFlow.Application.DTOs
{
    public class UserBanksDTO : BaseDTO<UserBanksDTO>
    {
        private UserBanksDTO(int idUser, IEnumerable<BankDTO> banks)
        {
            IdUser = idUser;

            foreach (var item in banks)
            {
                Banks.Add(item);
            }
        }

        public int IdUser { get; set; }
        public ObservableCollection<BankDTO> Banks { get; set; } = [];

        public static (UserBanksDTO UserBanksDTO, string Message) Create(int idUser, List<BankDTO> banks)
        {
            var message = string.Empty;
            var userBanks = new UserBanksDTO(idUser, banks);

            return (userBanks, message);
        }
    }
}
