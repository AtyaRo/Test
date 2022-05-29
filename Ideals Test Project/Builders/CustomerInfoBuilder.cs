using Bogus;
using Ideals_Test_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideals_Test_Project.Builders
{
    public class CustomerInfoBuilder
    {

        public CustomerInfoModel CustomerFixture = new Faker<CustomerInfoModel>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.EmailAddress, f => f.Person.Email)
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.AddressAlias, f => f.Address.StreetAddress())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Country, f => f.Address.Country())
            .RuleFor(x => x.PostalCode, f => f.Address.ZipCode("#####"))
            .RuleFor(x => x.MobilePhone, f => f.Phone.PhoneNumber("(###) ###-####"))
            .RuleFor(x => x.Password, f => f.Random.String(15))
            .Generate();
  
    }
}
