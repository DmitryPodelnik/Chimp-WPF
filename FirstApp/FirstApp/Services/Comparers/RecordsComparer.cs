using First_App.Models.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Services.Comparers
{
    // Custom comparer for the Product class
    class RecordsComparer : IEqualityComparer<Record>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Record x, Record y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.User.Username == y.User.Username;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Record record)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(record, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashUserRecord= record.User.Username == null ? 0 : record.User.Username.GetHashCode();

            //Get hash code for the Code field.
            int hashRecordRate = record.User.Profile.Rate.GetHashCode();

            //Calculate the hash code for the product.
            return hashUserRecord ^ hashRecordRate;
        }
    }
}
