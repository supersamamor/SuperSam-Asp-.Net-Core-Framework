using System;

namespace ProjectNamePlaceHolder.Core.Models
{
    public class BaseModel
    {       
        public int Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public string CreatedByUsername { get; private set; }
        public string UpdatedByUsername { get; private set; }
        public void SetCreatedInformation(string username)
        {
            this.CreatedDate = DateTime.Now;
            this.CreatedByUsername = username;
            this.UpdatedDate = DateTime.Now;
            this.UpdatedByUsername = username;
        }
        public void SetUpdatedInformation(string username)
        {
            this.UpdatedDate = DateTime.Now;
            this.UpdatedByUsername = username;
        }
    }
}
