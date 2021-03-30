using System;
using System.Linq;

namespace Task1_3
{
    class Program
    {
        static void Main(string[] args)
        {

            using (peopleContext context = new peopleContext())
            {
                Person p1 = new Person { Name = "Mikita", Age = 18 };
                Person p2 = new Person { Name = "Dasha", Age = 18 };

                context.People.Add(p1);
                context.People.Add(p2);
                context.SaveChanges();
            }

            using (peopleContext context = new peopleContext())
            {
                var people = context.People.ToList();
                Console.WriteLine("The Data");
                foreach (Person person in people)
                {
                    Console.WriteLine($"{person.Id}. {person.Name}, {person.Age}");
                }
            }

            using (peopleContext context = new peopleContext())
            {
                Person p1 = context.People.FirstOrDefault();
                if (!(p1 is null))
                {
                    p1.Name = "Someone";
                    p1.Age = 100;
                    context.Update(p1);
                    context.SaveChanges();
                }

                Console.WriteLine($"{context.People.FirstOrDefault().Name}");
            }

            using (peopleContext context = new peopleContext())
            {
                Person p1 = context.People.FirstOrDefault();
                if (!(p1 is null))
                {
                    context.Remove(p1);
                    context.SaveChanges();
                }
                Console.WriteLine(context.People.FirstOrDefault().Name);
            }
        }
    }
}
