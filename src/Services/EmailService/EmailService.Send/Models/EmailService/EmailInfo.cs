using System.Collections.Generic;

namespace EmailService.Send.Models.EmailService
{
    public class EmailInfo
    {
        public long TemplateId { get; set; }
        public List<Receiver> Receivers { get; set; } = new List<Receiver>();
        public Dictionary<string, object> Parameters { get; set;  } = new Dictionary<string, object>();
    }
}
