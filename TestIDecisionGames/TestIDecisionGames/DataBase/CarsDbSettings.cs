using TestIDecisionGames.Interfaces;

namespace TestIDecisionGames.DataBase
{
    public class CarsDbSettings:ICarsDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
