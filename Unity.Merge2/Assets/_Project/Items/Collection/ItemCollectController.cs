using Architecture_Base.Core;
using Assets._Project.Items.Merge;
using System.Threading.Tasks;

namespace Assets._Project.Items.Collection
{
    public class ItemCollectController : Controller
    {
        private readonly ItemsCollectionGrid _collectionGrid;
        private readonly MergeGrid _mergeGrid;
        private readonly Player _player;

        public ItemCollectController(ItemsCollectionGrid collectionGrid,
            MergeGrid mergeGrid, Player player)
        {
            _collectionGrid = collectionGrid;
            _mergeGrid = mergeGrid;
            _player = player;
        }

        public override async Task InitializeAsync()
        {
            await _collectionGrid.InitializeAsync(_player.CollectedItemsCount);
        }

        protected override void OnEnable()
        {
            _mergeGrid.OnAdded += OnItemAdded;
        }

        private void OnItemAdded(Item item)
        {
            if (item.MergeLevel > _player.CollectedItemsCount)
            {
                _player.CollectedItemsCount = item.MergeLevel;
                _collectionGrid.OpenItems(_player.CollectedItemsCount);
            }
        }

        protected override void OnDisable()
        {
            _mergeGrid.OnAdded -= OnItemAdded;
        }
    }
}
