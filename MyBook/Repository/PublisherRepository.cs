using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBook.Data.Models;
using MyBook.DTOS;
using MyBook.Execption;
using MyBook.IRepositories;
using MyBook.ViewModels;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MyBook.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;
        public PublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherVM publisherVM)
        {

            if(StringStartWithNumber(publisherVM.Name))
            {
                throw new PublisherNameException("Name cannot start with a number");
            }
            var publisher = new Publisher
          {
            Name = publisherVM.Name,
            Description = publisherVM.Description
          };

            _context.Publisher.Add(publisher);
            _context.SaveChanges();
        }

        public List<Publisher> GetAllPublishers(QueryParameters query)
        {

           IQueryable <Publisher> publishers = _context.Publisher;

            if (!string.IsNullOrEmpty(query.Search))
            {
                publishers = publishers.Where(p => p.Name.Contains(query.Search));
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                if (query.SortBy.ToLower() == "name")
                {
                    publishers = query.IsDescending ? publishers.OrderByDescending(p => p.Name) : publishers.OrderBy(p => p.Name);
                }
                // Add more sorting options as needed
            }
            int skip = (query.PageNumber - 1) * query.PageSize;
            publishers = publishers.Skip(skip).Take(query.PageSize);
            return publishers.ToList();
        }

        public Publisher GetPublisherById(int id)
        {
            return _context.Publisher.FirstOrDefault(p => p.Id == id);
        }

        public void UpdatePublisherById(int id, PublisherVM publisherVM)
        {
            var publisher = _context.Publisher.FirstOrDefault(p => p.Id == id);
            if (publisher != null)
            {
                publisher.Name = publisherVM.Name;
                publisher.Description = publisherVM.Description;
                _context.SaveChanges();
            }
        }

        public void DeletePublisherById(int id)
        {
            var publisher = _context.Publisher.FirstOrDefault(p => p.Id == id);
            if (publisher != null)
            {
                _context.Publisher.Remove(publisher);
                _context.SaveChanges();
            }

        }

        public bool publisherExists(int id)
        {
            return _context.Publisher.Any(p => p.Id == id);
        }

        private bool StringStartWithNumber(string name)=>
            Regex.IsMatch(name, @"^\d");

        public void DeletePublisher(int id)
        {
            _context.Remove(new Publisher { Id = id });

        }

        public void UpdatePublisher(int id, PublisherVM publisherVM)
        {
            _context.Publisher.Update(new Publisher
            {
                Id = id,
                Name = publisherVM.Name,
                Description = publisherVM.Description
            });
        }
    }
}
