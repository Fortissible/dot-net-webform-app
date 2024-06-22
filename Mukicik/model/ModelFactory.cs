using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mukicik.Models
{
    public class ModelFactory
    {
        public static Product CreateProduct(int id, string name, double price, string image, double rating)
        {
            return new Product
            {

                Id = id,
                Name = name,
                Price = price,
                Image = image,
                Rating = rating
            };
        }

        public static User CreateUser(int id, string username, string email, string password, string gender, DateTime dob, string profilePicture)
        {
            return new User
            {   
                Id = id,
                Username = username,
                Email = email,
                Password = password,
                Gender = gender,
                DOB = dob,
                Picture = profilePicture
            };
        }
    }
}