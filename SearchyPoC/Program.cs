using System;
using System.Linq;
using System.Collections.Generic;
using SearchyNET;
using static SearchyNET.Searchy;

namespace SearchyPoC
{
    internal class Program
    {
        private static void Main()
        {
            var users = GetTestUsers();
            Console.WriteLine("\nStart report...");
            TestAgainst(users, OrTestCriteria, "Or-ing");
            TestAgainst(users, AndTestCriteria, "And-ing");
            TestAgainst(users, AndTestCriteriaConfirmation, "Confirming And-ing with non-empty result.");
            Console.WriteLine("End report!");

            var userName = UserPropSelector(users[2], "UserName");
            var created = UserDatePropSelector(users[2], "DateCreated");
            Console.WriteLine(Chains.And.Doop(
                () => Operators.Equal.Doop(userName, "John James"),
                () => Operators.Equal.Doop(created, new DateTime(2018, 1, 5).Date))
            );
        }

        // Tests the given users against a given criteria described by the given approach.
        private static void TestAgainst(IEnumerable<User> users, Criteria criteria, string approach)
        {
            Console.WriteLine($"\nStarting {approach}...");
            // Criteria is capable of handling a simple, single where clause that
            // consists of complex criteria.
            var filteredUsers = users.Where(
                // SatisfiesAll builds a function and defers its execution
                // until all criterion are chained.
                criteria.SatisfiesAll
            // Execution begins with .ToList().
            ).ToList();
            filteredUsers.ForEach(PrintUser);
            Console.WriteLine($"Stopping {approach}...\n");
        }

        // Custom selector, aka how do we get to the value we want?
        // Any user prop value can be selected by prop name.
        // Could be any ISelectable to an arbitrary depth.
        private static IComparable UserPropSelector(ISelectable selectable, string propName)
        {
            // Selectors can encapsulate sanity checks and more!
            if (!(selectable is User))
            {
                throw new ArgumentException($"{nameof(ISelectable)} is not {nameof(User)}.");
            }

            var user = (User) selectable;
            return user.Props
                .First(p => p.Name == propName)
                .Value;
        }

        // Specialized prop selector for dates. It returns only the Date part of a
        // DateTime for better comparison against short, user-entered dates.
        private static IComparable UserDatePropSelector(ISelectable selectable, string propName)
        {
            var value = UserPropSelector(selectable, propName).ToString();
            if (!DateTime.TryParse(value, out var date))
            {
                throw new ArgumentException("User prop value wasn't a DateTime");
            }
            return date.Date;
        }

        // Fields for searching based on, in this case, User Props.
        // Fields can be built (with a FieldFactory, if you like) from user props.
        // The final arg is the UserPropSelector curried from: f(a, b), to: (a) => f(a, b) where b : IsKnown.
        private static readonly Field UserNameField = new Field(
            "UserName", DataType.String, user => UserPropSelector(user, "UserName"));

        private static readonly Field EmailField = new Field(
            "Email", DataType.String, user => UserPropSelector(user, "Email"));

        private static readonly Field DateCreatedField = new Field(
            "DateCreated", DataType.DateTime, user => UserDatePropSelector(user, "DateCreated"));

        #region Test Utils

        private static List<User> GetTestUsers()
        {
            var users = new List<User>
            {
                GetTestUser("Billy Bob"),
                GetTestUser("Happy Golucky"),
                GetTestUser("John James"),
                GetTestUser("Zoey Kilgore"),
                GetTestUser("Some Guy")
            };
            var dateProp = users[2].Props.First(p => p.Name == "DateCreated");
            dateProp.Value = new DateTime(2018, 1, 5);
            return users;
        }
        
        // Criteria for PoC. Shows And/Or behavior of linearly-linked filters.
        // Or against different props and values.
        private static Criteria OrTestCriteria
        { get; } = new Criteria(
            new List<Criterion>
            {
                GetRoot("Happy Golucky"),
                ChainSomeGuy(Chains.Or)
            }
        );

        // And against different props and values.
        private static Criteria AndTestCriteria
        { get; } = new Criteria(
            new List<Criterion>
            {
                GetRoot("Happy Golucky"),
                ChainSomeGuy(Chains.And)
            }
        );

        // And against different props with single expected user.
        private static Criteria AndTestCriteriaConfirmation
        { get; } = new Criteria(
            new List<Criterion>
            {
                GetRoot("Some Guy"),
                ChainSomeGuy(Chains.And)
            }
        );

        private static User GetTestUser(string name)
        {
            var props = new List<Prop>
            {
                GetTestProp("UserName", name),
                GetTestProp("Email", $"{name.Replace(" ", "")}@email.com"),
                GetTestProp("DateCreated", DateTime.Now.AddMonths(
                    new Random((int) DateTime.Now.Ticks).Next(1, 12)).Date)
            };
            return new User(props);
        }

        private static UserProp GetTestProp(string name, IComparable value)
        {
            return new UserProp(name, value);
        }

        private static Criterion GetRoot(string value)
        {
            return new Criterion(Chains.Root, UserNameField, Operators.Equal, new Value(value));
        }

        private static Criterion ChainSomeGuy(Chain chain)
        {
            return new Criterion(chain, EmailField, Operators.NotIn, new Value("  WhoKnows@email.com ,  SomeGuy@email.com   "));
        }

        private static Criterion ChainDateComp(Chain chain)
        {
            return new Criterion(chain, DateCreatedField, Operators.Equal, new Value("01/05/2018"));
        }

        private static void PrintUser(User user)
        {
            Console.WriteLine($"{UserPropSelector(user, "UserName")}");
        }

        #endregion
    }

    #region PoC Support

    internal class User : ISelectable
    {
        public User(List<Prop> props)
        {
            Props = props;
        }

        public List<Prop> Props { get; }
    }

    internal abstract class Prop
    {
        public string Name { get; protected set; }
        public IComparable Value { get; set; }
    }

    internal class UserProp : Prop
    {
        public UserProp(string name, IComparable value)
        {
            Name = name;
            Value = value;
        }
    }

    #endregion
}