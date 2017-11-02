using System;
using System.Collections.Generic;
using System.Text;

namespace Reckon.DomainService
{
    public static class StringExtension
    {
        public static bool IsEqualTo(this Char source, Char target)
        {
            return source.ToString().ToLower().Equals(target.ToString().ToLower());
        }
    }
}
