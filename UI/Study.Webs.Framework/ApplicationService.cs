using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.ApplicationServices;

namespace Study.Webs.Framework
{
    public class ApplicationService : ServiceStudyBase<Application, ApplicationDto, ApplicationQuery>, IApplicationService
    {
        /// <summary>
        /// 初始化应用程序服务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">应用程序仓储</param>
        public ApplicationService( )
        {
            //Repository = repository;
        }


        /// <summary>
        /// 转换为数据传输对象
        /// </summary>
        /// <param name="entity">实体</param>
        protected override ApplicationDto ToDto(Application entity)
        {
            return null;// entity.ToDto();
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        protected override Application ToEntity(ApplicationDto dto)
        {
            return null;// dto.ToEntity();
        }

        /// <summary>
        /// 创建应用程序
        /// </summary>
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
