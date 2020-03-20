using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Model
{
    class Infoforteacherwindow
    {
        public string t1 { get; set; }
        public string t2 { get; set; }
        public string t3 { get; set; }

        public class getlist
        {
            public List<Infoforteacherwindow> infolist()
            {
                return new List<Infoforteacherwindow>()
            {
                new Infoforteacherwindow ()
                {
                    t1 = "Tex1-1", t2 = "Test1-2", t3 = "Text1-3"

                },
                new Infoforteacherwindow ()
                {
                     t1 = "Tex2-1", t2 = "Test2-2", t3 = "Text2-3"

                }
            };
            }
        }
    }
}
