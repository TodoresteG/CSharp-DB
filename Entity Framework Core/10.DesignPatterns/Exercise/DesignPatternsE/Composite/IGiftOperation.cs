namespace Composite
{
    public interface IGiftOperation
    {
        void Add(GiftBase gift);

        void Remove(GiftBase gift);
    }
}
