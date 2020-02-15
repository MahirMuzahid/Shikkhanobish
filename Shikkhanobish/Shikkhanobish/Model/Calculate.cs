using Shikkhanobish.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Model
{
    public class Calculate
    {

        public int CalculateCost(TransferInfo info)
        {
            int cost = 0;
            if(info.StudyTimeInAPp > 0)
            {
                int totalMin = info.StudyTimeInAPp - info.Student.freeMin;
                if (info.Teacher.Teacher_Rank == "Placement")
                {
                    cost = 0;
                }
                else if(info.Teacher.Teacher_Rank == "Newbie")
                {
                    if(info.Class == "LS")
                    {

                    }
                    else if (info.Class == "S")
                    {

                    }
                    else if (info.Class == "HS")
                    {

                    }
                }
                else if (info.Teacher.Teacher_Rank == "Placement")
                {
                    if (info.Class == "LS")
                    {

                    }
                    else if (info.Class == "S")
                    {

                    }
                    else if (info.Class == "HS")
                    {

                    }
                }
                else if (info.Teacher.Teacher_Rank == "Average")
                {
                    if (info.Class == "LS")
                    {

                    }
                    else if (info.Class == "S")
                    {

                    }
                    else if (info.Class == "HS")
                    {

                    }
                }
                else if (info.Teacher.Teacher_Rank == "Good")
                {
                    if (info.Class == "LS")
                    {

                    }
                    else if (info.Class == "S")
                    {

                    }
                    else if (info.Class == "HS")
                    {

                    }
                }
                else if (info.Teacher.Teacher_Rank == "Vetaran")
                {
                    if (info.Class == "LS")
                    {

                    }
                    else if (info.Class == "S")
                    {

                    }
                    else if (info.Class == "HS")
                    {

                    }
                }
                else if (info.Teacher.Teacher_Rank == "Master")
                {
                    if (info.Class == "LS")
                    {

                    }
                    else if (info.Class == "S")
                    {

                    }
                    else if (info.Class == "HS")
                    {

                    }
                }


            }
            else
            {
                cost = 0;
            }
            
            
        }
    }
}
