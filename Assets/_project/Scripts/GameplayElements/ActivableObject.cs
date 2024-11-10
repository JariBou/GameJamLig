namespace _project.Scripts.GameplayElements
{
    public interface IActivableObject<in T>
    {
        public void Activate(T activator);

        public void RegisterActivator(T activator);
    }
}