
namespace CMS2.Common.Constants
{
    /// <summary>
    /// Used to indicate who accepted the transfer and from where it came from 
    /// </summary>
    public static class AcceptanceTypeConstant
    {
        public const string BsoToBco = "BsoToBco";
        public const string BsoToBso = "BsoToBso";
        public const string BcoToBco = "BcoToBco";
        public const string AreaToBco = "AreaToBco";
        public const string BcoToGateWay = "BcoToGateWay";
        public const string GatewayToBco = "GatewayToBco";
    }
}
