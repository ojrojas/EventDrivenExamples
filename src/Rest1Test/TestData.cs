using System;
using System.Collections.Generic;
using EventDrivenDesign.Rest1.Models;

namespace EventDrivenDesign.Rest1Test
{
    public class DataUsersSeed
    {
        public static List<User> UserDataList;
        public DataUsersSeed()
        {
            UserDataList = new List<User> {
            new User {
                Id=Guid.NewGuid(),
                Name="Ralf",
                LastName="Jones",
                Email ="ralfjones@email.com",
                OtherData="NaNaNaNaNaNana"
            },
            new User {
                 Id=Guid.NewGuid(),
                Name="Clark",
                LastName="Still",
                Email ="clarkstill@email.com",
                OtherData="Hey"
            },
            new User {
                 Id=Guid.NewGuid(),
                Name="Heidern",
                LastName="",
                Email ="heidern@email.com",
                OtherData="Atention!"
            }
            };
        }
    }
}