namespace Missive_CSharp
{
    public interface IMissiveListener {}

    public interface IMissiveListener<in T> : IMissiveListener where T : Missive
    {
        void HandleMissive(T missive);
    }
}