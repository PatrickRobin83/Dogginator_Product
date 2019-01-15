using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogginatorLibrary.Models
{
    public class DogModel
    {
        // TODO - Comment all Properties
        #region Fields

        #endregion

        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }

        public string Color { get; set; }

        public string Gender { get; set; }

        public string Birthday { get; set; }

        public string TassoRegistration { get; set; }

        public bool Chipped { get; set; }

        public string WhichPoint { get; set; }

        public bool Castrated { get; set; }

        public string CastratedSince { get; set; }

        public string CastrateMethod { get; set; }

        public List<DiseasesModel> Diseases {get ; set; }

        public List<CharacteristicsModel> Characteristics { get; set; }

        public string Create_Date { get; set; }

        public string Edit_Date { get; set; }

        public List<CustomerModel> CustomerList { get; set; }

        public string FullDog
        {
            get
            {
                return $" Name: { Name } | Rasse: { Breed } | Farbe: { Color } | Geschlecht: { Gender } | geb. Datum: { Birthday }";
            }
        }

        #endregion

        #region Contstructor
        public DogModel(string name)
        {
            this.Name = name;
        }
        public DogModel()
        {

        }
        #endregion

        #region Methods

        #endregion
    }
}
