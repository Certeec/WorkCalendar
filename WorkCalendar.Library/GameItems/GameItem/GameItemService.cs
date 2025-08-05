namespace WorkCalendar.Library.GameItems.GameItem
{
    public class GameItemService : IGameItemService
    {
        private IGameItemRepository _gameItemRepository;
        public GameItemService(IGameItemRepository gameItemRepository)
        {
            _gameItemRepository = gameItemRepository;
        }

        public List<Data.GameItem> GetAllItems()
            => _gameItemRepository.GetGameItems();
    }
}
