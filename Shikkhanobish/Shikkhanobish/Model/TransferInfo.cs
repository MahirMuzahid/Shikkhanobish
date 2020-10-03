using Shikkhanobish.Model;
using System.Collections.Generic;

namespace Shikkhanobish.ViewModel
{
    public class TransferInfo
    {
        public Student Student = new Student();
        public Teacher Teacher = new Teacher();
        public List<OfferAndVoucherSource> Offers = new List<OfferAndVoucherSource> ();
        public string Class { get; set; }
        public int StudyTimeInAPp { get; set; }
        public int GivenRating { get; set; }
        public string Subject { get; set; }
        public string SubjectName { get; set; }
        public string ClassCode { get; set; }
        public string SessionID { get; set; }
        public string UserToken { get; set; }
        public float StudentCost { get; set; }
        public float TeacherEarn { get; set; }
    }
}