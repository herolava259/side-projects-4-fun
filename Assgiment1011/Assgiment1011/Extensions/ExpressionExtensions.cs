using System.Linq.Expressions;

namespace Assgiment1011.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Func<T,bool> left, Func<T, bool> right)
        {
            if (right == null) return p => left(p);

            return param => left(param) && right(param);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (right == null) return left;

            return param => left.Compile()(param) || right.Compile()(param);
        }



    }
}
