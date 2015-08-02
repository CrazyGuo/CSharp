using System;
using Study.Entity;
using Study.BusinessService.Application;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace Study.BusinessService
{
    public class MoneyOutService : ServiceStudyBaseIntId<MoneyOut, MoneyOutDto, MoneyOutQuery>, IMoneyOutService
    {
        public MoneyOutService()
        {

        }

        //protected override MoneyOutDto ToDto(MoneyOut entity)
        //{
        //    return null;
        //}

        protected override MoneyOut ToEntity(MoneyOutDto dto)
        {
            var mapper = new ObjectMapperManager().
                GetMapper<MoneyOutDto, MoneyOut>(new DefaultMapConfig().ConvertUsing<MoneyOutDto, MoneyOut>(value => new MoneyOut(value.MoneyOutId)
                {
                    KindType = value.Id,
                    OutTime = value.OutTime,
                    Quality = value.Quality,
                    Remark = value.Remark,
                }));
            MoneyOut entity = mapper.Map(dto);
            return entity;
        }

        public override MoneyOutDto Create()
        {
            MoneyOutDto dto = new MoneyOutDto();
            return dto;
        }

        public override string  GetQuerySqlId()
        {
            return "qMoneyOut";
        }

        public override string  GetQuerySqlWithParameterIsId()
        {
            return "qMoneyOutId";
        }

        public override string  GetDeleteSqlId()
        {
            return string.Empty;
        }

        public override string  GetInsertSqlId()
        {
            return "iMoneyOut";
        }

        public override string  GetUpdateSqlId()
        {
            return "uMoneyOut";
        }
    }
}
