using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewNationals.Models
{
    public class ModelSendMails
    {
        public string FullName { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public long PageId { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
    }
}