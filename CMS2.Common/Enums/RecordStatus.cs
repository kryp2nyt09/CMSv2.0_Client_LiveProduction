using System.ComponentModel;

namespace CMS2.Common.Enums
{
    public enum RecordStatus
    {
        [Description("Active")]
        Active=1,
        [Description("Inactive,Unused,Disabled")]
        Inactive=2,
        [Description("Deleted")]
        Deleted=3
    }
}
