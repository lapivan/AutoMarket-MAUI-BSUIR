namespace AutoMarket.Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is not Entity other || GetType() != other.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return ReferenceEquals(this, other);

            return Id == other.Id;
        }
        public override int GetHashCode() => GetType().GetHashCode() * 31 + Id.GetHashCode();
    }
}
