using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearingSearch
{
    class Bearing : Part
    {
        public string ProductPosition;
        //public int Qty;
        public double[] StandardShaftSize= new double[2];
        public double[] HousingBore= new double[2];
        public double[] OilClearance= new double[2];
        public double MaxWallThickness;
        public double OverallLength;
//        public string Note; //separate from description, Note is bearing specific and in addition to part description


        public Bearing()
        {
            initBearing();
        }

        private void initBearing()
        {
            
            ProductPosition = "";
            //public int Qty;
            StandardShaftSize[0] = 0;
            HousingBore[0] = 0;
            OilClearance[0] = 0;
            StandardShaftSize[1] = 0;
            HousingBore[1] = 0;
            OilClearance[1] = 0;
            MaxWallThickness = 0;
            OverallLength = 0;
            //Desc = "";    // Note is bearing specific and in addition to part description
        }
        public Bearing(string brand, string application, string productPosition, int qty, string partNo, double[] standardShaftSize, double[] housingBore, double[] oilClearance, double maxWallThickness, double overallLength, string note, string linCode, string subLine, string subCode, double prcA)
        {        //Brand|Application|ProductPosition|Qty|PartNo|StandardShaftSize|HousingBore|OilClearance|MaxWallThickness|OverallLength|Note|LinCode|SubLine|SubCode|PrcA
            initBearing();

            this.Brand = brand;
            this.Application = application;
            this.ProductPosition = productPosition;
            this.Qty = qty;
            this.PartNo = partNo;
            this.StandardShaftSize = standardShaftSize;//pointer to array
            this.HousingBore = housingBore;//pointer
            this.OilClearance = oilClearance;//pointer
            this.MaxWallThickness = maxWallThickness;
            this.OverallLength = overallLength;
            //this.Note = note;
            this.Desc = note;//temporary until part database can be set up. then Desc will contain database value.
            base.PrcA = prcA;
        }

        /**constructor for incomplete part listing. indicates a partNo for one or a set of potential following complete parts. *
        public Bearing(string brand, string application, string productPosition, int qty, string partNo, string linCode, int subLine, int subCode, double prcA)
        {

        }*/
    }
}
