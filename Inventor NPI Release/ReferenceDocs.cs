using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventor_NPI_Release
{
    public class ReferenceDoc
    {
        private string mPartName;
        private string mPartPath;
        private string mDrawingName;
        private string mDrawingPath;
        private string mVaultDrawingPath;
        private string mRevision;
        private string mPDFName;
        private Boolean mIsSelected;

        public string PartName
        {
            get { return mPartName; }
            set { mPartName = value; }
        }

        public string PartPath
        {
            get { return mPartPath; }
            set { mPartPath = value; }
        }

        public string DrawingName
        {
            get { return mDrawingName; }
            set { mDrawingName = value; }
        }
        public string DrawingPath
        {
            get { return mDrawingPath; }
            set { mDrawingPath = value; }
        }

        public string VaultDrawingPath
        {
            get { return mVaultDrawingPath; }
            set { mVaultDrawingPath = value; }
        }

        public string Revision
        {
            get { return mRevision; }
            set { mRevision = value; }
        }

        public string PDFName
        {
            get { return mPDFName; }
            set { mPDFName = value; }
        }

        public Boolean IsSelected
        {
            get { return mIsSelected; }
            set { mIsSelected = value; }
        }
    }
}
