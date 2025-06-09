namespace Marketplace.Core.Interfaces
{
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
