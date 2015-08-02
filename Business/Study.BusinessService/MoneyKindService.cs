using System;
using Study.Entity;
using Study.BusinessService.Application;

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
        public override string  GetQuerySqlId()
        {
            return "qMoneyKind";
        }
        public override string  GetQuerySqlWithParameterIsId()
        {
            return "qMoneyKindId";
        }
        public override string  GetDeleteSqlId()
        {
            return "dMoneyKindId";
        }
        public override string  GetInsertSqlId()
        {
            return "iMoneyKindId";
        }
        public override string  GetUpdateSqlId()
        {
            return "uMoneyKindId";
        }
    }
}
