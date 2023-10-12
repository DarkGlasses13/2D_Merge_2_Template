using Zenject;

namespace Assets._Project.Items.Merge
{
    public class MergeGridFactory : IFactory<MergeGrid>
    {
        private readonly MergeGridView _view;

        public MergeGridFactory(MergeGridView view)
        {
            _view = view;
        }

        public MergeGrid Create() => new(_view.Capacity);
    }
}
