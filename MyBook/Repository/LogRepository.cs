using PublicLibrary.Data.Models;
using PublicLibrary.IRepositories;

namespace PublicLibrary.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly AppDbContext _context;
        public LogRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Log> GetAllLogs()
        => _context.Logs.ToList();
    }
}
