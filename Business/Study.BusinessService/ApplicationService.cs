using System;
using Study.ApplicationServices;
using Study.Entity;

namespace Study.BusinessService
{
    public class ApplicationService : ServiceStudyBase<Application, ApplicationDto, ApplicationQuery>, IApplicationService
    {
        public ApplicationService( )
        {
            
        }

        protected override ApplicationDto ToDto(Application entity)
        {
            return null;// entity.ToDto();
        }

        protected override Application ToEntity(ApplicationDto dto)
        {
            return null;// dto.ToEntity();
        }

        public override ApplicationDto Create()
        {
            ApplicationDto dto = new ApplicationDto { Enabled = true };
            dto.Id = Guid.NewGuid();
            dto.CreateTime = DateTime.Now;
            dto.Version = new byte[] { 251 };
            return dto;
        }

        public override string GetFetchQueryId()
        {
            return "qApplication";
        }

        public override string GetFetchId()
        {
            return "qApplicationId";
        }

        public override string GetDeleteId()
        {
            return "dApplicationId";
        }

        public override string GetAddId()
        {
            return "iApplicationId";
        }

        public override string GetUpdateId()
        {
            return "uApplicationId"; 
        }
    }
}
