namespace MoviesApi.DTOs
{
    public class PagingDTO
    {

        public int Page { get; set; }

        private int itemsPerPage = 10;
        private readonly int maxItemsPerPage = 50;

        public int ItemsPerPage
        {
            get => itemsPerPage;
            set
            {
                ItemsPerPage = (value > maxItemsPerPage) ? maxItemsPerPage : value;
            }
        }

    }
}
