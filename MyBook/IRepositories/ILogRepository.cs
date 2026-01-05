namespace PublicLibrary.IRepositories
{
    public interface ILogRepository
    {
        public List<Data.Models.Log> GetAllLogs();
    }
}
