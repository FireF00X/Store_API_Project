using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Critria { get; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderByAsc { get ; set ; }
        public Expression<Func<T, object>> OrderByDesc { get ; set ; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagenated { get; set; } = false;

        public BaseSpecifications()
        {
            
        }
        public BaseSpecifications(Expression<Func<T,bool>> critria)
        {
            Critria = critria;
        }
        public void AddOrderByAsc(Expression<Func<T,object>> condition)
            => OrderByAsc = condition;
        public void AddOrderByDesc(Expression<Func<T,object>> condition)
            => OrderByDesc = condition;

        public void AddPagination (int skip , int take)
        {
            IsPagenated = true;
            Skip = skip;
            Take = take;
        }
    }
}
