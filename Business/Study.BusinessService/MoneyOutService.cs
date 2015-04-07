using System;
using Study.Entity;
using Study.ApplicationServices;

namespace Study.BusinessService
{
    public class MoneyOutService : ServiceStudyBaseIntId<MoneyOut, MoneyOutDto, MoneyOutQuery>, IMoneyOutService
    {
        public MoneyOutService()
        {
        }
        protected override MoneyOutDto ToDto(MoneyOut entity)
        {
            return null;
        }
        protected override MoneyOut ToEntity(MoneyOutDto dto)
        {
            return null;
        }
        public override MoneyOutDto Create()
        {
            MoneyOutDto dto = new MoneyOutDto();
            return dto;
        }
        public override string  GetFetchQueryId()
        {
            return "qMoneyOut";
        }
        public override string  GetFetchId()
        {
            return "qMoneyOutId";
        }
        public override string  GetDeleteId()
        {
            return string.Empty;
        }
        public override string  GetAddId()
        {
            return "iMoneyOut";
        }
        public override string  GetUpdateId()
        {
            return "uMoneyOut";
        }
    }
}
