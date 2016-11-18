using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class FormAnalyticResponseModel 
    {

        public List<CityDD> Cities { get; set; }
        public List<Profession> Profession { get; set; }
        public String Question { get; set; }
        public List<QuizChoice> Choices { get; set; }
    }

    public class QuizChoice
    {
        public QuizChoice(String des, int id)
        {
            ChoiceDescription = des;
            ChoiceId = id;
        }
        public QuizChoice()
        {

        }  
        public String ChoiceDescription { get; set; }
        public int ChoiceId { get; set; }
    }
}

 public class Profession
    {
        public Profession(String _name, String _id)
        {
            id = id;
            name = _name;
        }
        public Profession()
        {

        }  
        public String id { get; set; }
        public String name { get; set; }
    }
 public class CityDD
 {
     public CityDD(String _name, int _id)
     {
         id = id;
         name = _name;
     }
     public CityDD()
     {

     }
     public int id { get; set; }
     public String name { get; set; }
 }