using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BearingSearch
{
    enum Columns : int
    { //Brand|Application|ProductPosition|Qty|PartNo|StandardShaftSize|HousingBore|OilClearance|MaxWallThickness|OverallLength|Note|LinCode|SubLine|SubCode|PrcA
        Brand=0, Application=1, ProductPosition=2, Qty=3, PartNo=4,  StandardShaftSize=5,  HousingBore=6,  OilClearance=7,  MaxWallThickness=8,  OverallLength=9, Note=10, LinCode=11, SubLine=12, SubCode=13, PrcA=14
    };
    class PartsList
    {
        public List<Part> Parts;
        public int Count
        {
            get { return Parts.Count; }
        }
        public double[] UniqueShaftSizes
        {
            get { return GrabUniqueShaftSizes(); }
        }
        public PartsList()//default constructor
        {
            Parts = new List<Part>();
        }
        public PartsList(List<Bearing> b)//copy constructor
        {
            List<Part> p = new List<Part>(b.Count);//deep copy of List?
            for (int i = 0; i < b.Count; i++)
            {

                    p.Add(b.ElementAt<Bearing>(i));
             
            }
            Parts = p;
        }
        public void SortBearings()
        {
            PartsList pL = new PartsList(InsertionSortBearingsByShaftSize(Parts));
            this.Parts = pL.Parts;
        }
        public string[] GrabColumnHeadings()
        {
            var columnsMemberCount = Enum.GetNames(typeof(Columns)).Length;
            string[] strOut = new string[columnsMemberCount];
            int x=0;//counter for array iteration
            foreach (Columns s in Enum.GetValues(typeof(Columns)))
            {
                Console.Write(s);
                strOut[x] = s.ToString();
                x++;
            }
            return strOut;
        }
        public double[] GrabUniqueShaftSizes()
        {
            List<double> sizes = new List<double>();
            List<Bearing> bearings = PartsList.PullOutBearings(Parts);
            PartsList pL = new PartsList(bearings);
            bearings = PartsList.InsertionSortBearingsByShaftSize(pL.Parts);
            double greatestUnique = 0.000;
            foreach (Bearing p in Parts)
            {
                if (p.StandardShaftSize[0] > greatestUnique)
                {
                    sizes.Add(p.StandardShaftSize[0]);
                    greatestUnique = p.StandardShaftSize[0];
                }
            }
            return sizes.ToArray<double>();
        }

        #region InsertionSorts
        private static List<Bearing> InsertionSortBearingsByShaftSize(List<Part> list)
        {
            List<Bearing> bearings = PullOutBearings(list);

            int n = bearings.Count;

            for (int x = 1; x < n; x++)
            {
                int j = x;
                while (j > 0)
                {
                    if (bearings[j-1].StandardShaftSize[0] > bearings[j].StandardShaftSize[0])//sorts by qty, proof of concept
                    {
                        Bearing temp = bearings[j - 1];
                        bearings[j - 1] = bearings[j];
                        bearings[j] = temp;
                        j--;
                    }
                    else
                        break;
                }
            }
            return bearings;
        }
        private static List<Bearing> InsertionSortByHousingBore(List<Part> list)
        {
            List<Bearing> bearings = PullOutBearings(list);
            int n = bearings.Count;

            for (int x = 1; x < n; x++)
            {
                int j = x;
                while (j > 0)
                {
                    if (bearings[j - 1].HousingBore[0] > bearings[j].HousingBore[0])//sorts by qty, proof of concept
                    {
                        Bearing temp = bearings[j - 1];
                        bearings[j - 1] = bearings[j];
                        bearings[j] = temp;
                        j--;
                    }
                    else
                        break;
                }
            }
            return bearings;
        }

        private static List<Bearing> PullOutBearings(List<Part> list)//known working
        {
            List<Bearing> bearings = new List<Bearing>();   //create a new List<Bearing> containing only measurable bearings
            foreach (Part p in list)
            {
                if (p.GetType().Name == "Bearing")
                {
                    bearings.Add((Bearing)p);
                }
            }
            return bearings;
        }
        private static List<Part> PullOutBearingsParts(List<Part> list)//unknown if working
        {
            List<Part> bearings = new List<Part>();   //create a new List<Bearing> containing only measurable bearings
            foreach (Part p in list)
            {
                if (p.GetType().Name == "Bearing")
                {
                    bearings.Add(p);
                }
            }
            return bearings;
        }
        /// <summary>
        /// Sorts a list of Parts by priceA using an insertion sorting algorithm.
        /// </summary>
        /// <param name="list">The list.</param>
        private static List<Part> InsertionSortByPrice(List<Part> list)
        {
            int n = list.Count;

            for (int x = 1; x < n; x++)
            {
                int j = x;
                while (j > 0)
                {
                    if (list[j - 1].PrcA > list[j].PrcA)//sorts by qty, proof of concept
                    {
                        Part temp = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = temp;
                        j--;
                    }
                    else
                        break;
                }
            }
            return list;
        }
        #endregion


        public bool AddPart(Part p)
        {
            Parts.Add(p);
            return true;
        }
        public static PartsList operator +(PartsList list, Part part)//addition operator
        {
            //PartsList TempList = new PartsList(list);
            list.AddPart(part);
            //TempList.SaveNeeded = true;
            return list;
        }
        /// <summary>
        /// Accepts a line-delimited list and returns a bearing when a corresponding match is found.
        /// </summary>
        public static PartsList ReadDelimitedFile(string filePath)
        {

            PartsList tempList = new PartsList();
            Part tempPart = new Part();
            string[] theFields;
            string theLine;
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    for (; ; )  //infinite loop
                    {
                        theLine = sr.ReadLine();

                        if (theLine == null)
                            break;

                        theFields = theLine.Split(new char[] { '|' });

                        String[] StandardShaftSize = theFields[5].Split(new char[] { '/' });
                        String[] HousingBore = theFields[6].Split(new char[] { '/' });
                        String[] OilClearance = theFields[7].Split(new char[] { '/' });
                        double[] StandardShaftSizeP = new double[2];
                        double[] HousingBoreP = new double[2];
                        double[] OilClearanceP = new double[2];
                        int qtyP = 0;//not safe
                        double MaxWallThicknessP = 0;
                        double OverallLengthP = 0;
                        bool[] ValidDecimals = new bool[2];//each dimension has a max and min specification
                        bool ValidParams = false;
                        /* DATA VALIDATION */
                        /*  this loop determines if theLine is a bearing part number with specified quantity of at least 0*/
                        if (theFields[5] != "")//if line contains enough info to potentially hold standard shaft size, Try parsing strings
                        {
                            for (int i = 0; i < 2; i++) //parse the min/max dimensions separated by "/"
                            {

                                if (Double.TryParse(StandardShaftSize[i], out StandardShaftSizeP[i]))
                                {
                                    if (Double.TryParse(HousingBore[i], out HousingBoreP[i]))
                                    {
                                        if (Double.TryParse(OilClearance[i], out OilClearanceP[i]))
                                        {
                                            if (Int32.TryParse(theFields[3], out qtyP))
                                            {
                                                ValidDecimals[i] = true;

                                            }
                                        }
                                    }
                                }
                            }

                            if (Double.TryParse(theFields[8], out MaxWallThicknessP))   //now parse the remaining columns not needing an array
                            {
                                if (Double.TryParse(theFields[9], out OverallLengthP))
                                {
                                    ValidParams = true;//parse of dimensions to double complete! 
                                }
                            }
                            double PrcA;
                            if (Double.TryParse(theFields[14], out PrcA))
                            {
                                if (ValidDecimals[0] && ValidDecimals[1] && ValidParams)      //if complete bearing listing such that the line is formatted as below:
                                {//Brand|Application|ProductPosition|Qty|PartNo|StandardShaftSize|HousingBore|OilClearance|MaxWallThickness|OverallLength|Note|LinCode|SubLine|SubCode|PrcA
                                    tempPart = new Bearing();
                                    tempPart = new Bearing(theFields[0], theFields[1], theFields[2], qtyP, theFields[4], StandardShaftSizeP, HousingBoreP, OilClearanceP, MaxWallThicknessP, OverallLengthP, theFields[10], theFields[11], theFields[12], theFields[13], PrcA);
                                    tempList = tempList + tempPart;
                                } 
                            }
                        }
                        else//no dimensional specifications
                            if (theFields[0] != "" && theFields[1] != "" && theFields[2] != "" && theFields[3] != "" && theFields[4] != "")//if incomplete listing containing only brand,application,description of set,partNo, LinCode, SubLine, SubCode, PrcA
                            {
                                if (theFields[3] == "0")//also checked in Part(str, str, str, str, str, str, str, str, double) i.e. incomplete listing
                                {
                                    double PrcA;
                                    if (Double.TryParse(theFields[14], out PrcA))
                                    {
                                        tempPart = new Part(theFields[0], theFields[1], theFields[2], theFields[3], theFields[4], theFields[11], theFields[12], theFields[13], PrcA);   //initiate an incomplete Part(str, str, str, str, str)
                                        tempList = tempList + tempPart; //add it to the tempList<Part>

                                    }
                                }
                            }

                    }

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return tempList;
        }
    }
}
