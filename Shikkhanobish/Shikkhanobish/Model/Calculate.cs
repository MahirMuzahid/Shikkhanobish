﻿using IdentityModel.Client;
using Shikkhanobish.ViewModel;
using System.Collections.Generic;

namespace Shikkhanobish.Model
{
    public class Calculate
    {
        public TransferInfo Info;
        public int CalculatedTuitionTime;

        public float RatingAndCostRange(string rank, string Class)
        {
            float pmc = 0;
            if (rank == "Placement")
            {
                pmc = 0;
            }
            else if (rank == "Newbie")
            {
                if (Class == "LS")
                {
                    pmc = 5;
                }
                else if (Class == "S")
                {
                    pmc = 6;
                }
                else if (Class == "HS")
                {
                    pmc = 7;
                }
            }
            else if (rank == "Average")
            {
                if (Class == "LS")
                {
                    pmc = 5.25f;
                }
                else if (Class == "S")
                {
                    pmc = 6.25f;
                }
                else if (Class == "HS")
                {
                    pmc = 7.25f;
                }
            }
            else if (rank == "Good")
            {
                if (Class == "LS")
                {
                    pmc = 5.50f;
                }
                else if (Class == "S")
                {
                    pmc = 6.50f;
                }
                else if (Class == "HS")
                {
                    pmc = 7.50f;
                }
            }
            else if (rank == "Vetaran")
            {
                if (Class == "LS")
                {
                    pmc = 5.75f;
                }
                else if (Class == "S")
                {
                    pmc = 6.75f;
                }
                else if (Class == "HS")
                {
                    pmc = 7.75f;
                }
            }
            else if (rank == "Master")
            {
                if (Class == "LS")
                {
                    pmc = 6;
                }
                else if (Class == "S")
                {
                    pmc = 7;
                }
                else if (Class == "HS")
                {
                    pmc = 8;
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

        public int CalculateCost(TransferInfo info)
        {
            Info = info;
            int cost = 0;
            if (info.StudyTimeInAPp > 0)
            {
                int totalMin = info.StudyTimeInAPp - info.Student.freeMin;
                cost = (int)(totalMin * RatingAndCostRange(info.Teacher.Teacher_Rank, info.ClassCode));
            }
            else
            {
                cost = 0;
            }
            return cost;
        }

        public int CalculateTuitionPoint(TransferInfo info)
        {
            int TuitionTime = info.StudyTimeInAPp * info.GivenRating;
            CalculatedTuitionTime = TuitionTime;
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