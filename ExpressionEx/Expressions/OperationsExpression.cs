using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEx.Expressions
{
    public class OperationsExpression : ExpressionVisitor
    {
        public Expression Modify(Expression expression)
        {
            return this.Visit(expression);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add)
            {
                var left = this.Visit(node.Left);
                var right = this.Visit(node.Right);
                return Expression.Subtract(left, right);
            }
            if (node.NodeType == ExpressionType.Multiply)
            {
                var left = this.Visit(node.Left);
                var right = this.Visit(node.Right);
                return Expression.Divide(left, right);
            }
            return base.VisitBinary(node);
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            return base.VisitConstant(node);
        }
    }
}
