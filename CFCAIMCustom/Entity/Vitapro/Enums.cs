namespace CFCAIMCustom.Entity.Vitapro
{

    public enum EnumReceivingStatus
    {
        Pending = 1,
        Accept = 2,
        Reject = 3,
        Ignore = 4,
        All = 99

    }

    public enum EnumProductionStatus
    {
        Unavailable = 1,
        Available = 2,
        Current = 3,
        Depleted = 4,
        InProcess = 5,
        All = 99
    }

}