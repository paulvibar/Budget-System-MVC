using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class ORSMainInformation : BaseEntity
    {
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? Date { get; set; }
        [DisplayName("Allotment Code")]
        public string AllotmentCode { get; set; }
        [DisplayName("Fund Source")]
        public string FundSource { get; set; }
        [DisplayName("Fund Cluster")]
        public string FundCluster { get; set; }
        
        private string _payee;
        [Required]
        public string Payee 
        { 
            get
            {
                if (string.IsNullOrEmpty(_payee))
                {
                    return _payee;
                }
                return _payee.ToUpper();
            }
            set
            {
                _payee = value;
            }
        }
        public string Office { get; set; }
        public string Address { get; set; }
        [Required]
        public string Particulars { get; set; }
        [Required]
        [DisplayName("Box A Signatory")]
        public string BoxASignatory { get; set; }
        [Required]
        [DisplayName("Box A Position")]
        public string BoxAPosition { get; set; }
        [DisplayName("Box B Signatory")]
        public int BoxBID { get; set; }
        public string Processor { get; set; }

        
        public string Caption { get { return AllotmentCode + "-" + FundSource + "-" +  Date.ToString().Substring(6,4) + "-" + Date.ToString().Substring(3, 2) + "-" + Id.ToString("00000"); } }
    }
}
