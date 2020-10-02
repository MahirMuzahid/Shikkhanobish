using IdentityModel.Client;
using Shikkhanobish.ViewModel;
using System.Collections.Generic;

namespace Shikkhanobish.Model
{
    public class Calculate
    {
        public TransferInfo Info;
        public int CalculatedTuitionTime;

        public float RatingAndCostRange ( string rank , string Class , int totalTime , int inappmin , bool isStudent )
        {
            float pmc = 0;

            if ( totalTime + inappmin < 20 && isStudent == false )
            {
                pmc = 0;
            }
            else if ( totalTime + inappmin > 20 && rank == "Placement" && isStudent == false )
            {
                if ( Class == "LS" )
                {
                    pmc = 2;
                }
                else if ( Class == "S" )
                {
                    pmc = 3;
                }
                else if ( Class == "HS" )
                {
                    pmc = 4;
                }
            }
            else if(rank == "Placement" && isStudent == true)
            {
                if ( Class == "LS" )
                {
                    pmc = 2;
                }
                else if ( Class == "S" )
                {
                    pmc = 3;
                }
                else if ( Class == "HS" )
                {
                    pmc = 4;
                }
            }
            else if ( rank == "Newbie" )
            {
                if (Class == "LS")
                {
                    pmc = 2;
                }
                else if (Class == "S")
                {
                    pmc = 3;
                }
                else if (Class == "HS")
                {
                    pmc = 4;
                }
            }
            else if (rank == "Average")
            {
                if (Class == "LS")
                {
                    pmc = 2.50f;
                }
                else if (Class == "S")
                {
                    pmc = 3.50f;
                }
                else if (Class == "HS")
                {
                    pmc = 4.50f;
                }
            }
            else if (rank == "Good")
            {
                if (Class == "LS")
                {
                    pmc = 3f;
                }
                else if (Class == "S")
                {
                    pmc = 4f;
                }
                else if (Class == "HS")
                {
                    pmc = 5f;
                }
            }
            else if (rank == "Vetaran")
            {
                if (Class == "LS")
                {
                    pmc = 3.50f;
                }
                else if (Class == "S")
                {
                    pmc = 4.50f;
                }
                else if (Class == "HS")
                {
                    pmc = 5.50f;
                }
            }
            else if (rank == "Master")
            {
                if (Class == "LS")
                {
                    pmc = 4;
                }
                else if (Class == "S")
                {
                    pmc = 5;
                }
                else if (Class == "HS")
                {
                    pmc = 6;
                }
            }
            return pmc;
        }

        public string RankRange(int tuitionPoint, double avg, int totalTuitionTime)
        {
            string rank = null;
            if (totalTuitionTime < 20)
            {
                rank = "Placement";
            }
            else if (tuitionPoint <= 999 || (tuitionPoint > 999 && avg < 3.75f))
            {
                rank = "Newbie";
            }
            else if (tuitionPoint <= 3999 && avg >= 3.750f || (tuitionPoint > 3999 && avg < 3.75f))
            {
                rank = "Average";
            }
            else if (tuitionPoint <= 8999 && avg >= 4.00f || (tuitionPoint > 8999 && avg < 4.00f))
            {
                rank = "Good";
            }
            else if (tuitionPoint <= 15999 && avg >= 4.30f || (tuitionPoint > 15999 && avg < 4.30f))
            {
                rank = "Veteran";
            }
            else if (tuitionPoint > 16000 && avg >= 4.50f)
            {
                rank = "Master";
            }
            return rank; //it shows rank 
        }

        public float CalculateCostForTeacher(TransferInfo info)
        {
            int totalMin = 0;
            Info = info;
            float cost = 0;
            totalMin = info.StudyTimeInAPp;

            cost =  ( totalMin * RatingAndCostRange ( info.Teacher.Teacher_Rank , info.ClassCode , info.Teacher.Total_Min , info.StudyTimeInAPp , false ) );
            cost = ( float ) ( cost - (cost * .3) ) ;
            return cost;
        }
        public float CalculateCost(TransferInfo info)
        {
            Info = info;
            float cost = 0;
            if (info.StudyTimeInAPp >= 0)
            {
                int totalMin = info.StudyTimeInAPp;
                cost = (totalMin * RatingAndCostRange(info.Teacher.Teacher_Rank, info.ClassCode, info.Teacher.Total_Min, info.StudyTimeInAPp, true ) );
            }
            else
            {
                cost = 0;
            }
            return cost;
        }
        public float CalculateCostPerminStudent ( TransferInfo info )
        {
            Info = info;
            float cost = 0;
            if ( info.StudyTimeInAPp >= 0 )
            {
                int totalMin = info.StudyTimeInAPp;
                cost = ( RatingAndCostRange ( info.Teacher.Teacher_Rank , info.ClassCode , info.Teacher.Total_Min , info.StudyTimeInAPp , true ) );
            }
            else
            {
                cost = 0;
            }
            return cost;
        }
        public float CalculateCostPerminTeacher ( TransferInfo info )
        {
            int totalMin = 0;
            Info = info;
            float cost = 0;
            totalMin = info.StudyTimeInAPp;

            cost =  ( RatingAndCostRange ( info.Teacher.Teacher_Rank , info.ClassCode , info.Teacher.Total_Min , info.StudyTimeInAPp , false ) );
            cost = (cost * .3f  );
            return cost;
        }

        public int CalculateTuitionPoint(TransferInfo info)
        {
            int TuitionTime = info.StudyTimeInAPp * info.GivenRating;
            CalculatedTuitionTime = TuitionTime + info.Teacher.Total_Min;
            return TuitionTime;
        }

        public string CalculateRank(TransferInfo info)
        {
            return RankRange(info.Teacher.OverallTP, info.Teacher.Avarage, CalculatedTuitionTime);
        }


        public List<int> GetTuitionPointInfo ( )
        {
            List<int> info = new List<int> ();

            info.Add ( 20 ); //max value of placement
            info.Add ( 1000 ); //max value of Newbie
            info.Add ( 4000 ); //max value of Average
            info.Add ( 8000 ); //max value of Good
            info.Add ( 16000 ); //max value of Veteran

            return info;

        }
        public List<float> GetAverageInfo ( )
        {
            List<float> info = new List<float> ();

            info.Add ( 3.75f ); //max value of placement
            info.Add ( 3.75f ); //max value of Newbie
            info.Add ( 4.00f ); //max value of Average
            info.Add ( 4.30f ); //max value of Good
            info.Add ( 4.30f ); //max value of Veteran

            return info;

        }
    }
}