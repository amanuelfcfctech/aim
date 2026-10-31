using CFCAIMCore.Manager;

namespace CFCAIMWorkerTest
{
    static class Program
    {
        static void Main()
        {
            CFCAPIManager.Setting = new CFCAIMCore.Entity.SettingInfo();
            //CFCAPIManager.Setting;
            UnitTest unitTest = new UnitTest();
            unitTest.ShowDialog();
        }
    }
}
