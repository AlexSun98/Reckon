using System;
using System.Collections.Generic;
using System.Text;

namespace Reckon.Domain
{
    public interface ISearchService
    {
        string Find(string testToSearch, string subtext);
    }
}
