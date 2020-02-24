﻿using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Model
{
    public class Calculate
    {
        public TransferInfo Info;
        public int CalculatedTuitionTime;
        public float RatingAndCostRange(string rank, string Class)
        {
            float pmc = 0;
            if (Info.Teacher.Teacher_Rank == "Placement")
            {
                pmc = 0;
            }
            else if (Info.Teacher.Teacher_Rank == "Newbie")
            {
                if (Info.Class == "LS")
                {
                    pmc = 5;
                }
                else if (Info.Class == "S")
                {
                    pmc = 6;
                }
                else if (Info.Class == "HS")
                {
                    pmc = 7;
                }
            }
            else if (Info.Teacher.Teacher_Rank == "Average")
            {
                if (Info.Class == "LS")
                {
                    pmc = 5.25f;
                }
                else if (Info.Class == "S")
                {
                    pmc = 6.25f;
                }
                else if (Info.Class == "HS")
                {
                    pmc = 7.25f;
                }
            }
            else if (Info.Teacher.Teacher_Rank == "Good")
            {
                if (Info.Class == "LS")
                {
                    pmc = 5.50f;
                }
                else if (Info.Class == "S")
                {
                    pmc = 6.50f;
                }
                else if (Info.Class == "HS")
                {
                    pmc = 7.50f;
                }
            }
            else if (Info.Teacher.Teacher_Rank == "Vetaran")
            {
                if (Info.Class == "LS")
                {
                    pmc = 5.75f;
                }
                else if (Info.Class == "S")
                {
                    pmc = 6.75f;
                }
                else if (Info.Class == "HS")
                {
                    pmc = 7.75f;
                }
            }
            else if (Info.Teacher.Teacher_Rank == "Master")
            {
                if (Info.Class == "LS")
                {
                    pmc = 6;
                }
                else if (Info.Class == "S")
                {
                    pmc = 7;
                }
                else if (Info.Class == "HS")
                {
                    pmc = 8;
                }
            }
            return pmc;
        }      
        public string RankRange(int tuitionPoint, float avg, int totalTuitionTime)
        {
            string rank = null;
            if(totalTuitionTime < 30 )
            {
                rank = "Placement";
            }
            else if(tuitionPoint <= 999 || (tuitionPoint > 999 && avg < 3.75f))
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
            return rank;
        }



        public int CalculateCost(TransferInfo info)
        {
            Info = info;
            int cost = 0;
            if(info.StudyTimeInAPp > 0)
            {
                int totalMin = info.StudyTimeInAPp - info.Student.freeMin;
                cost =(int)(totalMin * RatingAndCostRange(info.Teacher.Teacher_Rank, info.Class));
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

        public string CalculateRank (TransferInfo info)
        {
            return RankRange(CalculatedTuitionTime, info.Teacher.Avarage, info.Teacher.OverallTP);
        }

    }
}
