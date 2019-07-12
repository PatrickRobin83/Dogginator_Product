using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.DogginatorLibrary.Models
{
    public class DogModel
    {
       
        #region Fields

        #endregion

        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }

        public string Color { get; set; }

        public string Gender { get; set; }

        public string Birthday { get; set; }

        public string CastratedSince { get; set; }

        public bool PermanentCastrated { get; set; }

        public string EffectiveUntil { get; set; }

        public List<DiseasesModel> Diseases {get ; set; }

        public List<CharacteristicsModel> Characteristics { get; set; }

        public string Create_Date { get; set; }

        public string Edit_Date { get; set; }

        public List<CustomerModel> CustomerList { get; set; }

        public bool Active { get; set; }

        public string DogActive { get; set; }

        public string Marker { get; set; }

        public string FullDog
        {
            get
            {
                return $"{ Name } { Breed } { Color } { Gender } { Birthday } {DogActive}";
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
