using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class StatementOfAccountPrint:BaseEntity
    {
        public Guid StatementOfAccountPrintId { get; set; }
        public Guid StatementOfAccountId { get; set; }
        [ForeignKey("StatementOfAccountId")]
        public virtual StatementOfAccount StatementOfAccount { get; set; }
        public DateTime PrintDate { get; set; }
        public Guid PrintById { get; set; }
        [ForeignKey("PrintById")]
        public virtual Employee PrintBy { get; set; }
    }
}
