using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ims.Models
{
    public class Pagination
    {
        public int CountPages { get; private set; } = 0;
        public int CurrentPageNumber { get; set; } = 0;
        public int CountRecords { get; set; } = 0;
        public int CountRecordsOnPage { get; set; } = 0;
        public int FirstCurrentRecordNumber { get; private set; } = 0;
        public int LastCurrentRecordNumber { get; private set; } = 0;
        public int SkipNumber { get; private set; } = 0;

        public void SetFirstLastSkipNumbers()
        {
            if (CurrentPageNumber > 0 && CountRecords > 0 && CountRecordsOnPage > 0)
            {
                CountPages = (int)Math.Ceiling(CountRecords / (double)CountRecordsOnPage);
                FirstCurrentRecordNumber = CurrentPageNumber * CountRecordsOnPage - CountRecordsOnPage + 1;
                LastCurrentRecordNumber = FirstCurrentRecordNumber + CountRecordsOnPage - 1;
                SkipNumber = FirstCurrentRecordNumber - 1;
            }
        }
    }
}