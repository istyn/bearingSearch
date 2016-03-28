using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearingSearch
{
    class Part
    {
        public string Brand;
        public string Application;
        public int Qty;
        public string PartNo;
        private string LinCode;
        private int SubLine;
        private int SubCode;
        public string Desc; //potentially null
        public double PrcA; //potentially null
            //useful for incomplete listing, where a set of parts is described by a separate part.
        public bool PartOfSet = false;//Boolean true if belonging to a larger set of other parts in an application
        public Part[] Set;//null if ParentOfSet==false
        

        public Part()
        {
            initDefaults();
        }

        private void initDefaults()
        {
            Brand = "";
            Application = "";
            Qty = -1;//not functional as default constructor if application maintains running QOH
            PartNo = "";
            LinCode = "";
            SubLine = -1;
            SubCode = -1;
            Desc = "";
        }

        public Part(string brand,
            string application,
            int qty,
            string partNo,
            string linCode,
            int subLine,
            int subCode,
            string desc,
            double prcA)
        {
            this.Brand=brand;
            this.Application=application;
            this.Qty = qty;
            this.PartNo=partNo;
            this.LinCode=linCode;
            this.SubLine=subLine;
            this.SubCode=subCode;
            this.Desc=desc;
            this.PrcA=prcA;          
        }
        /// <summary>
        /// Initializes a new instance of an incomplete part listing, generally containing a description of the 
        /// upcoming parts in ProductPosition. Is the mother of an entire set of parts,  without dimensional specifications
        /// </summary>
        /// <param name="brand">The brand.</param>
        /// <param name="Application">The application.</param>
        /// <param name="ProductPosition">The product position.</param>
        /// <param name="Qty">The qty.</param>
        public Part(string brand, string application, string productPosition, string qty, string PartNo, string linCode, string subLine, string subCode, double prcA)
        {
            initDefaults();

            //if (Int32.TryParse(qty, out Qty) && Qty==0)//if the qty correctly parsed to int and indicates that no dimension exists for the part number,
            //{   //these are the parameters of an incomplete Part
                this.Application = application;
                this.Brand = brand;
                this.Desc = productPosition;    //productPosition column in data format contains description of following lines            
                this.PartNo = PartNo;
            //}
            this.LinCode = "";
            //this.Qty = Int32.Parse(qty);//

            //this.LinCode = linCode;
            //this.SubLine = Int32.Parse(subLine);
            //this.SubCode = Int32.Parse(subCode);
            this.PrcA = prcA;
        }
    }
}
