using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities
{
    public class Province : BaseEntity
    {
        public Guid ProvinceID { get; set; }

        [Required]
        [StringLength(30)]
        public string ProvinceName { get; set; }
                
        public Guid RegionID { get; set; }
        
        public  List<BranchCorpOffice> BranchCorpOffices { get; set; }
        [ForeignKey("RegionID")]
        public virtual Region Region { get; set; }

       
    }
}
