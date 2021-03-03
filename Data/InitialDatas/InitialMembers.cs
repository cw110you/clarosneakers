using webstore.Models;
using webstore.Helpers;

namespace webstore.Data
{
    public class InitialMembers
    {
        public Member[] members { get; set; }

        public InitialMembers()
        {
            PasswordWithSalt pws = HashHelper.Hash("test2020");

            members = new Member[]{
                new Member{ Account="test2020" , Password = pws.password, Salt= pws.salt  , Username="testUsername" , Address="testAddress" , Phone="0912345678" , Email="test@test.test" }
            };
        }
    }
}