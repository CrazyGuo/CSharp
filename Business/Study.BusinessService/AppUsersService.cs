using System;
using Study.Entity;
using Study.Entity;
using Study.BusinessService.Application;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using System.Collections.Generic;
using SeedWork;
using Log;

namespace Study.BusinessService
{
    public class AppUsersService :ServiceStudyBaseIntId <AppUsers, AppUsersDto,AppUsersQuery>, IAppUsersService
    {
        public AppUsersService()
        {
        }
        protected override AppUsersDto ToDto(AppUsers entity)
        {
            return null;
        }
        protected override AppUsers ToEntity(AppUsersDto dto)
        {
            return null;
        }
        public override AppUsersDto Create()
        {
            AppUsersDto dto = new AppUsersDto();
            return dto;
        }
        public override string  GetQuerySqlId()
        {
            return "qAppUsers";
        }
        public override string  GetQueryAllSqlId()
        {
            return "qAppUsersAll";
        }
        public override string  GetQuerySqlWithParameterIsId()
        {
            return "qAppUsersId";
        }
        public override string  GetDeleteSqlId()
        {
            return "dAppUsers";
        }
        public override string  GetInsertSqlId()
        {
            return "iAppUsers";
        }
        public override string  GetUpdateSqlId()
        {
            return "uAppUsers";
        }

        private string GetLogonSqlId()
        {
            return "qAppuserLogon";
        }

        public IList<AppUsersDto> FetchLogonUser(string userName, string pwd)
        {
            LogOuts.Debug("user:" + userName + " login system");
            string sqlId=GetLogonSqlId();
            AppUsersQuery parameter=new AppUsersQuery ();
            parameter.LogOnName = userName;
            parameter.LocalPassword = InfraUtils.Encrypt(pwd);
            return DataMapper.QueryForList<AppUsersDto>(sqlId, parameter);
        }
    }
}
