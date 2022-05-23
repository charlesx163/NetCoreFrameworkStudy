using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEx.DbExtension
{
    public static class NodeTypeExtensions
    {
        //ToSqlOpertor
        public static string ToSqlOpertor(this ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.Subtract:
                    return "-";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.And:
                    return "&";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Or:
                    return "|";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Coalesce:
                    return "COALESCE";
                case ExpressionType.Not:
                    return "NOT";
                case ExpressionType.ExclusiveOr:
                    return "^";
                case ExpressionType.RightShift:
                    return ">>";
                case ExpressionType.LeftShift:
                    return "<<";
                default:
                    throw new Exception("不支持的运算符");
            }
        }
    }
}
