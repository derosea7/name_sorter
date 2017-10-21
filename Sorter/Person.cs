using System;
using System.Collections.Generic;
using System.Text;

namespace Sorter
{
  class Person
  {
    public string firstName;
    public string lastName;

    public override string ToString()
    {
      return firstName + ' ' + lastName;
    }
  }
}
