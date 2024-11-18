using JM.Domain.Entities;
using JM.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.Application.Interfaces.I_Config
{
    public interface IPropertySetting: IBaseDapperRepository
    {
        Task<int> InsertPropertySettings(PropertySetting propertySetting); 
    }
}
