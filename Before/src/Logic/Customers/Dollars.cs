using Logic.Common;

namespace Logic.Customers
{
    public class Dollars : ValueObject<Dollars>
    {
        private const decimal MAX_DOLLAR_AMOUNT = 1_000_000;
        public decimal Value { get; }
        public bool IsZero => Value == 0;

        private Dollars(decimal value)
        {
            Value = value;
        }

        public static Result<Dollars> Create(decimal dollarAmount)
        {
            if (dollarAmount < 0)
                return Result.Fail<Dollars>("Dollar amount cannot be negative");

            if (dollarAmount > MAX_DOLLAR_AMOUNT)
                return Result.Fail<Dollars>("Dollar amount cannot be greater than " + MAX_DOLLAR_AMOUNT);

            if (dollarAmount % .01m > 0)
                return Result.Fail<Dollars>("Dollar amount cannot contain part of a cent");

            return Result.Ok(new Dollars(dollarAmount));
        }

        public static Dollars Of(decimal dollarAmount)
        {
            return Create(dollarAmount).Value;
        }

        public static Dollars operator +(Dollars dollars1, Dollars dollars2)
        {
            return new Dollars(dollars1.Value + dollars2.Value);
        }

        public static Dollars operator *(Dollars dollars, decimal multiplier)
        {
            return new Dollars(dollars.Value * multiplier);
        }

        protected override bool EqualsCore(Dollars other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Dollars dollars)
        {
            return dollars.Value;
        }
    }
}
