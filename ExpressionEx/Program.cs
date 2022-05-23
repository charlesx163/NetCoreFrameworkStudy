// See https://aka.ms/new-console-template for more information
using ExpressionEx;
using ExpressionEx.DbExtension;
using ExpressionEx.Expressions;
using System.Linq.Expressions;

Console.WriteLine("Hello, World!");
#region basic
//{
//    Expression<Func<int, int, int>> addExp = (x, y) => x + y;

//    var n = addExp.Compile()(1, 2);

//    Console.WriteLine(n);
//}
#endregion

#region write Expression<Func<int, int, int>> exp = (x, y) => x * y+2
//{
//    ParameterExpression parameterExpression = Expression.Parameter(typeof(int), "x");
//    ParameterExpression parameterExpression2 = Expression.Parameter(typeof(int), "y");
//    var multipy = Expression.Multiply(parameterExpression, parameterExpression2);
//    var constant = Expression.Constant(2,typeof(int));
//    BinaryExpression add = Expression.Add(multipy, constant);
//    Expression<Func<int, int, int>> addExp = Expression.Lambda<Func<int, int, int>>(add, new ParameterExpression[] { parameterExpression, parameterExpression2 });
//    Console.WriteLine(addExp);
//}
#endregion

#region Expression<Func<People,bool>> exp=x => x.Id.ToString().Equals("5")
//{
//    ParameterExpression parameterExpression = Expression.Parameter(typeof(People), "x") ;
//    Expression field = Expression.Field(parameterExpression, typeof(People).GetField("Id"));
//    MethodCallExpression toString = Expression.Call(field, typeof(People).GetMethod("ToString"));  
//    ConstantExpression constant = Expression.Constant("5", typeof(string));
//    MethodCallExpression equals = Expression.Call(toString, typeof(People).GetMethod("Equals"),constant);
//    Expression<Func<People, bool>> lambda = Expression.Lambda<Func<People, bool>>(equals, new ParameterExpression[] { parameterExpression });
//    Console.WriteLine(lambda);
//    var result = lambda.Compile()(new People() { Id = 5 });
//    Console.WriteLine(result);
//}
#endregion

#region Expression实现的模型装换,用目录树生成好一个转换的委托，并使用静态字典缓存
//Expression<Func<People,PeopleCopy>> exp= (p) => new PeopleCopy()
//{
//    Name=p.Name,
//    Age=p.Age,
//    Id=p.Id
//};
//var people = new People 
//{
//    Id=1,
//    Age=2,
//    Name="Xu"
//};
//var p=exp.Compile()(people);
//var result = ExpressionMapper.Trans<People, PeopleCopy>(people);
//Console.WriteLine(result.Name);
//Console.WriteLine(p.Name);
#endregion

#region 使用泛型缓存 
//var people = new People
//{
//    Id = 1,
//    Age = 2,
//    Name = "Xu"
//};

//var p=ExpressionGenericMapper<People, PeopleCopy>.Trans(people);
//Console.WriteLine(p.Name);
#endregion

#region Expression的遍历和修改
//Expression<Func<int, int, int>> exp =(m,n) => m * n + 2;
//var ov = new OperationsExpression();
//var newExp=ov.Modify(exp);
//Console.WriteLine(newExp);
#endregion

#region Expression 在sql中的应用，把你写的linq表达式转换成sql,Expression的作用就体现出来了，可以把写的linq当成一种sql数据结构保存起来，在你用的时候帮你转换成sql
Expression<Func<People, bool>> exp = x => x.Age > 5 && x.Id > 1 && x.Name!.StartsWith("X");
var v = new ConditionOpreations();
v.Visit(exp);
//((([Age] > 5) AND ([Id] > 1)) AND ([Name] LIKE 'X%'))
Console.WriteLine(v.Condition());
#endregion


