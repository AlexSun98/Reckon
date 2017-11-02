using Reckon.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reckon.DomainService
{
    public class SearchService : ISearchService
    {
        public string Find(string testToSearch, string subtext)
        {
            if (testToSearch == string.Empty || subtext == string.Empty)
                return "<No Output>";

            List<int> list = new List<int>();
            int offset = 0;
            int textToSearchLength = testToSearch.Length;
            int subtextLength = subtext.Length;

            for (int i = 0; i < textToSearchLength; i++)
            {
                if (testToSearch[i].IsEqualTo(subtext[0]))
                {
                    for (int m = i + 1, j = 1; j < subtextLength; j++, m++)
                    {
                        if (textToSearchLength > m && subtextLength > j)
                        {
                            if (testToSearch[m].IsEqualTo(subtext[j]))
                            {
                                offset++;
                            }
                        }
                    }

                    if (offset == subtextLength - 1)
                    {
                        list.Add(i + 1);
                        offset = 0;
                    }
                }

                offset = 0;
            }

            if (list.Count == 0)
                return "<No Output>";
            else
                return string.Join(", ", list.ToArray());
        }
    }
}
