namespace LinqAndLamdaExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var allUsers = ReadUsers("users.json");
            var allPosts = ReadPosts("posts.json");

            // 1 - find all users having email ending with ".net".
            //Ex1(allUsers);

            // 2 - find all posts for users having email ending with ".net".
            //Ex2(allUsers, allPosts);

            // 3 - print number of posts for each user.
            //Ex3(allUsers, allPosts);

            // 4 - find all users that have lat and long negative.
            //Ex4(allUsers);

            // 5 - find the post with longest body.
            //Ex5(allPosts);

            // 6 - print the name of the employee that have post with longest body.

        

            // 7 - select all addresses in a new List<Address>. print the list.


            // 8 - print the user with min lat


            // 9 - print the user with max long


            // 10 - create a new class: public class UserPosts { public User User {get; set}; public List<Post> Posts {get; set} }
            //    - create a new list: List<UserPosts>
            //    - insert in this list each user with his posts only


            // 11 - order users by zip code


            // 12 - order users by number of posts

            Console.ReadLine();
        }

        private static void Ex5(List<Post> allPosts)
        {
            var postBodyNumber = from post in allPosts
                                 select post.Body;

            var length = postBodyNumber.Max(s => s.Length);

            var biggest = postBodyNumber.FirstOrDefault(s => s.Length == length);

            Console.WriteLine(biggest.Length);
        }

        private static void Ex4(List<User> allUsers)
        {
            Console.WriteLine("User with negative Lat and long are: ");
            foreach (var user in allUsers)
            {
                if (user.Address.Geo.Lat < 0 && user.Address.Geo.Lng < 0)
                {
                    Console.WriteLine(user.Name);
                }
            }
        }

        private static void Ex3(List<User> allUsers, List<Post> allPosts)
        {
            var userNumber = from user in allUsers
                             select user.Id;
            int countUser = userNumber.Count();

            for (int i = 1; i <= countUser; i++)
            {
                var userPrintPosts = from post in allPosts
                                     where post.UserId.Equals(i)
                                     select post.UserId;
                int count = userPrintPosts.Count();
                Console.WriteLine("User {0} have {1} posts", i, count);
            }
        }

        private static void Ex2(List<User> allUsers, List<Post> allPosts)
        {
            var usersIdsWithDotNetMails = from user in allUsers
                                          where user.Email.EndsWith(".net")
                                          select user.Id;

            var posts = from post in allPosts
                        where usersIdsWithDotNetMails.Contains(post.UserId)
                        select post;

            foreach (var post in posts)
            {
                Console.WriteLine(post.Id + " " + "user: " + post.UserId);
            }
        }

        private static void Ex1(List<User> allUsers)
        {
            var users1 = from u in allUsers
                         where u.Email.EndsWith(".net")
                         select u;

            var users2 = allUsers.Where(x => x.Email.EndsWith(".net"));

            var emails = allUsers.Select(x => x.Email).ToList();
        }

        private static List<Post> ReadPosts(string file)
        {
            return ReadData.ReadFrom<Post>(file);
        }

        private static List<User> ReadUsers(string file)
        {
            return ReadData.ReadFrom<User>(file);
        }
    }
}
