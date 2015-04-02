using System;
using Study.Entity;
using Study.ApplicationServices;

namespace Study.BusinessService
{
    public class MoneyKindService :ServiceStudyBaseIntId <MoneyKind, MoneyKindDto,MoneyKindQuery>, IMoneyKindService
    {
        public MoneyKindService()
        {
        }
        protected override MoneyKindDto ToDto(MoneyKind entity)
        {
            return null;
        }
        protected override MoneyKind ToEntity(MoneyKindDto dto)
        {
            return null;
        }
        public override MoneyKindDto Create()
        {
            MoneyKindDto dto = new MoneyKindDto();
            return dto;
        }
        public override string  GetFetchQueryId()
        {
            return "qMoneyKind";
        }
        public override string  GetFetchId()
        {
            return "qMoneyKindId";
        }
        public override string  GetDeleteId()
        {
            return "dMoneyKindId";
        }
        public override string  GetAddId()
        {
            return "iMoneyKindId";
        }
        public override string  GetUpdateId()
        {
            return "uMoneyKindId";
        }
    }
}
