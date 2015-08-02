using System;
using Study.Entity;
using Study.BusinessService.Application;

namespace Study.BusinessService
{
    public class SportKindService : ServiceStudyBaseIntId<SportKind, SportKindDto, SportKindQuery>, ISportKindService
    {
        public SportKindService()
        {
        }
        protected override SportKindDto ToDto(SportKind entity)
        {
            return null;
        }
        protected override SportKind ToEntity(SportKindDto dto)
        {
            return null;
        }
        public override SportKindDto Create()
        {
            SportKindDto dto = new SportKindDto();
            return dto;
        }
        public override string GetQuerySqlId()
        {
            return "qSportKind";
        }

        public override string GetQueryAllSqlId()
        {
            return "qSportKindAll";
        }
        public override string GetQuerySqlWithParameterIsId()
        {
            return "qSportKindId";
        }
        public override string GetDeleteSqlId()
        {
            return "dSportKindId";
        }
        public override string GetInsertSqlId()
        {
            return "iSportKindId";
        }
        public override string GetUpdateSqlId()
        {
            return "uSportKindId";
        }
    }
}