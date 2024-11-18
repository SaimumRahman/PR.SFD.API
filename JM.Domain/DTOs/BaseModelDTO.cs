using System;

namespace JM.Domain.DTOs
{
    public class BaseModelDTO
    {
        public DateTime CREATED_AT { get; set; }
        public DateTime UPDATED_AT { get; set; }
        public DateTime DELETE_AT { get; set; }
        public int CREATED_BY{ get; set; }
        public int UPDATED_BY{ get; set; }
        public int IS_DELETED{ get; set; }
        public int DELETED_BY{ get; set; }
        public string CREATED_BY_NAME { get; set; }
        public string UPDATED_BY_NAME { get; set; }
        public string DELETED_BY_NAME { get; set; }
    }
}
