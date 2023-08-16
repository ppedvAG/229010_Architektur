namespace Decorator
{
    public interface IComponent
    {
        string Name { get; }
        decimal Price { get; }
    }

    public class Pizza : IComponent
    {
        public string Name => "Pizza";

        public decimal Price => 7.4m;
    }

    public class Brot : IComponent
    {
        public string Name => "Brot";

        public decimal Price => 3.2m;
    }

    public abstract class Deco : IComponent
    {
        protected readonly IComponent parent;

        public Deco(IComponent parent)
        {
            this.parent = parent;
        }

        public abstract string Name { get; }

        public abstract decimal Price { get; }
    }

    public class Salami : Deco
    {
        public Salami(IComponent parent) : base(parent)
        { }

        public override string Name => parent.Name + " Salami";
        public override decimal Price => parent.Price + 1.5m;
    }

    public class Käse : Deco
    {
        public Käse(IComponent parent) : base(parent)
        { }

        public override string Name => parent.Name + " Käse";
        public override decimal Price => parent.Price + 1.2m;
    }
}
