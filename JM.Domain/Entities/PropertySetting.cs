using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.Domain.Entities
{
    public class PropertySetting
    {
        public string Id { get; set; }    
        public string SettingName { get; set; }    
        public int IsChecked { get; set; }    
        public string Property_A { get; set; }    
        public string Property_B { get; set; }    
        public string Property_C { get; set; }    
    }
}
