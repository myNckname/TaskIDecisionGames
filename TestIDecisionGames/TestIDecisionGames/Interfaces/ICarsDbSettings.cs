namespace TestIDecisionGames.Interfaces
{
    public interface ICarsDbSettings
    {
         string ConnectionString { get; set; }
         string DatabaseName { get; set; }
    }
}
