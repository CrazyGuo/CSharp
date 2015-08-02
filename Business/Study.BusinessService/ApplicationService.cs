using System;
using Study.Entity;
using Study.BusinessService.Application;

namespace Study.BusinessService
{
    public class ApplicationService : ServiceStudyBase<Study.Entity.Application, ApplicationDto, ApplicationQuery>, IApplicationService
    {
        public ApplicationService( )
        {
            
        }

        protected override ApplicationDto ToDto(Study.Entity.Application entity)
        {
            return null;// entity.ToDto();
        }

        protected override Study.Entity.Application ToEntity(ApplicationDto dto)
        {
            return null;// dto.ToEntity();
        }

        public override ApplicationDto Create()
        {
            ApplicationDto dto = new ApplicationDto { Enabled = true };
            dto.CreateTime = DateTime.Now;
            dto.Version = new byte[] { 251 };
            return dto;
        }

        public override string GetQuerySqlId()
        {
            return "qApplication";
        }

        public override string GetQuerySqlWithParameterIsId()
        {
            return "qApplicationId";
        }

        public override string GetDeleteSqlId()
        {
            return "dApplicationId";
        }

        public override string GetInsertSqlId()
        {
            return "iApplicationId";
        }

        public override string GetUpdateSqlId()
        {
            return "uApplicationId"; 
        }
    }
}
