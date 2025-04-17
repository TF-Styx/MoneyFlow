using MoneyFlow.Application.ApplicationModel;
using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;

namespace MoneyFlow.Application.Services.Realization
{
    public class StatisticsService : IStatisticsService
    {
        public IEnumerable<NameValue> DetailingTransaction<TCollectionItem>
            (
                List<FinancialRecordViewingDTO> records,
                Func<FinancialRecordViewingDTO, TCollectionItem, bool> recordSelector,
                Func<FinancialRecordViewingDTO, bool> transactionTypeSelector,
                List<TCollectionItem> collectionItem,
                Func<TCollectionItem, string> collectionItemSelector
            ) where TCollectionItem : class
        {
            var values = collectionItem.Select(x =>
            {
                var moneySum = records.Where(record => recordSelector(record, x)).Where(record => transactionTypeSelector(record)).Sum(x => x.Amount);

                return new NameValue
                {
                    Name = collectionItemSelector(x),
                    Value = moneySum
                };

            }).ToList();

            return values;
        }
    }
}

//List<NameValue> values = [];

//foreach (var item in collectionItem)
//{
//    var moneySum = records.Where(record => recordSelector(record, item)).Where(record => transactionTypeSelector(record)).Sum(x => x.Amount);
//    values.Add(new NameValue()
//    {
//        Name = collectionItemSelector(item),
//        Value = moneySum
//    });
//}

//return values;
