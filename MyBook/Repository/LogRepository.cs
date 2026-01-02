using MyBook.Data.Models;
using MyBook.IRepositories;

namespace MyBook.Repository
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
