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

        public async Task<List<BankDTO>> GetAllBank()
        {
            var banks = await _banksRepository.GetAll();
            var banksDTO = banks.ToListDTO();

            return banksDTO;
        }

        public async Task<BankDTO> GetBank(int id)
        {
            var bank = await _banksRepository.Get(id);

            if (bank == null) { return null; } // TODO : Сделать исключение

            var bankDTO = bank.ToDTO();

            return bankDTO.BankDTO;
        }
    }
}
