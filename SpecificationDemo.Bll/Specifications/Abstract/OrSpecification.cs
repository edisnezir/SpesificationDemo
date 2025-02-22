﻿using SpesificationDemo.Bll.Spesifications.Replacers;
using System;
using System.Linq.Expressions;

namespace SpesificationDemo.Bll.Spesifications.Abstract
{
    public class OrSpecification<T> : BaseSpecification<T>
    {
        private readonly BaseSpecification<T> _left;
        private readonly BaseSpecification<T> _right;


        public OrSpecification(BaseSpecification<T> left, BaseSpecification<T> right)
        {
            _right = right;
            _left = left;
        }


        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();
            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

            return finalExpr;
        }
    }
}
