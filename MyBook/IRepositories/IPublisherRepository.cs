namespace MyBook.IRepositories
{
    public interface IPublisherRepository
    {
      void AddPublisher(ViewModels.PublisherVM publisherVM);
        List<Data.Models.Publisher> GetAllPublishers(DTOS.QueryParameters query);
        Data.Models.Publisher GetPublisherById(int id);
        void DeletePublisher(int id);
        void UpdatePublisher(int id, ViewModels.PublisherVM publisherVM);

    }
}
