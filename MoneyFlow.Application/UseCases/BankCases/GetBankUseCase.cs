using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.BankCases
{
    public class GetBankUseCase : IGetBankUseCase
    {
        private readonly IBanksRepository _banksRepository;

        public GetBankUseCase(IBanksRepository banksRepository)
        {
            _banksRepository = banksRepository;
        }

        public async Task<List<BankDTO>> GetAllAsyncBank()
        {
            var banks = await _banksRepository.GetAllAsync();
            var banksDTO = banks.ToListDTO();

            return banksDTO;
        }
        public List<BankDTO> GetAllBank()
        {
            var banks = _banksRepository.GetAll();
            var banksDTO = banks.ToListDTO();

            return banksDTO;
        }

        public async Task<BankDTO> GetAsyncBank(int idBank)
        {
            var bank = await _banksRepository.GetAsync(idBank);

            if (bank == null) { return null; } // TODO : Сделать исключение

            var bankDTO = bank.ToDTO();

            return bankDTO.BankDTO;
        }
        public BankDTO GetBank(int idBank)
        {
            var bank = _banksRepository.Get(idBank);

            if (bank == null) { return null; } // TODO : Сделать исключение

            var bankDTO = bank.ToDTO();

            return bankDTO.BankDTO;
        }

        public async Task<BankDTO> GetAsyncBank(string nameBank)
        {
            var bank = await _banksRepository.GetAsync(nameBank);

            if (bank == null) { return null; } // TODO : Сделать исключение

            var bankDTO = bank.ToDTO();

            return bankDTO.BankDTO;
        }
        public BankDTO GetBank(string nameBank)
        {
            var bank = _banksRepository.Get(nameBank);

            if (bank == null) { return null; } // TODO : Сделать исключение

            var bankDTO = bank.ToDTO();

            return bankDTO.BankDTO;
        }
    }
}
