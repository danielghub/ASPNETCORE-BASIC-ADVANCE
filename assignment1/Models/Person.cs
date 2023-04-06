using assignment1.Models.CustomModelValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace assignment1.Models

{
    public class Person
    {
        //constructor
        public Person()
        {

        }
        [CustomModelUserValidation]
        [DisplayName("First Name")]
        [StringLength(10)]
        public int Name { get; set; }
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public int Birthday { get; set; }
        public int Came { get; set; }
        public int Sex { get; set; }

        public int Sent { get; set; }
        public int Fax { get; set; }
        public int Phone { get; set; }
        public int PhoneNumber { get; set; }
        public int FaxNumber { get; set; }
        
        public int PhoneNumberNumberNumber { get;}
        public int Email { get; set; }
        public int EmailNumber { get; set; }
        public string Password { get; set; }
        private int myVar;

        public int Sorts
        {
            get { return myVar; }
            set { myVar = value; }
        }


    }
}
