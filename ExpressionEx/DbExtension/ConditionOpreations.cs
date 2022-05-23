using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEx.DbExtension
{
    public class ConditionOpreations : ExpressionVisitor
    {
        private Stack<string> _StringStack = new Stack<string>();
        public string Condition()
        {
            var condition = string.Concat(_StringStack.ToArray());
            _StringStack.Clear();
            return condition;
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            _StringStack.Push(")");
            Visit(node.Right);

            _StringStack.Push(" " + node.NodeType.ToSqlOpertor() + " ");
            Visit(node.Left);
            _StringStack.Push("(");
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            string format;
            switch (m.Method.Name)
            {
                case "StartsWith":
                    format = "({0} LIKE '{1}%')";
                    break;
                case "EndsWith":
                    format = "({0} LIKE '%{1}')";
                    break;
                case "Contains":
                    format = "({0} LIKE '%{1}%')";
                    break;
                default:
                    throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
            }
            this.Visit(m.Object);
            this.Visit(m.Arguments[0]);
            string right = _StringStack.Pop();
            string left = _StringStack.Pop();
            _StringStack.Push(string.Format(format, left, right));
            return m;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _StringStack.Push($"[{node.Member.Name}]");
            return node;
        }
        protected override Expression VisitConstant(ConstantExpression c)
        {
            _StringStack.Push($"{c.Value}");
            return c;
        }
    }
}
