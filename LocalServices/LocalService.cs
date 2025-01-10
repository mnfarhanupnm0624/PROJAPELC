//using APELC.LocalServices.ApelC;
using APELC.Model;
using APELC.LocalServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo.Agent;
using APELC.Data;
using MySql.Data.MySqlClient;
//using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;
using Microsoft.AspNetCore.Datasync;
using ServiceStack;

namespace APELC.LocalServices
{
    public interface ILocalService
    {
        ModelParameterAPELC GetModel();
    }

    public class LocalService : ILocalService
    {
        internal readonly string NAMA_PARAMETER;
        internal readonly string RESULTSET;

        public ModelParameterAPELC GetModel()
        {
            return new ModelParameterAPELC
            {
                // Name = "NAMA_PARAMETER"
                NAMA_PARAMETER = "NAMA_PARAMETER",
                RESULTSET = "RESULTSET",


            };
        }
    }
}
