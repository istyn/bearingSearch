using System;
using System.Data.Sql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BearingSearch
{
	class Program
	{
		static void Main(string[] args)
		{
			PartsList partsList = new PartsList();
			partsList = PartsList.ReadDelimitedFile("../../../dellist.txt");
            partsList.SortBearings();
            string[] columns = partsList.GrabColumnHeadings();
            double[] sizes = partsList.UniqueShaftSizes;

			Console.Beep();
		}

	}
}
