using System;
using Study.Entity;
using Study.BusinessService.Application;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace Study.BusinessService
{
    public class SportRecordService : ServiceStudyBaseIntId<SportRecord, SportRecordDto, SportRecordQuery>, ISportRecordService
    {
        public SportRecordService()
        {
        }
        protected override SportRecordDto ToDto(SportRecord entity)
        {
            return null;
        }
        protected override SportRecord ToEntity(SportRecordDto dto)
        {
            var mapper = new ObjectMapperManager().
               GetMapper<SportRecordDto, SportRecord>(new DefaultMapConfig().ConvertUsing<SportRecordDto, SportRecord>(value => new SportRecord(value.Id)
               {
                   Id = value.Id,
                   ActivityKind = 6,//value.ActivityKind,
                   ActivityTime = value.ActivityTime,
                   Numbers = value.Numbers,
                   Remark = value.Remark,
               }));
            SportRecord entity = mapper.Map(dto);
            return entity;
        }
        public override SportRecordDto Create()
        {
            SportRecordDto dto = new SportRecordDto();
            dto.ActivityTime = DateTime.Now;
            return dto;
        }

        public override string GetQuerySqlId()
        {
            return "qSportRecord";
        }
        public override string GetQuerySqlWithParameterIsId()
        {
            return "qSportRecordId";
        }
        
        public override string GetDeleteSqlId()
        {
            return "dSportRecord";
        }
        public override string GetInsertSqlId()
        {
            return "iSportRecord";
        }
        public override string GetUpdateSqlId()
        {
            return "uSportRecord";
        }
    }
}