using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BearingSearch
{

    class Mine
    {
        public static double mmToIn = 0.0393701;
        public int NumOfColumns;
        public string[] lines;
        public Mine()
        {
            NumOfColumns = -1;

        }
        public Mine(string fileName)
        {

        }
        public string[] ReadLines(string filePath)
        {
            return null;
        }
    }
}
