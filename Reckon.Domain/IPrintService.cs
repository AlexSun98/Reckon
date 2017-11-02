using System;
using System.Collections.Generic;

namespace Reckon.Domain
{
    public interface IPrintService
    {
        void Print(List<int> numbers);

        List<int> SetupNumeric();

        string NumberConverter(int number);
    }
}
