using JM.Application.Interfaces.I_Config;
using JM.Domain.Entities;
using JM.Infrastructure.Base;
using JM.Infrastructure.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.Application.Repositories.R_Config
{
    public class PropertySettingRepo : BaseRepository<PropertySetting>, IPropertySetting
    {
        public readonly ILogger<PropertySettingRepo> _logger;

        public PropertySettingRepo(ILogger<PropertySettingRepo> logger, IConnectionFactory connectionFactory) : base(logger, connectionFactory)
        {
            _logger = logger;
        }

        public async  Task<int> InsertPropertySettings(PropertySetting propertySetting)
        {
            try
            {
                string sql = $@" insert into PropertySetting (SettingName,IsChecked,Property_A)";
                var obj = await base.ExecuteIdentityAsync(sql, propertySetting);

                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
