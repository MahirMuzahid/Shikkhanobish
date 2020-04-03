namespace Shikkhanobish.ViewModel
{
    public class TransferInfo
    {
        public Student Student = new Student();
        public Teacher Teacher = new Teacher();
        public string Class { get; set; }
        public int StudyTimeInAPp { get; set; }
        public int GivenRating { get; set; }
        public string Subject { get; set; }
        public string SubjectName { get; set; }
        public string ClassCode { get; set; }
    }
}