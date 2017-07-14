using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.ReportModel
{
    public class StatusModel
    {
        public int changesUploaded { get; set; }
        public int changesDownloaded { get; set; }
        public int totalChangesUploaded { get; set; }
        public int totalChangesDownloaded { get; set; }
    }
}
